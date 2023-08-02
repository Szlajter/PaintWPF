using PaintWPF.interfaces;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace PaintWPF
{
    internal class PencilTool : IShape
    {       
        private Polyline polyline;

        public void Draw(Canvas paintSurface, Point startingPoint, Point currentPoint)
        {
            polyline.Points.Add(currentPoint);
        }

        public UIElement getShape(Color color, Color color2, int thickness, bool isFill = false)
        { 
            polyline = new Polyline
            {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = thickness,
                StrokeDashCap = PenLineCap.Round,
                StrokeLineJoin = PenLineJoin.Round,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round
            };

            return polyline;
        }
    }
}
