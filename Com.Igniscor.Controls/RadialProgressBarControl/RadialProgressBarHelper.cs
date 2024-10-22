﻿using SkiaSharp;

namespace RadialProgressBarControl;

internal static class RadialProgressBarHelper
{
    internal const float MinPercentageValue = 0;
    internal const float MaxPercentageValue = 1;
    internal const float MinAngle = 0;
    internal const float MaxAngle = 360;

    private const float StartLeft = 0;
    private const float StartTop = 0;
    private const float Multiplier = 0.5f;

    internal static void DrawCircle(SKCanvas canvas, SKImageInfo info, float barWidth, float startAngle,
        float sweepAngle, SKColor startColor, SKColor endColor)
    {
        var center = new SKPoint(info.Width * Multiplier, info.Height * Multiplier);

        using var path = CreateCirclePath(info.Width, info.Height, barWidth);
        using var paint = CreatePaint(center, startAngle, sweepAngle, startColor, endColor);

        canvas.DrawPath(path, paint);
    }

    internal static void DrawArc(SKCanvas canvas, SKImageInfo info, float barWidth, float startAngle,
        float sweepAngle, SKColor startColor, SKColor endColor)
    {
        var center = new SKPoint(info.Width * Multiplier, info.Height * Multiplier);

        using var path = CreateArcPath(info.Width, info.Height, barWidth, startAngle, sweepAngle, center);
        using var paint = CreatePaint(center, startAngle, sweepAngle, startColor, endColor);

        canvas.DrawPath(path, paint);
    }

    private static SKPath CreateCirclePath(int width, int height, float barWidth)
    {
        var path = new SKPath { FillType = SKPathFillType.EvenOdd };
        {
            var center = new SKPoint(width * Multiplier, height * Multiplier);

            var outerRect = new SKRect(StartLeft, StartTop, width, height);
            var innerRect = new SKRect(barWidth, barWidth, width - barWidth, height - barWidth);

            path.MoveTo(center);
            path.AddOval(outerRect);
            path.AddOval(innerRect);

            path.Close();

            return path;
        }
    }

    private static SKPath CreateArcPath(int width, int height, float barWidth, float startAngle, float sweepAngle,
        SKPoint center)
    {
        var path = new SKPath { FillType = SKPathFillType.EvenOdd };
        
        var startX1 = center.X + (float)(center.X * Math.Cos(DegreesToRadians(startAngle)));
        var startY1 = center.Y + (float)(center.Y * Math.Sin(DegreesToRadians(startAngle)));

        var startX2 = center.X + (float)((center.X - barWidth) * Math.Cos(DegreesToRadians(startAngle)));
        var startY2 = center.Y + (float)((center.Y - barWidth) * Math.Sin(DegreesToRadians(startAngle)));

        var endX1 = center.X + (float)(center.X * Math.Cos(DegreesToRadians(sweepAngle + startAngle)));
        var endY1 = center.Y + (float)(center.Y * Math.Sin(DegreesToRadians(sweepAngle + startAngle)));

        var start1 = new SKPoint(startX1, startY1);
        var end1 = new SKPoint(endX1, endY1);
        var start2 = new SKPoint(startX2, startY2);

        var outerRect = new SKRect(StartLeft, StartTop, width, height);
        var innerRect = new SKRect(barWidth, barWidth, width - barWidth, height - barWidth);

        path.MoveTo(start1);
        path.LineTo(start2);
        path.ArcTo(innerRect, startAngle, sweepAngle, false);
        path.LineTo(end1);
        path.ArcTo(outerRect, startAngle, sweepAngle, false);

        path.Close();

        return path;
    }

    private static SKPaint CreatePaint(SKPoint center, float startAngle, float sweepAngle, SKColor startColor,
        SKColor endColor)
    {
        var matrix = SKMatrix.CreateRotationDegrees(startAngle, center.X, center.Y);

        var shader = SKShader.CreateSweepGradient(new SKPoint(center.X, center.Y), new[] { startColor, endColor },
            null, SKShaderTileMode.Clamp, MinAngle, sweepAngle, matrix);

        return new SKPaint
        {
            IsAntialias = true,
            Shader = shader
        };
    }

    private static float DegreesToRadians(float angleInDegrees) =>
        (float)(angleInDegrees * Math.PI / 180);
}