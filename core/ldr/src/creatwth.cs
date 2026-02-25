// SPDX-License-Identifier: MIT

using System.Runtime.Versioning;

namespace Fahrenheit.Core.Stage0;

/* [fkelava 09/02/26 16:12]
 * Comment blocks starting with // are original comments from the Detours source provided for posterity.
 * Comment blocks wrapped with /* are my own comments.
 */

[SupportedOSPlatform("windows")]
internal unsafe static partial class Detours {

    internal const int MM_ALLOCATION_GRANULARITY  = 0x10000;
    internal const int DETOUR_PAGE_EXECUTE_ALL    = PAGE.PAGE_EXECUTE  | PAGE.PAGE_EXECUTE_READ | PAGE.PAGE_EXECUTE_READWRITE | PAGE.PAGE_EXECUTE_WRITECOPY;
    internal const int DETOUR_PAGE_NO_EXECUTE_ALL = PAGE.PAGE_NOACCESS | PAGE.PAGE_READONLY     | PAGE.PAGE_READWRITE         | PAGE.PAGE_WRITECOPY;
    internal const int DETOUR_PAGE_ATTRIBUTES     = ~(DETOUR_PAGE_EXECUTE_ALL | DETOUR_PAGE_NO_EXECUTE_ALL);

    public static uint DetourPageProtectAdjustExecute(uint dwOldProtect, uint dwNewProtect) {
        bool fOldExecute = (dwOldProtect & DETOUR_PAGE_EXECUTE_ALL) != 0;
        bool fNewExecute = (dwNewProtect & DETOUR_PAGE_EXECUTE_ALL) != 0;

        if (fOldExecute && !fNewExecute) {
            dwNewProtect = uint.CreateChecked( ((dwNewProtect & DETOUR_PAGE_NO_EXECUTE_ALL) << 4) | (dwNewProtect & DETOUR_PAGE_ATTRIBUTES) );
        }
        else if (!fOldExecute && fNewExecute) {
            dwNewProtect = uint.CreateChecked( ((dwNewProtect & DETOUR_PAGE_EXECUTE_ALL)    >> 4) | (dwNewProtect & DETOUR_PAGE_ATTRIBUTES) );
        }

        return dwNewProtect;
    }

    // Some systems do not allow executability of a page to change. This function applies
    // dwNewProtect to [pAddress, nSize), but preserving the previous executability.
    // This function is meant to be a drop-in replacement for some uses of VirtualProtectEx.
    // When "restoring" page protection, there is no need to use this function.
    public static BOOL DetourVirtualProtectSameExecuteEx(HANDLE hProcess, void* pAddress, nuint nSize, uint dwNewProtect, uint* pdwOldProtect) {
        MEMORY_BASIC_INFORMATION mbi        = default;
        nuint                    mbi_sizeof = nuint.CreateChecked(Unsafe.SizeOf<MEMORY_BASIC_INFORMATION>());

        // Query to get existing execute access.
        if (Windows.VirtualQueryEx(hProcess, pAddress, &mbi, mbi_sizeof) == 0)
            return BOOL.FALSE;

        return Windows.VirtualProtectEx(hProcess, pAddress, nSize, DetourPageProtectAdjustExecute(mbi.Protect, dwNewProtect), pdwOldProtect);
    }

    /* [fkelava 08/02/26 23:01]
     * I do not understand the meaning of the return value. It's originally typed PVOID and seems to just be a pointer
     * to pNtHeader for the purpose of 'modifying the payload later'. Bisection originates it to https://github.com/microsoft/Detours/pull/81.
     *
     * I replicated it (strongly typed) because I'm genuinely too afraid to change it, but it seems vaguely useless for our uses.
     * It's also not clear to me why pNtHeader is typed PIMAGE_NT_HEADERS32.
     */

