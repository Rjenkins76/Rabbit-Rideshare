using System.Drawing;

namespace RabbitSoft2
{
    internal interface IColorManager
    {
        string Alpha { get; }
        int AlphaValue { get; }
        string Blue { get; }
        int BlueValue { get; }
        Color CurrentColor { get; }
        string Green { get; }
        int GreenValue { get; }
        string Red { get; }
        int RedValue { get; }

        void UpdateColor(Color color);
        void UpdateColor(int r, int g, int b, int a);
    }
}