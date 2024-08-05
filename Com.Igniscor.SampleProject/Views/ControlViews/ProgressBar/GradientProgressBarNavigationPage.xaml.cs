using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.ProgressBar;

public partial class GradientProgressBarNavigationPage
{
    public GradientProgressBarNavigationPage()
    {
        InitializeComponent();

        BindingContext = new GradientProgressBarNavigationViewModel();
    }
}