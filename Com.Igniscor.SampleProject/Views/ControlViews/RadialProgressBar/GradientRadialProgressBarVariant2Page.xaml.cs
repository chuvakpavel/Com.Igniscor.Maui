using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.RadialProgressBar;

public partial class GradientRadialProgressBarVariant2Page
{

    public GradientRadialProgressBarVariant2Page()
    {
        InitializeComponent();
        BindingContext = new GradientRadialProgressBarViewModel();
    }
}