    public static IMAGE_NT_HEADERS32* LoadNtHeaderFromProcess(HANDLE hProcess, HMODULE hModule, IMAGE_NT_HEADERS32* pNtHeader) {
        if (hModule == HMODULE.NULL)
            return null;

        byte* pbModule = (byte*)hModule;

        MEMORY_BASIC_INFORMATION mbi = default;
        IMAGE_DOS_HEADER         idh = default;

        nuint mbi_sizeof = nuint.CreateChecked(Unsafe.SizeOf<MEMORY_BASIC_INFORMATION>());
        nuint idh_sizeof = nuint.CreateChecked(Unsafe.SizeOf<IMAGE_DOS_HEADER>());
        nuint inh_sizeof = nuint.CreateChecked(Unsafe.SizeOf<IMAGE_NT_HEADERS32>());

        if (Windows.VirtualQueryEx(hProcess, hModule, &mbi, mbi_sizeof) == 0) {
            Console.WriteLine("VirtualQueryEx() failed in LoadNtHeaderFromProcess()");
            return null;
        }

        if (!Windows.ReadProcessMemory(hProcess, hModule, &idh, idh_sizeof, null)) {
            Console.WriteLine("ReadProcessMemory(idh) failed in LoadNtHeaderFromProcess()");
            return null;
        }

        nuint e_lfanew = nuint.CreateChecked(idh.e_lfanew);

        if (idh.e_magic != IMAGE.IMAGE_DOS_SIGNATURE || e_lfanew > mbi.RegionSize || e_lfanew < idh_sizeof)
            return null;

        if (!Windows.ReadProcessMemory(hProcess, pbModule + idh.e_lfanew, pNtHeader, inh_sizeof, null)) {
            Console.WriteLine("ReadProcessMemory(inh) failed in LoadNtHeaderFromProcess()");
            return null;
        }

        if (pNtHeader->Signature != IMAGE.IMAGE_NT_SIGNATURE)
            return null;

        return (IMAGE_NT_HEADERS32*)(pbModule + idh.e_lfanew);
    }


    /* [fkelava 08/02/26 18:46]
     * https://github.com/microsoft/Detours/blob/9764cebcb1a75940e68fa83d6730ffaf0f669401/src/creatwth.cpp#L86
     *
     * EnumProcessModulesEx and similar functions do not seem to work on suspended processes
     * because they have not yet run any code, including the process startup code in `ntdll`.
     * See https://devblogs.microsoft.com/oldnewthing/20150716-00/?p=45131.
     *
     * Detours seems to work around this by walking through every single page
     * in the process until it finds one that has a mapped PE header.
     */

    public static HMODULE EnumerateModulesInProcess(HANDLE hProcess, HMODULE hModuleLast, IMAGE_NT_HEADERS32* pNtHeader, IMAGE_NT_HEADERS32** pRemoteNtHeader) {
        if (pRemoteNtHeader != null) {
            *pRemoteNtHeader = null;
        }

        byte* pbLast = (byte*)hModuleLast + MM_ALLOCATION_GRANULARITY;

        MEMORY_BASIC_INFORMATION mbi        = default;
        nuint                    mbi_sizeof = nuint.CreateChecked(Unsafe.SizeOf<MEMORY_BASIC_INFORMATION>());

        for (; ; pbLast = (byte*)mbi.BaseAddress + mbi.RegionSize ) {
            if (Windows.VirtualQueryEx(hProcess, pbLast, &mbi, mbi_sizeof) == 0)
                break;

            // Usermode address space has such an unaligned region size always at the
            // end and only at the end.
            //

            if ((mbi.RegionSize & 0xFFF) == 0xFFF)
                break;

            if ((byte*)mbi.BaseAddress + mbi.RegionSize < pbLast)
                break;

            // Skip uncommitted regions and guard pages.
            //

            if (mbi.State != MEM.MEM_COMMIT || (mbi.Protect & 0xFF) == PAGE.PAGE_NOACCESS || (mbi.Protect & PAGE.PAGE_GUARD) == PAGE.PAGE_GUARD)
                continue;

            IMAGE_NT_HEADERS32* remoteHeader = LoadNtHeaderFromProcess(hProcess, (HMODULE)pbLast, pNtHeader);
            if (remoteHeader != null) {
                if (pRemoteNtHeader != null) {
                    *pRemoteNtHeader = remoteHeader;
                }

                return (HMODULE)pbLast;
            }
        }

        return HMODULE.NULL;
    }

