using RabbitSoft2.Tools;
using System.Drawing;
using System.Drawing.Imaging;

namespace RabbitSoft2
{
    internal interface ICanvasManager
    {
        void ClearCanvas(Color clearColor = default);
        void DrawOnCanvas(ITool tool, Color color, Point point, int size);
        Color GetColorFromPixel(int x, int y);
        void LoadCanvas(string filePath);
        void SaveCanvas(string filePath, ImageFormat format);
    }
}