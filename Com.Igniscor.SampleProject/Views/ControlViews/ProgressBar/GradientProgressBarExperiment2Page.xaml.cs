using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.ProgressBar;

public partial class GradientProgressBarExperiment2Page
{
    public GradientProgressBarExperiment2Page()
    {
        InitializeComponent();

        BindingContext = new GradientProgressBarViewModel();
    }
}