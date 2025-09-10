using IronXL;
using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace RabbitSoft2
{
    public class UberDeliveriesDataProcessClass
    {
        public static DataTable tripinfo = new DataTable();
        public static DataTable ledger = new DataTable();

        public UberDeliveriesDataProcessClass()
        {
            tripinfo.TableName = "Trip_Activity";
            
            ledger.TableName = "LEDGER";
        }

        public void SetupDatabases()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM Trip_Activity";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                {
                    adapter.Fill(tripinfo);
                }

                tripinfo.PrimaryKey = new DataColumn[] { tripinfo.Columns[0] };

                sqlQuery = "SELECT * FROM LEDGER";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                {
                    adapter.Fill(ledger);
                }

                connection.Close();
            }
        }

        private void UpdateDatabases(DataTable dt)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=RABBIT_DESKTOP\\SQLEXPRESS;Initial Catalog=RABBIT_RIDESHARE;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM " + dt.TableName;

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                {
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.Update(dt);
                }

                connection.Close();
            }
        }

        public void AddTripActivity(string Trip_FileName,string Payment_FileName)
        {
            WorkBook workbook = WorkBook.LoadCSV(Trip_FileName);
            WorkSheet sheet = workbook.WorkSheets.First();
            int j = 1;

            for (int i = 2; i < sheet.Rows.Length+1; i++)
            {
                if (!tripinfo.Rows.Contains(sheet["A" + i].Value))
                {
                    DataRow newRow = tripinfo.NewRow();

                    newRow[0] = sheet["A" + i].Value;
                    newRow[1] = "UBER DELIVERIES";
                    newRow[2] = Convert.ToDateTime(sheet["H" + i].Value);
                    newRow[3] = sheet["J" + i].Value;
                    newRow[4] = 0;
                    newRow[5] = 0;
                    newRow[7] = sheet["L" + i].Value;


                    tripinfo.Rows.Add(newRow);
                    j++;
                }
                
            }

            UpdateDatabases(tripinfo);

            WorkBook workbook2 = WorkBook.LoadCSV(Payment_FileName);
            WorkSheet sheet2 = workbook2.WorkSheets.First();

            int j2 = 0;
            for (int i = 2; i < sheet2.Rows.Length + 1; i++)
            {
                string test = sheet2["E" + i].Value.ToString();

                foreach (DataRow Row in tripinfo.Rows) 
                {
                    if(Row[0].ToString() == test)
                    {
                        if (sheet2["F" + i].Value.ToString().Contains("eats"))
                        {
                            Row[4] = Convert.ToDouble(sheet2["J" + i].Value);
                        }
                        else if (sheet2["F" + i].Value.ToString().Contains("Business Order for: marketplace: DELIVERY_SERVICE"))
                        {
                            Row[5] = Convert.ToDouble(sheet2["O" + i].Value);
                            j2++;
                        }
                    }

                }

                foreach (DataRow Row in tripinfo.Rows)
                {
                    if (Row[0].ToString() == test)
                    {
                        if (sheet2["F" + i].Value.ToString().Contains("adjust"))
                        {
                            Row[5] = Convert.ToDouble(sheet2["J" + i].Value);
                            j2++;
                        }
                        else if (sheet2["F" + i].Value.ToString().Contains("Business Adjustment Order for: marketplace: DELIVERY_SERVICE"))
                        {
                            Row[5] = Convert.ToDouble(sheet2["P" + i].Value);
                            j2++;
                        }

                    }
                }

                foreach (DataRow Row in tripinfo.Rows)
                {
                    if (Row[0].ToString() == test)
                    {
                        Row[6] = Convert.ToDouble(Row[4].ToString()) + Convert.ToDouble(Row[5].ToString());
                    }
                }

                //Row[6] = Convert.ToDouble(Row[4].ToString()) + Convert.ToDouble(Row[4].ToString());
            }

            UpdateDatabases(tripinfo);
            XtraMessageBox.Show("TRIP ACTIVITY PROCESSING COMPLETED...\n\n");
        }
    }
}
