using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaApp.Controls;

public class CircularProgress : Control
{
    public static readonly StyledProperty<double> ValueProperty =
        AvaloniaProperty.Register<CircularProgress, double>(
            nameof(Value),
            0);

    public double Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    static CircularProgress()
    {
        AffectsRender<CircularProgress>(ValueProperty);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var width = Bounds.Width;
        var height = Bounds.Height;

        if (width <= 0 || height <= 0)
            return;

        var size = Math.Min(width, height);

        const double thickness = 14;

        var radius = (size - thickness) / 2.0;

        var center = new Point(
            width / 2.0,
            height / 2.0);

        var backgroundPen = new Pen(
            new SolidColorBrush(
                Color.Parse("#1C2431")),
            thickness);

        var progressPen = new Pen(
            new SolidColorBrush(
                Color.Parse("#7B6AF0")),
            thickness)
        {
            LineCap = PenLineCap.Round
        };

        // Background circle.
        context.DrawEllipse(
            null,
            backgroundPen,
            center,
            radius,
            radius);

        var percentage = Math.Clamp(Value, 0, 100);

        if (percentage <= 0)
            return;

        var sweepAngle =
            percentage / 100.0 * 360.0;

        // Avalonia uses the center point + radii
        // for elliptical arcs.
        var geometry = new StreamGeometry();

        using (var builder = geometry.Open())
        {
            var startAngle = -90.0;
            var endAngle = startAngle + sweepAngle;

            var start =
                PointOnCircle(
                    center,
                    radius,
                    startAngle);

            var end =
                PointOnCircle(
                    center,
                    radius,
                    endAngle);

            builder.BeginFigure(
                start,
                false);

            builder.ArcTo(
                end,
                new Size(radius, radius),
                0,
                sweepAngle > 180,
                SweepDirection.Clockwise);
        }

        context.DrawGeometry(
            null,
            progressPen,
            geometry);
    }

    private static Point PointOnCircle(
        Point center,
        double radius,
        double angle)
    {
        var radians =
            angle * Math.PI / 180.0;

        return new Point(
            center.X + radius * Math.Cos(radians),
            center.Y + radius * Math.Sin(radians));
    }
}