    public static BOOL RecordExeRestore(HANDLE hProcess, HMODULE hModule, ref _DETOUR_EXE_RESTORE der) {
        uint der_sizeof = uint.CreateChecked(Unsafe.SizeOf<_DETOUR_EXE_RESTORE>());
        uint idh_sizeof = uint.CreateChecked(Unsafe.SizeOf<IMAGE_DOS_HEADER>());
        uint ish_sizeof = uint.CreateChecked(Unsafe.SizeOf<IMAGE_SECTION_HEADER>());
        uint clr_sizeof = uint.CreateChecked(Unsafe.SizeOf<_DETOUR_CLR_HEADER>());

        der.cb    = der_sizeof;
        der.pidh  = (byte*)hModule;
        der.cbidh = idh_sizeof;

        fixed (IMAGE_DOS_HEADER* idh_ptr = &der.idh) {
            if (!Windows.ReadProcessMemory(hProcess, der.pidh, idh_ptr, idh_sizeof, null)) {
                Console.WriteLine("ReadProcessMemory(idh) failed in RecordExeRestore()");
                return BOOL.FALSE;
            }
        }

        Console.WriteLine($"IDH: 0x{(nint)der.pidh:X}-0x{(nint)(der.pidh + der.cbidh)}");

        // We read the NT header in two passes to get the full size.
        // First we read just the Signature and FileHeader.

        /* [fkelava 09/02/26 16:12]
         * FIELD_OFFSET(IMAGE_NT_HEADERS, OptionalHeader); -> sizeof(DWORD) + sizeof(IMAGE_FILE_HEADER)
         */

        der.pinh  = der.pidh + der.idh.e_lfanew;
        der.cbinh = 24;

        fixed (InlineArray1544<byte>* inh_ptr = &der.union.raw) {
            if (!Windows.ReadProcessMemory(hProcess, der.pinh, inh_ptr, der.cbinh, null)) {
                Console.WriteLine("ReadProcessMemory(idh) failed in RecordExeRestore()");
                return BOOL.FALSE;
            }
        }

        // Second we read the OptionalHeader and Section headers.
        der.cbinh = uint.CreateChecked(24 + der.union.inh32.FileHeader.SizeOfOptionalHeader + (der.union.inh32.FileHeader.NumberOfSections * ish_sizeof));

        /* [fkelava 09/02/26 16:12]
         * der.cbinh > sizeof(der.raw)
         */

        if (der.cbinh > 1544) {
            Console.WriteLine("der.cbinh > sizeof(der.raw) in RecordExeRestore()");
            return BOOL.FALSE;
        }

        fixed (InlineArray1544<byte>* inh_ptr = &der.union.raw) {
            if (!Windows.ReadProcessMemory(hProcess, der.pinh, inh_ptr, der.cbinh, null)) {
                Console.WriteLine("ReadProcessMemory(inh) failed in RecordExeRestore()");
                return BOOL.FALSE;
            }
        }

        Console.WriteLine($"INH: 0x{(nint)der.pinh:X}-0x{(nint)(der.pinh + der.cbinh)}");

        // Third, we read the CLR header
        if (der.union.inh32.OptionalHeader.Magic == IMAGE.IMAGE_NT_OPTIONAL_HDR32_MAGIC) {
            IMAGE_DATA_DIRECTORY clr_directory = der.union.inh32.OptionalHeader.DataDirectory[IMAGE.IMAGE_DIRECTORY_ENTRY_COM_DESCRIPTOR];

            if (clr_directory.VirtualAddress != 0 && clr_directory.Size != 0) {
                Console.WriteLine($"CLR32.VirtAddr=0x{clr_directory.VirtualAddress:X}, CLR.Size={clr_directory.VirtualAddress}");
                der.pclr = ((byte*)hModule) + clr_directory.VirtualAddress;
            }
        }
        else if (der.union.inh64.OptionalHeader.Magic == IMAGE.IMAGE_NT_OPTIONAL_HDR64_MAGIC) {
            IMAGE_DATA_DIRECTORY clr_directory = der.union.inh64.OptionalHeader.DataDirectory[IMAGE.IMAGE_DIRECTORY_ENTRY_COM_DESCRIPTOR];

            if (clr_directory.VirtualAddress != 0 && clr_directory.Size != 0) {
                Console.WriteLine($"CLR64.VirtAddr=0x{clr_directory.VirtualAddress:X}, CLR.Size={clr_directory.VirtualAddress}");
                der.pclr = ((byte*)hModule) + clr_directory.VirtualAddress;
            }
        }

        if (der.pclr != null) {
            der.cbclr = clr_sizeof;

            fixed (_DETOUR_CLR_HEADER* clr_ptr = &der.clr) {
                if (!Windows.ReadProcessMemory(hProcess, der.pclr, clr_ptr, der.cbclr, null)) {
                    Console.WriteLine("ReadProcessMemory(clr) failed in RecordExeRestore()");
                    return BOOL.FALSE;
                }
            }

            Console.WriteLine($"CLR: 0x{(nint)der.pclr:X}-0x{(nint)(der.pclr + der.cbclr)}");
        }

        return BOOL.TRUE;
    }

/* [fkelava 09/02/26 16:19]
 * #if DETOURS_64BIT
 */

