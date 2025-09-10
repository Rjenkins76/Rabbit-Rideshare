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
    public partial class UpdateTripMileage : DevExpress.XtraEditors.XtraUserControl
    {
        public UpdateTripMileage()
        {
            InitializeComponent();
            

            lEDGERTableAdapter.Fill(rABBIT_RIDESHAREDataSet.LEDGER);
        }

        private void UpdateTripMileage_BackColorChanged(object sender, EventArgs e)
        {
            dataGridView1.BackgroundColor = this.BackColor;
            dataGridView1.DefaultCellStyle.ForeColor = this.ForeColor;
            dataGridView1.DefaultCellStyle.BackColor = this.BackColor;
        }
    }
}
