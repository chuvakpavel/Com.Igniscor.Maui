using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.ProgressBar;

public partial class GradientProgressBarVariant1Page
{
    public GradientProgressBarVariant1Page()
    {
        InitializeComponent();

        BindingContext = new GradientProgressBarViewModel();
    }
}