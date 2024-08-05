using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.ProgressBar;

public partial class GradientProgressBarVariant2Page
{
    public GradientProgressBarVariant2Page()
    {
        InitializeComponent();

        BindingContext = new GradientProgressBarViewModel();
    }
}