using SkiaSharpControls.Abstracts;

namespace SkiaSharpControls.ViewModels;

internal class GradientRadialProgressBarViewModel : BaseViewModel
{
    public float StartAngle { get; set;} = 180f;
    public float SweepAngle { get; set; } = 360f;
    public float PercentageValue { get; set; }
    public float BarWidth { get; set; } = 50f;
}