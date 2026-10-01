// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>The base class for custom save/load user interfaces.</summary>
public abstract class FhSaveUi : FhModule {
    /// <summary>The helper used to scale Rects to the game's forced 16:9 aspect ratio.</summary>
    protected Rect aspect_helper;

    /// <summary>The ratio between the current and reference display sizes.</summary>
    /// <example>
    ///     To scale a rectangle from the reference size to the display size,
    ///     we can use the <c>Rect.scale_raw</c> method.
    ///     <code>
    ///     Rect rect = new Rect {
    ///         pos  = pos,
    ///         size = size,
    ///     };
    ///
    ///     Rect rect_scaled     = rect       .scale_raw(scale_factor);
    ///     UV   rect_screen_uvs = rect_scaled.as_uv();
    ///     </code>
    ///
    ///     To scale a font size, we should use only the vertical scale factor:
    ///     <code>
    ///     float font_size = 36f * scale_factor.Y;
    ///     </code>
    /// </example>
    protected Vector2 display_scale => FhApi.Gui.display_size / get_ref_size();

    /// <summary>The ratio between the game's forced 16:9 aspect ratio and reference display sizes.</summary>
    protected Vector2 aspect_scale => aspect_helper.size;

    /// <summary>The scale for font sizes at the game's forced 16:9 aspect ratio.</summary>
    protected float font_scale => float.Min(aspect_scale.X, aspect_scale.Y);

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        _logger.Info($"Registering new save UI: {ModuleType}");
        FhApi.Saves.register_ui(this);

        FhApi.Events.Common.GameLoop.PostOpenSaveMenu.subscribe(post_open);

        return true;
    }

    private void post_open(EventArgs e) {
        /*
         * NOTE:
         *   Unlike in vanilla, Fahrenheit does not allow you to open the Escape menu,
         *   and hence change your resolution, while the save UI is open.
         *
         *   Because of this, we can set up the aspect_helper once in PostOpenSaveMenu
         *   instead of redoing it every frame.
         *
         */

        Vector2 window_size   = FhApi.Gui.display_size;
        float   window_aspect = window_size.X / window_size.Y;
        float   target_aspect = 16f / 9f;

        if (float.Abs(window_aspect - target_aspect) > 0.0001f) {
            aspect_helper.size = window_aspect > target_aspect
                ? window_size with { X = window_size.Y * target_aspect }
                : window_size with { Y = window_size.X / target_aspect };

            aspect_helper.pos  = (window_size - aspect_helper.size) / 2f;
        }
        else {
            aspect_helper.size = window_size;
            aspect_helper.pos  = Vector2.Zero;
        }

        aspect_helper.size = display_scale * (aspect_helper.size / FhApi.Gui.display_size);
    }

    public sealed override void render_imgui() { }

    /// <summary>Retrieve the reference size that is used by the UI.</summary>
    /// <remarks>
    ///     On-screen coordinates used in the UI should first be expressed
    ///     within these bounds, then scaled by <see cref="display_scale"/>.
    /// </remarks>
    /// <returns>The reference size used by the UI.</returns>
    protected abstract Vector2 get_ref_size();

    /// <summary>Render the save UI.</summary>
    protected internal abstract void render_ui();
}
