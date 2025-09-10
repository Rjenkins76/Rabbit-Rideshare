using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitSoft2
{
    public partial class MonthYearSelect : DevExpress.XtraEditors.XtraForm
    {
        public MonthYearSelect()
        {
            InitializeComponent();

            cb_MonthSelect.SelectedIndex = DateTime.Now.Month;
            cb_YearSelect.Text = DateTime.Now.Year.ToString();


        }

        private void MonthYearSelect_Load(object sender, EventArgs e)
        {

        }
    }
}