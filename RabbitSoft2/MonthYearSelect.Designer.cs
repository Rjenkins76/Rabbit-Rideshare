namespace RabbitSoft2
{
    partial class MonthYearSelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_MonthSelect = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cb_YearSelect = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cb_MonthSelect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_YearSelect.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_MonthSelect
            // 
            this.cb_MonthSelect.Location = new System.Drawing.Point(179, 12);
            this.cb_MonthSelect.Name = "cb_MonthSelect";
            this.cb_MonthSelect.Properties.Appearance.Font = new System.Drawing.Font("Cascadia Mono", 14F);
            this.cb_MonthSelect.Properties.Appearance.Options.UseFont = true;
            this.cb_MonthSelect.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_MonthSelect.Properties.Items.AddRange(new object[] {
            "",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cb_MonthSelect.Size = new System.Drawing.Size(222, 32);
            this.cb_MonthSelect.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Cascadia Mono", 14F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(19, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(154, 25);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "SELECT MONTH: ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Cascadia Mono", 14F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(30, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(143, 25);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "SELECT YEAR: ";
            // 
            // cb_YearSelect
            // 
            this.cb_YearSelect.Location = new System.Drawing.Point(179, 50);
            this.cb_YearSelect.Name = "cb_YearSelect";
            this.cb_YearSelect.Properties.Appearance.Font = new System.Drawing.Font("Cascadia Mono", 14F);
            this.cb_YearSelect.Properties.Appearance.Options.UseFont = true;
            this.cb_YearSelect.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_YearSelect.Properties.Items.AddRange(new object[] {
            "",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030",
            "2031",
            "2032",
            "2033",
            "2034",
            "2035",
            "2036",
            "2037",
            "2038",
            "2039",
            "2040"});
            this.cb_YearSelect.Size = new System.Drawing.Size(222, 32);
            this.cb_YearSelect.TabIndex = 2;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Cascadia Mono", 14F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton1.Location = new System.Drawing.Point(297, 88);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(104, 30);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "OK";
            // 
            // MonthYearSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 135);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cb_YearSelect);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cb_MonthSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MonthYearSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Month - Year Select";
            this.Load += new System.EventHandler(this.MonthYearSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cb_MonthSelect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_YearSelect.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.ComboBoxEdit cb_YearSelect;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.ComboBoxEdit cb_MonthSelect;
    }
}