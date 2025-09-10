using RabbitSoft2.REPORTS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RabbitSoft2
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            panelControl1.Controls.Add(new HomeScreen(this));
            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UberDeliveriesCollectData data = new UberDeliveriesCollectData(this);
            data.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(data);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HomeScreen home = new HomeScreen(this);
            home.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(home);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UberDeliveriesViewDatacs viewData = new UberDeliveriesViewDatacs();
            viewData.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(viewData);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MonthYearSelect selector = new MonthYearSelect();
            DialogResult result = selector.ShowDialog();

            if (result == DialogResult.OK)
            {
                Ledger ledger = new Ledger(selector.cb_MonthSelect.SelectedIndex, Convert.ToInt32(selector.cb_YearSelect.Text));
                ledger.Dock = DockStyle.Fill;

                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(ledger);
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IncomeExpenseWorkSheet workSheet = new IncomeExpenseWorkSheet();
            workSheet.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(workSheet);
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Bills bills = new Bills();
            bills.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(bills);
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoorDashViewData viewData = new DoorDashViewData();
            viewData.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(viewData);
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoorDashCollectData collectData = new DoorDashCollectData(this);
            collectData.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(collectData);
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateTripMileage tripMileage = new UpdateTripMileage();
            tripMileage.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(tripMileage);

        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IncomeExpenseReport report = new IncomeExpenseReport();
            report.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(report);
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TaxOrganizer TAX= new TaxOrganizer();
            TAX.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(TAX);

        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MileageLogBook mileage = new MileageLogBook();
            mileage.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(mileage);
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UberRidesCollectData data = new UberRidesCollectData(this);
            data.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(data);
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UberRidesViewDatacs uberRidesView = new UberRidesViewDatacs();
            uberRidesView.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(uberRidesView);

        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            School school = new School();
            school.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(school);
        }
    }
}
