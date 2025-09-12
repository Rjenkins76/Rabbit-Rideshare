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
    public partial class TaxForm1040 : DevExpress.XtraEditors.XtraUserControl
    {
        public TaxForm1040()
        {
            InitializeComponent();
        }

        private void te_1a_EditValueChanged(object sender, EventArgs e)
        {
            double te_1a_amount = te_1a.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_1a.EditValue);
            double te_1c_amount = te_1c.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_1c.EditValue);
            double te_1g_amount = te_1g.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_1g.EditValue);
            double te_1h_amount = te_1h.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_1h.EditValue);

            double amount = te_1a_amount + te_1c_amount + te_1g_amount + te_1h_amount;

            te_1z.Text = amount.ToString();
        }
    }
}
