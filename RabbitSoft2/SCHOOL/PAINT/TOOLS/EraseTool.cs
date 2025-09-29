using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSoft2.Tools
{
    internal class EraseTool : ITool, IInteractiveTool
    {

        private SolidBrush brush;

        public EraseTool(int brushSize)
        {
            brush = new SolidBrush(Color.White);
            BrushSize = brushSize;
        }

        public bool ColorCanChange => false;

        public int BrushSize { get; set; }

        public void UseTool(Graphics canvas, Color color, Point position, int brushSize)
        {
            canvas.FillEllipse(brush, position.X - brushSize / 2, position.Y - brushSize / 2, brushSize, brushSize);
        }
    }
}
