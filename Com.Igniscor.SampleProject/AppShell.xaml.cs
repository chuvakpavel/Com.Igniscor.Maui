using SkiaSharpControls.Views.ControlViews.DonutChart;
using SkiaSharpControls.Views.ControlViews.ProgressBar;
using SkiaSharpControls.Views.ControlViews.RadialProgressBar;

namespace SkiaSharpControls;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(GradientRadialProgressBarVariant1Page),typeof(GradientRadialProgressBarVariant1Page));
        Routing.RegisterRoute(nameof(GradientRadialProgressBarVariant2Page),typeof(GradientRadialProgressBarVariant2Page));
        Routing.RegisterRoute(nameof(GradientRadialProgressBarVariant3Page),typeof(GradientRadialProgressBarVariant3Page));
        Routing.RegisterRoute(nameof(GradientRadialProgressBarExperimentalPage),typeof(GradientRadialProgressBarExperimentalPage));
        Routing.RegisterRoute(nameof(GradientProgressBarVariant1Page),typeof(GradientProgressBarVariant1Page));
        Routing.RegisterRoute(nameof(GradientProgressBarExperiment1Page),typeof(GradientProgressBarExperiment1Page));
        Routing.RegisterRoute(nameof(GradientProgressBarVariant2Page),typeof(GradientProgressBarVariant2Page));
        Routing.RegisterRoute(nameof(GradientProgressBarExperiment2Page),typeof(GradientProgressBarExperiment2Page));
        Routing.RegisterRoute(nameof(DonutChartVariant1Page),typeof(DonutChartVariant1Page));
        Routing.RegisterRoute(nameof(DonutChartVariant2Page),typeof(DonutChartVariant2Page));
        Routing.RegisterRoute(nameof(DonutChartExperimentPage),typeof(DonutChartExperimentPage));
    }
}