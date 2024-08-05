using SkiaSharpControls.Abstracts;
using SkiaSharpControls.Models;
using SkiaSharpControls.Views.ControlViews.RadialProgressBar;

namespace SkiaSharpControls.ViewModels
{
    internal class GradientRadialProgressBarNavigationViewModel : BaseNavigationViewModel
    {
        public GradientRadialProgressBarNavigationViewModel()
        {
            Items = new List<NavigationButtonItem>()
            {
                new(Helpers.Constants.Texts.RadialProgressBarTitle, nameof(GradientRadialProgressBarVariant1Page)),
                new(Helpers.Constants.Texts.RadialHorizontalProgressBarTitle, nameof(GradientRadialProgressBarVariant2Page)),
                new(Helpers.Constants.Texts.RadialVerticalProgressBarTitle, nameof(GradientRadialProgressBarVariant3Page)),
                new(Helpers.Constants.Texts.Experiment, nameof(GradientRadialProgressBarExperimentalPage))
            };
        }
    }
}