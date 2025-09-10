using DevExpress.DataAccess.Excel;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitSoft2
{
    public partial class UberDeliveriesCollectData : DevExpress.XtraEditors.XtraUserControl
    {
        string appPath = AppDomain.CurrentDomain.BaseDirectory;
        UberDeliveriesDataProcessClass dataProcessClass;
        Form1 MainScreen;
        string trip_activity_file, trip_payments_order_file;

        public UberDeliveriesCollectData(Form1 main)
        {
            InitializeComponent();
            dataProcessClass = new UberDeliveriesDataProcessClass();
            MainScreen = main;
        }

        private void UberDeliveriesCollectData_Load(object sender, EventArgs e)
        {
            
        }

        private void webView21_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            webView21.DefaultBackgroundColor = this.BackColor;
            webView21.CoreWebView2.Profile.DefaultDownloadFolderPath = appPath + "DOWNLOADS\\";
        }

        private void dateEdit1_DateChanged(object sender, EventArgs e)
        {
            //XtraMessageBox.Show(dateEdit1.DateOnly.ToString("yyyMMdd"));

            string[] files = Directory.GetFiles(appPath + "DOWNLOADS\\");
            foreach (string file in files)
            {
                if (file.Contains(dateEdit1.DateOnly.ToString("yyyMMdd") + "-trip_activity"))
                {
                    comboBoxEdit1.Properties.Items.Add(file);
                }
                else if (file.Contains(dateEdit1.DateOnly.ToString("yyyMMdd") + "-payments_order"))
                {
                    comboBoxEdit2.Properties.Items.Add(file);
                }
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            XtraOpenFileDialog dialog = new XtraOpenFileDialog();
            dialog.DefaultViewMode = DevExpress.Dialogs.Core.View.ViewMode.Content;
            dialog.ShowNavigationPane = false;
            dialog.Filter = "CSV (*.csv)|*" + dateEdit1.DateOnly.ToString("yyyMMdd") + "-trip_activity*.csv|All Files (*.*)|*.*";
            dialog.Title = "Select a File";
            dialog.InitialDirectory = appPath + "DOWNLOADS\\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                trip_activity_file = dialog.FileName;

                labelControl5.Text = dialog.SafeFileName;

                //ExcelDataSource excelDataTripActivity = dataProcessClass.excelData_TripActivity(selectedFilePath);
                //dataProcessClass.dt = dataProcessClass.ToDataTable(excelDataTripActivity);
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            XtraOpenFileDialog dialog = new XtraOpenFileDialog();
            dialog.DefaultViewMode = DevExpress.Dialogs.Core.View.ViewMode.Content;
            dialog.ShowNavigationPane = false;
            dialog.Filter = "CSV (*.csv)|*" + dateEdit1.DateOnly.ToString("yyyMMdd") + "-payments_order*.csv|All Files (*.*)|*.*";
            dialog.Title = "Select a File";
            dialog.InitialDirectory = appPath + "DOWNLOADS\\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                trip_payments_order_file = dialog.FileName;
                labelControl6.Text = dialog.SafeFileName;

                //ExcelDataSource excelDataPaymentOrders = dataProcessClass.excelData_PaymentOrders(selectedFilePath);
                //dataProcessClass.dt2 = dataProcessClass.ToDataTable(excelDataPaymentOrders);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dataProcessClass.SetupDatabases();
            dataProcessClass.AddTripActivity(comboBoxEdit1.SelectedText, comboBoxEdit2.SelectedText);

            WorkShiftDataClass workShift = new WorkShiftDataClass();
            workShift.UberDeliveriesProcessData(dateEdit1.DateTime.Date);

            DialogResult result = XtraMessageBox.Show("DO YOU WANT TO VIEW UPDATED LEDGER?", "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Ledger data = new Ledger(dateEdit1.DateTime.Month, dateEdit1.DateTime.Year);
                data.Dock = DockStyle.Fill;

                MainScreen.panelControl1.Controls.Clear();
                MainScreen.panelControl1.Controls.Add(data);

            }
        }
    }
}
