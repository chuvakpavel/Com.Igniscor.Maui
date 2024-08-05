using System.Collections.ObjectModel;
using SkiaSharp;

namespace DonutChartControl;

internal static partial class DonutChartHelper
{
    private const float UprightAngle = 1.57079637050629f;
    private const float TotalAngle = 6.28318548202515f;

    internal static SKPath DrawHole(SKCanvas canvas, float innerRadius, SKColor holeColor)
    {
        var holePath = CreateHolePath(innerRadius);

        using var paint = new SKPaint();
        paint.Style = SKPaintStyle.Fill;
        paint.Color = holeColor;
        paint.IsAntialias = true;

        canvas.DrawPath(holePath, paint);

        return holePath;
    }

    internal static void DrawEmptyState(SKCanvas canvas, float outerRadius, float innerRadius,
        SKColor emptyStateColor)
    {
        using var paint = new SKPaint();
        paint.Style = SKPaintStyle.Fill;
        paint.Color = emptyStateColor;
        paint.IsAntialias = true;

        var emptyStatePath = CreateEmptyStatePath(outerRadius, innerRadius);

        canvas.DrawPath(emptyStatePath, paint);
    }

    internal static void DrawSeparators(SKCanvas canvas, float outerRadius, float innerRadius,
        SKColor separatorsColor, float separatorsWidth, ObservableCollection<BaseDonutChartItem> itemsSource)
    {
        if (separatorsWidth <= 0 || separatorsColor == SKColors.Transparent)
        {
            return;
        }

        using var paint = new SKPaint();
        paint.Style = SKPaintStyle.Stroke;
        paint.StrokeWidth = separatorsWidth;
        paint.Color = separatorsColor;
        paint.IsAntialias = true;

        var radiusSeparatorsPath = CreateRadiusSeparatorsPath(outerRadius, innerRadius);

        canvas.DrawPath(radiusSeparatorsPath, paint);

        if (itemsSource.Count == 0)
        {
            return;
        }

        var sumValues = itemsSource.Sum(x => Math.Abs(x.Value));
        var start = 0f;

        for (var index = 0; index < itemsSource.Count; ++index)
        {
            var chartItem = itemsSource.ElementAt(index);
            var end = start + Math.Abs(chartItem.Value) / sumValues;

            var sectorSeparatorPath = CreateSectorSeparatorPath(start, end, outerRadius, innerRadius);

            canvas.DrawPath(sectorSeparatorPath, paint);

            start = end;
        }
    }

    internal static IList<SKPath> DrawSectors(SKCanvas canvas, float outerRadius, float innerRadius,
        ObservableCollection<BaseDonutChartItem> itemsSource)
    {
        if (itemsSource.Count == 0)
        {
            return new List<SKPath>();
        }

        var sectorsPaths = new List<SKPath>();

        var sumValues = itemsSource.Sum(x => Math.Abs(x.Value));
        var start = 0f;

        for (var index = 0; index < itemsSource.Count; ++index)
        {
            var chartItem = itemsSource.ElementAt(index);
            var end = start + Math.Abs(chartItem.Value) / sumValues;

            var sectorPath = CreateSectorPath(start, end, outerRadius, innerRadius);

            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Fill;
                paint.Color = SKColor.Parse(chartItem.SectionHexColorStart);
                paint.IsAntialias = true;
                var a = SKShader.CreateRadialGradient(new SKPoint(0, 0),
                    outerRadius,
                    new[]
                    {
                        SKColor.Parse(chartItem.SectionHexColorStart),
                        SKColor.Parse(chartItem.SectionHexColorEnd)
                    },
                    new[] { 0f, 1f },
                    SKShaderTileMode.Clamp);
                paint.Shader = a;
                canvas.DrawPath(sectorPath, paint);
            }

            start = end;

            sectorsPaths.Add(sectorPath);
        }

