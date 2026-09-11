// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime;

/* [fkelava 11/02/26 04:03]
 * By default, the game only probes its VBF data archives for files and fails if it cannot find them.
 * Repacking the archives is tiresome, so we want to permit direct loading of modded files from disk.
 *
 * While the game never uses it, it has full support for native file I/O. On Windows, this manifests as HANDLEs.
 * Thus, at file load time, we can give the game a HANDLE to a file on disk, and it will do the book-keeping for us.
 *
 * As a bonus, we permit the user to load assets from the currently inactive game.
 *
 * Some files load under slightly different rules and need different handling. See `cd.cs`.
 */

using EflIndex = Dictionary<string, string>;

/// <summary>
///     Provides the ability to replace files loaded by the game with files outside the VBF archives,
///     and the ability to cross-load the inactive game's assets from the active game.
/// </summary>
/// <remarks>
///     Place any files you wish to use in this way
///     in the <c>efl\x</c> or <c>efl\x2</c> subdirectory of your mod folder.
///     <para/>
///     These subdirectories are treated as the root of the VBF archive for their respective games;
///     from that point, you must mirror the VBF's directory structure.
///     <para/>
///     For example, to replace <c>FFX_Data\ffx_ps2\ffx\master\jppc\battle\kernel\takara.bin</c>,
///     the full path is <c>{...}\efl\x\FFX_Data\ffx_ps2\ffx\master\jppc\battle\kernel\takara.bin</c>.
/// </remarks>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
[SupportedOSPlatform("windows6.1")]
public unsafe sealed class FhFileLoaderModule : FhModule {

    private static ReadOnlySpan<byte> _stream_prefix      => "/"u8;
    private static ReadOnlySpan<byte> _vbf_secondary_path => FhGlobal.game_id is FhGameId.FFX
        ? @"data\FFX2_Data.vbf"u8
        : @"data\FFX_Data.vbf"u8;

    /* [fkelava 21/08/26 02:12]
     * BigFileStream and BigFileHandle (and PStreamFile) will store pointers
     * to their stream prefixes and/or VBF names, and expect them to be permanently valid.
     *
     * Thus we allocate some unmanaged memory to keep them alive and pinned forever.
     */

    private readonly EflIndex       _index;
    private readonly BigFileStream* _ptr_vbf_secondary;
    private readonly byte*          _ptr_vbf_secondary_path;

    public FhFileLoaderModule() {
        _index                  = [];
        _ptr_vbf_secondary      = (BigFileStream*) NativeMemory.AllocZeroed((nuint) sizeof(BigFileStream));
        _ptr_vbf_secondary_path = (byte*)          NativeMemory.AllocZeroed((nuint) _vbf_secondary_path.Length);

        _vbf_secondary_path.CopyTo(new (_ptr_vbf_secondary_path, _vbf_secondary_path.Length));
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        _init_index();

        return FhCall.Phyre_PSerialization_PStreamFile_ctor           .hook(this, h_fopen)
            && FhCall.ClusterManager_loadPCluster                     .hook(this, h_pcluster_ld)
            && FhCall.ClusterManager_getPClusterByName                .hook(this, h_pcluster_get)
            && FhCall.Phyre_PSerialization_PStreamFile_SetStreamPrefix.hook(this, h_sf_sp_set)
            && FhCall.BigFileStream_setStreamPrefix                   .hook(this, h_vbf_sp_set)
            && FhCall.BigFileStream_openFile                          .hook(this, h_vbf_fopen)
            && FhCall.fiosUnifyFilename                               .hook(this, h_fiosUnifyFilename);
    }

    /// <summary>
    ///     Indexes one of a given mod's root EFL directories.
    /// </summary>
    /// <param name="mod">The mod for which indexing is being carried out.</param>
    /// <param name="path_efl_dir">The absolute path to the EFL directory to index.</param>
    private void _index_dir(FhModContext mod, string path_efl_dir) {
        foreach (FileInfo efl_file in Directory.CreateDirectory(path_efl_dir).GetFiles("*.*", SearchOption.AllDirectories)) {
            string path_rel            = Path.GetRelativePath(path_efl_dir, efl_file.FullName);
            string path_rel_normalized = $"/{path_rel.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).ToUpperInvariant()}";

            if (_index.ContainsKey(path_rel_normalized)) {
                _logger.Warning($"{path_rel} is being superseded by mod {mod.Manifest.Name}");
            }

            _index[path_rel_normalized] = efl_file.FullName;
            _logger.Info($"Mod {mod.Manifest.Name} replaces file {path_rel}");
        }
    }

