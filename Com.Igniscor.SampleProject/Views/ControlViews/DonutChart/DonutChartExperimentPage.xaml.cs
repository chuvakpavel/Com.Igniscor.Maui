using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.DonutChart;

public partial class DonutChartExperimentPage
{
	public DonutChartExperimentPage()
    {
        InitializeComponent();

        BindingContext = new DonutChartViewModel();
	}
}