using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitSoft2
{
    public partial class DoorDashCollectData : DevExpress.XtraEditors.XtraUserControl
    {
        Form1 home;
        public DoorDashCollectData(Form1 Home)
        {
            InitializeComponent();
            home = Home;
            
        }

        private void DoorDashCollectData_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = new ListViewItem(dateEdit1.DateOnly.ToString("yyyy-MM-dd"));
            lvi.SubItems.Add(textEdit1.Text);
            lvi.SubItems.Add(textEdit2.Text);
            lvi.SubItems.Add(textEdit3.Text);
            lvi.SubItems.Add(textEdit4.Text);

            listView1.Items.Add(lvi);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                foreach (ListViewItem item in listView1.Items)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Doordash_Trip_Activity (DATE, PICKUPLOCATION, BASEPAY, TIPPAY, TOTALPAY) VALUES (@Column1, @Column2, @Column3, @Column4, @Column5)", connection))
                    {
                        command.Parameters.AddWithValue("@Column1", item.SubItems[0].Text);
                        command.Parameters.AddWithValue("@Column2", item.SubItems[1].Text);
                        command.Parameters.AddWithValue("@Column3", Convert.ToDouble(item.SubItems[2].Text));
                        command.Parameters.AddWithValue("@Column4", Convert.ToDouble(item.SubItems[3].Text));
                        command.Parameters.AddWithValue("@Column5", Convert.ToDouble(item.SubItems[4].Text));

                        command.ExecuteNonQuery();
                    }
                }

                DialogResult result = XtraMessageBox.Show("DO YOU WANT TO UPDATE LEDGER?", "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    WorkShiftDataClass workShift = new WorkShiftDataClass();
                    workShift.DoorDashProcessData(dateEdit1.DateTime.Date);

                    Ledger data = new Ledger(dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
                    data.Dock = DockStyle.Fill;

                    home.panelControl1.Controls.Clear();
                    home.panelControl1.Controls.Add(data);

                }

                connection.Close();
            }
        }

        private void DoorDashCollectData_BackColorChanged(object sender, EventArgs e)
        {
            listView1.BackColor = this.BackColor;
            listView1.ForeColor = this.ForeColor;
        }
    }
}
