using DevExpress.DataAccess.Sql;
using DevExpress.Utils.StructuredStorage.Internal;
using DevExpress.Xpo.DB.Helpers;
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
using static DevExpress.Xpo.Helpers.CommandChannelHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace RabbitSoft2
{
    public partial class TaxForm1040 : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable UberRidesdataTable = new DataTable();
        double UberRides_lineTotals = 0;

        DataTable UberDeliveriesdataTable = new DataTable();
        double UberDeliveries_lineTotals = 0;

        DataTable DoordashdataTable = new DataTable();
        double Doordash_lineTotals = 0;

        public TaxForm1040()
        {
            InitializeComponent();
            sqlDataSource1.FillAsync();

            //DataTable UberRidesdataTable = sqlDataSource1.Result[0] as DataTable;
            //DataTable UberDeliveriesdataTable = sqlDataSource1.Result[1] as DataTable;
            //DataTable DoordashdataTable = sqlDataSource1.Result[2] as DataTable;

            getTotals();
        }

        private void TaxForm1040_Load(object sender, EventArgs e)
        {
            te_UberRides_amount.EditValue = UberRides_lineTotals;
            te_UberEats_amount.EditValue= UberDeliveries_lineTotals;
            te_Doordash_amount.EditValue = Doordash_lineTotals;
        }

        private void getTotals()
        {

            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM Trip_Activity WHERE PROVIDER = 'UBER RIDES'";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                {
                    adapter.Fill(UberRidesdataTable);
                }

                sqlQuery = "SELECT * FROM Trip_Activity WHERE PROVIDER = 'UBER DELIVERIES'";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                {
                    adapter.Fill(UberDeliveriesdataTable);
                }

                sqlQuery = "SELECT * FROM Doordash_Trip_Activity";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                {
                    adapter.Fill(DoordashdataTable);
                }

                connection.Close();
            }


            foreach (DataRow row in UberRidesdataTable.Rows)
            {
                UberRides_lineTotals += row[6].ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(row[6].ToString());
            }

            foreach (DataRow row in UberDeliveriesdataTable.Rows)
            {
                UberDeliveries_lineTotals += row[6].ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(row[6].ToString());
            }

            foreach (DataRow row in DoordashdataTable.Rows)
            {
                Doordash_lineTotals += row[5].ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(row[5].ToString());
            }


            UberRides_lineTotals = Math.Round(UberRides_lineTotals, 2);
            UberDeliveries_lineTotals = Math.Round(UberDeliveries_lineTotals, 2);
            Doordash_lineTotals = Math.Round(Doordash_lineTotals, 2);
        }

        private void RideShare_AmountsUpdated(object sender, EventArgs e)
        {
            double UberEats_amount = te_UberEats_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_UberEats_amount.EditValue);
            double UberRides_amount = te_UberRides_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_UberRides_amount.EditValue);
            double Lyft_amount = te_Lyft_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_Lyft_amount.EditValue);
            double Doordash_amount = te_Doordash_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_Doordash_amount.EditValue);

            double UberEats_ServiceFee_amount = te_UberEatsServiceFee_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_UberEatsServiceFee_amount.EditValue);
            //double UberRides_ServiceFee_amount = te_UberRidesServiceFee_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_UberRidesServiceFee_amount.EditValue);
            double Lyft_ServiceFee_amount = te_LyftServiceFee_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_LyftServiceFee_amount.EditValue);
            double Doordash_ServiceFee_amount = te_DoordashServiceFee_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_DoordashServiceFee_amount.EditValue);
            //double Instacart_amount = te_Instacart_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_Instacart_amount.EditValue);

            double Totals = UberEats_amount + UberRides_amount + Lyft_amount + Doordash_amount + UberEats_ServiceFee_amount + Lyft_ServiceFee_amount + Doordash_ServiceFee_amount; //+ UberRides_ServiceFee_amount
            double uberRecordedTotals = (UberEats_amount + UberRides_amount + UberEats_ServiceFee_amount);
            double uberDifferenceTotals = uberRecordedTotals - (te_UberStatement_Amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_UberStatement_Amount.EditValue));


            te_RideshareTotal_amount.EditValue = Totals;
            te_UberRecorded_Amount.EditValue = uberRecordedTotals;
            te_UberDifference_Amount.EditValue = uberDifferenceTotals;

            if (Convert.ToDouble(te_UberDifference_Amount.EditValue) < -20.00)
            {
                te_UberDifference_Amount.ForeColor = Color.Red;
            }
            else if (Convert.ToDouble(te_UberDifference_Amount.EditValue) > 20.00)
            {
                te_UberDifference_Amount.ForeColor = Color.Red;
            }
            else { te_UberDifference_Amount.ForeColor = Color.Green; }
        }

        private void IncomeTotalsUpdating(object sender, EventArgs e)
        {
            //double w2 = te_W2total_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_W2total_amount.EditValue);
            double ride = te_RideshareTotal_amount.EditValue.ToString() == "" ? Convert.ToDouble(0.00) : Convert.ToDouble(te_RideshareTotal_amount.EditValue);

            double totals = ride;

            //te_IncomeTotals.EditValue = totals;
        }

        
    }
}
