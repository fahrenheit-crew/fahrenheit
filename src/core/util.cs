// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

public unsafe static class FhUtil {

    /// <summary>Selects between <typeparamref name="T"/>s based on the currently executing game.</summary>
    /// <remarks>If no game is executing, throws.</remarks>
    public static T select<T>(T ffx, T ffx2, T ffx2lm) {
        return FhGlobal.game_id switch {
            FhGameId.FFX    => ffx,
            FhGameId.FFX2   => ffx2,
            FhGameId.FFX2LM => ffx2lm,
            _               => throw new Exception("Attempted to `select` without a game running."),
        };
    }

    /// <summary>
    ///     Returns a pointer to <typeparamref name="T"/> at the absolute address obtained by
    ///     adding the given offset to the base address of the running game's executable.
    /// </summary>
    /// <param name="offset">The offset, from the running game executable's base address, to return a pointer to <typeparamref name="T"/> at.</param>
    /// <returns>A pointer to a value of type <typeparamref name="T"/> at the given address.</returns>
    public static T* ptr_at<T>(nint offset) where T : unmanaged => (T*)(FhEnvironment.BaseAddr + offset);

    /// <summary>
    ///     Reads a value of type <typeparamref name="T"/> from the address obtained by adding
    ///     the given offset to the base address of the running game's executable.
    /// </summary>
    /// <param name="offset">The offset, from the running game executable's base address, to read a value of type <typeparamref name="T"/> from.</param>
    /// <returns>The current value at the given address.</returns>
    public static T get_at<T>(nint offset) where T : unmanaged => *ptr_at<T>(offset);

    /// <summary>
    ///     Writes a given value of type <typeparamref name="T"/> to the address obtained
    ///     by adding the given offset to the base address of the running game's executable.
    /// </summary>
    /// <remarks>
    ///     The target offset must be in a writable memory region. If it is not, you must
    ///     use a <see cref="FhVirtualProtectScope{T}"/> to avoid an <see cref="AccessViolationException"/>.
    /// </remarks>
    /// <param name="offset">The offset, from the running game executable's base address, to write the given value to.</param>
    /// <param name="value">The value of type <typeparamref name="T"/> to write at the given address.</param>
    /// <returns>The previous value at the given address.</returns>
    public static T set_at<T>(nint offset, T value) where T : unmanaged {
        T* ptr = ptr_at<T>(offset);
        T  old = *ptr;

        *ptr = value;
        return old;
    }

    /// <summary>
    ///     Generic bitwise NOT.
    /// </summary>
    public static T not_with_cast<T>(this T x) where T : IBinaryInteger<T> {
        unchecked { return ~x; }
    }

    /// <param name="offset">A 0-based offset of the bit into the <paramref name="bitfield"/>.</param>
    /// <returns>
    ///     Whether bit at <paramref name="offset"/> into <paramref name="bitfield"/> is set.
    /// </returns>
    public unsafe static bool get_bit<T>(this T bitfield, int offset) where T : unmanaged, IBinaryInteger<T> {
        if (offset < 0 || offset >= sizeof(T) * 8) throw new ArgumentOutOfRangeException(nameof(offset));

        return ((bitfield >> offset) & T.One) != T.Zero;
    }

    /// <param name="offset">A 0-based offset into the <paramref name="bitfield"/>.</param>
    /// <param name="len">The amount of bits to read.</param>
    /// <returns>
    ///     The value of type <typeparamref name="T"/> made from <paramref name="len"/> bits <paramref name="offset"/> into <paramref name="bitfield"/>.
    /// </returns>
    public unsafe static T get_bits<T>(this T bitfield, int offset, int len) where T : unmanaged, IBinaryInteger<T> {
        if (offset <  0 || offset >=  sizeof(T) * 8)           throw new ArgumentOutOfRangeException(nameof(offset));
        if (len    <= 0 || len    >  (sizeof(T) * 8) - offset) throw new ArgumentOutOfRangeException(nameof(len));

        return (bitfield >> offset) & ((T.One << len) - T.One);
    }

