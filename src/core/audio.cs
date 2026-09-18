// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

public sealed class FhAudio {
    /// <summary>Play a game sound with a given ID.</summary>
    /// <remarks>
    ///     The sound bank containing the desired sound must be loaded,
    ///     otherwise this functions will silently fail.
    /// </remarks>
    /// <param name="sound_id">The ID of the sound to play.</param>
    /// <param name="volume">
    ///     The volume to play the sound at, between <c>0</c> and <c>1</c>.
    ///     Defaults to <c>1</c>.
    /// </param>
    /// <param name="pan">
    ///     The panning from left to right to play the sound at, between <c>0</c> and <c>1</c>.
    ///     Defaults to <c>0.5</c>, playing from both the left and right equally.
    /// </param>
    public int play_sound(uint sound_id, float volume = 1.0f, float pan = 0.5f) {
        //TODO: Ensure the sound bank for the sound is loaded

        volume = float.Clamp(volume, 0.0f, 1.0f);
        pan    = float.Clamp(pan,    0.0f, 1.0f);

        uint int_volume = uint.CreateSaturating(volume * 127);
        uint int_pan    = uint.CreateSaturating(pan    * 127);

        return FhCall.SndSepPlay.fnptr!(sound_id, int_pan, int_volume);
    }
}
