using SkiaSharp.Views.Maui.Controls.Hosting;

namespace RadialProgressBarControl;

/// <summary>
/// extensions of the <see cref="MauiAppBuilder"/>.
/// </summary>
public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Register all need services.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>
    /// builder
    /// </returns>
    public static MauiAppBuilder UseRadialProgressBarControl(this MauiAppBuilder builder)
    {
        builder.UseSkiaSharp();

        return builder;
    }

}