    public static BOOL UpdateFrom32To64(HANDLE hProcess, HMODULE hModule, ushort machine, ref _DETOUR_EXE_RESTORE der) {

        IMAGE_DOS_HEADER       idh   = default;
        IMAGE_NT_HEADERS32     inh32 = default;
        IMAGE_NT_HEADERS64     inh64 = default;
        IMAGE_SECTION_HEADER[] sects = new IMAGE_SECTION_HEADER[32];

        uint   idh_sizeof   = uint  .CreateChecked(Unsafe.SizeOf<IMAGE_DOS_HEADER>());
        uint   inh32_sizeof = uint  .CreateChecked(Unsafe.SizeOf<IMAGE_NT_HEADERS32>());
        uint   inh64_sizeof = uint  .CreateChecked(Unsafe.SizeOf<IMAGE_NT_HEADERS64>());
        uint   ish_sizeof   = uint  .CreateChecked(Unsafe.SizeOf<IMAGE_SECTION_HEADER>());
        ushort ioh64_sizeof = ushort.CreateChecked(Unsafe.SizeOf<IMAGE_OPTIONAL_HEADER64>());

        byte* pbModule = (byte*)hModule;

        if (!Windows.ReadProcessMemory(hProcess, pbModule, &idh, idh_sizeof, null)) {
            Console.WriteLine("ReadProcessMemory(idh) failed in UpdateFrom32To64()");
            return BOOL.FALSE;
        }

        Console.WriteLine($"IDH: 0x{(nint)pbModule:X}-0x{(nint)(pbModule + idh_sizeof)}");

        byte* pnh = pbModule + idh.e_lfanew;

        if (!Windows.ReadProcessMemory(hProcess, pnh, &inh32, inh32_sizeof, null)) {
            Console.WriteLine("ReadProcessMemory(inh32) failed in UpdateFrom32To64()");
            return BOOL.FALSE;
        }

        Console.WriteLine($"INH32: 0x{(nint)pnh:X}-0x{(nint)(pnh + inh32_sizeof)}");

        if (inh32.FileHeader.NumberOfSections > 32)
            return BOOL.FALSE;

        /* [fkelava 09/02/26 16:12]
         * 24 -> FIELD_OFFSET(IMAGE_NT_HEADERS, OptionalHeader)
         */

        byte* psects = pnh + 24 + inh32.FileHeader.SizeOfOptionalHeader;
        nuint cb     = inh32.FileHeader.NumberOfSections * ish_sizeof;

        fixed (IMAGE_SECTION_HEADER* ptr_sect_arr = &sects[0]) {
            if (!Windows.ReadProcessMemory(hProcess, psects, ptr_sect_arr, cb, null)) {
                Console.WriteLine("ReadProcessMemory(ish) failed in UpdateFrom32To64()");
                return BOOL.FALSE;
            }
        }

        Console.WriteLine($"ISH: 0x{(nint)psects:X}-0x{(nint)(psects + cb)}");

        inh64.Signature                       = inh32.Signature;
        inh64.FileHeader                      = inh32.FileHeader;
        inh64.FileHeader.Machine              = machine;
        inh64.FileHeader.SizeOfOptionalHeader = ioh64_sizeof;

        inh64.OptionalHeader.Magic                       = IMAGE.IMAGE_NT_OPTIONAL_HDR64_MAGIC;
        inh64.OptionalHeader.MajorLinkerVersion          = inh32.OptionalHeader.MajorLinkerVersion;
        inh64.OptionalHeader.MinorLinkerVersion          = inh32.OptionalHeader.MinorLinkerVersion;
        inh64.OptionalHeader.SizeOfCode                  = inh32.OptionalHeader.SizeOfCode;
        inh64.OptionalHeader.SizeOfInitializedData       = inh32.OptionalHeader.SizeOfInitializedData;
        inh64.OptionalHeader.SizeOfUninitializedData     = inh32.OptionalHeader.SizeOfUninitializedData;
        inh64.OptionalHeader.AddressOfEntryPoint         = inh32.OptionalHeader.AddressOfEntryPoint;
        inh64.OptionalHeader.BaseOfCode                  = inh32.OptionalHeader.BaseOfCode;
        inh64.OptionalHeader.ImageBase                   = inh32.OptionalHeader.ImageBase;
        inh64.OptionalHeader.SectionAlignment            = inh32.OptionalHeader.SectionAlignment;
        inh64.OptionalHeader.FileAlignment               = inh32.OptionalHeader.FileAlignment;
        inh64.OptionalHeader.MajorOperatingSystemVersion = inh32.OptionalHeader.MajorOperatingSystemVersion;
        inh64.OptionalHeader.MinorOperatingSystemVersion = inh32.OptionalHeader.MinorOperatingSystemVersion;
        inh64.OptionalHeader.MajorImageVersion           = inh32.OptionalHeader.MajorImageVersion;
        inh64.OptionalHeader.MinorImageVersion           = inh32.OptionalHeader.MinorImageVersion;
        inh64.OptionalHeader.MajorSubsystemVersion       = inh32.OptionalHeader.MajorSubsystemVersion;
        inh64.OptionalHeader.MinorSubsystemVersion       = inh32.OptionalHeader.MinorSubsystemVersion;
        inh64.OptionalHeader.Win32VersionValue           = inh32.OptionalHeader.Win32VersionValue;
        inh64.OptionalHeader.SizeOfImage                 = inh32.OptionalHeader.SizeOfImage;
        inh64.OptionalHeader.SizeOfHeaders               = inh32.OptionalHeader.SizeOfHeaders;
        inh64.OptionalHeader.CheckSum                    = inh32.OptionalHeader.CheckSum;
        inh64.OptionalHeader.Subsystem                   = inh32.OptionalHeader.Subsystem;
        inh64.OptionalHeader.DllCharacteristics          = inh32.OptionalHeader.DllCharacteristics;
        inh64.OptionalHeader.SizeOfStackReserve          = inh32.OptionalHeader.SizeOfStackReserve;
        inh64.OptionalHeader.SizeOfStackCommit           = inh32.OptionalHeader.SizeOfStackCommit;
        inh64.OptionalHeader.SizeOfHeapReserve           = inh32.OptionalHeader.SizeOfHeapReserve;
        inh64.OptionalHeader.SizeOfHeapCommit            = inh32.OptionalHeader.SizeOfHeapCommit;
        inh64.OptionalHeader.LoaderFlags                 = inh32.OptionalHeader.LoaderFlags;
        inh64.OptionalHeader.NumberOfRvaAndSizes         = inh32.OptionalHeader.NumberOfRvaAndSizes;

        for (int n = 0; n < IMAGE.IMAGE_NUMBEROF_DIRECTORY_ENTRIES; n++) {
            inh64.OptionalHeader.DataDirectory[n] = inh32.OptionalHeader.DataDirectory[n];
        }

        uint dwProtect = 0;
        if (!DetourVirtualProtectSameExecuteEx(hProcess, pbModule, inh64.OptionalHeader.SizeOfHeaders, PAGE.PAGE_EXECUTE_READWRITE, &dwProtect)) {
            Console.WriteLine("DetourVirtualProtectSameExecuteEx() failed in UpdateFrom32To64()");
            return BOOL.FALSE;
        }

        if (!Windows.WriteProcessMemory(hProcess, pnh, &inh64, inh64_sizeof, null)) {
            Console.WriteLine("WriteProcessMemory(inh) failed in UpdateFrom32To64()");
            return BOOL.FALSE;
        }

        Console.WriteLine($"WriteProcessMemory(inh): 0x{(nint)pnh:X}-0x{(nint)(pnh + inh64_sizeof)}");

        /* [fkelava 09/02/26 16:12]
         * 24 -> FIELD_OFFSET(IMAGE_NT_HEADERS, OptionalHeader)
         */

        psects = pnh + 24 + inh64.FileHeader.SizeOfOptionalHeader;
        cb     = inh64.FileHeader.NumberOfSections * ish_sizeof;

        fixed (IMAGE_SECTION_HEADER* ptr_sect_arr = &sects[0]) {
            if (!Windows.WriteProcessMemory(hProcess, psects, ptr_sect_arr, cb, null)) {
                Console.WriteLine("WriteProcessMemory(ish) failed in UpdateFrom32To64()");
                return BOOL.FALSE;
            }
        }

        Console.WriteLine($"WriteProcessMemory(ish): 0x{(nint)psects:X}-0x{(nint)(psects + cb)}");

        if (!RecordExeRestore(hProcess, hModule, ref der))
            return BOOL.FALSE;

        /* [fkelava 09/02/26 16:12]
         * 1 -> ReplacesCorHdrNumericDefines.COMIMAGE_FLAGS_ILONLY
         */

        if (der.pclr != null && ((der.clr.Flags & 1) == 1)) {
            der.union.inh64.OptionalHeader.DataDirectory[IMAGE.IMAGE_DIRECTORY_ENTRY_IMPORT].VirtualAddress = 0;
            der.union.inh64.OptionalHeader.DataDirectory[IMAGE.IMAGE_DIRECTORY_ENTRY_IMPORT].Size           = 0;

            if (!Windows.WriteProcessMemory(hProcess, pnh, &inh64, inh64_sizeof, null)) {
                Console.WriteLine("WriteProcessMemory(inh) failed in UpdateFrom32To64()");
                return BOOL.FALSE;
            }
        }

        uint dwOld = 0;
        return Windows.VirtualProtectEx(hProcess, pbModule, inh64.OptionalHeader.SizeOfHeaders, dwProtect, &dwOld);
    }

/* [fkelava 09/02/26 16:19]
 * #endif // DETOURS_64BIT
 */

