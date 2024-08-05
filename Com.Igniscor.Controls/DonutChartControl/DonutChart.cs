using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Helpers;

namespace DonutChartControl;

/// <summary>
/// Donut chart control.
/// </summary>
/// <remarks>
/// Register SkiaSharp in the application to use this control.
/// </remarks>
public class DonutChart : SKCanvasView
{
    #region Bindable Properties

    #region ItemsSource Property

    /// <summary>
    /// Bindable property for <see cref="ItemsSource"/>.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IEnumerable),
        typeof(DonutChart),
        new ObservableCollection<BaseDonutChartItem>(),
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets the items source for <see cref="DonutChart"/>.
    /// </summary>
    public ObservableCollection<BaseDonutChartItem> ItemsSource
    {
        get => (ObservableCollection<BaseDonutChartItem>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    #endregion ItemsSource Property

    #region SectorCommand Property

    /// <summary>
    /// Bindable property for <see cref="SectorCommand"/>.
    /// </summary>
    public static readonly BindableProperty SectorCommandProperty = BindableProperty.Create(
        nameof(SectorCommand),
        typeof(ICommand),
        typeof(DonutChart));

    /// <summary>
    /// <br>
    /// Gets or sets command that executed when tap on the description circle.
    /// </br>
    /// <br>
    /// May be null
    /// </br>
    /// </summary>
    public ICommand? SectorCommand
    {
        get => (ICommand?)GetValue(SectorCommandProperty);
        set => SetValue(SectorCommandProperty, value);
    }

    #endregion SectorCommand Property

    #region HoleCommand Property

    /// <summary>
    /// Bindable property for <see cref="HoleCommand"/>.
    /// </summary>
    public static readonly BindableProperty HoleCommandProperty = BindableProperty.Create(
        nameof(HoleCommand),
        typeof(ICommand),
        typeof(DonutChart));

    /// <summary>
    /// <br>
    /// Gets or sets command that executed when tap on the hole inside the circle.
    /// </br>
    /// <br>
    /// May bee null.
    /// </br>
    /// </summary>
    public ICommand? HoleCommand
    {
        get => (ICommand?)GetValue(HoleCommandProperty);
        set => SetValue(HoleCommandProperty, value);
    }

    #endregion HoleCommand Property

    #region EmptyStateColor Property

    /// <summary>
    /// Bindable property for <see cref="EmptyStateColor"/>.
    /// </summary>
    public static readonly BindableProperty EmptyStateColorProperty = BindableProperty.Create(
        nameof(EmptyStateColor),
        typeof(Color),
        typeof(DonutChart),
        Colors.Transparent,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets color for <see cref="DonutChart"/> which applies, when <see cref="ItemsSource"/> empty.
    /// </summary>
    public Color EmptyStateColor
    {
        get => (Color)GetValue(EmptyStateColorProperty);
        set => SetValue(EmptyStateColorProperty, value);
    }

    #endregion EmptyStateColor Property

    #region HoleRadius Property

    /// <summary>
    /// Bindable property for <see cref="HoleRadius"/>.
    /// </summary>
    public static readonly BindableProperty HoleRadiusProperty = BindableProperty.Create(
        nameof(HoleRadius),
        typeof(float),
        typeof(DonutChart),
        0.5f,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets radius value for the hole inside the circle.
    /// </summary>
    /// <returns>
    /// from 0 to 1
    /// </returns>
    public float HoleRadius
    {
        get
        {
            var holeRadius = (float)GetValue(HoleRadiusProperty);

            if (holeRadius < 0)
            {
                holeRadius = 0;
            }
            else if (holeRadius > 1)
            {
                holeRadius = 1;
            }

            return holeRadius;
        }
        set => SetValue(HoleRadiusProperty, value);
    }

    #endregion HoleRadius Property

    #region HolePrimaryText Property

    /// <summary>
    /// Bindable property for <see cref="HolePrimaryText"/>.
    /// </summary>
    public static readonly BindableProperty HolePrimaryTextProperty = BindableProperty.Create(
        nameof(HolePrimaryText),
        typeof(string),
        typeof(DonutChart),
        string.Empty,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets value for upper text in the hole.
    /// </summary>
    public string HolePrimaryText
    {
        get => (string)GetValue(HolePrimaryTextProperty);
        set => SetValue(HolePrimaryTextProperty, value);
    }

    #endregion HolePrimaryText Property

    #region HoleSecondaryText Property

    /// <summary>
    /// Bindable property for <see cref="HoleSecondaryText"/>.
    /// </summary>
    public static readonly BindableProperty HoleSecondaryTextProperty = BindableProperty.Create(
        nameof(HoleSecondaryText),
        typeof(string),
        typeof(DonutChart),
        string.Empty,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets value for lower text in the hole.
    /// </summary>
    public string HoleSecondaryText
    {
        get => (string)GetValue(HoleSecondaryTextProperty);
        set => SetValue(HoleSecondaryTextProperty, value);
    }

    #endregion HoleSecondaryText Property

    #region HolePrimaryTextScale Property

    /// <summary>
    /// Bindable property for <see cref="HolePrimaryTextScale"/>.
    /// </summary>
    public static readonly BindableProperty HolePrimaryTextScaleProperty = BindableProperty.Create(
        nameof(HolePrimaryTextScale),
        typeof(float),
        typeof(DonutChart),
        1f,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets scale for upper text in the hole.
    /// </summary>
    /// <returns>
    /// From 0 to 1
    /// </returns>
    public float HolePrimaryTextScale
    {
        get
        {
            var textScale = (float)GetValue(HolePrimaryTextScaleProperty);

            if (textScale < 0)
            {
                textScale = 0;
            }
            else if (textScale > 1)
            {
                textScale = 1;
            }

            return textScale;
        }
        set => SetValue(HolePrimaryTextScaleProperty, value);
    }

    #endregion HolePrimaryTextScale Property

    #region HoleSecondaryTextScale Property

    /// <summary>
    /// Bindable property for <see cref="HoleSecondaryTextScale"/>.
    /// </summary>
    public static readonly BindableProperty HoleSecondaryTextScaleProperty = BindableProperty.Create(
        nameof(HoleSecondaryTextScale),
        typeof(float),
        typeof(DonutChart),
        1f,
        propertyChanged: OnChartChanged);

    /// <summary>Gets or sets scale for lower text in the hole.</summary>
    /// <returns>From 0 to 1</returns>
    public float HoleSecondaryTextScale
    {
        get
        {
            var textScale = (float)GetValue(HoleSecondaryTextScaleProperty);

            if (textScale < 0)
            {
                textScale = 0;
            }
            else if (textScale > 1)
            {
                textScale = 1;
            }

            return textScale;
        }
        set => SetValue(HoleSecondaryTextScaleProperty, value);
    }

    #endregion HoleSecondaryTextScale Property

    #region HoleColor Property

    /// <summary>
    /// Bindable property for <see cref="HoleColor"/>.
    /// </summary>
    public static readonly BindableProperty HoleColorProperty = BindableProperty.Create(
        nameof(HoleColor),
        typeof(Color),
        typeof(DonutChart),
        Colors.Transparent,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets the color of the hole inside the circle.
    /// </summary>
    public Color HoleColor
    {
        get => (Color)GetValue(HoleColorProperty);
        set => SetValue(HoleColorProperty, value);
    }

    #endregion HoleColor Property

    #region HolePrimaryTextColor Property

    /// <summary>
    /// Bindable property for <see cref="HolePrimaryTextColor"/>.
    /// </summary>
    public static readonly BindableProperty HolePrimaryTextColorProperty = BindableProperty.Create(
        nameof(HolePrimaryTextColor),
        typeof(Color),
        typeof(DonutChart),
        Colors.Black,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets color for upper text in the hole.
    /// </summary>
    public Color HolePrimaryTextColor
    {
        get => (Color)GetValue(HolePrimaryTextColorProperty);
        set => SetValue(HolePrimaryTextColorProperty, value);
    }

    #endregion HolePrimaryTextColor Property

    #region HoleSecondaryTextColor Property

    /// <summary>
    /// Bindable property for <see cref="HoleSecondaryTextColor"/>.
    /// </summary>
    public static readonly BindableProperty HoleSecondaryTextColorProperty = BindableProperty.Create(
        nameof(HoleSecondaryTextColor),
        typeof(Color),
        typeof(DonutChart),
        Colors.Black,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets color upper text in the hole.
    /// </summary>
    public Color HoleSecondaryTextColor
    {
        get => (Color)GetValue(HoleSecondaryTextColorProperty);
        set => SetValue(HoleSecondaryTextColorProperty, value);
    }

    #endregion HoleSecondaryTextColor Property

    #region OuterRadius Property

    /// <summary>
    /// Bindable property for <see cref="OuterRadius"/>.
    /// </summary>
    public static readonly BindableProperty OuterRadiusProperty = BindableProperty.Create(
        nameof(OuterRadius),
        typeof(float),
        typeof(DonutChart),
        100f,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Sets the radius value of the circle in DUI.
    /// </summary>
    /// <returns>
    /// Get value in pixels
    /// </returns>
    public float OuterRadius
    {
        get => UnitsHelper.ConvertDIUToPixels((float)GetValue(OuterRadiusProperty));
        set => SetValue(OuterRadiusProperty, value);
    }

    #endregion OuterRadius Property

    #region LineToCircleLength Property

    /// <summary>
    /// Bindable property for <see cref="OuterRadius"/>.
    /// </summary>
    public static readonly BindableProperty LineToCircleLengthProperty = BindableProperty.Create(
        nameof(LineToCircleLength),
        typeof(float),
        typeof(DonutChart),
        10f,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Sets the length of the line from description circles to donut chart in DUI.
    /// </summary>
    /// <returns>
    /// Get value in pixels
    /// </returns>
    public float LineToCircleLength
    {
        get => UnitsHelper.ConvertDIUToPixels((float)GetValue(LineToCircleLengthProperty));

        set => SetValue(LineToCircleLengthProperty, value);
    }

    #endregion LineToCircleLength Property

    #region DescriptionCircleRadius Property

    /// <summary>
    /// Bindable property for <see cref="DescriptionCircleRadius"/>.
    /// </summary>
    public static readonly BindableProperty DescriptionCircleRadiusProperty = BindableProperty.Create(
        nameof(DescriptionCircleRadius),
        typeof(float),
        typeof(DonutChart),
        20f,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Sets the description circles radius in DUI.
    /// </summary>
    /// <returns>
    /// Get value from 0 to value in pixels
    /// </returns>
    public float DescriptionCircleRadius
    {
        get
        {
            var circleRadius = UnitsHelper.ConvertDIUToPixels((float)GetValue(DescriptionCircleRadiusProperty));

            if (circleRadius < 0)
            {
                circleRadius = 0;
            }

            return circleRadius;
        }
        set => SetValue(DescriptionCircleRadiusProperty, value);
    }

    #endregion DescriptionCircleRadius Property

    #region SeparatorsWidth Property

    /// <summary>
    /// Bindable property for <see cref="SeparatorsWidth"/>.
    /// </summary>
    public static readonly BindableProperty SeparatorsWidthProperty = BindableProperty.Create(
        nameof(SeparatorsWidth),
        typeof(float),
        typeof(DonutChart),
        2f,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets the width of the separators lines for the donut chart,
    /// description circles, segments, line from description circles to donut chart.
    /// </summary>
    /// <returns>
    /// From 0 to value in pixels
    /// </returns>
    public float SeparatorsWidth
    {
        get
        {
            var separatorsWidth = UnitsHelper.ConvertDIUToPixels((float)GetValue(SeparatorsWidthProperty));

            if (separatorsWidth < 0)
            {
                separatorsWidth = 0;
            }

            return separatorsWidth;
        }
        set => SetValue(SeparatorsWidthProperty, value);
    }

    #endregion SeparatorsWidth Property

    #region SeparatorsColor Property

    /// <summary>
    /// Bindable property for <see cref="SeparatorsColor"/>.
    /// </summary>
    public static readonly BindableProperty SeparatorsColorProperty = BindableProperty.Create(
        nameof(SeparatorsColor),
        typeof(Color),
        typeof(DonutChart),
        Colors.Black,
        propertyChanged: OnChartChanged);

    /// <summary>
    /// Gets or sets color of the separators lines for the donut chart,
    /// description circles, segments, line from description circles to donut chart.
    /// </summary>
    public Color SeparatorsColor
    {
        get => (Color)GetValue(SeparatorsColorProperty);
        set => SetValue(SeparatorsColorProperty, value);
    }

    #endregion SeparatorsColor Property

    #endregion Bindable Properties

    private readonly List<long> _touchIds = new();
    private IList<SKPath>? _sectorsPaths;
    private IList<SKPath?>? _descriptionsPaths;
    private SKPath? _holePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="DonutChart" /> class.
    /// </summary>
    public DonutChart()
    {
        PaintSurface += OnPaintCanvas;

        EnableTouchEvents = true;
        Touch += OnDonutChartViewTouch;
    }

    private static void OnChartChanged(BindableObject bindable, object oldValue, object newValue)
        => InvalidateSurface((SKCanvasView)bindable);

    private void OnPaintCanvas(object? sender, SKPaintSurfaceEventArgs e)
    {
        DrawContent(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        if (ItemsSource.Count != 0)
        {
            ItemsSource.CollectionChanged += (_, _) => InvalidateSurface(sender as SKCanvasView);
        }
    }

    private static void InvalidateSurface(SKCanvasView? skCanvasView)
    {
        if (skCanvasView == null) throw new ArgumentNullException(nameof(skCanvasView));
        skCanvasView.InvalidateSurface();
    }

    private void OnDonutChartViewTouch(object? sender, SKTouchEventArgs e)
    {
        if (sender == null)
        {
            return;
        }

        e.Handled = true;

        var touchLocation = e.Location;

        switch (e.ActionType)
        {
            case SKTouchAction.Pressed:
            {
                if (DonutChartHelper.HitTest(touchLocation, CanvasSize.Width, CanvasSize.Height))
                {
                    _touchIds.Add(e.Id);
                }

                break;
            }
            case SKTouchAction.Released:
            {
                if (_touchIds.Contains(e.Id))
                {
                    _touchIds.Remove(e.Id);
                }

                OnTouched(touchLocation);

                break;
            }
            case SKTouchAction.Cancelled:
            case SKTouchAction.Exited:
            {
                if (_touchIds.Contains(e.Id))
                {
                    _touchIds.Remove(e.Id);
                }

                break;
            }
        }
    }

    private void OnTouched(SKPoint touchLocation)
    {
        var translatedLocation = new SKPoint(touchLocation.X - CanvasSize.Width / 2,
            touchLocation.Y - CanvasSize.Height / 2);

        if (_holePath != null && _holePath.Contains(translatedLocation.X, translatedLocation.Y))
        {
            HoleCommand?.Execute(null);

            return;
        }

        for (var i = 0; i < _sectorsPaths?.Count; i++)
        {
            if (_descriptionsPaths?[i] == null ||
                (!_sectorsPaths[i].Contains(translatedLocation.X, translatedLocation.Y) &&
                 !_descriptionsPaths[i]!.Contains(translatedLocation.X, translatedLocation.Y))) continue;

            SectorCommand?.Execute(ItemsSource[i]);

            return;
        }
    }

    private void DrawContent(SKCanvas canvas, int width, int height)
    {
        _sectorsPaths = null;
        _descriptionsPaths = null;
        _holePath = null;

        canvas.Clear();

        using (new SKAutoCanvasRestore(canvas))
        {
            canvas.Translate(width / 2f, height / 2f);

            var outerRadius = OuterRadius / 2;
            var innerRadius = outerRadius * HoleRadius;

            if (ItemsSource.Count == 0)
            {
                DonutChartHelper.DrawEmptyState(canvas, outerRadius, innerRadius, EmptyStateColor.ToSKColor());
            }
            else
            {
                _sectorsPaths = DonutChartHelper.DrawSectors(canvas, outerRadius, innerRadius, ItemsSource);
            }

            _holePath = DonutChartHelper.DrawHole(canvas, innerRadius, HoleColor.ToSKColor());

            DonutChartHelper.DrawTextInHole(canvas, innerRadius, HolePrimaryTextScale, HoleSecondaryTextScale,
                HolePrimaryText, HoleSecondaryText, HolePrimaryTextColor.ToSKColor(),
                HoleSecondaryTextColor.ToSKColor());

            DonutChartHelper.DrawSeparators(canvas, outerRadius, innerRadius, SeparatorsColor.ToSKColor(),
                SeparatorsWidth, ItemsSource);

            if (DescriptionCircleRadius > 0)
            {
                _descriptionsPaths = DonutChartHelper.DrawDescriptions(canvas, outerRadius,
                    SeparatorsColor.ToSKColor(), SeparatorsWidth, ItemsSource, DescriptionCircleRadius,
                    LineToCircleLength);
            }
        }
    }
}