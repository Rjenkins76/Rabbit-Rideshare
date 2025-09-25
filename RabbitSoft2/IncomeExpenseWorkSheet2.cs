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
        double ExpensesBudgetAmount = 0;

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
            //spinEdit1.Value = Convert.ToDecimal(worksheet.Cells["B22"].Value.ToString());

            //XtraMessageBox.Show("Number of Trips: " + UberRidesNumberTrips.ToString() + "\n\nTotal Pay: " + TotalTripsPay.ToString("c") + "\n\nAverage Amount Per Trip: " + AvgTripAmount.ToString("c"));

            ExpensesBudgetAmount = Convert.ToDouble(worksheet.Cells["B12"].Value.ToString()) - Convert.ToDouble(worksheet.Cells["U2"].Value.ToString());
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
            ExpensesBudgetAmount = Convert.ToDouble(worksheet.Cells["B12"].Value.ToString()) - Convert.ToDouble(worksheet.Cells["U2"].Value.ToString());
            //if (!worksheet.Cells["B22"].Value.IsError) { spinEdit1.Value = Convert.ToDecimal(worksheet.Cells["B22"].Value.ToString()); }
            
            textEdit2.EditValue = Convert.ToDouble(worksheet.Cells["B21"].Value.ToString());
            Update_Calculations(new Label(), null);
        }

        private void Update_Calculations(object sender, EventArgs e)
        {
            // start period goal taking the amount needed for period and subtracting incoming stating amount...
            double startPeriodAmountNeeded = Convert.ToDouble(worksheet.Cells["B12"].Value.ToString()) - Convert.ToDouble(worksheet.Cells["U2"].Value.ToString());

            double startPeriodAmountNeeded10Day = startPeriodAmountNeeded / 10;
            lb_startPeriodAmountNeeded10Day.Text = " 10-DAY\n" + startPeriodAmountNeeded10Day.ToString("C");

            double startPeriodAmountNeeded11Day = startPeriodAmountNeeded / 11;
            lb_startPeriodAmountNeeded11Day.Text = " 11-DAY\n" + startPeriodAmountNeeded11Day.ToString("C");

            double startPeriodAmountNeeded12Day = startPeriodAmountNeeded / 12;
            lb_startPeriodAmountNeeded12Day.Text = " 12-DAY\n" + startPeriodAmountNeeded12Day.ToString("C");

            double startPeriodAmountNeeded13Day = startPeriodAmountNeeded / 13;
            lb_startPeriodAmountNeeded13Day.Text = " 13-DAY\n" + startPeriodAmountNeeded13Day.ToString("C");

            double startPeriodAmountNeeded14Day = startPeriodAmountNeeded / 14;
            lb_startPeriodAmountNeeded14Day.Text = " 14-DAY\n" + startPeriodAmountNeeded14Day.ToString("C");



            // running trip average goal - taking avg trip with number of trips and calculating difference for profit
            double uberRidesTripAmount = double.Parse(textEdit2.EditValue.ToString());
            int uberRidesTripsPerDay = int.Parse(spinEdit1.Value.ToString());
            double uberRidesIncomeTotal = uberRidesTripAmount *  uberRidesTripsPerDay;

            double lyftRidesTripAmount = double.Parse(textEdit3.EditValue.ToString());
            int lyftRidesTripsPerDay = int.Parse(spinEdit2.Value.ToString());
            double lyftRidesIncomeTotal = lyftRidesTripAmount * lyftRidesTripsPerDay;

            double uberDeliverTripAmount = double.Parse(textEdit4.EditValue.ToString());
            int uberDeliverTripsPerDay = int.Parse(spinEdit3.Value.ToString());
            double uberDeliverIncomeTotal = uberDeliverTripAmount * uberDeliverTripsPerDay;

            double doorDashTripAmount = double.Parse(textEdit5.EditValue.ToString());
            int doorDashTripsPerDay = int.Parse(spinEdit4.Value.ToString());
            double doorDashIncomeTotal = doorDashTripAmount * doorDashTripsPerDay;

            double AvgTotalTripIncome = uberRidesIncomeTotal + lyftRidesIncomeTotal + uberDeliverIncomeTotal + doorDashIncomeTotal;// + instacartIncomeTotal;
            lbl_AmtNeedPerDay_Profit.Text = AvgTotalTripIncome.ToString("c");

            double TripBaseAmountNeeded10Day = (AvgTotalTripIncome * 10);
            double TripBaseAmountNeeded10Day_Difference = TripBaseAmountNeeded10Day - startPeriodAmountNeeded;
            lb_TripBaseAmountNeeded10Day.Text = " 10-DAY\n" + TripBaseAmountNeeded10Day.ToString("c") + "\nD: " + TripBaseAmountNeeded10Day_Difference.ToString("c");

            double TripBaseAmountNeeded11Day = (AvgTotalTripIncome * 11);
            double TripBaseAmountNeeded11Day_Difference = TripBaseAmountNeeded11Day - startPeriodAmountNeeded;
            lb_TripBaseAmountNeeded11Day.Text = " 11-DAY\n" + TripBaseAmountNeeded11Day.ToString("c") + "\nD: " + TripBaseAmountNeeded11Day_Difference.ToString("c");

            double TripBaseAmountNeeded12Day = (AvgTotalTripIncome * 12);
            double TripBaseAmountNeeded12Day_Difference = TripBaseAmountNeeded12Day - startPeriodAmountNeeded;
            lb_TripBaseAmountNeeded12Day.Text = " 12-DAY\n" + TripBaseAmountNeeded12Day.ToString("c") + "\nD: " + TripBaseAmountNeeded12Day_Difference.ToString("c");

            double TripBaseAmountNeeded13Day = (AvgTotalTripIncome * 13);
            double TripBaseAmountNeeded13Day_Difference = TripBaseAmountNeeded13Day - startPeriodAmountNeeded;
            lb_TripBaseAmountNeeded13Day.Text = " 13-DAY\n" + TripBaseAmountNeeded13Day.ToString("c") + "\nD: " + TripBaseAmountNeeded13Day_Difference.ToString("c");

            double TripBaseAmountNeeded14Day = (AvgTotalTripIncome * 14);
            double TripBaseAmountNeeded14Day_Difference = TripBaseAmountNeeded14Day - startPeriodAmountNeeded;
            lb_TripBaseAmountNeeded14Day.Text = " 14-DAY\n" + TripBaseAmountNeeded14Day.ToString("c") + "\nD: " + TripBaseAmountNeeded14Day_Difference.ToString("c");


            // personal goal  @ $135.00 a day - taking avg trip with number of trips and calculating difference for profit
            double PersonalAmountIncome = 135.00;// uberRidesIncomeTotal + lyftRidesIncomeTotal + uberDeliverIncomeTotal + doorDashIncomeTotal;// + instacartIncomeTotal;
            lbl_PersonalAmtNeedPerDay_Profit.Text = PersonalAmountIncome.ToString("c");

            double PersonalAmountNeeded10Day = (135 * 10);
            double PersonalAmountNeeded10Day_Difference = PersonalAmountNeeded10Day - startPeriodAmountNeeded;
            lb_PersonalAmountNeeded10Day.Text = " 10-DAY\n" + PersonalAmountNeeded10Day.ToString("c") + "\n" + PersonalAmountNeeded10Day_Difference.ToString("c");

            double PersonalAmountNeeded11Day = (135 * 11);
            double PersonalAmountNeeded11Day_Difference = PersonalAmountNeeded11Day - startPeriodAmountNeeded;
            lb_PersonalAmountNeeded11Day.Text = " 11-DAY\n" + PersonalAmountNeeded11Day.ToString("c") + "\n" + PersonalAmountNeeded11Day_Difference.ToString("c");

            double PersonalAmountNeeded12Day = (135 * 12);
            double PersonalAmountNeeded12Day_Difference = PersonalAmountNeeded12Day - startPeriodAmountNeeded;
            lb_PersonalAmountNeeded12Day.Text = " 12-DAY\n" + PersonalAmountNeeded12Day.ToString("c") + "\n" + PersonalAmountNeeded12Day_Difference.ToString("c");

            double PersonalAmountNeeded13Day = (135 * 13);
            double PersonalAmountNeeded13Day_Difference = PersonalAmountNeeded13Day - startPeriodAmountNeeded;
            lb_PersonalAmountNeeded13Day.Text = " 13-DAY\n" + PersonalAmountNeeded13Day.ToString("c") + "\n" + PersonalAmountNeeded13Day_Difference.ToString("c");

            double PersonalAmountNeeded14Day = (135 * 14);
            double PersonalAmountNeeded14Day_Difference = PersonalAmountNeeded14Day - startPeriodAmountNeeded;
            lb_PersonalAmountNeeded14Day.Text = " 14-DAY\n" + PersonalAmountNeeded14Day.ToString("c") + "\n" + PersonalAmountNeeded14Day_Difference.ToString("c");


        }
    }
}
