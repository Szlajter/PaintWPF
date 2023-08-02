using PaintWPF.interfaces;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Windows.Shapes;
using System.Windows;

namespace PaintWPF
{
    internal class EraserTool : IShape
    {
        private Polyline polyline;

        public void Draw(Canvas paintSurface, Point startingPoint, Point currentPoint)
        {
            polyline.Points.Add(currentPoint);
        }

        public UIElement getShape(Color color1, Color color2, int thickness, bool isFill = false)
        {
            polyline = new Polyline
            {
                Stroke = new SolidColorBrush(color2),
                StrokeThickness = thickness*4,
                StrokeDashCap = PenLineCap.Square,
                StrokeLineJoin = PenLineJoin.Round,
                StrokeStartLineCap = PenLineCap.Square,
                StrokeEndLineCap = PenLineCap.Square
            };

            return polyline;
        }
    }
}
