using SkiaSharp.Views.Maui;

namespace ProgressBarControl;

///<summary>
///Gradient progress bar with text shows the fill value as a percentage.
///</summary>
///<inheritdoc cref="GradientProgressBar"/>
public class DetailedGradientProgressBar : GradientProgressBar
{
    #region Bindable Properties

    #region FontSize Property    
    /// <summary>
    /// Bindable property for <see cref="FontSize"/> 
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
        nameof(FontSize),
        typeof(float),
        typeof(DetailedGradientProgressBar),
        12f,
        BindingMode.OneWay,
        (_, value) => value != null && (float)value >= 0,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets the size of the font.
    /// </summary>
    public float FontSize
    {
        get => (float)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    #endregion FontSize Property

    #region StringFormat Property    
    /// <summary>
    /// Bindable property for <see cref="StringFormat"/> 
    /// </summary>
    public static readonly BindableProperty StringFormatProperty = BindableProperty.Create(
        nameof(StringFormat),
        typeof(string),
        typeof(DetailedGradientProgressBar),
        "{0:0%}",
        BindingMode.OneWay,
        (_, value) => value != null,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets the string format.
    /// </summary>
    public string StringFormat
    {
        get => (string)GetValue(StringFormatProperty);
        set => SetValue(StringFormatProperty, value);
    }

    #endregion StringFormat Property

    #region PrimaryTextColor Property    
    /// <summary>
    /// Bindable property for <see cref="PrimaryTextColor"/> 
    /// </summary>
    public static readonly BindableProperty PrimaryTextColorProperty = BindableProperty.Create(
        nameof(PrimaryTextColor),
        typeof(Color),
        typeof(DetailedGradientProgressBar),
        Colors.White,
        BindingMode.OneWay,
        (_, value) => value != null,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets the color of the text in the progress bar fill.
    /// </summary>
    public Color PrimaryTextColor
    {
        get => (Color)GetValue(PrimaryTextColorProperty);
        set => SetValue(PrimaryTextColorProperty, value);
    }

    #endregion PrimaryTextColor Property

    #region SecondaryTextColor Property

    /// <summary>
    /// Bindable property for <see cref="SecondaryTextColor"/> 
    /// </summary>
    public static readonly BindableProperty SecondaryTextColorProperty = BindableProperty.Create(
        nameof(SecondaryTextColor),
        typeof(Color),
        typeof(DetailedGradientProgressBar),
        Colors.Blue,
        BindingMode.OneWay,
        (_, value) => value != null,
        OnPropertyChangedInvalidate);

    /// <summary>
    /// Gets or sets the color of the text in the progress bar background.
    /// </summary>
    public Color SecondaryTextColor
    {
        get => (Color)GetValue(SecondaryTextColorProperty);
        set => SetValue(SecondaryTextColorProperty, value);
    }

    #endregion SecondaryTextColor Property

    #endregion Bindable Properties

    /// <summary>
    /// Raises the <see cref="E:PaintSurface" /> event.
    /// </summary>
    /// <param name="e">The <see cref="SKPaintSurfaceEventArgs"/> instance containing the event data.</param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        base.OnPaintSurface(e);

        var canvas = e.Surface.Canvas;
        var info = e.Info;

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

        var textSize = FontSize * scale;

        ProgressBarHelper.DrawText(canvas, Orientation, e.Info, percentageWidth, textSize,
            PercentageValue, StringFormat, PrimaryTextColor.ToSKColor(), SecondaryTextColor.ToSKColor());
    }
}