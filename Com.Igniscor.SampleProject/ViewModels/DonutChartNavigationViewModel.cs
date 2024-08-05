using SkiaSharpControls.Abstracts;
using SkiaSharpControls.Models;
using SkiaSharpControls.Views.ControlViews.DonutChart;

namespace SkiaSharpControls.ViewModels
{
    internal class DonutChartNavigationViewModel : BaseNavigationViewModel
    {
        public DonutChartNavigationViewModel()
        {
            Items = new List<NavigationButtonItem>()
            {
                new(Helpers.Constants.Texts.DonutChartTitle, nameof(DonutChartVariant1Page)),
                new(Helpers.Constants.Texts.DonutChartWithoutDescriptionTitle, nameof(DonutChartVariant2Page)),
                new(Helpers.Constants.Texts.Experiment, nameof(DonutChartExperimentPage))
            };
        }
    }
}