using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.DonutChart;

public partial class DonutChartNavigationPage
{
	public DonutChartNavigationPage()
	{
		InitializeComponent();

        BindingContext = new DonutChartNavigationViewModel();
    }
}