using DevExpress.DataAccess.Excel;
using DevExpress.DataAccess.Sql;
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

namespace RabbitSoft
{
    public partial class UberDeliveriesCollectData : DevExpress.XtraEditors.XtraUserControl
    {
        string appPath = AppDomain.CurrentDomain.BaseDirectory;
        UberDeliveriesDataProcessClass dataProcessClass;

        public UberDeliveriesCollectData()
        {
            InitializeComponent();

            dataProcessClass = new UberDeliveriesDataProcessClass(sqlDataSource1);
        }

        private void webView21_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            webView21.DefaultBackgroundColor = this.BackColor;
            webView21.CoreWebView2.Profile.DefaultDownloadFolderPath = appPath + "DOWNLOADS\\";
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            XtraOpenFileDialog dialog = new XtraOpenFileDialog();
            dialog.DefaultViewMode = DevExpress.Dialogs.Core.View.ViewMode.Content;
            dialog.ShowNavigationPane = false;
            dialog.Filter = "CSV (*.csv)|*trip_activity*.csv|All Files (*.*)|*.*";
            dialog.Title = "Select a File";
            dialog.InitialDirectory = appPath + "DOWNLOADS\\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = dialog.FileName;

                labelControl5.Text = dialog.SafeFileName;

                ExcelDataSource excelDataTripActivity = dataProcessClass.excelData_TripActivity(selectedFilePath);
                dataProcessClass.dt = dataProcessClass.ToDataTable(excelDataTripActivity);
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            XtraOpenFileDialog dialog = new XtraOpenFileDialog();
            dialog.DefaultViewMode = DevExpress.Dialogs.Core.View.ViewMode.Content;
            dialog.ShowNavigationPane = false;
            dialog.Filter = "CSV (*.csv)|*payments_order*.csv|All Files (*.*)|*.*";
            dialog.Title = "Select a File";
            dialog.InitialDirectory = appPath + "DOWNLOADS\\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = dialog.FileName;
                labelControl6.Text = dialog.SafeFileName;

                ExcelDataSource excelDataPaymentOrders = dataProcessClass.excelData_PaymentOrders(selectedFilePath);
                dataProcessClass.dt2 = dataProcessClass.ToDataTable(excelDataPaymentOrders);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dataProcessClass.addTripActivity(dataProcessClass.dt);
            dataProcessClass.UpdateTripActivityWithPayments(dataProcessClass.dt2);
            dataProcessClass.UpdateTripActivityWithTotals();

            dataProcessClass.GetDateListInfo();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            dataProcessClass.GetDateListInfo();
        }

        private void UberDeliveriesCollectData_Load(object sender, EventArgs e)
        {

        }
    }
}
