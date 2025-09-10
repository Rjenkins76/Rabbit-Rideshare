using DevExpress.DataAccess.Sql;
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

namespace RabbitSoft
{
    public partial class HomeScreen : DevExpress.XtraEditors.XtraUserControl
    {
        Income_Expenses_Report_Class processor; // = new Income_Expenses_Report_Class();
        Form1 Home;
        public HomeScreen(Form1 home)
        {
            InitializeComponent();
            Home = home;
            sqlDataSource1.FillAsync();
            processor = new Income_Expenses_Report_Class(this, sqlDataSource1);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            processor.Uber_Eats_GetWeeksIncomeData();
            processor.Door_Dash_GetWeeksIncomeData();
            processor.calcTotals();
            processor.CalcFinalTotals();

            simpleButton3.Visible = false;
        }

        private void HomeScreen_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AddWorkShift workShift = new AddWorkShift();

            // Create a DevExpress XtraForm to host the UserControl
            XtraForm customDialog = new XtraForm();
            customDialog.Text = "ADD WORK SHIFT";
            customDialog.Controls.Add(workShift);
            customDialog.Width = workShift.Width;
            customDialog.Height = workShift.Height + 50;
            customDialog.StartPosition = FormStartPosition.CenterScreen;

            // Show the dialog modally
            if (customDialog.ShowDialog() == DialogResult.OK)
            {
                DialogResult result = MessageBox.Show("Do you want to Collect Activity Data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Home.barButtonItem5.PerformClick();
                }
                else { simpleButton3.PerformClick(); }
                    
            }
        }
    }
}
