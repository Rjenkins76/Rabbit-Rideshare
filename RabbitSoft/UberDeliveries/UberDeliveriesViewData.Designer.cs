namespace RabbitSoft
{
    partial class UberDeliveriesViewData
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
            DevExpress.DataAccess.Sql.SelectQuery selectQuery1 = new DevExpress.DataAccess.Sql.SelectQuery();
            DevExpress.DataAccess.Sql.Column column1 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression1 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Table table1 = new DevExpress.DataAccess.Sql.Table();
            DevExpress.DataAccess.Sql.Column column2 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression2 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column3 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression3 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column4 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression4 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column5 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression5 = new DevExpress.DataAccess.Sql.ColumnExpression();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UberDeliveriesViewData));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTRIP_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTAL_FARE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTAL_TIPS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTAL_COLLECTED = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gridControl1.DataMember = "Trip_Activity";
            this.gridControl1.DataSource = this.sqlDataSource1;
            this.gridControl1.Location = new System.Drawing.Point(10, 10);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(680, 720);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "RabbitSoft.Properties.Settings.RABBIT_RIDESHAREConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            columnExpression1.ColumnName = "TRIP_ID";
            table1.MetaSerializable = "<Meta X=\"30\" Y=\"30\" Width=\"125\" Height=\"225\" />";
            table1.Name = "Trip_Activity";
            columnExpression1.Table = table1;
            column1.Expression = columnExpression1;
            columnExpression2.ColumnName = "DATE";
            columnExpression2.Table = table1;
            column2.Expression = columnExpression2;
            columnExpression3.ColumnName = "TOTAL_FARE";
            columnExpression3.Table = table1;
            column3.Expression = columnExpression3;
            columnExpression4.ColumnName = "TOTAL_TIPS";
            columnExpression4.Table = table1;
            column4.Expression = columnExpression4;
            columnExpression5.ColumnName = "TOTAL_COLLECTED";
            columnExpression5.Table = table1;
            column5.Expression = columnExpression5;
            selectQuery1.Columns.Add(column1);
            selectQuery1.Columns.Add(column2);
            selectQuery1.Columns.Add(column3);
            selectQuery1.Columns.Add(column4);
            selectQuery1.Columns.Add(column5);
            selectQuery1.Name = "Trip_Activity";
            selectQuery1.Tables.Add(table1);
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            selectQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Orange;
            this.gridView1.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gridView1.ColumnPanelRowHeight = 60;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTRIP_ID,
            this.colDATE,
            this.colTOTAL_FARE,
            this.colTOTAL_TIPS,
            this.colTOTAL_COLLECTED});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_COLLECTED", this.colTOTAL_COLLECTED, "${0:0.00}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_FARE", this.colTOTAL_FARE, "${0:0.00}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_TIPS", this.colTOTAL_TIPS, "${0:0.00}")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDATE, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colTRIP_ID
            // 
            this.colTRIP_ID.AppearanceHeader.Options.UseTextOptions = true;
            this.colTRIP_ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTRIP_ID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTRIP_ID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTRIP_ID.FieldName = "TRIP_ID";
            this.colTRIP_ID.Name = "colTRIP_ID";
            this.colTRIP_ID.Visible = true;
            this.colTRIP_ID.VisibleIndex = 0;
            this.colTRIP_ID.Width = 431;
            // 
            // colDATE
            // 
            this.colDATE.AppearanceHeader.Options.UseTextOptions = true;
            this.colDATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDATE.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDATE.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDATE.FieldName = "DATE";
            this.colDATE.Name = "colDATE";
            this.colDATE.Visible = true;
            this.colDATE.VisibleIndex = 1;
            // 
            // colTOTAL_FARE
            // 
            this.colTOTAL_FARE.AppearanceHeader.Options.UseTextOptions = true;
            this.colTOTAL_FARE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTOTAL_FARE.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTOTAL_FARE.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTOTAL_FARE.Caption = "TOTAL FARE";
            this.colTOTAL_FARE.DisplayFormat.FormatString = "${0:0.00}";
            this.colTOTAL_FARE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTAL_FARE.FieldName = "TOTAL_FARE";
            this.colTOTAL_FARE.Name = "colTOTAL_FARE";
            this.colTOTAL_FARE.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_FARE", "${0:0.00}")});
            this.colTOTAL_FARE.Visible = true;
            this.colTOTAL_FARE.VisibleIndex = 1;
            this.colTOTAL_FARE.Width = 78;
            // 
            // colTOTAL_TIPS
            // 
            this.colTOTAL_TIPS.AppearanceHeader.Options.UseTextOptions = true;
            this.colTOTAL_TIPS.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTOTAL_TIPS.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTOTAL_TIPS.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTOTAL_TIPS.Caption = "TOTAL TIPS";
            this.colTOTAL_TIPS.DisplayFormat.FormatString = "${0:0.00}";
            this.colTOTAL_TIPS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTAL_TIPS.FieldName = "TOTAL_TIPS";
            this.colTOTAL_TIPS.Name = "colTOTAL_TIPS";
            this.colTOTAL_TIPS.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_TIPS", "${0:0.00}")});
            this.colTOTAL_TIPS.Visible = true;
            this.colTOTAL_TIPS.VisibleIndex = 2;
            this.colTOTAL_TIPS.Width = 76;
            // 
            // colTOTAL_COLLECTED
            // 
            this.colTOTAL_COLLECTED.AppearanceHeader.Options.UseTextOptions = true;
            this.colTOTAL_COLLECTED.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTOTAL_COLLECTED.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTOTAL_COLLECTED.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTOTAL_COLLECTED.Caption = "TOTAL COLLECTED";
            this.colTOTAL_COLLECTED.DisplayFormat.FormatString = "${0:0.00}";
            this.colTOTAL_COLLECTED.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTAL_COLLECTED.FieldName = "TOTAL_COLLECTED";
            this.colTOTAL_COLLECTED.Name = "colTOTAL_COLLECTED";
            this.colTOTAL_COLLECTED.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_COLLECTED", "${0:0.00}")});
            this.colTOTAL_COLLECTED.Visible = true;
            this.colTOTAL_COLLECTED.VisibleIndex = 3;
            this.colTOTAL_COLLECTED.Width = 95;
            // 
            // UberDeliveriesViewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "UberDeliveriesViewData";
            this.Size = new System.Drawing.Size(704, 745);
            this.Load += new System.EventHandler(this.UberDeliveriesViewData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colTRIP_ID;
        private DevExpress.XtraGrid.Columns.GridColumn colDATE;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTAL_FARE;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTAL_TIPS;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTAL_COLLECTED;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    }
}
