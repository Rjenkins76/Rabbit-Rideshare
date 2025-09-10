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

namespace RabbitSoft2
{
    public partial class IncomeExpenseReport : DevExpress.XtraEditors.XtraUserControl
    {
        IncomeExpenseClass incomeExpenseClass = new IncomeExpenseClass();
        public IncomeExpenseReport()
        {
            InitializeComponent();
            sqlDataSource1.Fill();
        }

        private void IncomeExpenseReport_Load(object sender, EventArgs e)
        {
            incomeExpenseClass.Uber_Rides_GetWeeksIncomeData(sqlDataSource1, this);
            incomeExpenseClass.Uber_Eats_GetWeeksIncomeData(sqlDataSource1,this);
            incomeExpenseClass.Door_Dash_GetWeeksIncomeData(sqlDataSource1);
        }
    }
}
