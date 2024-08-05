using CommunityToolkit.Maui;
using DonutChartControl;
using Microsoft.Extensions.Logging;
using ProgressBarControl;
using RadialProgressBarControl;

namespace SkiaSharpControls;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseRadialProgressBarControl()
            .UseProgressBarControl()
            .UseDonutChartControl()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}