    public static BOOL DetourUpdateProcessWithDll(HANDLE hProcess, byte* rlpDlls, uint nDlls) {

        BOOL bIs32BitProcess;
        BOOL bIs64BitOS = Environment.Is64BitOperatingSystem;

        HMODULE hModule = HMODULE.NULL;
        HMODULE hLast   = HMODULE.NULL;

        while (true) {
            IMAGE_NT_HEADERS32 inh;

            if ((hLast = EnumerateModulesInProcess(hProcess, hLast, &inh, null)) == HMODULE.NULL) {
                break;
            }

            Console.WriteLine($"0x{hLast:X} - machine {inh.FileHeader.Machine}, magic {inh.OptionalHeader.Magic}");

            if ((inh.FileHeader.Characteristics & IMAGE.IMAGE_FILE_DLL) == 0) {
                hModule = hLast;
                Console.WriteLine($"0x{hLast:X} - found EXE");
            }
        }

        if (hModule == HMODULE.NULL)
            return BOOL.FALSE;

        if (bIs64BitOS) {
            if (!Windows.IsWow64Process(hProcess, &bIs32BitProcess)) {
                Console.WriteLine("IsWow64Process() failed in DetourUpdateProcessWithDll()");
                return BOOL.FALSE;
            }
        }
        else bIs32BitProcess = BOOL.TRUE;

        return DetourUpdateProcessWithDllEx(hProcess, hModule, bIs32BitProcess, rlpDlls, nDlls);
    }

