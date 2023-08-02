using PaintWPF.interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintWPF
{
    internal class PaintRectangle: IShape
    {
        private Rectangle rect;

        public void Draw(Canvas paintSurface, Point startingPoint, Point currentPoint)
        {
            double left = Math.Min(startingPoint.X, currentPoint.X);
            double top = Math.Min(startingPoint.Y, currentPoint.Y);
            double width = Math.Abs(currentPoint.X - startingPoint.X);
            double height = Math.Abs(currentPoint.Y - startingPoint.Y);

            Canvas.SetLeft(rect, left);
            Canvas.SetTop(rect, top);
            
            rect.Width = width;
            rect.Height = height;
        }

        public UIElement getShape(Color color1, Color color2, int thickness, bool isFill = false) 
        {

            rect = new Rectangle()
            {
                StrokeThickness = thickness,
                Stroke = new SolidColorBrush(color1)
            };
            if (isFill) rect.Fill = new SolidColorBrush(color2);

            return rect; 
        }
    }
}
