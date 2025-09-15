using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Html;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitSoft2
{
    public class WorkShiftDataClass
    {
        public double RunningTotal = 0;
        Dictionary<int, List<KeyValuePair<DateTime, List<TimeOnly>>>> LineItem = new Dictionary<int, List<KeyValuePair<DateTime, List<TimeOnly>>>>();
        Dictionary<int, DateTime> DDLineItem = new Dictionary<int, DateTime>();
        public WorkShiftDataClass() 
        {

        }

        public void UberDeliveriesProcessData(DateTime processdate)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM LEDGER", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<KeyValuePair<DateTime, List<TimeOnly>>> WorkShiftTimes = new List<KeyValuePair<DateTime, List<TimeOnly>>>();
                            //XtraMessageBox.Show(processdate.ToShortDateString() + "\n\n" + Convert.ToDateTime(reader[1].ToString()).ToShortDateString());
                            if(reader[11].ToString() != string.Empty)
                            {
                                RunningTotal = double.Parse(reader[11].ToString());
                            }
                            else if (reader[11].ToString() == string.Empty)
                            {
                                if (processdate.ToShortDateString() == Convert.ToDateTime(reader[1].ToString()).ToShortDateString())
                                {
                                    if (reader[6].ToString() == "UBER DELIVERIES")
                                    {
                                        TimeOnly startTime = TimeOnly.Parse(reader[2].ToString());
                                        TimeOnly endTime = TimeOnly.Parse(reader[3].ToString());

                                        List<TimeOnly> times = new List<TimeOnly>() { endTime, startTime };

                                        WorkShiftTimes.Add(new KeyValuePair<DateTime, List<TimeOnly>>(processdate, times));
                                        LineItem.Add(Convert.ToInt32(reader[0].ToString()), WorkShiftTimes);
                                    }
                                        
                                }
                            }
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

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Trip_Activity", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TimeOnly LineItemTime = TimeOnly.FromDateTime(Convert.ToDateTime(reader[2].ToString()));
                                if (Convert.ToDateTime(reader[2].ToString()).ToShortDateString() == processdate.ToShortDateString())
                                {
                                    if (LineItemTime.IsBetween(starttime, endtime))
                                    {
                                        BasePay += Convert.ToDouble(reader[4].ToString());
                                        Tip_OtherPay += Convert.ToDouble(reader[5].ToString());
                                        TotalPay += Convert.ToDouble(reader[6].ToString());
                                        totalTrips++;
                                    }
                                }
                                else if (Convert.ToDateTime(reader[2].ToString()).ToShortDateString() == processdate.AddDays(1).ToShortDateString())
                                {
                                    if (LineItemTime.IsBetween(starttime, endtime))
                                    {
                                        BasePay += Convert.ToDouble(reader[4].ToString());
                                        Tip_OtherPay += Convert.ToDouble(reader[5].ToString());
                                        TotalPay += Convert.ToDouble(reader[6].ToString());
                                        totalTrips++;
                                    }
                                }

                                totaltotal += TotalPay;
                                hourlyPay = TotalPay / duration.TotalHours;
                                avgtripamount = TotalPay / totalTrips;

                            }

                            MessageBox.Show(totalTrips.ToString() + "\n\n" + BasePay.ToString() + "\n\n" + Tip_OtherPay.ToString() + "\n\n" + TotalPay.ToString("c") + "\n\n" + hourlyPay.ToString("c") + "\n\n" + avgtripamount.ToString("c"));
                        }
                    }

                    
                    try
                    {
                        string query = "UPDATE LEDGER SET TOTALTRIPS = @trips, BASEPAY = @base, TIPS_OTHER = @tips, TOTALPAY = @totalpay, RUNNINGTOTAL = @runtotal, HOURLYPAY = @hourly, AVGTRIPAMOUNT = @avg WHERE ID = @LINEITEM";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@LINEITEM", line.Key);

                            command.Parameters.AddWithValue("@trips", totalTrips);
                            command.Parameters.AddWithValue("@base", Math.Round(BasePay, 2));
                            command.Parameters.AddWithValue("@tips", Math.Round(Tip_OtherPay, 2));
                            command.Parameters.AddWithValue("@totalpay", Math.Round(TotalPay, 2));
                            RunningTotal += TotalPay;
                            command.Parameters.AddWithValue("@runtotal", Math.Round(RunningTotal, 2));
                            command.Parameters.AddWithValue("@hourly", Math.Round(hourlyPay, 2));
                            command.Parameters.AddWithValue("@avg", Math.Round(avgtripamount, 2));

                            command.ExecuteNonQuery();
                        }

                        int RowNumberUpdating = 0;
                        using (SqlCommand command = new SqlCommand("SELECT ID, DATE, ACTIVITY, STARTTIME, NUMBERTRIPS FROM MILEAGELOG", connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader["DATE"].ToString() == processdate.Date.ToString())
                                    {
                                        if(reader["ACTIVITY"].ToString() == "UBER DELIVERIES")
                                        {
                                            if(TimeOnly.Parse(reader["STARTTIME"].ToString()) == starttime)
                                            {
                                                    RowNumberUpdating = Convert.ToInt32(reader["ID"].ToString());
                                            }
                                            
                                        }
                                    }
                                }
                            }
                        }

                        query = "UPDATE MILEAGELOG SET NUMBERTRIPS = @NUMTRIPS WHERE ID = @LINEITEM";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@LINEITEM", RowNumberUpdating);
                            command.Parameters.AddWithValue("@NUMTRIPS", totalTrips);
                            command.ExecuteNonQuery();
                        }

                    }
                    catch (Exception e) { MessageBox.Show(e.Message); }
                    
                }


                connection.Close();
            }                


        }

        public void DoorDashProcessData(DateTime processdate)
        {
            
            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();

                TimeOnly startTime = TimeOnly.Parse("00:00") , EndTime = TimeOnly.Parse("00:00");
                TimeSpan duration;

                using (SqlCommand command = new SqlCommand("SELECT * FROM LEDGER", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<KeyValuePair<DateTime, List<TimeOnly>>> WorkShiftTimes = new List<KeyValuePair<DateTime, List<TimeOnly>>>();
                            //XtraMessageBox.Show(processdate.ToShortDateString() + "\n\n" + Convert.ToDateTime(reader[1].ToString()).ToShortDateString());
                            if (reader[13].ToString() != string.Empty)
                            {
                                RunningTotal = double.Parse(reader[13].ToString());
                            }
                            else if (reader[13].ToString() == string.Empty)
                            {
                                if (processdate.ToShortDateString() == Convert.ToDateTime(reader[1].ToString()).ToShortDateString())
                                {
                                    if (reader[8].ToString() == "DOOR DASH")
                                    {
                                        startTime = TimeOnly.Parse(reader[2].ToString());
                                        EndTime = TimeOnly.Parse(reader[3].ToString());
                                        DDLineItem.Add(Convert.ToInt32(reader[0].ToString()), processdate);
                                    }

                                }
                            }
                        }
                    }
                }

                duration = (EndTime < startTime) ? (EndTime.AddHours(24) - startTime) : (EndTime - startTime);
                double totaltotal = 0;

                foreach (KeyValuePair<int, DateTime> line in DDLineItem)
                {
                    double BasePay = 0;
                    double Tip_OtherPay = 0;
                    double TotalPay = 0;
                    int totalTrips = 0;
                    double hourlyPay = 0;
                    double avgtripamount = 0;

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Doordash_Trip_Activity", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToDateTime(reader[1].ToString()).ToShortDateString() == processdate.ToShortDateString())
                                {
                                    BasePay += Convert.ToDouble(reader[3].ToString());
                                    Tip_OtherPay += Convert.ToDouble(reader[4].ToString());
                                    TotalPay += Convert.ToDouble(reader[5].ToString());
                                    totalTrips++;
                                }
                                    
                            }
                        }
                    }

                    totaltotal += TotalPay;
                    hourlyPay = TotalPay / duration.TotalHours;
                    avgtripamount = TotalPay / totalTrips;

                    XtraMessageBox.Show(totalTrips.ToString() + "\n\n" + BasePay.ToString() + "\n\n" + Tip_OtherPay.ToString() + "\n\n" + TotalPay.ToString("c") + "\n\n" + hourlyPay.ToString("c") + "\n\n" + avgtripamount.ToString("c"));


                    try
                    {
                        string query = "UPDATE LEDGER SET TOTALTRIPS = @trips, BASEPAY = @base, TIPS_OTHER = @tips, TOTALPAY = @totalpay, RUNNINGTOTAL = @runtotal, HOURLYPAY = @hourly, AVGTRIPAMOUNT = @avg WHERE ID = @LINEITEM";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@LINEITEM", line.Key);

                            command.Parameters.AddWithValue("@trips", totalTrips);
                            command.Parameters.AddWithValue("@base", Math.Round(BasePay,2));
                            command.Parameters.AddWithValue("@tips", Math.Round(Tip_OtherPay, 2));
                            command.Parameters.AddWithValue("@totalpay", Math.Round(TotalPay, 2));
                            RunningTotal += TotalPay;
                            command.Parameters.AddWithValue("@runtotal", Math.Round(RunningTotal, 2));
                            command.Parameters.AddWithValue("@hourly", Math.Round(hourlyPay, 2));
                            command.Parameters.AddWithValue("@avg", Math.Round(avgtripamount, 2));

                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e) { continue; }
                }

                connection.Close();
            }
        }

        public void UberRidesProcessData(DateTime processdate)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM LEDGER", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<KeyValuePair<DateTime, List<TimeOnly>>> WorkShiftTimes = new List<KeyValuePair<DateTime, List<TimeOnly>>>();
                            //XtraMessageBox.Show(processdate.ToShortDateString() + "\n\n" + Convert.ToDateTime(reader[1].ToString()).ToShortDateString());
                            if (reader[11].ToString() != string.Empty)
                            {
                                RunningTotal = double.Parse(reader[11].ToString());
                            }
                            else if (reader[11].ToString() == string.Empty)
                            {
                                if (processdate.ToShortDateString() == Convert.ToDateTime(reader[1].ToString()).ToShortDateString())
                                {
                                    if (reader[6].ToString() == "UBER RIDES")
                                    {
                                        TimeOnly startTime = TimeOnly.Parse(reader[2].ToString());
                                        TimeOnly endTime = TimeOnly.Parse(reader[3].ToString());

                                        List<TimeOnly> times = new List<TimeOnly>() { endTime, startTime };

                                        WorkShiftTimes.Add(new KeyValuePair<DateTime, List<TimeOnly>>(processdate, times));
                                        LineItem.Add(Convert.ToInt32(reader[0].ToString()), WorkShiftTimes);
                                    }

                                }
                            }
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

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Trip_Activity", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    TimeOnly LineItemTime = TimeOnly.FromDateTime(Convert.ToDateTime(reader[2].ToString()));
                                    if (Convert.ToDateTime(reader[2].ToString()).ToShortDateString() == processdate.ToShortDateString())
                                    {
                                        if (LineItemTime.IsBetween(starttime, endtime))
                                        {
                                            if(LineItemTime > TimeOnly.Parse("05:00 AM"))
                                            {
                                                BasePay += Convert.ToDouble(reader[4].ToString());
                                                Tip_OtherPay += Convert.ToDouble(reader[5].ToString());
                                                TotalPay += Convert.ToDouble(reader[6].ToString());
                                                totalTrips++;
                                            }
                                            
                                        }
                                    }
                                    else if (Convert.ToDateTime(reader[2].ToString()).ToShortDateString() == processdate.AddDays(1).ToShortDateString())
                                    {
                                        if (LineItemTime.IsBetween(starttime, endtime))
                                        {
                                            BasePay += Convert.ToDouble(reader[4].ToString());
                                            Tip_OtherPay += Convert.ToDouble(reader[5].ToString());
                                            TotalPay += Convert.ToDouble(reader[6].ToString());
                                            totalTrips++;
                                        }
                                    }

                                    totaltotal += TotalPay;
                                    hourlyPay = TotalPay / duration.TotalHours;
                                    avgtripamount = TotalPay / totalTrips;
                                }
                                catch { continue; }
                                

                            }

                            MessageBox.Show(totalTrips.ToString() + "\n\n" + BasePay.ToString() + "\n\n" + Tip_OtherPay.ToString() + "\n\n" + TotalPay.ToString("c") + "\n\n" + hourlyPay.ToString("c") + "\n\n" + avgtripamount.ToString("c"));
                        }
                    }


                    try
                    {
                        string query = "UPDATE LEDGER SET TOTALTRIPS = @trips, BASEPAY = @base, TIPS_OTHER = @tips, TOTALPAY = @totalpay, RUNNINGTOTAL = @runtotal, HOURLYPAY = @hourly, AVGTRIPAMOUNT = @avg WHERE ID = @LINEITEM";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@LINEITEM", line.Key);

                            command.Parameters.AddWithValue("@trips", totalTrips);
                            command.Parameters.AddWithValue("@base", Math.Round(BasePay, 2));
                            command.Parameters.AddWithValue("@tips", Math.Round(Tip_OtherPay, 2));
                            command.Parameters.AddWithValue("@totalpay", Math.Round(TotalPay, 2));
                            RunningTotal += TotalPay;
                            command.Parameters.AddWithValue("@runtotal", Math.Round(RunningTotal, 2));
                            command.Parameters.AddWithValue("@hourly", Math.Round(hourlyPay, 2));
                            command.Parameters.AddWithValue("@avg", Math.Round(avgtripamount, 2));

                            command.ExecuteNonQuery();
                        }

                        int RowNumberUpdating = 0;
                        using (SqlCommand command = new SqlCommand("SELECT ID, DATE, ACTIVITY, STARTTIME, NUMBERTRIPS FROM MILEAGELOG", connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader["DATE"].ToString() == processdate.Date.ToString())
                                    {
                                        if (reader["ACTIVITY"].ToString() == "UBER RIDES")
                                        {
                                            if (TimeOnly.Parse(reader["STARTTIME"].ToString()) == starttime)
                                            {
                                                RowNumberUpdating = Convert.ToInt32(reader["ID"].ToString());
                                            }

                                        }
                                    }
                                }
                            }
                        }

                        query = "UPDATE MILEAGELOG SET NUMBERTRIPS = @NUMTRIPS WHERE ID = @LINEITEM";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@LINEITEM", RowNumberUpdating);
                            command.Parameters.AddWithValue("@NUMTRIPS", totalTrips);
                            command.ExecuteNonQuery();
                        }

                    }
                    catch (Exception e) { MessageBox.Show(e.Message); }

                }


                connection.Close();
            }


        }
    }
}
