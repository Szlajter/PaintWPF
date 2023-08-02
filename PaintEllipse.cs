using PaintWPF.interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintWPF
{
    internal class PaintEllipse: IShape
    {
        Ellipse ellipse;

        public void Draw(Canvas paintSurface, Point startingPoint, Point currentPoint)
        {
            double left = Math.Min(startingPoint.X, currentPoint.X);
            double top = Math.Min(startingPoint.Y, currentPoint.Y);
            double width = Math.Abs(currentPoint.X - startingPoint.X);
            double height = Math.Abs(currentPoint.Y - startingPoint.Y);

            Canvas.SetLeft(ellipse, left);
            Canvas.SetTop(ellipse, top);

            ellipse.Width = width;
            ellipse.Height = height;
        }

        
        public UIElement getShape(Color color1, Color color2, int thickness, bool isFill = false)
        {
            ellipse = new Ellipse()
            {
                StrokeThickness = thickness,
                Stroke = new SolidColorBrush(color1)
            };
            if (isFill) ellipse.Fill = new SolidColorBrush(color2);

            return ellipse;
        }

    }
}

