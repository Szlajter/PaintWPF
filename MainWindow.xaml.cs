using Microsoft.Win32;
using PaintWPF.interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media; 
using System.Windows.Media.Imaging;


namespace PaintWPF
{
    public partial class MainWindow : Fluent.RibbonWindow
    {
        public enum Tools
        {
            Pencil,
            Eraser,
            Rectangle,
            Line,
            Ellipse
        }

        private Tools currentTool = Tools.Pencil;
        private IShape shapePreview = null;
        private Point startingPoint = new Point();
        private Color color1 = Colors.Black;
        private Color color2 = Colors.White;
        private bool colorOne = true;
        private bool isFill = false;
        private int strokeThickness = 3;

        private Cursor eraserCursor = new Cursor(Application.GetResourceStream(new Uri("Cursors/eraser.cur", UriKind.Relative)).Stream);

        private ToolFactory factory = ToolFactory.GetInstance();

        public MainWindow()
        {
            InitializeComponent();
            paintSurface.Cursor = Cursors.Pen;
        }

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startingPoint = e.GetPosition(paintSurface);

            shapePreview = factory.createTool(currentTool);

            UIElement element = shapePreview.getShape(color1, color2, strokeThickness, isFill);
            paintSurface.Children.Add(element);
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                shapePreview.Draw(paintSurface, startingPoint, e.GetPosition(paintSurface));
            }
        }
        
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            paintSurface.ReleaseMouseCapture();
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = Tools.Rectangle;
            paintSurface.Cursor = Cursors.Cross;
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = Tools.Line;
            paintSurface.Cursor = Cursors.Cross;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = Tools.Ellipse;
            paintSurface.Cursor = Cursors.Cross;
        }

        private void PencilButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = Tools.Pencil;
            paintSurface.Cursor = Cursors.Pen;
        }

        private void EraserButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = Tools.Eraser;
            paintSurface.Cursor = eraserCursor;
        }

        private void SetFill_Click(object sender, RoutedEventArgs e)
        {
            isFill = !isFill;
            if(isFill)
            {
                FillButton.Header = "Fill";
            }
            else
            {
                FillButton.Header = "No Fill";
            }
        }
        private void ChangeColorMode_Click(object sender, RoutedEventArgs e)
        {
            this.colorOne = !this.colorOne;
            if(this.colorOne)
            {
                ColorMode.Header = "Color 1";
                ColorRect.Fill = new SolidColorBrush(color1);
            }
            else
            {
                ColorMode.Header = "Color 2";
                ColorRect.Fill = new SolidColorBrush(color2);
            }
        }

        private void SetColor_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            if(colorOne == true)
            {
                color1 = (Color)ColorConverter.ConvertFromString(tag.ToString());
                ColorRect.Fill = new SolidColorBrush(color1);
            }
            else
            {
                color2 = (Color)ColorConverter.ConvertFromString(tag.ToString());
                ColorRect.Fill = new SolidColorBrush(color2);
            }
        }

        private void SetSize_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            int value = int.Parse(radioButton.Tag.ToString());

            strokeThickness = value;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await SaveCanvasAsync();
        }

        private async Task SaveCanvasAsync()
        {
            //make bitmap
            Rect bounds = VisualTreeHelper.GetDescendantBounds(paintSurface);
            double dpi = 96d;

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, PixelFormats.Default);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(paintSurface);
                dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }

            rtb.Render(dv);

            //convert into png and save
            BitmapEncoder encoder;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG (*.png) | *.png|JPG (*.jpg, *jpeg) | *.jpg;*.jpeg|GIF (*.gif) | *.gif";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filename = saveFileDialog.FileName;
                    string fileType = System.IO.Path.GetExtension(filename);
                 
                    switch(fileType)
                    {
                        case ".png":
                            encoder = new PngBitmapEncoder();
                            break;
                        case ".jpg":
                            encoder = new JpegBitmapEncoder();
                            break;
                        case ".gif":
                            encoder = new GifBitmapEncoder();
                            break;
                        default:
                            encoder = new PngBitmapEncoder();
                            break;
                    }

                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    MemoryStream ms = new MemoryStream();
                    encoder.Save(ms);
                    ms.Close();
                    await File.WriteAllBytesAsync(filename, ms.ToArray());
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void LoadCanvas(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG (*.png) | *.png|JPG (*.jpg, *jpeg) | *.jpg;*.jpeg|GIF (*.gif) | *.gif";


            if (openFileDialog.ShowDialog() == true)
            {
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Relative));
                paintSurface.Background = brush;
            }
        }

        public void NewCanvas(object sender, RoutedEventArgs e)
        {
            paintSurface.Children.Clear();
        }
    }
}
