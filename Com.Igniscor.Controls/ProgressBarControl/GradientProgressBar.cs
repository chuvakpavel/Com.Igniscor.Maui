using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace ProgressBarControl;

/// <summary>
/// Gradient progress bar control.
/// </summary>
/// <remarks>
/// Register SkiaSharp in the application to use this control.
/// </remarks>
public class GradientProgressBar : SKCanvasView
{
    #region Bindable Properties

    #region Orientation Property    
    /// <summary>
    /// Bindable property for <see cref="Orientation"/>.
    /// </summary>
    public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
        nameof(Orientation),
        typeof(ProgressBarOrientation),
        typeof(GradientProgressBar),
        ProgressBarOrientation.Horizontal,
        BindingMode.OneWay,
        (_, value) => value != null,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets the orientation of the progress bar.
    /// </summary>
    public ProgressBarOrientation Orientation
    {
        get => (ProgressBarOrientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    #endregion Orientation Property

    #region PecentageValue Property    
    /// <summary>
    /// Bindable property for <see cref="PercentageValue"/>.
    /// </summary>
    public static readonly BindableProperty PercentageValueProperty = BindableProperty.Create(
        nameof(PercentageValue),
        typeof(float),
        typeof(GradientProgressBar),
        0f,
        BindingMode.OneWay,
        (_, value) => value != null,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or set the progress bar fill from 0 to 1.
    /// </summary>
    public float PercentageValue
    {
        get => (float)GetValue(PercentageValueProperty);
        set => SetValue(PercentageValueProperty, value);
    }

    #endregion PecentageValue Property

    #region OuterCornerRadius Property    
    /// <summary>
    /// Binding property for <see cref="OuterCornerRadius"/>.
    /// </summary>
    public static readonly BindableProperty OuterCornerRadiusProperty = BindableProperty.Create(
        nameof(OuterCornerRadius),
        typeof(float),
        typeof(GradientProgressBar),
        5f,
        BindingMode.OneWay,
        (_, value) => value != null && (float)value >= 0,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets corner radius for the background bar.
    /// </summary>
    public float OuterCornerRadius
    {
        get => (float)GetValue(OuterCornerRadiusProperty);
        set => SetValue(OuterCornerRadiusProperty, value);
    }

    #endregion OuterCornerRadius Property

    #region InnerCornerRadiusProperty    
    /// <summary>
    /// Bindable property for <see cref="InnerCornerRadius"/>.
    /// </summary>
    public static readonly BindableProperty InnerCornerRadiusProperty = BindableProperty.Create(
        nameof(InnerCornerRadius),
        typeof(float),
        typeof(GradientProgressBar),
        5f,
        BindingMode.OneWay,
        (_, value) => value != null && (float)value >= 0,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets corner radius for progress bar fill.
    /// </summary>
    public float InnerCornerRadius
    {
        get => (float)GetValue(InnerCornerRadiusProperty);
        set => SetValue(InnerCornerRadiusProperty, value);
    }

    #endregion InnerCornerRadiusProperty

    #region StartProgressColor Property    
    /// <summary>
    /// Bindable property for <see cref="StartProgressColor"/>. 
    /// </summary>
    public static readonly BindableProperty StartProgressColorProperty = BindableProperty.Create(
        nameof(StartProgressColor),
        typeof(Color),
        typeof(GradientProgressBar),
        Colors.White,
        BindingMode.OneWay,
        (_, value) => value != null,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets the initial color of the progress bar's fill gradient.
    /// </summary>
    public Color StartProgressColor
    {
        get => (Color)GetValue(StartProgressColorProperty);
        set => SetValue(StartProgressColorProperty, value);
    }

    #endregion StartProgressColor Property

    #region EndProgressColor Property    
    /// <summary>
    /// Bindable property for <see cref="EndProgressColor"/>.
    /// </summary>
    public static readonly BindableProperty EndProgressColorProperty = BindableProperty.Create(
        nameof(EndProgressColor),
        typeof(Color),
        typeof(GradientProgressBar),
        Colors.White,
        BindingMode.OneWay,
        (_, value) => value != null,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets the final gradient color of the progress.
    /// </summary>
    public Color EndProgressColor
    {
        get => (Color)GetValue(EndProgressColorProperty);
        set => SetValue(EndProgressColorProperty, value);
    }

    #endregion EndProgressColor Property

    #region StartBackgroundColor Property    
    /// <summary>
    /// Bindable property for <see cref="StartBackgroundColor"/>.
    /// </summary>
    public static readonly BindableProperty StartBackgroundColorProperty = BindableProperty.Create(
        nameof(StartBackgroundColor),
        typeof(Color),
        typeof(GradientProgressBar),
        Colors.Blue,
        BindingMode.OneWay,
        (_, value) => value != null,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets the initial color of the progress bar's background.
    /// </summary>
    public Color StartBackgroundColor
    {
        get => (Color)GetValue(StartBackgroundColorProperty);
        set => SetValue(StartBackgroundColorProperty, value);
    }

    #endregion StartBackgroundColor Property

    #region EndBackgroundColor Property    
    /// <summary>
    /// Bindable property for <see cref="EndBackgroundColor"/>.
    /// </summary>
    public static readonly BindableProperty EndBackgroundColorProperty = BindableProperty.Create(
        nameof(EndBackgroundColor),
        typeof(Color),
        typeof(GradientProgressBar),
        Colors.Blue,
        BindingMode.OneWay,
        (_, value) => value != null,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets the final color of the progress bar's background.
    /// </summary>
    public Color EndBackgroundColor
    {
        get => (Color)GetValue(EndBackgroundColorProperty);
        set => SetValue(EndBackgroundColorProperty, value);
    }

    #endregion EndBackgroundColor Property

    #endregion Bindable Properties

    /// <summary>
    /// Called when [property changed invalidate].
    /// </summary>
    /// <param name="bindable">The bindable.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected static void OnPropertyChangedInvalidate(BindableObject bindable, object oldValue, object newValue)
    {
        if (oldValue != newValue)
        {
            ((SKCanvasView)bindable).InvalidateSurface();
        }
    }

    /// <summary>
    /// Raises the <see cref="E:PaintSurface" /> event.
    /// </summary>
    /// <param name="e">The <see cref="SKPaintSurfaceEventArgs"/> instance containing the event data.</param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        var info = e.Info;

        canvas.Clear();

        float scale;
        int percentageWidth;

        switch (Orientation)
        {
            case ProgressBarOrientation.Horizontal:
            {
                scale = CanvasSize.Width / (float)Width;
                percentageWidth = (int)Math.Floor(info.Width * PercentageValue);

                break;
            }
            case ProgressBarOrientation.Vertical:
            {
                scale = CanvasSize.Height / (float)Height;
                percentageWidth = (int)Math.Floor(info.Height * PercentageValue);

                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        var outerCornerRadius = OuterCornerRadius * scale;
        var innerCornerRadius = InnerCornerRadius * scale;

        ProgressBarHelper.SetClip(canvas, info, outerCornerRadius);

        ProgressBarHelper.DrawBackground(canvas, Orientation, e.Info, outerCornerRadius,
            StartBackgroundColor.ToSKColor(), EndBackgroundColor.ToSKColor());
        ProgressBarHelper.DrawProgress(canvas, Orientation, e.Info, percentageWidth,
            innerCornerRadius, StartProgressColor.ToSKColor(), EndProgressColor.ToSKColor());
    }
}