    /// <summary>
    ///     Indexes all mods' EFL directories to gather the replacement files for this session.
    /// </summary>
    /// <remarks>
    ///     After this method is called, any files subsequently added to the EFL directories
    ///     will not be available in this game session.
    /// </remarks>
    private void _init_index() {
        foreach (FhModContext mod in FhApi.Mods.get_mods()) {
            string path_efl_subdir_primary   = Path.Join(mod.Paths.EflDir.FullName, FhUtil.select("x" , "x2", "x2"));
            string path_efl_subdir_secondary = Path.Join(mod.Paths.EflDir.FullName, FhUtil.select("x2", "x" , "x" ));

            _index_dir(mod, path_efl_subdir_secondary);
            _index_dir(mod, path_efl_subdir_primary);
        }
    }

    /* [fkelava 25/08/26 20:00]
     * There's a nasty race condition hidden here. Normally, Fahrenheit initialization does not run game code. It is intended
     * that game code does not run until all hooks have installed, to ensure no calls 'escape' hooking from an interested module.
     *
     * The 'allocator fix' module wants to hook that initializer. If it doesn't, we lose its benefits. However, intra-DLL, Fahrenheit
     * leaves the initialization order of modules undefined. If we blithely `_init_crossload` in `init`, and this module ran `init`
     * before the 'allocator fix' module did, the call would go through before that module could hook it.
     * We therefore defer it to `fiosInitialize`, when the game sets up the primary VBF.
     *
     * Note also the 'creative' use of chaining from another method, our hook of the stream prefix setter, to avoid a stack overflow.
     */

    private void _init_crossload() {
        byte* ptr_prefix = (byte*) NativeMemory.AllocZeroed((nuint) _stream_prefix.Length);
        _stream_prefix.CopyTo(new (ptr_prefix, _stream_prefix.Length));

        FhCall.BigFileStream_ctor                                    .fnptr!(_ptr_vbf_secondary);
        FhCall.BigFileStream_setStreamPrefix.chain_from(h_vbf_sp_set).fnptr!(_ptr_vbf_secondary, ptr_prefix);

        if (FhCall.BigFileStream_registerBigFile.fnptr!(_ptr_vbf_secondary, _ptr_vbf_secondary_path) == 0)
            throw new Exception("Failed to initialize cross-loader function. Your game data may be corrupt or missing.");
    }

    /// <summary>
    ///     Searches the secondary VBF for an asset to match the given path,
    ///     and attempts to load it into the given <see cref="PStreamFile"/>.
    /// </summary>
    /// <param name="ptr_this">The <see cref="PStreamFile"/> to attempt a crossload into.</param>
    /// <param name="ptr_path">The asset's relative path from the VBF root.</param>
    /// <returns>
    ///     The unmodified input if it already contained a valid file, or
    ///     the input modified to contain the result of a load into the secondary VBF.
    /// </returns>
    private PStreamFile* _crossload(PStreamFile* ptr_this, byte* ptr_path) {
        if (ptr_this->handle_vbf != null)
            return ptr_this;

        FhCall.FUN_00607F10_008910A0.fnptr!(ptr_path);

        VFile* ptr_crossload_file = FhCall.BigFileStream_openFile.fnptr!(_ptr_vbf_secondary, ptr_path);
        ptr_this->handle_vbf = ptr_crossload_file;

        return ptr_this;
    }

