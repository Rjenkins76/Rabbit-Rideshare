
using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RabbitSoft
{
    public class Income_Expenses_Report_Class
    {
        SqlDataSource data;
        //Report_IncomeExpense page;

        HomeScreen page;

        public DataTable Uber_Eats_dt = new DataTable();
        public DataTable Door_Dash_dt = new DataTable();
        DataTable register = new DataTable();
        public List<double> weekTOTALS = new List<double>();

        double expenses = 0;

        //public Income_Expenses_Report_Class(Report_IncomeExpense form, SqlDataSource Data)
        //{
        //    page = form;
        //    data = Data;

        //    //page.textEdit1.Text = "11";
        //}

        public Income_Expenses_Report_Class(HomeScreen Page,SqlDataSource Data)
        {
            page = Page;
            data = Data;
        }

        public void Uber_Eats_GetWeeksIncomeData()
        {
            if (Uber_Eats_dt.Columns.Count < 1)
            {
                foreach (var column in data.Result["Trip_Activity"].Columns)
                {
                    Uber_Eats_dt.Columns.Add(column.Name, column.Type);
                }

                foreach (var row in data.Result["Trip_Activity"])
                {
                    DataRow dataRow = Uber_Eats_dt.NewRow();
                    for (int i = 0; i < Uber_Eats_dt.Columns.Count; i++)
                    {
                        if (row[Uber_Eats_dt.Columns[i].ColumnName] != DBNull.Value)
                        {
                            dataRow[i] = row[Uber_Eats_dt.Columns[i].ColumnName];
                        }
                        else
                        {
                            dataRow[i] = 0;
                        }
                    }
                    Uber_Eats_dt.Rows.Add(dataRow);
                }
            }

            Uber_Eats_ProcessWeeksIncomeData();
        }

        private void Uber_Eats_ProcessWeeksIncomeData()
        {
            //MessageBox.Show(dt.Rows.Count.ToString());

            List<double> WeeksList = new List<double>();
            List<int> tripcount = new List<int>();
            List<DateTime> weeks = GetWeekDates();
            for (int i = 0; i < weeks.Count; i++)
            {
                DateTime weekstart = weeks[i];
                double total = 0;
                int count = 0;
                foreach (DataRow row in Uber_Eats_dt.Rows)
                {
                    //string PROVIDER = row["PROVIDER"].ToString();
                    DateTime date = DateTime.Parse(row["DATE"].ToString());
                    double TOTAL_COLLECTED = double.Parse(row["TOTAL_COLLECTED"].ToString());

                    if(weekstart.Date <= date)
                    {
                        if (weekstart.Date.AddDays(7) >= date)
                        {
                            total += TOTAL_COLLECTED;
                            count ++;
                        }
                    }
                }

                WeeksList.Add(total);
                tripcount.Add(count);
            }

            double total2 = 0;
            //if (WeeksList[0] != 0) { page.lblUberDeliveriesWK1_TotalIncome.Text = WeeksList[0].ToString("C"); total2 += WeeksList[0]; page.lblUberDeliveryWK12_AvgTrip.Text = (total2 / tripcount[0]).ToString("C"); }
            //if (WeeksList[1] != 0) { page.lblUberDeliveriesWK2_TotalIncome.Text = WeeksList[1].ToString("C"); total2 += WeeksList[1]; page.lblUberDeliveryWK12_AvgTrip.Text = (total2 / (tripcount[0] + tripcount[1])).ToString("C"); }
            //if (WeeksList[2] != 0) { page.lblUberDeliveriesWK3_TotalIncome.Text = WeeksList[2].ToString("C"); total2 += WeeksList[2]; page.lblUberDeliveryWK34_AvgTrip.Text = (total2 / tripcount[3]).ToString("C"); }
            //if (WeeksList[3] != 0) { page.lblUberDeliveriesWK4_TotalIncome.Text = WeeksList[3].ToString("C"); total2 += WeeksList[3]; page.lblUberDeliveryWK34_AvgTrip.Text = (total2 / (tripcount[3] + tripcount[4])).ToString("C"); }

            //page.lblUBERDELIVERIES_TotalIncome.Text = total2.ToString("C");

            weekTOTALS.Add(Convert.ToDouble(WeeksList[0]));
            weekTOTALS.Add(Convert.ToDouble(WeeksList[1]));
            weekTOTALS.Add(Convert.ToDouble(WeeksList[2]));
            weekTOTALS.Add(Convert.ToDouble(WeeksList[3]));

        }

        public void Door_Dash_GetWeeksIncomeData()
        {
            if (Door_Dash_dt.Columns.Count < 1)
            {
                foreach (var column in data.Result["Doordash_Trip_Activity"].Columns)
                {
                    Door_Dash_dt.Columns.Add(column.Name, column.Type);
                }

                foreach (var row in data.Result["Doordash_Trip_Activity"])
                {
                    DataRow dataRow = Door_Dash_dt.NewRow();
                    for (int i = 0; i < Door_Dash_dt.Columns.Count; i++)
                    {
                        if (row[Door_Dash_dt.Columns[i].ColumnName] != DBNull.Value)
                        {
                            dataRow[i] = row[Door_Dash_dt.Columns[i].ColumnName];
                        }
                        else
                        {
                            dataRow[i] = 0;
                        }
                    }
                    Door_Dash_dt.Rows.Add(dataRow);
                }
            }

            Door_Dash_ProcessWeeksIncomeData();
        }

        private void Door_Dash_ProcessWeeksIncomeData()
        {
            //MessageBox.Show(dt.Rows.Count.ToString());

            List<double> WeeksList = new List<double>();
            List<int> tripcount = new List<int>();
            List<DateTime> weeks = GetWeekDates();
            for (int i = 0; i < weeks.Count; i++)
            {
                DateTime weekstart = weeks[i];
                double total = 0;
                int count = 0;
                foreach (DataRow row in Door_Dash_dt.Rows)
                {
                    //string PROVIDER = row["PROVIDER"].ToString();
                    DateTime date = DateTime.Parse(row["DATE"].ToString());
                    double TOTAL_COLLECTED = double.Parse(row["TOTALPAY"].ToString());

                    if (weekstart.Date <= date)
                    {
                        if (weekstart.Date.AddDays(7) >= date)
                        {
                            total += TOTAL_COLLECTED;
                            count++;
                        }
                    }
                }

                WeeksList.Add(total);
                tripcount.Add(count);
            }

            double total2 = 0;
            //if (WeeksList[0] != 0) { page.lblDoorDashWK1_TotalIncome.Text = WeeksList[0].ToString("C"); total2 += WeeksList[0]; page.lblDoorDashWK12_AvgTrip.Text = (total2 / tripcount[0]).ToString("C"); }
            //if (WeeksList[1] != 0) { page.lblDoorDashWK2_TotalIncome.Text = WeeksList[1].ToString("C"); total2 += WeeksList[1]; page.lblDoorDashWK12_AvgTrip.Text = (total2 / (tripcount[0] + tripcount[1])).ToString("C"); }
            //if (WeeksList[2] != 0) { page.lblDoorDashWK3_TotalIncome.Text = WeeksList[2].ToString("C"); total2 += WeeksList[2]; page.lblDoorDashWK34_AvgTrip.Text = (total2 / tripcount[3]).ToString("C"); }
            //if (WeeksList[3] != 0) { page.lblDoorDashWK4_TotalIncome.Text = WeeksList[3].ToString("C"); total2 += WeeksList[3]; page.lblDoorDashWK34_AvgTrip.Text = (total2 / (tripcount[3] + tripcount[4])).ToString("C"); }

            //page.lblDOORDASH_TotalIncome.Text = total2.ToString("C");


            weekTOTALS[0] += (Convert.ToDouble(WeeksList[0]));
            weekTOTALS[1] += (Convert.ToDouble(WeeksList[1]));
            weekTOTALS[2] += (Convert.ToDouble(WeeksList[2]));
            weekTOTALS[3] += (Convert.ToDouble(WeeksList[3]));
        }

        public void calcTotals()
        {
            page.lblWeek1Totals.Text = Convert.ToDouble(weekTOTALS[0]) > 0.01 ? weekTOTALS[0].ToString("C") : "";
            page.lblWeek2Totals.Text = Convert.ToDouble(weekTOTALS[1]) > 0.01 ? weekTOTALS[1].ToString("C") : "";
            page.lblWeek3Totals.Text = Convert.ToDouble(weekTOTALS[2]) > 0.01 ? weekTOTALS[2].ToString("C") : "";
            page.lblWeek4Totals.Text = Convert.ToDouble(weekTOTALS[3]) > 0.01 ? weekTOTALS[3].ToString("C") : "";
        }

        private List<DateTime> GetWeekDates()
        {
            List<DateTime> weekDates = new List<DateTime>();

            DateTime date = DateTime.Parse("08/01/2025"); //DateTime.Today;
            // first generate all dates in the month of 'date'
            var dates = Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month)).Select(n => new DateTime(date.Year, date.Month, n));
            // then filter the only the start of weeks
            var weekends = from d in dates
                           where d.DayOfWeek == DayOfWeek.Monday
                           select d;

            foreach (DateTime week in weekends)
            {
                weekDates.Add(week);

            }

            return weekDates;
        }

        public void registerStart()
        {
            foreach (var column in data.Result["REGIESTER"].Columns)
            {
                register.Columns.Add(column.Name, column.Type);
            }

            foreach (var row in data.Result["REGIESTER"])
            {
                DataRow dataRow = register.NewRow();
                for (int i = 0; i < register.Columns.Count; i++)
                {
                    if (row[register.Columns[i].ColumnName] != DBNull.Value)
                    {
                        dataRow[i] = row[register.Columns[i].ColumnName];
                    }
                    else
                    {
                        dataRow[i] = 0;
                    }
                }
                register.Rows.Add(dataRow);
            }
        }

        public void CalcFinalTotals()
        {
            double startAmount = 0;
            foreach (DataRow row in register.Rows)
            {
                if (Convert.ToInt32(row[5]) == 1)
                {
                    expenses += Convert.ToDouble(row[3]);
                }
                else if (Convert.ToInt32(row[5]) == 2)
                {
                    startAmount = Convert.ToDouble(row[3]);
                }
            }

            page.lblMothExpenses.Text = (-expenses).ToString("c");

            double totals = 0;
            foreach (double items in weekTOTALS)
            {
                totals += items;
            }

            page.lblMothIncome.Text = (startAmount + totals).ToString("C");

            double whatwewant = (startAmount + totals) + expenses;
            page.lblFinalFinal.Text = whatwewant.ToString("C");

        }
    }
}
