using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.RadialProgressBar;

public partial class GradientRadialProgressBarVariant3Page
{
	public GradientRadialProgressBarVariant3Page()
	{
        BindingContext = new GradientRadialProgressBarViewModel();

        InitializeComponent();
    }
}