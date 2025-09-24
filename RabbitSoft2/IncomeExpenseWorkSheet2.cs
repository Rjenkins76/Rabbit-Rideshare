using DevExpress.Spreadsheet;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace RabbitSoft2
{
    public partial class IncomeExpenseWorkSheet2 : DevExpress.XtraEditors.XtraUserControl
    {
        IWorkbook workbook;
        Worksheet worksheet;

        DateTime todaysDate = DateTime.Now;
        DateTime StartDateDate;// = todaysDate.AddDays(-14);

        double cellUpdateAmountNeeded = 0;
        double ExpensesAmountBiWeek = 0;

        public IncomeExpenseWorkSheet2()
        {
            InitializeComponent();
            // Loads data from the sql server
            lEDGERTableAdapter.Fill(rABBIT_RIDESHAREDataSet.LEDGER);
            trip_ActivityTableAdapter.Fill(rABBIT_RIDESHAREDataSet.Trip_Activity);
            doordash_Trip_ActivityTableAdapter.Fill(rABBIT_RIDESHAREDataSet.Doordash_Trip_Activity);

            StartDateDate = todaysDate.AddDays(-7);
            labelControl5.Text = StartDateDate.ToShortDateString();
        }

        private void IncomeExpenseWorkSheet2_Load(object sender, EventArgs e)
        {
            GetStartingTripAmounts();
        }

        // Process Data for starting Calculations
        
        private void GetStartingTripAmounts()
        {
            int UberRidesNumberTrips = 0;
            double TotalTripsPay = 0;
            double AvgTripAmount = 0;

            // Collect recorded number of trips and total income for each day per the ledger for UBER RIDES ONLY
            foreach (DataRow row in rABBIT_RIDESHAREDataSet.LEDGER.Rows)
            {
                if (Convert.ToDateTime(row[1]).Date >= StartDateDate.Date)// Convert.ToDateTime(worksheet.Cells["T3"].Value.ToString()))
                {
                    if (row[6].ToString() == "UBER RIDES")
                    {

                        UberRidesNumberTrips += Convert.ToInt32(row[7].ToString());
                        TotalTripsPay += Convert.ToDouble(row[10]);

                        // adds the daily amount to the edge of spreedsheet
                        for (int i = 3; i <= 16; i++)
                        {
                            if(Convert.ToDateTime(worksheet.Cells["T"+i].Value.ToString()) == Convert.ToDateTime(row[1]).Date)
                            {
                                worksheet.Cells["U" + i].Value = Convert.ToDouble(row[10]);
                            }
                        }
  
                    }
                    
                }
            }
            // gets the average amount per trip calculated from the ledger 
            AvgTripAmount = Math.Round(TotalTripsPay / UberRidesNumberTrips,2);

            // updates both the textbox and spreedsheet with average trip amounts 
            textEdit2.Text = AvgTripAmount.ToString();
            worksheet.Cells["B21"].Value = AvgTripAmount;
            spinEdit1.Value = Convert.ToDecimal(worksheet.Cells["B22"].Value.ToString());

            //XtraMessageBox.Show("Number of Trips: " + UberRidesNumberTrips.ToString() + "\n\nTotal Pay: " + TotalTripsPay.ToString("c") + "\n\nAverage Amount Per Trip: " + AvgTripAmount.ToString("c"));

            ExpensesAmountBiWeek = Convert.ToDouble(worksheet.Cells["B12"].Value.ToString());
            Update_Calculations(new Label(), null);
        }

        private void IncomeExpenseWorkSheet2_ParentChanged(object sender, EventArgs e)
        {
            // This will load and save the worksheet on entery and exit
            if (this.Parent != null)
            {
                spreadsheetControl1.LoadDocument("IncomeExpenseWorksheet.xlsx");

                // updates the global values for the spreedsheet to allow for further use.
                workbook = spreadsheetControl1.Document;
                worksheet = workbook.Worksheets[0];
                worksheet.Cells["A21"].Value = "AVG PER TRIP:";

            }
            else
            {
                spreadsheetControl1.SaveDocument("IncomeExpenseWorksheet.xlsx");
            }
        }

        private void spreadsheetControl1_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e)
        {
            ExpensesAmountBiWeek = Convert.ToDouble(worksheet.Cells["B12"].Value.ToString());
            if(!worksheet.Cells["B22"].Value.IsError) { spinEdit1.Value = Convert.ToDecimal(worksheet.Cells["B22"].Value.ToString()); }
            
            textEdit2.EditValue = Convert.ToDouble(worksheet.Cells["B21"].Value.ToString());
            Update_Calculations(new Label(), null);
        }

        private void Update_Calculations(object sender, EventArgs e)
        {
            //if (sender.GetType() == typeof(SimpleButton)) { SimpleButton button = (SimpleButton)sender; }

            double uberRidesTripAmount = double.Parse(textEdit2.EditValue.ToString());
            int uberRidesTripsPerDay = int.Parse(spinEdit1.Value.ToString());
            double uberRidesIncomeTotal = uberRidesTripAmount * uberRidesTripsPerDay;

            double lyftRidesTripAmount = double.Parse(textEdit3.EditValue.ToString());
            int lyftRidesTripsPerDay = int.Parse(spinEdit2.Value.ToString());
            double lyftRidesIncomeTotal = lyftRidesTripAmount * lyftRidesTripsPerDay;

            double uberDeliverTripAmount = double.Parse(textEdit4.EditValue.ToString());
            int uberDeliverTripsPerDay = int.Parse(spinEdit3.Value.ToString());
            double uberDeliverIncomeTotal = uberDeliverTripAmount * uberDeliverTripsPerDay;

            double doorDashTripAmount = double.Parse(textEdit5.EditValue.ToString());
            int doorDashTripsPerDay = int.Parse(spinEdit4.Value.ToString());
            double doorDashIncomeTotal = doorDashTripAmount * doorDashTripsPerDay;

            double TotalIncome = uberRidesIncomeTotal + lyftRidesIncomeTotal + uberDeliverIncomeTotal + doorDashIncomeTotal;// + instacartIncomeTotal;
            double twoWeekTotalIncome_10Days = (TotalIncome * 10);
            double twoWeekTotalIncome_11Days = (TotalIncome * 11);
            double twoWeekTotalIncome_12Days = (TotalIncome * 12);
            double twoWeekTotalIncome_13Days = (TotalIncome * 13);
            double twoWeekTotalIncome_14Days = (TotalIncome * 14);

            double profitWeek_10Days = (twoWeekTotalIncome_10Days / 2);
            double profitWeek_11Days = (twoWeekTotalIncome_11Days / 2);
            double profitWeek_12Days = (twoWeekTotalIncome_12Days / 2);
            double profitWeek_13Days = (twoWeekTotalIncome_13Days / 2);
            double profitWeek_14Days = (twoWeekTotalIncome_14Days / 2);

            double profitDay = (twoWeekTotalIncome_10Days / 10);

            lbl_AmtNeedPerDay_Profit.Text = profitDay.ToString("c");

            lbl_AmtNeedPerWeek_Profit_10Days.Text = profitWeek_10Days.ToString("C");
            lbl_AmtNeedPerWeek_Profit_11Days.Text = profitWeek_11Days.ToString("C");
            lbl_AmtNeedPerWeek_Profit_12Days.Text = profitWeek_12Days.ToString("C");
            lbl_AmtNeedPerWeek_Profit_13Days.Text = profitWeek_13Days.ToString("C");
            lbl_AmtNeedPerWeek_Profit_14Days.Text = profitWeek_14Days.ToString("C");

            double difference_10Day = (profitWeek_10Days * 2) - ExpensesAmountBiWeek;
            double difference_11Day = (profitWeek_11Days * 2) - ExpensesAmountBiWeek;
            double difference_12Day = (profitWeek_12Days * 2) - ExpensesAmountBiWeek;
            double difference_13Day = (profitWeek_13Days * 2) - ExpensesAmountBiWeek;
            double difference_14Day = (profitWeek_14Days * 2) - ExpensesAmountBiWeek;

            lblDifference_10Day.Text = difference_10Day.ToString("c");
            lblDifference_11Day.Text = difference_11Day.ToString("c");
            lblDifference_12Day.Text = difference_12Day.ToString("c");
            lblDifference_13Day.Text = difference_13Day.ToString("c");
            lblDifference_14Day.Text = difference_14Day.ToString("c");

        }
    }
}