    /// <summary>
    ///     Normalizes the paths the game uses to address files.
    /// </summary>
    private static void normalize_path(ReadOnlySpan<byte> src, Span<byte> dest) {

        /* [fkelava 22/08/26 18:16]
        * `size` is NOT the length of the string passed in `src`.
        *
        * In fact, `src` and `dest` will regularly contain garbage off the end,
        * so all searches must be constrained by `strlen(src)`.
        */

        int strlen = src.IndexOf((byte)0x00);

        /* [fkelava 22/08/26 15:40]
         * There are three errors the game's path normalizer fixes.
         * - A path might not have the stream prefix prepended.
         * - Some shader paths have the wrong file extension.
         * - Some paths have the wrong platform ID.
         *
         * The second and third could have been completely avoided by the developers
         * if they were more attentive, but they weren't, so we replicate those fixes.
         *
         * The first, however, is different in Fahrenheit's case; because we simplify the stream
         * prefix from '../../..' to '/', we must also remove now-invalid prefixes. We also
         * fix the fourth case where an old-style 'host0' path is used.
         */

        ReadOnlySpan<byte> bad_path_prefix   = "host0:"u8;
        ReadOnlySpan<byte> bad_shader_suffix = ".cgfx.phyre"u8;
        ReadOnlySpan<byte> bad_stream_prefix = "../../.."u8;
        ReadOnlySpan<byte> bad_platform_id   = "GCM"u8;
        ReadOnlySpan<byte> valid_path_prefix = "/ffx_ps2"u8;
        ReadOnlySpan<byte> valid_platform_id = "D3D11"u8;

        int pos_bad_shader_suffix = src[ .. strlen ].IndexOf(bad_shader_suffix);
        int pos_bad_stream_prefix = src[ .. strlen ].IndexOf(bad_stream_prefix);

        Index copy_range_start = pos_bad_stream_prefix != -1
            ? bad_stream_prefix.Length
            : 0;
        Index copy_range_end   = pos_bad_shader_suffix != -1
            ? pos_bad_shader_suffix
            : strlen;

        ReadOnlySpan<byte> src_valid = src[ copy_range_start .. copy_range_end ];
        src_valid.CopyTo(dest);

        if (pos_bad_shader_suffix != -1) {
            ".fx.phyre"u8.CopyTo(dest[ src_valid.Length .. ]);
        }

        int pos_bad_platform_id = dest[ .. strlen ].IndexOf(bad_platform_id);

        if (pos_bad_platform_id != -1) {
            dest[ (pos_bad_platform_id + bad_platform_id.Length) .. strlen ].CopyTo(dest [ (pos_bad_platform_id + valid_platform_id.Length) .. ]);
            valid_platform_id.CopyTo(dest[ pos_bad_platform_id .. ]);
        }

        int pos_bad_path_prefix = dest[ .. strlen ].IndexOf(bad_path_prefix);

        if (pos_bad_path_prefix != -1) {
            dest[ (pos_bad_path_prefix + bad_path_prefix.Length) .. strlen ].CopyTo(dest [ (pos_bad_path_prefix + valid_path_prefix.Length) .. ]);
            valid_path_prefix.CopyTo(dest[ pos_bad_path_prefix .. ]);
        }
    }

