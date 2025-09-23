namespace RabbitSoft2
{
    partial class Bills
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraScheduler.TimeRuler timeRuler7 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler8 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler9 = new DevExpress.XtraScheduler.TimeRuler();
            this.schedulerControl1 = new DevExpress.XtraScheduler.SchedulerControl();
            this.schedulerDataStorage1 = new DevExpress.XtraScheduler.SchedulerDataStorage(this.components);
            this.appointmentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rABBIT_RIDESHAREDataSet = new RabbitSoft2.RABBIT_RIDESHAREDataSet();
            this.resourcesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.appointmentsTableAdapter = new RabbitSoft2.RABBIT_RIDESHAREDataSetTableAdapters.AppointmentsTableAdapter();
            this.resourcesTableAdapter = new RabbitSoft2.RABBIT_RIDESHAREDataSetTableAdapters.ResourcesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerDataStorage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rABBIT_RIDESHAREDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resourcesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // schedulerControl1
            // 
            this.schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
            this.schedulerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.schedulerControl1.DataStorage = this.schedulerDataStorage1;
            this.schedulerControl1.Location = new System.Drawing.Point(3, 3);
            this.schedulerControl1.Name = "schedulerControl1";
            this.schedulerControl1.OptionsView.FirstDayOfWeek = DevExpress.XtraScheduler.FirstDayOfWeek.Monday;
            this.schedulerControl1.OptionsView.HighlightTodayDate = DevExpress.Utils.DefaultBoolean.True;
            this.schedulerControl1.Size = new System.Drawing.Size(960, 482);
            this.schedulerControl1.Start = new System.DateTime(2025, 8, 18, 0, 0, 0, 0);
            this.schedulerControl1.TabIndex = 0;
            this.schedulerControl1.Text = "schedulerControl1";
            this.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler7);
            this.schedulerControl1.Views.FullWeekView.Enabled = true;
            this.schedulerControl1.Views.FullWeekView.TimeRulers.Add(timeRuler8);
            this.schedulerControl1.Views.MonthView.AppointmentDisplayOptions.AppointmentAutoHeight = true;
            this.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler9);
            this.schedulerControl1.Views.YearView.UseOptimizedScrolling = false;
            // 
            // schedulerDataStorage1
            // 
            // 
            // 
            // 
            this.schedulerDataStorage1.AppointmentDependencies.AutoReload = false;
            // 
            // 
            // 
            this.schedulerDataStorage1.Appointments.CustomFieldMappings.Add(new DevExpress.XtraScheduler.AppointmentCustomFieldMapping("CustomField1", "CustomField1"));
            this.schedulerDataStorage1.Appointments.DataSource = this.appointmentsBindingSource;
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(0, "None", "&None", System.Drawing.SystemColors.Window);
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(1, "Important", "&Important", System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(194)))), ((int)(((byte)(190))))));
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(2, "Business", "&Business", System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(213)))), ((int)(((byte)(255))))));
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(3, "Personal", "&Personal", System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(244)))), ((int)(((byte)(156))))));
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(4, "Vacation", "&Vacation", System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(228)))), ((int)(((byte)(199))))));
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(5, "Must Attend", "Must &Attend", System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(206)))), ((int)(((byte)(147))))));
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(6, "Travel Required", "&Travel Required", System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(244)))), ((int)(((byte)(255))))));
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(7, "Needs Preparation", "&Needs Preparation", System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(219)))), ((int)(((byte)(152))))));
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(8, "Birthday", "&Birthday", System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(207)))), ((int)(((byte)(233))))));
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(9, "Anniversary", "&Anniversary", System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(233)))), ((int)(((byte)(223))))));
            this.schedulerDataStorage1.Appointments.Labels.CreateNewLabel(10, "Phone Call", "Phone &Call", System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(165))))));
            this.schedulerDataStorage1.Appointments.Mappings.AllDay = "AllDay";
            this.schedulerDataStorage1.Appointments.Mappings.Description = "Description";
            this.schedulerDataStorage1.Appointments.Mappings.End = "EndDate";
            this.schedulerDataStorage1.Appointments.Mappings.Label = "Label";
            this.schedulerDataStorage1.Appointments.Mappings.Location = "Location";
            this.schedulerDataStorage1.Appointments.Mappings.OriginalOccurrenceEnd = "QueryEndDate";
            this.schedulerDataStorage1.Appointments.Mappings.OriginalOccurrenceStart = "QueryStartDate";
            this.schedulerDataStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo";
            this.schedulerDataStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo";
            this.schedulerDataStorage1.Appointments.Mappings.ResourceId = "ResourceID";
            this.schedulerDataStorage1.Appointments.Mappings.Start = "StartDate";
            this.schedulerDataStorage1.Appointments.Mappings.Status = "Status";
            this.schedulerDataStorage1.Appointments.Mappings.Subject = "Subject";
            this.schedulerDataStorage1.Appointments.Mappings.TimeZoneId = "TimeZoneId";
            this.schedulerDataStorage1.Appointments.Mappings.Type = "Type";
            // 
            // 
            // 
            this.schedulerDataStorage1.Resources.CustomFieldMappings.Add(new DevExpress.XtraScheduler.ResourceCustomFieldMapping("CustomField1", "CustomField1"));
            this.schedulerDataStorage1.Resources.DataSource = this.resourcesBindingSource;
            this.schedulerDataStorage1.Resources.Mappings.Caption = "ResourceName";
            this.schedulerDataStorage1.Resources.Mappings.Color = "Color";
            this.schedulerDataStorage1.Resources.Mappings.Id = "ResourceID";
            this.schedulerDataStorage1.Resources.Mappings.Image = "Image";
            this.schedulerDataStorage1.Resources.Mappings.ParentId = "UniqueID";
            // 
            // appointmentsBindingSource
            // 
            this.appointmentsBindingSource.DataMember = "Appointments";
            this.appointmentsBindingSource.DataSource = this.rABBIT_RIDESHAREDataSet;
            // 
            // rABBIT_RIDESHAREDataSet
            // 
            this.rABBIT_RIDESHAREDataSet.DataSetName = "RABBIT_RIDESHAREDataSet";
            this.rABBIT_RIDESHAREDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // resourcesBindingSource
            // 
            this.resourcesBindingSource.DataMember = "Resources";
            this.resourcesBindingSource.DataSource = this.rABBIT_RIDESHAREDataSet;
            // 
            // appointmentsTableAdapter
            // 
            this.appointmentsTableAdapter.ClearBeforeFill = true;
            // 
            // resourcesTableAdapter
            // 
            this.resourcesTableAdapter.ClearBeforeFill = true;
            // 
            // Bills
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.schedulerControl1);
            this.Name = "Bills";
            this.Size = new System.Drawing.Size(971, 488);
            this.ParentChanged += new System.EventHandler(this.Bills_ParentChanged);
            ((System.ComponentModel.ISupportInitialize)(this.schedulerControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerDataStorage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rABBIT_RIDESHAREDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resourcesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraScheduler.SchedulerControl schedulerControl1;
        private DevExpress.XtraScheduler.SchedulerDataStorage schedulerDataStorage1;
        private System.Windows.Forms.BindingSource appointmentsBindingSource;
        private RABBIT_RIDESHAREDataSet rABBIT_RIDESHAREDataSet;
        private System.Windows.Forms.BindingSource resourcesBindingSource;
        private RABBIT_RIDESHAREDataSetTableAdapters.AppointmentsTableAdapter appointmentsTableAdapter;
        private RABBIT_RIDESHAREDataSetTableAdapters.ResourcesTableAdapter resourcesTableAdapter;
    }
}
