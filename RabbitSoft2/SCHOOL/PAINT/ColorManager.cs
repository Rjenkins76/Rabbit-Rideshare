using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSoft2
{
    internal class ColorManager : IColorManager
    {
        private Color currentColor = Color.Black;
        public Color CurrentColor => currentColor;
        public String Red => currentColor.R.ToString();
        public String Green => currentColor.G.ToString();
        public String Blue => currentColor.B.ToString();
        public String Alpha => currentColor.A.ToString();

        public int RedValue => currentColor.R;
        public int GreenValue => currentColor.G;
        public int BlueValue => currentColor.B;
        public int AlphaValue => currentColor.A;

        public void UpdateColor(int r, int g, int b, int a)
        {
            currentColor = Color.FromArgb(a, r, g, b);
        }

        public void UpdateColor(Color color)
        {
            currentColor = color;
        }

    }
}
