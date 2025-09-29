using System.Drawing;


namespace RabbitSoft2.Tools
{
    internal class PenTool : ITool, IInteractiveTool
    {
        Brush brush;

        public PenTool(int size)
        {
            brush = new SolidBrush(Color.White);
            BrushSize = size;
        }
        
        public bool ColorCanChange => true;

        public int BrushSize { get; set; }

        public void UseTool(Graphics canvas, Color color, Point position, int brushSize)
        {
            brush = new SolidBrush(color);
            canvas.FillEllipse(brush, position.X - brushSize / 2, position.Y - brushSize / 2, brushSize, brushSize);
        }
    }
}
