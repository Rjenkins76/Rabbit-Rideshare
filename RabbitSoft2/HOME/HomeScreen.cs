using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
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
    public partial class HomeScreen : DevExpress.XtraEditors.XtraUserControl
    {
        Form1 MainScreen;
        public HomeScreen(Form1 main)
        {
            InitializeComponent();

            // TODO: This line of code loads data into the 'rABBIT_RIDESHAREDataSet.Resources' table. You can move, or remove it, as needed.
            this.resourcesTableAdapter.Fill(this.rABBIT_RIDESHAREDataSet.Resources);
            // TODO: This line of code loads data into the 'rABBIT_RIDESHAREDataSet.Appointments' table. You can move, or remove it, as needed.
            this.appointmentsTableAdapter.Fill(this.rABBIT_RIDESHAREDataSet.Appointments);

            MainScreen = main;
            sqlDataSource1.Fill();

        }

        private void HomeScreen_Load(object sender, EventArgs e)
        {

            //SchedulerControl schedulerControl = new SchedulerControl();

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddDays(14); // Next 7 days

            AppointmentBaseCollection appointments = schedulerDataStorage1.GetAppointments(new TimeInterval(start, end));
            string Appointments = "";
            foreach (Appointment appointment in appointments)
            {
                Appointments += appointment.Start.ToShortDateString() + " - " + appointment.Subject + " - " + appointment.Description + "\n";
                //XtraMessageBox.Show($"Subject: {appointment.Subject}, Date: {appointment.Start.ToShortDateString()}");
            }

            labelControl15.Text = Appointments;

            double balance = 0;
            foreach (var row in sqlDataSource1.Result["LEDGER"])
            {
                if(row[11] != null)
                {
                    balance = Convert.ToDouble(row[11].ToString());
                }
                    
            }

            lblBalance.Text = balance.ToString("c");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (de_ExpenseDate.EditValue != null)
            {
                double RunningTotal = 0;
                foreach (var row in sqlDataSource1.Result["LEDGER"])
                {
                    if (row[11] != null)
                    {
                        RunningTotal = Convert.ToDouble(row[11].ToString());
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
                        lblBalance.Text = RunningTotal.ToString("c");
                        command.ExecuteNonQuery();
                    }
                    XtraMessageBox.Show("Added Expense to Ledger", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();
                }
            }

            if (de_IncomeDate.EditValue != null)
            {
                string GetEndTime = $"{de_IncomeDate.DateOnly} {de_EndTime.Text}";
                DateTime EndTime = DateTime.Parse(GetEndTime);

                string GetStartTime = $"{de_IncomeDate.DateOnly} {de_StartTime.Text}";
                DateTime StartTime = DateTime.Parse(GetStartTime);

                if (EndTime.TimeOfDay < StartTime.TimeOfDay) 
                {
                    //MessageBox.Show("I am LESS THAN");
                    EndTime = EndTime.AddDays(1); 
                }

                TimeSpan timeSpan = EndTime - StartTime;

                //Insert Income
                using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO ledger (DATE, START_TIME, END_TIME, TOTALHOURS, TOTALHOURS_DEC, ACTIVITY ) VALUES (@Column1, @Column2, @Column3, @Column4, @Column5, @Column8)", connection))
                    {
                        command.Parameters.AddWithValue("@Column1", de_IncomeDate.DateOnly.ToShortDateString());
                        command.Parameters.AddWithValue("@Column2", de_StartTime.Text);
                        command.Parameters.AddWithValue("@Column3", de_EndTime.Text);
                        command.Parameters.AddWithValue("@Column4", timeSpan.Duration().ToString());
                        command.Parameters.AddWithValue("@Column5", (Math.Round((double)timeSpan.TotalHours, 2)));
                        //command.Parameters.AddWithValue("@Column6", Convert.ToInt32(te_StartMileage.Text) );
                        //command.Parameters.AddWithValue("@Column7", Convert.ToInt32(te_EndMileage.Text));
                        command.Parameters.AddWithValue("@Column8", cb_Activity.Text);

                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand("INSERT INTO MILEAGELOG (DATE, STARTTIME, ENDTIME, ACTIVITY ) VALUES (@Column1, @Column2, @Column3, @Column8)", connection))
                    {
                        command.Parameters.AddWithValue("@Column1", de_IncomeDate.DateOnly.ToShortDateString());
                        command.Parameters.AddWithValue("@Column2", de_StartTime.Text);
                        command.Parameters.AddWithValue("@Column3", de_EndTime.Text);
                        command.Parameters.AddWithValue("@Column8", cb_Activity.Text);

                        command.ExecuteNonQuery();


                        XtraMessageBox.Show("Added Income to Ledger", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    connection.Close();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                string query = "SELECT * FROM LEDGER";
                DataTable dataTable = new DataTable();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "MILEAGELOG";

                    bulkCopy.ColumnMappings.Add("DATE", "DATE");
                    bulkCopy.ColumnMappings.Add("ACTIVITY", "ACTIVITY");
                    bulkCopy.ColumnMappings.Add("START_TIME", "STARTTIME");
                    bulkCopy.ColumnMappings.Add("END_TIME", "ENDTIME");                    
                    bulkCopy.ColumnMappings.Add("TOTALTRIPS", "NUMBERTRIPS");


                    bulkCopy.WriteToServer(dataTable);
                }


                connection.Close();
            }
        }
    }
}
