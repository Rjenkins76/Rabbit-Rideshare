using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RabbitSoft2
{
    public partial class School : DevExpress.XtraEditors.XtraUserControl
    {
        public School()
        {
            InitializeComponent();

            double[] dataX = { 0,5,10,15,20,30,35,40,45,50 };
            double[] dataY = { 0,20,40,50,55,60,70,70,70,55 };

            formsPlot1.Plot.Add.Scatter(dataX, dataY);
            formsPlot1.Plot.Axes.Title.Label.Text = "Distance vs Time of a Moving Object";
            formsPlot1.Plot.XLabel("TIME");
            formsPlot1.Plot.YLabel("DISPLACEMENT");
            formsPlot1.Refresh();
        }
    }
}
