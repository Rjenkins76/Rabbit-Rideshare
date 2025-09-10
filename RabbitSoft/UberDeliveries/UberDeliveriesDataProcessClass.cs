using DevExpress.DataAccess.Excel;
using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraLayout.Painting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RabbitSoft
{
    public class UberDeliveriesDataProcessClass
    {
        public DataTable dt;
        public DataTable dt2;
        public DataTable dt3;
        SqlDataSource data;

        DateTime currentDate;// = new DateTime(2025,08,05);


        public UberDeliveriesDataProcessClass(SqlDataSource Data) 
        {
            data = Data;

        }

        public void addTripActivity(DataTable table)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();

                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("INSERT INTO Trip_Activity (TRIP_ID, DATE, PICKUP_LOCATION, TOTAL_MILEAGE) VALUES (@Column1, @Column2, @Column3, @Column4)", connection))
                        {
                            command.Parameters.AddWithValue("@Column1", row["Trip UUID"]);
                            command.Parameters.AddWithValue("@Column2", row["Trip request time"]);
                            currentDate = Convert.ToDateTime(row["Trip request time"].ToString());
                            command.Parameters.AddWithValue("@Column3", row["Pickup address"]);
                            command.Parameters.AddWithValue("@Column4", row["Trip distance"]);
                            command.ExecuteNonQuery();

                            
                            
                        }
                    }
                    catch (Exception e){ continue; }
                    
                }

                connection.Close();
            }
        }

        public void UpdateTripActivityWithPayments(DataTable table)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                foreach (DataRow row in table.Rows)
                {
                    if (row["Description"].ToString() == "eats completed order")
                    {
                        //MessageBox.Show(row["Trip UUID"].ToString());
                        string query = "UPDATE Trip_Activity SET PROVIDER = @provider,TOTAL_FARE = @fare,TOTAL_TIPS = 0,TOTAL_COLLECTED = 0 WHERE TRIP_ID = @ID";

                        string provider = "UBER DELIVERIES";
                        string ID = row["Trip UUID"].ToString();
                        double fare = double.Parse(row["Paid to you"].ToString());

                        

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@provider", provider);
                            command.Parameters.AddWithValue("@ID", ID);
                            command.Parameters.AddWithValue("@fare", fare);

                            command.ExecuteNonQuery();
                        }
                    }
                    else if (row["Description"].ToString() == "trip fare adjust order")
                    {
                        string query = "UPDATE Trip_Activity SET TOTAL_TIPS = @tips WHERE TRIP_ID = @ID";


                        double tips = double.Parse(row["Paid to you"].ToString());
                        string ID = row["Trip UUID"].ToString();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@tips", tips);
                            command.Parameters.AddWithValue("@ID", ID);


                            command.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();

            }
        }

        public void UpdateTripActivityWithTotals()
        {
            data.Fill();
            dt3 = ToDataTable2(data);

            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                foreach (DataRow row in dt3.Rows)
                {
                    string query = "UPDATE Trip_Activity SET TOTAL_COLLECTED = @total WHERE TRIP_ID = @ID";

                    string ID = row["TRIP_ID"].ToString();
                    double total = double.Parse(row["TOTAL_COLLECTED"].ToString());

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@total", total);
                        command.Parameters.AddWithValue("@ID", ID);


                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }

        public DataTable ToDataTable(ExcelDataSource excelDataSource)
        {
            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<PropertyDescriptor> props = dataView.Columns.ToList<PropertyDescriptor>();
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public DataTable ToDataTable2(SqlDataSource sqlDataSource)
        {
            DataTable dataTable = new DataTable();
            foreach (var column in sqlDataSource.Result["Trip_Activity"].Columns)
            {
                dataTable.Columns.Add(column.Name, column.Type);
            }

            foreach (var row in sqlDataSource.Result["Trip_Activity"])
            {
                DataRow dataRow = dataTable.NewRow();
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (row[dataTable.Columns[i].ColumnName] != DBNull.Value)
                    {
                        dataRow[i] = row[dataTable.Columns[i].ColumnName];
                    }
                    else
                    {
                        dataRow[i] = 0;
                    }



                }
                dataTable.Rows.Add(dataRow);
            }


            foreach (DataRow row in dataTable.Rows)
            {
                double FARE = double.Parse(row["TOTAL_FARE"].ToString());
                double TIP = double.Parse(row["TOTAL_TIPS"].ToString());
                double TOTAL = FARE + TIP;

                row["TOTAL_COLLECTED"] = TOTAL;
            }

            return dataTable;

        }

        public ExcelDataSource excelData_TripActivity(string filename)
        {
            ExcelDataSource excelDataSource = new ExcelDataSource();
            excelDataSource.FileName = filename;// "C:\\Users\\jenki\\Downloads\\20250728-20250803-trip_activity-ROGER_JENKINS.csv";
            excelDataSource.SourceOptions = new ExcelSourceOptions(new ExcelWorksheetSettings("20250728-20250803-trip_activity"));
            //excelDataSource.ResultSchemaSerializable = resources.GetString("excelDataSource1.ResultSchemaSerializable");

            DevExpress.DataAccess.Excel.FieldInfo fieldInfo1 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo2 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo3 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo4 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo5 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo6 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo7 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo8 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo9 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo10 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo11 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo12 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo13 = new DevExpress.DataAccess.Excel.FieldInfo();

            fieldInfo1.Name = "Trip UUID";
            fieldInfo1.OriginalName = "Trip UUID";
            fieldInfo1.Type = typeof(string);
            fieldInfo2.Name = "Driver UUID";
            fieldInfo2.OriginalName = "Driver UUID";
            fieldInfo2.Selected = false;
            fieldInfo2.Type = typeof(string);
            fieldInfo3.Name = "Driver first name";
            fieldInfo3.OriginalName = "Driver first name";
            fieldInfo3.Selected = false;
            fieldInfo3.Type = typeof(string);
            fieldInfo4.Name = "Driver last name";
            fieldInfo4.OriginalName = "Driver last name";
            fieldInfo4.Selected = false;
            fieldInfo4.Type = typeof(string);
            fieldInfo5.Name = "Vehicle UUID";
            fieldInfo5.OriginalName = "Vehicle UUID";
            fieldInfo5.Selected = false;
            fieldInfo5.Type = typeof(string);
            fieldInfo6.Name = "License plate";
            fieldInfo6.OriginalName = "License plate";
            fieldInfo6.Selected = false;
            fieldInfo6.Type = typeof(string);
            fieldInfo7.Name = "Service type";
            fieldInfo7.OriginalName = "Service type";
            fieldInfo7.Selected = false;
            fieldInfo7.Type = typeof(string);
            fieldInfo8.Name = "Trip request time";
            fieldInfo8.OriginalName = "Trip request time";
            fieldInfo8.Type = typeof(System.DateTime);
            fieldInfo9.Name = "Trip drop off time";
            fieldInfo9.OriginalName = "Trip drop off time";
            fieldInfo9.Selected = false;
            fieldInfo9.Type = typeof(System.DateTime);
            fieldInfo10.Name = "Pickup address";
            fieldInfo10.OriginalName = "Pickup address";
            fieldInfo10.Type = typeof(string);
            fieldInfo11.Name = "Drop off address";
            fieldInfo11.OriginalName = "Drop off address";
            fieldInfo11.Selected = false;
            fieldInfo11.Type = typeof(string);
            fieldInfo12.Name = "Trip distance";
            fieldInfo12.OriginalName = "Trip distance";
            fieldInfo12.Type = typeof(double);
            fieldInfo13.Name = "Trip status";
            fieldInfo13.OriginalName = "Trip status";
            fieldInfo13.Selected = false;
            fieldInfo13.Type = typeof(string);
            excelDataSource.Schema.AddRange(new DevExpress.DataAccess.Excel.FieldInfo[] {
            fieldInfo1,
            fieldInfo2,
            fieldInfo3,
            fieldInfo4,
            fieldInfo5,
            fieldInfo6,
            fieldInfo7,
            fieldInfo8,
            fieldInfo9,
            fieldInfo10,
            fieldInfo11,
            fieldInfo12,
            fieldInfo13});
            CsvSourceOptions csvSourceOptions1 = new CsvSourceOptions();
            csvSourceOptions1.Culture = new System.Globalization.CultureInfo("");
            csvSourceOptions1.DetectNewlineType = true;
            csvSourceOptions1.DetectValueSeparator = true;
            //csvSourceOptions1.Encoding = ((System.Text.Encoding)(resources.GetObject("csvSourceOptions1.Encoding")));
            excelDataSource.SourceOptions = csvSourceOptions1;

            excelDataSource.Fill();

            return excelDataSource;
        }

        public ExcelDataSource excelData_PaymentOrders(string filename)
        {
            ExcelDataSource excelDataSource = new ExcelDataSource();
            excelDataSource.FileName = filename;// "C:\\Users\\jenki\\Downloads\\20250728-20250803-payments_order-ROGER_JENKINS.csv";
            //excelDataSource.SourceOptions = new ExcelSourceOptions(new ExcelWorksheetSettings("20250803-20250803-payments_orde"));
            DevExpress.DataAccess.Excel.CsvSourceOptions csvSourceOptions1 = new DevExpress.DataAccess.Excel.CsvSourceOptions();

            DevExpress.DataAccess.Excel.FieldInfo fieldInfo1 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo2 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo3 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo4 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo5 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo6 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo7 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo8 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo9 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo10 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo11 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo12 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo13 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo14 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo15 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo16 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo17 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo18 = new DevExpress.DataAccess.Excel.FieldInfo();

            fieldInfo1.Name = "transaction UUID";
            fieldInfo1.OriginalName = "transaction UUID";
            fieldInfo1.Selected = false;
            fieldInfo1.Type = typeof(string);
            fieldInfo2.Name = "Driver UUID";
            fieldInfo2.OriginalName = "Driver UUID";
            fieldInfo2.Selected = false;
            fieldInfo2.Type = typeof(string);
            fieldInfo3.Name = "Driver first name";
            fieldInfo3.OriginalName = "Driver first name";
            fieldInfo3.Selected = false;
            fieldInfo3.Type = typeof(string);
            fieldInfo4.Name = "Driver last name";
            fieldInfo4.OriginalName = "Driver last name";
            fieldInfo4.Selected = false;
            fieldInfo4.Type = typeof(string);
            fieldInfo5.Name = "Trip UUID";
            fieldInfo5.OriginalName = "Trip UUID";
            fieldInfo5.Type = typeof(string);
            fieldInfo6.Name = "Description";
            fieldInfo6.OriginalName = "Description";
            fieldInfo6.Type = typeof(string);
            fieldInfo7.Name = "Organization name";
            fieldInfo7.OriginalName = "Organization name";
            fieldInfo7.Selected = false;
            fieldInfo7.Type = typeof(string);
            fieldInfo8.Name = "Org alias";
            fieldInfo8.OriginalName = "Org alias";
            fieldInfo8.Selected = false;
            fieldInfo8.Type = typeof(string);
            fieldInfo9.Name = "vs reporting";
            fieldInfo9.OriginalName = "vs reporting";
            fieldInfo9.Selected = false;
            fieldInfo9.Type = typeof(string);
            fieldInfo10.Name = "Paid to you";
            fieldInfo10.OriginalName = "Paid to you";
            fieldInfo10.Type = typeof(double);
            fieldInfo11.Name = "Paid to you : Your earnings";
            fieldInfo11.OriginalName = "Paid to you : Your earnings";
            fieldInfo11.Selected = false;
            fieldInfo11.Type = typeof(double);
            fieldInfo12.Name = "Paid to you : Trip balance : Payouts : Cash Collected";
            fieldInfo12.OriginalName = "Paid to you : Trip balance : Payouts : Cash Collected";
            fieldInfo12.Selected = false;
            fieldInfo12.Type = typeof(double);
            fieldInfo13.Name = "Paid to you : Your earnings : Fare";
            fieldInfo13.OriginalName = "Paid to you : Your earnings : Fare";
            fieldInfo13.Selected = false;
            fieldInfo13.Type = typeof(double);
            fieldInfo14.Name = "Paid to you : Your earnings : Taxes";
            fieldInfo14.OriginalName = "Paid to you : Your earnings : Taxes";
            fieldInfo14.Selected = false;
            fieldInfo14.Type = typeof(double);
            fieldInfo15.Name = "Paid to you:Your earnings:Fare:Fare";
            fieldInfo15.OriginalName = "Paid to you:Your earnings:Fare:Fare";
            fieldInfo15.Type = typeof(double);
            fieldInfo16.Name = "Paid to you:Your earnings:Tip";
            fieldInfo16.OriginalName = "Paid to you:Your earnings:Tip";
            fieldInfo16.Type = typeof(double);
            fieldInfo17.Name = "Paid to you:Trip balance:Expenses:Instant Pay Fees";
            fieldInfo17.OriginalName = "Paid to you:Trip balance:Expenses:Instant Pay Fees";
            fieldInfo17.Selected = false;
            fieldInfo17.Type = typeof(double);
            fieldInfo18.Name = "Paid to you:Trip balance:Payouts:Transferred To Bank Account";
            fieldInfo18.OriginalName = "Paid to you:Trip balance:Payouts:Transferred To Bank Account";
            fieldInfo18.Selected = false;
            fieldInfo18.Type = typeof(double);

            excelDataSource.Schema.AddRange(new DevExpress.DataAccess.Excel.FieldInfo[] {
            fieldInfo1,
            fieldInfo2,
            fieldInfo3,
            fieldInfo4,
            fieldInfo5,
            fieldInfo6,
            fieldInfo7,
            fieldInfo8,
            fieldInfo9,
            fieldInfo10,
            fieldInfo11,
            fieldInfo12,
            fieldInfo13,
            fieldInfo14,
            fieldInfo15,
            fieldInfo16,
            fieldInfo17,
            fieldInfo18});
            csvSourceOptions1.Culture = new System.Globalization.CultureInfo("");
            csvSourceOptions1.DetectNewlineType = true;
            csvSourceOptions1.DetectValueSeparator = true;
            //csvSourceOptions1.Encoding = ((System.Text.Encoding)(resources.GetObject("csvSourceOptions1.Encoding")));
            csvSourceOptions1.NewlineType = DevExpress.DataAccess.Excel.CsvNewlineType.LF;
            excelDataSource.SourceOptions = csvSourceOptions1;

            excelDataSource.Fill();

            return excelDataSource;
        }

        public void GetDateListInfo()
        {

            Dictionary<int, List<KeyValuePair<DateTime, List<TimeOnly>>>> LineItem = new Dictionary<int, List<KeyValuePair<DateTime, List<TimeOnly>>>>();
            double RunningTotal = 0;
            foreach (var row in data.Result["LEDGER"])
            {
                List<KeyValuePair<DateTime, List<TimeOnly>>> WorkShiftTimes = new List<KeyValuePair<DateTime, List<TimeOnly>>>();

                if (row[13] != null)
                {
                    RunningTotal = Convert.ToDouble(row[13].ToString());
                }


                DateTime checkingDate = Convert.ToDateTime(row["DATE"]);
                if (currentDate.ToShortDateString() == checkingDate.ToShortDateString())
                {
                    MessageBox.Show(currentDate.ToShortDateString() + "\n" + checkingDate.ToShortDateString());
                    if (row[2] != null)
                    {
                        TimeOnly startTime = TimeOnly.Parse(row[2].ToString());
                        TimeOnly endTime = TimeOnly.Parse(row[3].ToString());

                        List<TimeOnly> times = new List<TimeOnly>() { endTime, startTime };

                        WorkShiftTimes.Add(new KeyValuePair<DateTime, List<TimeOnly>>(currentDate, times));
                        LineItem.Add(Convert.ToInt32(row[0].ToString()), WorkShiftTimes);
                    }
                }
                
                else if (currentDate.AddDays(1).ToShortDateString() == checkingDate.ToShortDateString())
                {
                    MessageBox.Show(currentDate.AddDays(1).ToShortDateString() + "\n" + checkingDate.ToShortDateString());

                    if (row[2] != null)
                    {
                        TimeOnly startTime = TimeOnly.Parse(row[2].ToString());
                        TimeOnly endTime = TimeOnly.Parse(row[3].ToString());

                        List<TimeOnly> times = new List<TimeOnly>() { endTime, startTime };

                        WorkShiftTimes.Add(new KeyValuePair<DateTime, List<TimeOnly>>(currentDate, times));
                        LineItem.Add(Convert.ToInt32(row[0].ToString()), WorkShiftTimes);
                    }
                }

            }


            double totaltotal = 0;
            
            foreach (KeyValuePair<int, List<KeyValuePair<DateTime, List<TimeOnly>>>> line in LineItem)
            {
                TimeOnly endtime = TimeOnly.Parse(line.Value[0].Value[0].ToString());
                TimeOnly starttime = TimeOnly.Parse(line.Value[0].Value[1].ToString());
                TimeSpan duration = (endtime < starttime) ? (endtime.AddHours(24) - starttime) : (endtime - starttime);

                double BasePay = 0;
                double Tip_OtherPay = 0;
                double TotalPay = 0;
                int totalTrips = 0;
                double hourlyPay = 0;
                double avgtripamount = 0;

                //MessageBox.Show(line.Value[0].Value[0].ToString());
                //MessageBox.Show(dt3.Rows[0][2].ToString());

                foreach(DataRow row in dt3.Rows)
                {
                    TimeOnly LineItemTime = TimeOnly.FromDateTime(Convert.ToDateTime(row[2].ToString()));

                    if(Convert.ToDateTime(row[2].ToString()).ToShortDateString() == currentDate.ToShortDateString())
                    {
                        if (LineItemTime.IsBetween(starttime, endtime))
                        {
                            BasePay += Convert.ToDouble(row[4].ToString());
                            Tip_OtherPay += Convert.ToDouble(row[5].ToString());
                            TotalPay += Convert.ToDouble(row[6].ToString());
                            totalTrips++;


                        }
                    } 
                }

                totaltotal += TotalPay;
                hourlyPay = TotalPay / duration.TotalHours;
                avgtripamount = TotalPay / totalTrips;

                MessageBox.Show(TotalPay.ToString("c") + "\n\n" + hourlyPay.ToString("c") + "\n\n" + avgtripamount.ToString("c"));

                using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
                {
                    connection.Open();

                    string query = "UPDATE LEDGER SET TOTALTRIPS = @trips, BASEPAY = @base, TIPS_OTHER = @tips, TOTALPAY = @totalpay, RUNNINGTOTAL = @runtotal, HOURLYPAY = @hourly, AVGTRIPAMOUNT = @avg WHERE ID = @LINEITEM";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LINEITEM", line.Key);

                        command.Parameters.AddWithValue("@trips", totalTrips);
                        command.Parameters.AddWithValue("@base", BasePay);
                        command.Parameters.AddWithValue("@tips", Tip_OtherPay);
                        command.Parameters.AddWithValue("@totalpay", TotalPay);
                        RunningTotal += TotalPay;
                        command.Parameters.AddWithValue("@runtotal", RunningTotal);
                        command.Parameters.AddWithValue("@hourly", hourlyPay);
                        command.Parameters.AddWithValue("@avg", avgtripamount);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

            }

            MessageBox.Show("DONE !!!!!");

        }
    }
}

