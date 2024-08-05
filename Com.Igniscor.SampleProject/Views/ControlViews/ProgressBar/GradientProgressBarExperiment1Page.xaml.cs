using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.ProgressBar;

public partial class GradientProgressBarExperiment1Page
{
	public GradientProgressBarExperiment1Page()
	{
        InitializeComponent();

        BindingContext = new GradientProgressBarViewModel();
	}
}