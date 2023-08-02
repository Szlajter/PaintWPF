using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintWPF.interfaces
{
    public interface IShape
    {
        void Draw(Canvas paintSurface, Point startingPoint, Point currentPoint);

        UIElement getShape(Color color1, Color color2, int thickness, bool isFill = false);

    }
}
