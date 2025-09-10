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
    public partial class TaxOrganizer : DevExpress.XtraEditors.XtraUserControl
    {
        public TaxOrganizer()
        {
            InitializeComponent();
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch1.IsOn) { textEdit5.Enabled = true; }
            else { textEdit5.Enabled = false; }
        }
    }
}
