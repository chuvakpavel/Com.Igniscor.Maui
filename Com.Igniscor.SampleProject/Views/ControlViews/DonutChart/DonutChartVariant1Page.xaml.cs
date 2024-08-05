using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.DonutChart;

public partial class DonutChartVariant1Page
{
	public DonutChartVariant1Page()
    {
		InitializeComponent();

        BindingContext = new DonutChartViewModel();
	}
}