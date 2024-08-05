using SkiaSharpControls.Abstracts;
using SkiaSharpControls.Models;
using SkiaSharpControls.Views.ControlViews.ProgressBar;

namespace SkiaSharpControls.ViewModels;

internal class GradientProgressBarNavigationViewModel : BaseNavigationViewModel
{
    public GradientProgressBarNavigationViewModel()
    {
        Items = new List<NavigationButtonItem>()
        {
            new(Helpers.Constants.Texts.HorizontalProgressBarTitle, nameof(GradientProgressBarVariant1Page)),
            new(Helpers.Constants.Texts.ExperimentalHorizontalProgressBarTitle, nameof(GradientProgressBarExperiment1Page)),
            new(Helpers.Constants.Texts.VerticalProgressBarTitle, nameof(GradientProgressBarVariant2Page)),
            new(Helpers.Constants.Texts.ExperimentalVerticalProgressBarTitle, nameof(GradientProgressBarExperiment2Page))
        };
    }
}