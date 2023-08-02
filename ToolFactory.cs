using PaintWPF.interfaces;
using System.Collections.Generic;
using System.Security.Policy;

namespace PaintWPF
{
    internal class ToolFactory
    {
        private static ToolFactory _instance;
        private static readonly object _lock = new object();

        //double null check for thread safety
        public static ToolFactory GetInstance()
        {
            if (_instance == null)
            {
                lock(_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ToolFactory();
                    }
                }
            }
            return _instance;
        }
        public IShape createTool(MainWindow.Tools tool)
        {
            switch(tool)
            {
                case MainWindow.Tools.Ellipse:
                    return new PaintEllipse();
                case MainWindow.Tools.Rectangle:
                    return new PaintRectangle();
                case MainWindow.Tools.Line:
                    return new PaintLine();
                case MainWindow.Tools.Pencil:
                    return new PencilTool();
                case MainWindow.Tools.Eraser:
                    return new EraserTool();
            }
            //default for now 
            return new PaintRectangle();
        }

    }
}
