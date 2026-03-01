namespace Fahrenheit.FFX;

// This is likely to get folded into a different file once we figure out what it's for
[StructLayout(LayoutKind.Explicit, Size = 0x4)]
public struct StNumber {
    [FieldOffset(0x0)] public byte  category;     // Can be a character id or menu
    [FieldOffset(0x1)] public byte  type; 
    [FieldOffset(0x2)] public short command_name; // Can be a Command id or Aeon id if category is 0x1 (Yuna)
}
