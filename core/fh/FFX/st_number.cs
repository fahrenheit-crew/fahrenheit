namespace Fahrenheit.FFX;

/// <summary>
///     Responsible for displaying commands the player has unlocked in menus.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x4)]
public struct StNumber {
    [FieldOffset(0x0)] public byte  category;   // Can either be a valid <see cref="PlySaveId"> id or submenu id
    [FieldOffset(0x1)] public byte  type;
    [FieldOffset(0x2)] public short command_id; // Can also be an Aeon id if category is 0x1 (Yuna)
}
