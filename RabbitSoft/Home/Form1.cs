using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RabbitSoft
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();

            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HomeScreen Home = new HomeScreen(this);
            Home.Dock = DockStyle.Fill;
            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(Home);
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // LEDGER
            Ledger ledger = new Ledger();
            ledger.Dock = DockStyle.Fill;
            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(ledger);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // UBER DELIVERIES COLLECT DATA

            UberDeliveriesCollectData uberDeliveriesCollectData = new UberDeliveriesCollectData();
            uberDeliveriesCollectData.Dock = DockStyle.Fill;
            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(uberDeliveriesCollectData);

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // UBER DELIVERIES VIEW DATA

            UberDeliveriesViewData uberDeliveriesViewData = new UberDeliveriesViewData();
            uberDeliveriesViewData.Dock = DockStyle.Fill;
            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(uberDeliveriesViewData);
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // MONTHLY BILLS

            Bills bills = new Bills();
            bills.Dock = DockStyle.Fill;
            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(bills);
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // WORKSHEET

            IncomeExpenseWorkSheet incomeExpenseWorkSheet = new IncomeExpenseWorkSheet();
            incomeExpenseWorkSheet.Dock = DockStyle.Fill;
            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(incomeExpenseWorkSheet);
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Report

            IncomeExpenseReport incomeExpenseReport = new IncomeExpenseReport();
            incomeExpenseReport.Dock = DockStyle.Fill;
            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(incomeExpenseReport);
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // LEDGER
            HomeScreen Home = new HomeScreen(this);
            Home.Dock = DockStyle.Fill;
            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(Home);
        }
    }
}