    /* [fkelava 21/08/26 14:10]
     * The game has the concept of a 'stream prefix', prepended to any and all paths. For some silly reason,
     * the default is '../../..'. We can simplify it, which is desirable so the user need not remember it.
     *
     * Note that the VBF stream prefix can't be empty. The game will take an access violation if so.
     */

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvThiscall) ] )]
    private void h_vbf_sp_set(BigFileStream* ptr_this, byte* ptr_stream_prefix) {
        _init_crossload();

        byte* ptr_prefix = (byte*) NativeMemory.AllocZeroed((nuint) _stream_prefix.Length);
        _stream_prefix.CopyTo(new (ptr_prefix, _stream_prefix.Length));

        FhCall.BigFileStream_setStreamPrefix.chain_from(h_vbf_sp_set).fnptr!(ptr_this, ptr_prefix);
    }

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void h_sf_sp_set(byte* ptr_stream_prefix) {
        byte* ptr_prefix = (byte*) NativeMemory.AllocZeroed((nuint) _stream_prefix.Length);

        FhCall.Phyre_PSerialization_PStreamFile_SetStreamPrefix.chain_from(h_sf_sp_set).fnptr!(ptr_prefix);
    }

    /* [fkelava 22/08/26 15:20]
     * Buckle up. This is where things get bad.
     *
     * Simplifying the stream prefix, on paper, should not be problematic. The game has a path normalizer
     * function `fiosUnifyFilename`, so at worst we have to reimplement just that, right?
     *
     * The game's usage of path normalization is, at best, inconsistent. It manages to fail in almost every way possible:
     * - Blindly hardcoding the normal '../../..' stream prefix into a path.
     * - Blindly opening a path without normalizing.
     * - Blindly hashing a path without normalizing, then looking up tables with it.
     *
     * In other words, the only reason why the path handling in the default game works _at all_ is because
     * the hardcoded stream prefix ties together an incoherent mess of code that does not function independently.
     *
     * To fix this, we have to insert path normalization in all the places the developers failed to. As you may well imagine,
     * this is a minefield and you can never tell exactly where the whole thing will fall off the rails next.
     */

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvThiscall) ] )]
    private PCluster* h_pcluster_ld(uint ptr_this, byte* ptr_name) {
        ReadOnlySpan<byte> buf_path            = new(ptr_name, 0x100);
        Span        <byte> buf_path_normalized = stackalloc byte [ 0x100 ];

        normalize_path(buf_path, buf_path_normalized);

        fixed (byte* ptr_path_normalized = buf_path_normalized) {
            return FhCall.ClusterManager_loadPCluster.chain_from(h_pcluster_ld).fnptr!(ptr_this, ptr_path_normalized);
        }
    }

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvThiscall) ] )]
    private PCluster* h_pcluster_get(uint ptr_this, byte* ptr_name) {
        ReadOnlySpan<byte> buf_path            = new(ptr_name, 0x100);
        Span        <byte> buf_path_normalized = stackalloc byte [ 0x100 ];

        normalize_path(buf_path, buf_path_normalized);

        fixed (byte* ptr_path_normalized = buf_path_normalized) {
            return FhCall.ClusterManager_getPClusterByName.chain_from(h_pcluster_get).fnptr!(ptr_this, ptr_path_normalized);
        }
    }

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void h_fiosUnifyFilename(byte* ptr_src, byte* ptr_dest, int size) {
        ReadOnlySpan<byte> src  = new(ptr_src,  size);
        Span        <byte> dest = new(ptr_dest, size);

        dest.Clear();

        normalize_path(src, dest);
    }

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvThiscall) ] )]
    private VFile* h_vbf_fopen(BigFileStream* ptr_this, byte* ptr_file_name) {
        VFile* rv = FhCall.BigFileStream_openFile.chain_from(h_vbf_fopen).fnptr!(ptr_this, ptr_file_name);

        if (rv == null) {
            _logger.Error($"{Marshal.PtrToStringAnsi((nint)ptr_file_name)} not found in VBF {Marshal.PtrToStringAnsi((nint)ptr_this->ptr_handle_0x10->ptr_file_path)}");
        }

        return rv;
    }

    /* [fkelava 23/08/26 15:53]
     * The path length limit of 0x100 is a game invariant that we replicate faithfully.
     */

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvThiscall) ] )]
    private PStreamFile* h_fopen(PStreamFile* ptr_this, byte* ptr_path, bool read_only, uint p3, uint p4, bool p5) {
        ReadOnlySpan<byte> buf_path            = new(ptr_path, 0x100);
                Span<byte> buf_path_normalized = stackalloc byte [ 0x100 ];

        normalize_path(buf_path, buf_path_normalized);

        fixed (byte* ptr_path_normalized = buf_path_normalized) {
            string path_str = new string((sbyte*)ptr_path_normalized).ToUpperInvariant();

            if (!_index.TryGetValue(path_str, out string? path_modded)) {
                PStreamFile* rv = FhCall.Phyre_PSerialization_PStreamFile_ctor.chain_from(h_fopen).fnptr!(ptr_this, ptr_path_normalized, read_only, p3, p4, p5);
                return _crossload(rv, ptr_path_normalized);
            }

            /* [fkelava 01/10/24 16:49]
             * FFX.exe+208100 at +2081B9 onward:
             * if (readOnly) { pvVar4 = CreateFileW(path, 1, 1, 0, 3, 0x08000000, 0); }
             * else          { pvVar4 = CreateFileW(path, 2, 0, 0, 4, 0x08000000, 0); }
             */

            fixed (char* ptr_path_modded = path_modded) {
                FILE_ACCESS_RIGHTS        access      = read_only
                    ? FILE_ACCESS_RIGHTS.FILE_READ_DATA
                    : FILE_ACCESS_RIGHTS.FILE_WRITE_DATA;
                FILE_SHARE_MODE           sharing     = read_only
                    ? FILE_SHARE_MODE.FILE_SHARE_READ
                    : FILE_SHARE_MODE.FILE_SHARE_NONE;
                FILE_CREATION_DISPOSITION disposition = read_only
                    ? FILE_CREATION_DISPOSITION.OPEN_EXISTING
                    : FILE_CREATION_DISPOSITION.OPEN_ALWAYS;

                FILE_FLAGS_AND_ATTRIBUTES flags = FILE_FLAGS_AND_ATTRIBUTES.FILE_FLAG_SEQUENTIAL_SCAN;

                ptr_this->handle_vbf = null;
                ptr_this->handle_os  = PInvoke.CreateFileW(
                    ptr_path_modded,
                    (uint)access,
                    sharing,
                    null,
                    disposition,
                    flags,
                    HANDLE.Null);
            }

            if (ptr_this->handle_os == HANDLE.INVALID_HANDLE_VALUE) {
                _logger.Error($"Replacement file open failed for {path_modded} - bailing out");

                PStreamFile* rv = FhCall.Phyre_PSerialization_PStreamFile_ctor.chain_from(h_fopen).fnptr!(ptr_this, ptr_path_normalized, read_only, p3, p4, p5);
                return _crossload(rv, ptr_path_normalized);
            }

            return ptr_this;
        }
    }
}
