using SkiaSharpControls.ViewModels;

namespace SkiaSharpControls.Views.ControlViews.DonutChart;

public partial class DonutChartVariant2Page
{
	public DonutChartVariant2Page()
    {
		InitializeComponent();

        BindingContext = new DonutChartViewModel();
	}
}