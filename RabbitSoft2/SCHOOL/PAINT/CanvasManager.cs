using RabbitSoft2.Tools;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace RabbitSoft2
{
    internal class CanvasManager : ICanvasManager
    {
        private Bitmap canvasBitmap;
        private Graphics canvasGraphics;
        private PictureBox canvasPictureBox;

        public CanvasManager(PictureBox pictureBox)
        {
            this.canvasPictureBox = pictureBox;
            InitializeCanvas();
        }

        private void InitializeCanvas()
        {
            canvasBitmap = new Bitmap(canvasPictureBox.Width, canvasPictureBox.Height);
            canvasGraphics = Graphics.FromImage(canvasBitmap);
            canvasPictureBox.Image = canvasBitmap;
            ClearCanvas();
        }

        public void ClearCanvas(Color clearColor = default(Color))
        {
            clearColor = clearColor == default(Color) ? Color.White : clearColor;

            canvasGraphics.Clear(clearColor);
            canvasPictureBox.Invalidate();
        }

        public void DrawOnCanvas(ITool tool, Color color, Point point, int size)
        {
            tool.UseTool(canvasGraphics, color, point, size);
            canvasPictureBox.Invalidate();
        }

        public Color GetColorFromPixel(int x, int y)
        {
            return canvasBitmap.GetPixel(x, y);
        }

        public void SaveCanvas(string filePath, ImageFormat format)
        {
            canvasBitmap.Save(filePath, format);
        }

        public void LoadCanvas(string filePath)
        {
            var image = Image.FromFile(filePath);
            canvasGraphics.DrawImage(image, 0, 0);
            canvasPictureBox.Invalidate();
        }
    }
}
