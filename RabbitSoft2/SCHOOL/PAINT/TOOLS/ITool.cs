using System.Drawing;


namespace RabbitSoft2.Tools
{
    internal interface ITool
    {
        void UseTool(Graphics canvas, Color color, Point position, int size);
    }
}
