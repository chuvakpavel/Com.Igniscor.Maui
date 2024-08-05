using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.RadialProgressBar;

public partial class GradientRadialProgressBarExperimentalPage
{
    public GradientRadialProgressBarExperimentalPage()
    {
        InitializeComponent();

        BindingContext = new GradientRadialProgressBarViewModel();
    }
}