using PaintWPF.interfaces;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Windows.Shapes;
using System.Windows;

namespace PaintWPF
{
    internal class PaintLine: IShape
    {
        Line line;

        public void Draw(Canvas paintSurface, Point startingPoint, Point currentPoint)
        {
            line.X1 = startingPoint.X;
            line.Y1 = startingPoint.Y;
            line.X2 = currentPoint.X;
            line.Y2 = currentPoint.Y;

        }


        public UIElement getShape(Color color1, Color color2, int thickness, bool isFill = false) 
        {
             line = new Line()
             {
                 StrokeThickness = thickness,
                 Stroke = new SolidColorBrush(color1),
             };

            return line; 
        }

    }
}
