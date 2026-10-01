// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[Flags]
public enum ActorFlags : uint {
    KEEP_FPS = 1 << 14,
}

[InlineArray(26)]
public struct ChrPosVectors {
    private Vector4 _e0;
}

[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto, Pack = 4, Size = 0x880)]
public unsafe struct Actor {
    [FieldOffset(0x0)]   public ushort     chr_id;
    [FieldOffset(0x2)]   public ushort     chr_enabled;
    [FieldOffset(0x4)]   public char*      chr_name;
    [FieldOffset(0xC)]   public Vector4    chr_pos_vec;
    [FieldOffset(0x1C)]  public Vector4    chr_pos_vec_bkup;
    [FieldOffset(0x5C)]  public Vector4    chr_scale_vec;
    [FieldOffset(0x6C)]  public Vector4    chr_offset_vec;
    [FieldOffset(0x154)] public float      chr_speed;
    [FieldOffset(0x158)] public float      chr_rotation_rad;
    [FieldOffset(0x168)] public float      chr_direction;
    [FieldOffset(0x170)] public float      chr_run_anim_spd_threshold;
    [FieldOffset(0x194)] public ActorFlags chr_flags;
    [FieldOffset(0x330)] public LVec3f     chr_shade_r_vec;
    [FieldOffset(0x33C)] public LVec3f     chr_shade_g_vec;
    [FieldOffset(0x348)] public LVec3f     chr_shade_b_vec;
    [FieldOffset(0x354)] public LVec3f     chr_transparency_vec;
    [FieldOffset(0x414)] public float      chr_neck_mot_target_pct;
    [FieldOffset(0x418)] public float      chr_neck_mot_actual_pct;
    [FieldOffset(0x41C)] public float      chr_neck_facing_target_pct;
    [FieldOffset(0x420)] public float      chr_neck_speed;
    [FieldOffset(0x424)] public float      chr_neck_rot_limit_l; // in deg
    [FieldOffset(0x428)] public float      chr_neck_rot_limit_h; // in deg
    [FieldOffset(0x42C)] public Matrix4x4  chr_neck_matrix_0; // battle only during targeting
    [FieldOffset(0x46C)] public Matrix4x4  chr_neck_matrix_1; // battle only during targeting
    // ? character sub-positions (limbs et al.)
    [FieldOffset(0x524)] public ChrPosVectors chr_optpos;
    [FieldOffset(0x824)] public uint          chr_var1; // TODO: Figure out a better name. Comes from CSR.
}