    public static BOOL DetourUpdateProcessWithDllEx(HANDLE hProcess, HMODULE hModule, BOOL bIs32BitProcess, byte* rlpDlls, uint nDlls) {
        BOOL bIs32BitExe = BOOL.FALSE;

        IMAGE_NT_HEADERS32 inh;

        if (hModule == HMODULE.NULL || LoadNtHeaderFromProcess(hProcess, hModule, &inh) == null)
            return BOOL.FALSE;

        if (inh.OptionalHeader.Magic == IMAGE.IMAGE_NT_OPTIONAL_HDR32_MAGIC && inh.FileHeader.Machine != 0)
            bIs32BitExe = BOOL.TRUE;

        /*
         * Spurious null check elided https://github.com/microsoft/Detours/blob/9764cebcb1a75940e68fa83d6730ffaf0f669401/src/creatwth.cpp#L777
         */

        _DETOUR_EXE_RESTORE der = default;

        if (!RecordExeRestore(hProcess, hModule, ref der))
            return BOOL.FALSE;

        if (Environment.Is64BitProcess) {
            // Try to convert a neutral 32-bit managed binary to a 64-bit managed binary.
            if (bIs32BitExe && !bIs32BitProcess) {
                /* [fkelava 09/02/26 16:12]
                 * 1 -> ReplacesCorHdrNumericDefines.COMIMAGE_FLAGS_ILONLY
                 * 2 -> ReplacesCorHdrNumericDefines.COMIMAGE_FLAGS_32BITREQUIRED
                 */

                // Native binary        or mixed-mode MSIL          or 32-bit required MSIL
                if (der.pclr == null || (der.clr.Flags & 1) == 1 || (der.clr.Flags & 2) != 2) {
                    Console.WriteLine("unsupported process type in DetourUpdateProcessWithDllEx()");
                    return BOOL.FALSE;
                }

                /* [fkelava 09/02/26 17:18]
                 * TODO: unsuppress ARM64/IA64 branches
                 */

                if (!UpdateFrom32To64(hProcess, hModule, IMAGE.IMAGE_FILE_MACHINE_AMD64, ref der)) {
                    Console.WriteLine("UpdateFrom32To64() failed in DetourUpdateProcessWithDllEx()");
                    return BOOL.FALSE;
                }
            }
        }

        if (!Environment.Is64BitProcess) {
            if (bIs32BitProcess) {
                
            }
        }


        return BOOL.TRUE;
    }



}
