using DevExpress.XtraEditors;
using RabbitSoft2.Tools;
using ScottPlot.Colormaps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RabbitSoft2
{
    public partial class School : DevExpress.XtraEditors.XtraUserControl
    {
        private IColorManager colorManager;
        private ICanvasManager canvasManager;
        private ICanvasManager canvasManager2;
        private ICanvasManager canvasManager3;
        private ToolState toolState;

        public School()
        {
            InitializeComponent();

            
        }

        private void School_Load(object sender, EventArgs e)
        {
            colorManager = new ColorManager();
            canvasManager = new CanvasManager(pictureBox1);
            canvasManager2 = new CanvasManager(pictureBox2);
            canvasManager3 = new CanvasManager(pictureBox4);
            toolState = new ToolState(15);
            toolState.CurrentTool = new PenTool(toolState.BrushSize);

            AlphaTB.Value = 255;

            UpdateColorDisplay(colorManager);

            canvasManager.LoadCanvas("C:\\Users\\jenki\\OneDrive\\Desktop\\tttt.png");
            canvasManager2.LoadCanvas("C:\\Users\\jenki\\OneDrive\\Desktop\\tttt2.png");
            canvasManager3.LoadCanvas("C:\\Users\\jenki\\OneDrive\\Desktop\\tttt3.png");

            Question1();
            Question3();
        }

        private void UpdateColorDisplay(IColorManager colorManager)
        {
            pictureBox3.BackColor = colorManager.CurrentColor;
            RedL.Text = "R: " + colorManager.Red;
            BlueL.Text = "B: " + colorManager.Blue;
            GreenL.Text = "G: " + colorManager.Green;
            AlphaL.Text = "A: " + colorManager.Alpha;
        }

        private void TB_Scroll(object sender, EventArgs e)
        {
            UpdateColorDisplay(colorManager);
        }

        private void RedTB_ValueChanged(object sender, EventArgs e)
        {
            colorManager.UpdateColor(RedTB.Value, GreenTB.Value, BlueTB.Value, AlphaTB.Value);

            UpdateColorDisplay(colorManager);
        }

        private void Penbt_Click(object sender, EventArgs e)
        {
            toolState.CurrentTool = new PenTool(toolState.BrushSize);
        }

        private void EraserTB_Click(object sender, EventArgs e)
        {
            toolState.CurrentTool = new EraseTool(toolState.BrushSize);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            toolState.BrushSize = (int)numericUpDown1.Value;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                canvasManager.SaveCanvas("C:\\Users\\jenki\\OneDrive\\Desktop\\tttt.png", ImageFormat.Bmp);

            }
            else 
            {
                toolState.IsActive = true;
            }
                
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            toolState.IsActive = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //colorManager.UpdateColor(255, 255, 255, 255);
            //toolStripTextBox1.Text = "X: " + e.X.ToString() + "   Y: " + e.Y.ToString();
            if (toolState.IsActive)
                canvasManager.DrawOnCanvas(toolState.CurrentTool, colorManager.CurrentColor, e.Location, 5);
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                canvasManager2.SaveCanvas("C:\\Users\\jenki\\OneDrive\\Desktop\\tttt2.png", ImageFormat.Bmp);
            }
            else
            {
                toolState.IsActive = true;
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            toolState.IsActive = false;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (toolState.IsActive)
                canvasManager2.DrawOnCanvas(toolState.CurrentTool, colorManager.CurrentColor, e.Location, 5);
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                canvasManager3.SaveCanvas("C:\\Users\\jenki\\OneDrive\\Desktop\\tttt3.png", ImageFormat.Bmp);
            }
            else
            {
                toolState.IsActive = true;
            }
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            toolState.IsActive = false;
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            if (toolState.IsActive)
                canvasManager3.DrawOnCanvas(toolState.CurrentTool, colorManager.CurrentColor, e.Location, 5);
        }

        private void Question1()
        {
            string output;
            double S = 2650; //Height of Plane
            double U = 0; //Start Velocity
            double A = 9.8; //Gravity 
            double T; //Time took to land 

            output = "S = 0t + (1/2) * 9.8 * t^2";
            double step1= (U) + ((0.5)*(A));
            output += "\nstep 1 - calc for time: 2650 = " + step1 + "t^2";
            output += "\nstep 2 - divide both sides by 4.9 = (2650 / 4.9) = (4.9t^2 / 4.9)";
            output += "\nstep 3 - solve ( 2650 / 4.9)  = " + (2650 / 4.9).ToString();
            output += "\nstep 4 - square time to get answer: " + Math.Round(Math.Sqrt(2650 / 4.9), 2);

            labelControl1.Text = output;
            labelControl3.Text += Math.Round(1609.34 / 3600, 3);
            double converts = Math.Round(250 * 1609.34 / 3600, 2);
            labelControl3.Text += "\n250 * 0.447 = " + converts.ToString();

            labelControl7.Text += Math.Round((Math.Sqrt(2650 / 4.9) * converts),2).ToString();

            labelControl4.Text += Math.Round(240*0.447,2);
            labelControl9.Text += Math.Round((240 * 0.447) * 23.26, 2);

        }

        private void Question3()
        {
            double v0 = 67; // init velocity
            double degrees = 50;
            double convert_degrees_to_radians = degrees * Math.PI / 180;

            double cos = Math.Cos(convert_degrees_to_radians);
            double sin = Math.Sin(convert_degrees_to_radians);

            double Vx = v0 * Math.Cos(convert_degrees_to_radians);
            double Vy = v0 * Math.Sin(convert_degrees_to_radians);

            labelControl5.Text += Math.Round(cos, 2) + " = " + Math.Round(Vx, 2);
            labelControl5.Text += "\nVy = V0 * sin(50 degrees = 67 * " + Math.Round(sin, 2) + " = " + Math.Round(Vy, 2);

            double time = Math.Round(400 / 43.07, 2);
            labelControl6.Text += time;
            double first = (51.32 * time);
            labelControl10.Text += Math.Round(first,2).ToString() + " - 0.5 * 9.8 * ";
            labelControl10.Text += Math.Round((time * time),2).ToString();
            double second = 0.5 * 9.8;
            double third = first - second * (time * time);

            labelControl10.Text += "\nY = " + Math.Round(first,2).ToString() + " - " + Math.Round(second) + " * " + Math.Round((time * time),2).ToString() + " = ";
            labelControl10.Text += "\nY = " + Math.Round(third,2).ToString();

            labelControl10.Text += "\n\nWe can now see that we have met the height requirement and the amunition will reach its target ";

        }

        private void labelControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // Right-click to copy
            {
                Clipboard.SetText(labelControl1.Text);
                MessageBox.Show("Text copied to clipboard!");
            }
        }
    }
}
