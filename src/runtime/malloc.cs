// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime;

/* [fkelava 06/08/26 14:02]
 * See generally issue #253.
 *
 * The game has a long-standing issue with "green screens" in FMVs and other crashes.
 * The band-aid fix is applying the '4GB' patch. But _why_ does it happen? Can we provide a better fix?
 *
 * The game reserves 37.5% of the default address space (0x3000_0000 bytes)
 * for its primary memory pool. It then commits things into it slowly over time.
 *
 * Reserved memory is considered used by the system regardless of how much of it is actually
 * _committed_. The problem is that the game does not service all memory requests from the pool.
 * For things like FMVs, there still has to be enough additional contiguous free memory to load them.
 *
 * This worked ten or so years ago when it was made, and it works on most other platforms
 * because the high reaches of the address space should be entirely empty.
 *
 * But in the decade since, ASLR started shipping on Windows, and is the default for anything
 * built today. The OS loads such DLLs at a random high address, breaking up the previously contiguous
 * free space into smaller chunks. No block large enough to fit an FMV remains.
 *
 * The true solution is to reserve less. By deferring the moment memory is considered 'used'
 * to the _actual point of usage_, the truth is revealed; there was never a shortage of memory.
 */

/// <summary>
///     Manipulates the game's allocator to improve its behavior.
/// </summary>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
[SupportedOSPlatform("windows5.1.2600")]
public unsafe sealed class FhMallocModule : FhModule {

    private static nuint _reserved {
        get => FhUtil.get_at<nuint>(FhUtil.select(0x153CD44, 0x14E6AB4, 0x14E6AB4));
        set => FhUtil.set_at       (FhUtil.select(0x153CD44, 0x14E6AB4, 0x14E6AB4), value);
    }

    private static nuint _committed {
        get => FhUtil.get_at<nuint>(FhUtil.select(0x153CD48, 0x14E6AB8, 0x14E6AB8));
        set => FhUtil.set_at       (FhUtil.select(0x153CD48, 0x14E6AB8, 0x14E6AB8), value);
    }

    public FhMallocModule() { }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return FhCall._malloc_pool_init       .hook(this, h_mpool_init)
            && FhCall._VirtualAlloc_Reserve_NA.hook(this, h_mreserve);
    }

    /// <summary>
    ///     Replaces the game's primary memory pool initializer.
    /// </summary>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void h_mpool_init() {

        /* [fkelava 06/08/26 14:49]
         * If the game's default pool initializer fails to reserve 0x3000_0000 bytes,
         * it'll decrement that by 0x100_0000 and keep trying until it succeeds or throws.
         *
         * For sanity's sake, we don't replicate that. If we can't reserve much less than
         * the game normally uses, then failing here gives a clear indication of what is wrong.
         */

        FhCall.FUN_009428A0_008772A0.fnptr!();

        /* [fkelava 06/08/26 23:51]
         * Be VERY careful. The pool size can't be _too small_ because it's reused as the upper
         * bound of any future allocation through the primary allocator. If that size is too small
         * for whatever the game has in mind, the allocator will spiral out of control reserving
         * {POOL_SIZE} in a loop until it exhausts the entire address space, killing the process.
         *
         * Maybe one day we'll fix that latent bug, but today ain't the one.
         */

        __ALLOC_STRUCT* alloc_struct = (__ALLOC_STRUCT*) NativeMemory.Alloc(0x30);
        uint            pool_size    = 0x800_0000; // default: 0x3000_0000

        PInvoke.InitializeCriticalSection(&alloc_struct->crit_sec);

        FhUtil.set_at(FhUtil.select(0x8E900C, 0x9EDBE4, 0x9EDBE4), (uint)alloc_struct);
        _ = FhCall.FUN_00942A40_00877440.fnptr!(&alloc_struct->data, pool_size, 0, uint.CreateChecked( pool_size - 0x100000 ));

        PInvoke.EnterCriticalSection(&alloc_struct->crit_sec);
        void* rv = FhCall.FUN_00942B60_00877560.fnptr!(&alloc_struct->data, 0x10);
        PInvoke.LeaveCriticalSection(&alloc_struct->crit_sec);

        if (rv == null) {
            throw new Exception($"Failed to allocate primary memory pool of size 0x{pool_size:X8}.");
        }

        FhUtil.set_at(FhUtil.select(0x8E9010, 0x9EDBE8, 0x9EDBE8), (uint)rv);
        FhUtil.set_at(FhUtil.select(0x8E9014, 0x9EDBEC, 0x9EDBEC), pool_size);
    }

    /* [fkelava 06/08/26 14:29]
     * We previously experimented with allocating top-down. However, 'persistent' magic effects
     * handled using the `op_*` system seem to exhibit some manner of pointer tagging/truncation bug
     * which precludes our ability to shift the main pool into the space over 0x7FFF_FFFF.
     *
     * This bug recurs regardless as long as the 4GB patch is applied, just less frequently.
     * Therefore we _temporarily_ disable top-down allocation.
     */

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void* h_mreserve(uint size) {
        void* rv = PInvoke.VirtualAlloc(
            null,
            size,
            VIRTUAL_ALLOCATION_TYPE.MEM_RESERVE,
            PAGE_PROTECTION_FLAGS  .PAGE_NOACCESS
        );

        // The game does not track reversed allocations for some reason.
        if (rv != null) {
            _reserved += size;
        }

        return rv;
    }

    public override void render_imgui() {
#if DEBUG
        if (!ImGui.Begin("Fh.MDbg")) {
            ImGui.End();
            return;
        }

        ImGui.Text($"Committed: 0x{_committed:X8}");
        ImGui.Text($"Reserved:  0x{_reserved:X8}");
        ImGui.End();
#endif
    }

}