    /// <summary>
    ///     Sets bit at <paramref name="offset"/> into <paramref name="bitfield"/> based on <paramref name="value"/>.
    /// </summary>
    /// <param name="offset">A 0-based offset into the <paramref name="bitfield"/>.</param>
    /// <param name="value">Whether the bit should be set to <c>1</c> if <c>true</c> or <c>0</c> if <c>false</c>.</param>
    public unsafe static void set_bit<T>(this ref T bitfield, int offset, bool value) where T : unmanaged, IBinaryInteger<T> {
        if (offset < 0 || offset >= sizeof(T) * 8) throw new ArgumentOutOfRangeException(nameof(offset));

        if (value) bitfield |=  T.One << offset;
        else       bitfield &= (T.One << offset).not_with_cast();
    }

    /// <summary>
    ///     Sets bit at <paramref name="offset"/> into <paramref name="bitfield"/> to <paramref name="value"/>.
    /// </summary>
    /// <param name="offset">A 0-based offset into the <paramref name="bitfield"/>.</param>
    /// <param name="len">The amount of bits to write.</param>
    /// <param name="value">The value to set the bits to. Only the first <paramref name="len"/> bits matter.</param>
    public unsafe static void set_bits<T>(this ref T bitfield, int offset, int len, T value) where T : unmanaged, IBinaryInteger<T> {
        if (offset < 0  || offset >=  sizeof(T) * 8)           throw new ArgumentOutOfRangeException(nameof(offset));
        if (len    <= 0 || len    >  (sizeof(T) * 8) - offset) throw new ArgumentOutOfRangeException(nameof(len));

        for (; len > 0; len--, offset++) { bitfield.set_bit(offset, value.get_bit(offset)); }
    }

    internal static JsonSerializerOptions InternalJsonOpts { get; } = new JsonSerializerOptions {
        Converters = {
            new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseUpper)
        },
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        IncludeFields  = true, // For serialization of objects such as System.Numerics.Vector{2|3|4}.
        WriteIndented  = true, // Settings and state should, within reason, be user-inspectable and editable.
        IndentSize     = 4
    };

    /// <summary>
    ///     Attempts to deserialize a <typeparamref name="T"/> at the specified <paramref name="key"/> in a JSON document.
    ///     Only keys at the <paramref name="reader"/>'s current depth are searched.
    /// </summary>
    internal static bool try_find_key_and_deserialize<T>(this ref Utf8JsonReader reader, string key, [NotNullWhen(true)] out T? t) {
        t = default;

        bool key_found = false;
        int  depth     = reader.CurrentDepth;

        while (!key_found) {
            key_found = reader.TokenType    == JsonTokenType.PropertyName
                     && reader.CurrentDepth == depth
                     && reader.GetString()  == key;

            if (key_found || !reader.Read()) break;
        }

        if (!key_found)
            return false;

        try {
            _ = reader.Read();
            t = JsonSerializer.Deserialize<T>(ref reader, InternalJsonOpts) ?? throw new Exception($"Value at key {key} was null");
            _ = reader.Read();

            return true;
        }
        catch (Exception ex) {
            FhInternal.Log.Error(ex.ToString());
            return false;
        }
    }

    internal static void enter_json_object(this ref Utf8JsonReader reader) {
        while (reader.TokenType != JsonTokenType.StartObject) {
            reader.Read();
        }
        reader.Read();
    }

    internal static void enter_json_array(this ref Utf8JsonReader reader) {
        while (reader.TokenType != JsonTokenType.StartArray) {
            reader.Read();
        }
        reader.Read();
    }

    public static Vector2 inverse(this Vector2 vec) {
        return new Vector2(1f) / vec;
    }

    public static uint as_rgba(this Vector4 vec) {
        return (uint)(
            ((byte)(vec.X * 255f) << 0)
          & ((byte)(vec.Y * 255f) << 8)
          & ((byte)(vec.Z * 255f) << 16)
          & ((byte)(vec.W * 255f) << 24)
        );
    }
}
