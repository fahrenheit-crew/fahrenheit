// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     An opaque structure used by the game's allocator.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct __ALLOC_STRUCT {
    public CRITICAL_SECTION crit_sec;
    public __ALLOC_DATA     data;
}

/// <summary>
///     An opaque structure used by the game's allocator.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct __ALLOC_DATA {
    public uint size;
    public uint _0x04;
    public uint _0x08;
    public uint addr_primary_pool;
    public uint align;
    public uint addr_last_reserve;
}

/// <summary>
///     Tracks native allocations incurred by a single caller.
/// </summary>
internal sealed record FhNativeAllocInfo(
    string     FileName,
    int        LineNumber,
    List<nint> Allocations);

/// <summary>
///     Tracks native allocations incurred by Fahrenheit objects.
/// </summary>
internal sealed unsafe class FhNativeAllocator {

    // Allocation limits
    private const nuint ALLOC_WARN_LIMIT = 8_192_000U;

    // State tracking
    private readonly Dictionary<FhModule, FhNativeAllocInfo> _caller_info_map  = [];
    private readonly Dictionary<FhModule, nuint>             _caller_total_map = [];
    private readonly Dictionary<nint,     nuint>             _alloc_to_sz_map  = [];
    private readonly Lock                                    _state_lock       = new();

    // Helpers
    private nuint _alloc_get_size(void* ptr)               => _alloc_to_sz_map[(nint)ptr];
    private void  _alloc_set_size(void* ptr, nuint length) => _alloc_to_sz_map[(nint)ptr] = length;
    private bool  _alloc_rem_size(void* ptr)               => _alloc_to_sz_map.Remove((nint)ptr);

    private nuint _caller_add_total(FhModule caller, nuint length) => _caller_total_map[caller] += length;
    private nuint _caller_sub_total(FhModule caller, nuint length) => _caller_total_map[caller] -= length;

    private void _caller_add_alloc(FhNativeAllocInfo info, void* ptr) => info.Allocations.Add((nint)ptr);
    private bool _caller_rem_alloc(FhNativeAllocInfo info, void* ptr) => info.Allocations.Remove((nint)ptr);

    private void _caller_set_info(FhModule caller, FhNativeAllocInfo info) => _caller_info_map[caller] = info;

    /// <summary>
    ///     Obtains allocation tracking information for a given <paramref name="caller"/>,
    ///     throwing if it does not exist. Used when the data is presumed to exist,
    ///     e.g. <see cref="Free(FhModule, void*)"/>.
    /// </summary>
    private FhNativeAllocInfo _caller_get_info(FhModule caller) {
        lock (_state_lock) {
            if (_caller_info_map.TryGetValue(caller, out FhNativeAllocInfo? caller_alloc_info)) {
                return caller_alloc_info;
            }

            throw new Exception($"No data for caller {caller.ModuleType} - likely called free() before alloc()?");
        }
    }

    /// <summary>
    ///     Obtains allocation tracking information for a given <paramref name="caller"/>,
    ///     creating it if it does not exist. Used when the data may not yet exist,
    ///     e.g. <see cref="Alloc(int, FhModule, string, int)"/>.
    /// </summary>
    private FhNativeAllocInfo _caller_get_info(FhModule caller,
                                               string   caller_fpath,
                                               int      caller_lnb) {
        lock (_state_lock) {
            if (_caller_info_map.TryGetValue(caller, out FhNativeAllocInfo? caller_alloc_info)) {
                return caller_alloc_info;
            }

            _caller_total_map[caller] = 0;
            return _caller_info_map[caller] = new FhNativeAllocInfo(caller_fpath, caller_lnb, []);
        }
    }

    /// <summary>
    ///     Allocates a block of memory of size <paramref name="len"/> on behalf of <paramref name="caller"/>.
    /// </summary>
    public void* alloc(    int      len,
                           FhModule caller,
        [CallerFilePath]   string   caller_fpath  = "",
        [CallerLineNumber] int      caller_lnb    = 0) {

        nuint             length = nuint.CreateChecked(len);
        FhNativeAllocInfo info   = _caller_get_info(caller, caller_fpath, caller_lnb);

        void* ptr = NativeMemory.AllocZeroed(length);
        nuint new_total;

        lock (_state_lock) {
            _alloc_set_size  (ptr, length);
            new_total = _caller_add_total(caller, length);
            _caller_add_alloc(info, ptr);
            _caller_set_info(caller, info);
        }

        if (new_total > ALLOC_WARN_LIMIT) {
            FhInternal.Log.Warning($"Caller {caller_fpath}:{caller_lnb} allocating unusually large amount of native memory ({new_total})");
        }

        FhInternal.Log.Debug($"Caller {caller_fpath}:{caller_lnb}: +{length} -> {new_total}");
        return ptr;
    }

    /// <summary>
    ///     Frees a block of memory pointed to by <paramref name="ptr"/> allocated on behalf of <paramref name="caller"/>.
    /// </summary>
    public void free(FhModule caller, void* ptr) {
        FhNativeAllocInfo info = _caller_get_info(caller);

        NativeMemory.Free(ptr);
        nuint new_total;
        nuint alloc_size;

        lock (_state_lock) {
            alloc_size = _alloc_get_size  (ptr);
            new_total  = _caller_sub_total(caller, alloc_size);
            _caller_rem_alloc(info, ptr);
            _caller_set_info (caller, info);
            _alloc_rem_size(ptr);
        }

        FhInternal.Log.Debug($"Caller {info.FileName}:{info.LineNumber}: -{alloc_size} -> {new_total}");
    }
}
