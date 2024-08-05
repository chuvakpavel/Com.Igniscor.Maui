using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.RadialProgressBar;

public partial class GradientRadialProgressBarVariant1Page
{
	public GradientRadialProgressBarVariant1Page()
	{
        InitializeComponent();

        BindingContext = new GradientRadialProgressBarViewModel();
	}
}