        return sectorsPaths;
    }

    internal static IList<SKPath?> DrawDescriptions(SKCanvas canvas, float outerRadius, SKColor separatorsColor,
        float separatorsWidth, ObservableCollection<BaseDonutChartItem> itemsSource, float circleRadius,
        float lineToCircleLength)
    {
        if (itemsSource.Count == 0 || circleRadius <= 0)
        {
            return new List<SKPath?>();
        }

        var descriptionsPaths = new List<SKPath?>();

        var sumValues = itemsSource.Sum(x => Math.Abs(x.Value));
        var resizedBitmapSide = (int)GetInnerRectSideOfCircle(circleRadius);
        var separatorPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = separatorsWidth,
            Color = separatorsColor,
            IsAntialias = true
        };
        var descPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            IsAntialias = true
        };

        var start = 0f;

        for (var index = 0; index < itemsSource.Count; ++index)
        {
            var chartItem = itemsSource.ElementAt(index);

            var end = start + Math.Abs(chartItem.Value) / sumValues;

            if (chartItem.IconData.Length != 0 && !end.Equals(start))
            {
                var angle1 = TotalAngle * start - UprightAngle;
                var angle2 = TotalAngle * end - UprightAngle;
                var angle = (angle1 + angle2) / 2;

                var circlePoint1 = GetCirclePoint(outerRadius, angle);
                var circlePoint2 = GetCirclePoint(outerRadius + lineToCircleLength, angle);
                var circlePoint3 = GetCirclePoint(outerRadius + lineToCircleLength + circleRadius, angle);

                var descriptionSeparatorPath =
                    CreateDescriptionSeparatorPath(circleRadius, circlePoint1, circlePoint2, circlePoint3);

                descPaint.Color = SKColor.Parse(chartItem.SectionHexColorStart);

                var skBitmap = GetSKBitmap(chartItem.IconData);

                var resizedBitmap = skBitmap.Resize(new SKImageInfo(resizedBitmapSide, resizedBitmapSide),
                    SKFilterQuality.Medium);

                if (resizedBitmap != null)
                {
                    if (separatorsWidth > 0)
                    {
                        canvas.DrawPath(descriptionSeparatorPath, separatorPaint);
                    }

                    canvas.DrawCircle(circlePoint3.X, circlePoint3.Y, circleRadius - separatorsWidth / 2,
                        descPaint);

                    canvas.DrawBitmap(resizedBitmap, circlePoint3.X - resizedBitmap.Width / 2f,
                        circlePoint3.Y - resizedBitmap.Height / 2f);
                }

                descriptionsPaths.Add(descriptionSeparatorPath);
            }
            else
            {
                descriptionsPaths.Add(null);
            }

            start = end;
        }

        descPaint.Dispose();
        separatorPaint.Dispose();

        return descriptionsPaths;
    }

    internal static void DrawTextInHole(SKCanvas canvas, float innerRadius, float holePrimaryTextScale,
        float holeSecondaryTextScale, string holePrimaryText, string holeSecondaryText,
        SKColor holePrimaryTextColor, SKColor holeSecondaryTextColor)
    {
        if (innerRadius <= 0 || (string.IsNullOrEmpty(holePrimaryText) && string.IsNullOrEmpty(holeSecondaryText)))
        {
            return;
        }

        var squareSide = (float)GetInnerRectSideOfCircle(innerRadius);

        if (string.IsNullOrEmpty(holePrimaryText))
        {
            holePrimaryTextScale = 0;
        }

        if (string.IsNullOrEmpty(holeSecondaryText))
        {
            holeSecondaryTextScale = 0;
        }

        var prTextPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = holePrimaryTextColor
        };

        var startPrTextWidth = prTextPaint.MeasureText(holePrimaryText);
        prTextPaint.TextSize = GetTextSize(holePrimaryTextScale, squareSide, startPrTextWidth);

        var prTextHeight = prTextPaint.FontMetrics.CapHeight;
        var prTextWidth = prTextPaint.MeasureText(holePrimaryText);

        var secTextPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = holeSecondaryTextColor
        };

        var startSecTextWidth = secTextPaint.MeasureText(holeSecondaryText);
        secTextPaint.TextSize = GetTextSize(holeSecondaryTextScale, squareSide, startSecTextWidth);

        var secTextHeight = secTextPaint.FontMetrics.CapHeight;
        var secTextWidth = secTextPaint.MeasureText(holeSecondaryText);


        while (prTextHeight > squareSide || secTextHeight > squareSide ||
               prTextHeight + secTextHeight > squareSide)
        {
            if (prTextHeight > secTextHeight)
            {
                ReduceTextSize(ref holePrimaryTextScale, ref prTextPaint, squareSide, startPrTextWidth,
                    holePrimaryText, out prTextHeight, out prTextWidth);
            }
            else if (prTextHeight < secTextHeight)
            {
                ReduceTextSize(ref holeSecondaryTextScale, ref secTextPaint, squareSide, startSecTextWidth,
                    holeSecondaryText, out secTextHeight, out secTextWidth);
            }
            else
            {
                ReduceTextSize(ref holePrimaryTextScale, ref prTextPaint, squareSide, startPrTextWidth,
                    holePrimaryText, out prTextHeight, out prTextWidth);
                ReduceTextSize(ref holeSecondaryTextScale, ref secTextPaint, squareSide, startSecTextWidth,
                    holeSecondaryText, out secTextHeight, out secTextWidth);
            }
        }

        var emptySectorHeight = holePrimaryTextScale <= 0 || holeSecondaryTextScale <= 0
            ? holePrimaryTextScale <= 0
                ? GetEmptySectorHeight(squareSide, secTextHeight)
                : GetEmptySectorHeight(squareSide, prTextHeight)
            : GetEmptySectorHeight(squareSide, prTextHeight, secTextHeight);

        var prTemp = -(squareSide / 2 - emptySectorHeight - prTextHeight);
        var secTemp = (squareSide / 2 - emptySectorHeight);

        if (holePrimaryTextScale > 0)
            canvas.DrawText(holePrimaryText, -prTextWidth / 2, prTemp, prTextPaint);

        if (holeSecondaryTextScale > 0)
            canvas.DrawText(holeSecondaryText, -secTextWidth / 2, secTemp, secTextPaint);

        prTextPaint.Dispose();
        secTextPaint.Dispose();
    }

    internal static bool HitTest(SKPoint touchLocation, float canvasWidth, float canvasHeight) =>
        new SKRect(0, 0, canvasWidth, canvasHeight).Contains(touchLocation);
}