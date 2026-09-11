// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Atel;

public enum AtelSignalState : byte {
    Ignore       = 0x0,
    Run          = 0x1,
    Complete     = 0x2,
    Acknowledged = 0x3,
}

[StructLayout(LayoutKind.Sequential, Size = 0x16)]
public unsafe struct AtelSignal {
    public  AtelSignal*     next;        
    public  AtelSignal*     prev;        
    public  ushort          entry_point; 
    public  ushort          src_work_idx;
    public  ushort          tgt_work_idx;
    public  byte            flags;       
    public  AtelSignalState state;       
    private short           __0x10;      
    private short           __0x12;      
    public  ushort          ctrl_idx;    

    public byte priority       { readonly get { return flags.get_bits(0, 4); } set { flags.set_bits(0, 4, value); } }
    public byte process_status { readonly get { return flags.get_bits(4, 4); } set { flags.set_bits(4, 4, value); } }
}
