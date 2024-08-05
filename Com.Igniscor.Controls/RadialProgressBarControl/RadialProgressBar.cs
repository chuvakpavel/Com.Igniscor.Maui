using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace RadialProgressBarControl;

/// <summary>
/// Radial progress bar control
/// </summary>
/// <remarks>
/// Register RadialProgressBarControl in the builder to use this control.
/// </remarks>
public class RadialProgressBar : SKCanvasView
{
    #region Bindable Properties

    #region StartAngle Property

    /// <summary>
    /// Bindable property for <see cref="StartAngle"/>.
    /// </summary>
    public static readonly BindableProperty StartAngleProperty = BindableProperty.Create(
        nameof(StartAngle),
        typeof(float),
        typeof(RadialProgressBar),
        0f,
        propertyChanged: RadialProgressBar_OnPropertyChanged);


    /// <summary>
    /// Gets or sets the start angel from where starts filing of the progress bar.
    /// </summary>
    public float StartAngle
    {
        get => (float)GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    #endregion StartAngle Property

    #region SweepAngle Property

    /// <summary>
    /// Bindable property for <see cref="SweepAngle"/>.
    /// </summary>
    public static readonly BindableProperty SweepAngleProperty = BindableProperty.Create(
        nameof(SweepAngle),
        typeof(float),
        typeof(RadialProgressBar),
        90f,
        propertyChanged: RadialProgressBar_OnPropertyChanged);


    /// <summary>
    /// Gets or sets the angle of the arc.
    /// </summary>
    public float SweepAngle
    {
        get => (float)GetValue(SweepAngleProperty);
        set => SetValue(SweepAngleProperty, value);
    }

    #endregion SweepAngle Property

    #region PercentageValue Property

    /// <summary>
    /// Bindable property for <see cref="PercentageValue"/>.
    /// </summary>
    public static readonly BindableProperty PercentageValueProperty = BindableProperty.Create(
        nameof(PercentageValue),
        typeof(float),
        typeof(RadialProgressBar),
        RadialProgressBarHelper.MinPercentageValue,
        BindingMode.OneWay,
        PercentageValue_OnValidateValue,
        RadialProgressBar_OnPropertyChanged);


    /// <summary>
    /// Gets or set the progress bar fill from 0 to 1.
    /// </summary>
    public float PercentageValue
    {
        get => (float)GetValue(PercentageValueProperty);
        set => SetValue(PercentageValueProperty, value);
    }

    private static bool PercentageValue_OnValidateValue(BindableObject bindable, object? value) =>
        value != null && (float)value >= RadialProgressBarHelper.MinPercentageValue &&
        (float)value <= RadialProgressBarHelper.MaxPercentageValue;

    #endregion PercentageValue Property

    #region BarWidth Property

    /// <summary>
    /// Bindable property for <see cref="BarWidth"/>.
    /// </summary>
    public static readonly BindableProperty BarWidthProperty = BindableProperty.Create(
        nameof(BarWidth),
        typeof(float),
        typeof(RadialProgressBar),
        10f,
        BindingMode.OneWay,
        (_, value) => value != null && (float)value >= 0,
        RadialProgressBar_OnPropertyChanged);

    /// <summary>
    /// Gets or sets the width of the progress bar.
    /// </summary>
    public float BarWidth
    {
        get => (float)GetValue(BarWidthProperty);
        set => SetValue(BarWidthProperty, value);
    }

    #endregion BarWidth Property

    #region StartProgressColor Property

    /// <summary>
    /// Bindable property for <see cref="StartColor"/>.
    /// </summary>
    public static readonly BindableProperty StartColorProperty = BindableProperty.Create(
        nameof(StartColor),
        typeof(Color),
        typeof(RadialProgressBar),
        Colors.White,
        BindingMode.OneWay,
        (_, value) => value != null,
        RadialProgressBar_OnPropertyChanged);

    /// <summary>
    /// Gets or sets the initial color of the progress bar's fill gradient.
    /// </summary>

    public Color StartColor
    {
        get => (Color)GetValue(StartColorProperty);
        set => SetValue(StartColorProperty, value);
    }

    #endregion StartProgressColor Property

    #region EndProgressColor Property

    /// <summary>
    /// Bindable property for <see cref="EndColor"/>.
    /// </summary>
    public static readonly BindableProperty EndColorProperty = BindableProperty.Create(
        nameof(EndColor),
        typeof(Color),
        typeof(RadialProgressBar),
        Colors.Black,
        BindingMode.OneWay,
        (_, value) => value != null,
        RadialProgressBar_OnPropertyChanged);

    /// <summary>
    /// Gets or sets the final color of the progress bar's fill gradient.
    /// </summary>
    public Color EndColor
    {
        get => (Color)GetValue(EndColorProperty);
        set => SetValue(EndColorProperty, value);
    }

    #endregion EndProgressColor Property

    #region StartBackgroundColor Property

    /// <summary>
    /// Bindable property for <see cref="StartBackgroundColor"/>.
    /// </summary>
    public static readonly BindableProperty StartBackgroundColorProperty = BindableProperty.Create(
        nameof(StartBackgroundColor),
        typeof(Color),
        typeof(RadialProgressBar),
        Colors.Red,
        BindingMode.OneWay,
        (_, value) => value != null,
        RadialProgressBar_OnPropertyChanged);

    /// <summary>
    /// Gets or sets the initial color of the progress bar's background gradient.
    /// </summary>
    public Color StartBackgroundColor
    {
        get => (Color)GetValue(StartBackgroundColorProperty);
        set => SetValue(StartBackgroundColorProperty, value);
    }

    #endregion StartBackgroundColor Property

    #region EndBackgroundColor Property

    /// <summary>
    /// Bindable property fro <see cref="EndBackgroundColor"/>.
    /// </summary>
    public static readonly BindableProperty EndBackgroundColorProperty = BindableProperty.Create(
        nameof(EndBackgroundColor),
        typeof(Color),
        typeof(RadialProgressBar),
        Colors.Blue,
        BindingMode.OneWay,
        (_, value) => value != null,
        RadialProgressBar_OnPropertyChanged);

    /// <summary>
    /// Gets or sets the final color of the progress bar's background gradient.
    /// </summary>
    public Color EndBackgroundColor
    {
        get => (Color)GetValue(EndBackgroundColorProperty);
        set => SetValue(EndBackgroundColorProperty, value);
    }

    #endregion EndBackgroundColor Property

    #endregion Bindable Properties

    private static void RadialProgressBar_OnPropertyChanged(BindableObject bindable, object oldValue,
        object newValue) =>
        ((SKCanvasView)bindable).InvalidateSurface();

    /// <summary>
    /// Method that is called when a bound property is changed.
    /// </summary>
    /// <param name="propertyName">The name of the bound property that changed.</param>
    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        this.InvalidateSurface();
    }

