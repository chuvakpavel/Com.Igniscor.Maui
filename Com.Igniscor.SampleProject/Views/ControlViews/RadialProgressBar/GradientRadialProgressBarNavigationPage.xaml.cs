using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.RadialProgressBar;

public sealed partial class GradientRadialProgressBarNavigationPage
{
    public GradientRadialProgressBarNavigationPage()
    {
        if (Application.Current != null) Application.Current.UserAppTheme = AppTheme.Light;

        InitializeComponent();

        BindingContext = new GradientRadialProgressBarNavigationViewModel();
    }
}