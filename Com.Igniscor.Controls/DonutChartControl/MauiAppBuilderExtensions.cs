using SkiaSharp.Views.Maui.Controls.Hosting;

namespace DonutChartControl;

/// <summary>
/// extensions of the <see cref="MauiAppBuilder"/>.
/// </summary>
public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Register all need services.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns></returns>
    public static MauiAppBuilder UseDonutChartControl(this MauiAppBuilder builder)
    {
        builder.UseSkiaSharp();

        return builder;
    }

}