using System.Windows.Shapes;

namespace PaintWPF.interfaces
{
    public interface IToolFactory
    {
        public IShape createTool(MainWindow.Tools tool);
    }
}
