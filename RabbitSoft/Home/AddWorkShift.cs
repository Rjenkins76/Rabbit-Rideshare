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

namespace RabbitSoft
{
    public partial class AddWorkShift : DevExpress.XtraEditors.XtraUserControl
    {
        public AddWorkShift()
        {
            InitializeComponent();
        }

        private void AddWorkShift_Load(object sender, EventArgs e)
        {
            sqlDataSource1.FillAsync();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(de_ExpenseDate.EditValue != null)
            {
                double RunningTotal = 0;
                foreach (var row in sqlDataSource1.Result["LEDGER"])
                {
                    if (row[13] != null)
                    {
                        RunningTotal = Convert.ToDouble(row[13].ToString());
                    }
                }


                    //Insert Expense
                using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO ledger (DATE, ACTIVITY, TOTALPAY, RUNNINGTOTAL, NOTES) VALUES (@Column1, @Column8, @Column12,@Column13,@Column14)", connection))
                    {
                        command.Parameters.AddWithValue("@Column1", de_ExpenseDate.DateOnly.ToShortDateString());
                        command.Parameters.AddWithValue("@Column8", cb_ExpenseDescription.Text);
                        command.Parameters.AddWithValue("@Column12", 0 - Convert.ToDouble(te_ExpenseCost.EditValue));
                        RunningTotal += (0 - Convert.ToDouble(te_ExpenseCost.EditValue));
                        command.Parameters.AddWithValue("@Column13", RunningTotal);
                        command.Parameters.AddWithValue("@Column14", me_Notes.Text);

                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Added Expense to Ledger");
                    connection.Close();
                }
            }

            if(de_IncomeDate.EditValue != null)
            {
                DateTime EndTime = Convert.ToDateTime(de_EndTime.EditValue);
                DateTime StartTime = Convert.ToDateTime(de_StartTime.EditValue);

                TimeSpan timeSpan = EndTime - StartTime;

                //Insert Income
                using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO ledger (DATE, START_TIME, END_TIME, TOTALHOURS, TOTALHOURS_DEC, STARTMILEAGE, ENDMILEAGE, ACTIVITY ) VALUES (@Column1, @Column2, @Column3, @Column4, @Column5, @Column6, @Column7, @Column8)", connection))
                    {
                        command.Parameters.AddWithValue("@Column1", de_IncomeDate.DateOnly.ToShortDateString());
                        command.Parameters.AddWithValue("@Column2", de_StartTime.Text);
                        command.Parameters.AddWithValue("@Column3", de_EndTime.Text);
                        command.Parameters.AddWithValue("@Column4", timeSpan.Duration().ToString());
                        command.Parameters.AddWithValue("@Column5", (Math.Round((double)timeSpan.TotalHours, 2)));
                        command.Parameters.AddWithValue("@Column6", Convert.ToInt32(te_StartMileage.Text));
                        command.Parameters.AddWithValue("@Column7", Convert.ToInt32(te_EndMileage.Text));
                        command.Parameters.AddWithValue("@Column8", cb_Activity.Text);

                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Added Income to Ledger");
                    connection.Close();
                }
            }

            
        }
    }
}