    /// <summary>
    /// Raises the <see cref="E:PaintSurface" /> event.
    /// </summary>
    /// <param name="e">
    /// The <see cref="SKPaintSurfaceEventArgs" /> instance containing the event data.
    /// </param>
    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        var info = e.Info;

        canvas.Clear();

        var startColor = StartColor.ToSKColor();
        var endColor = EndColor.ToSKColor();
        var startBackgroundColor = StartBackgroundColor.ToSKColor();
        var endBackgroundColor = EndBackgroundColor.ToSKColor();
        var scale = CanvasSize.Width / (float)Width;
        var barWidth = BarWidth * scale;

        if (barWidth > info.Width / 2f)
        {
            barWidth = info.Width / 2f;
        }

        if (!SweepAngle.Equals(RadialProgressBarHelper.MinAngle) &&
            (SweepAngle % RadialProgressBarHelper.MaxAngle).Equals(RadialProgressBarHelper.MinAngle))
        {
            switch (PercentageValue)
            {
                case RadialProgressBarHelper.MaxPercentageValue:
                {
                    RadialProgressBarHelper.DrawCircle(canvas, info, barWidth, StartAngle, SweepAngle, startColor,
                        endColor);

                    break;
                }
                case RadialProgressBarHelper.MinPercentageValue:
                {
                    RadialProgressBarHelper.DrawCircle(canvas, info, barWidth, StartAngle, SweepAngle,
                        startBackgroundColor, endBackgroundColor);

                    break;
                }
                default:
                {
                    var percentageSweepAngle = (float)Math.Floor(SweepAngle * PercentageValue);

                    RadialProgressBarHelper.DrawCircle(canvas, info, barWidth, StartAngle, SweepAngle,
                        startBackgroundColor, endBackgroundColor);
                    RadialProgressBarHelper.DrawArc(canvas, info, barWidth, StartAngle, percentageSweepAngle,
                        startColor, endColor);

                    break;
                }
            }
        }
        else
        {
            switch (PercentageValue)
            {
                case RadialProgressBarHelper.MaxPercentageValue:
                {
                    RadialProgressBarHelper.DrawArc(canvas, info, barWidth, StartAngle, SweepAngle, startColor,
                        endColor);

                    break;
                }
                case RadialProgressBarHelper.MinPercentageValue:
                {
                    RadialProgressBarHelper.DrawArc(canvas, info, barWidth, StartAngle, SweepAngle,
                        startBackgroundColor, endBackgroundColor);

                    break;
                }
                default:
                {
                    var percentageSweepAngle = (float)Math.Floor(SweepAngle * PercentageValue);

                    RadialProgressBarHelper.DrawArc(canvas, info, barWidth, StartAngle, SweepAngle,
                        startBackgroundColor, endBackgroundColor);
                    RadialProgressBarHelper.DrawArc(canvas, info, barWidth, StartAngle, percentageSweepAngle,
                        startColor, endColor);

                    break;
                }
            }
        }
    }
}