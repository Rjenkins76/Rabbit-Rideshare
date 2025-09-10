namespace RabbitSoft2
{
    partial class DoorDashViewData
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
            DevExpress.DataAccess.Sql.Column column6 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression6 = new DevExpress.DataAccess.Sql.ColumnExpression();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoorDashViewData));
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPICKUPLOCATION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBASEPAY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIPPAY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTALPAY = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "RABBIT_RIDESHAREConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            columnExpression1.ColumnName = "ID";
            table1.MetaSerializable = "<Meta X=\"30\" Y=\"30\" Width=\"125\" Height=\"185\" />";
            table1.Name = "Doordash_Trip_Activity";
            columnExpression1.Table = table1;
            column1.Expression = columnExpression1;
            columnExpression2.ColumnName = "DATE";
            columnExpression2.Table = table1;
            column2.Expression = columnExpression2;
            columnExpression3.ColumnName = "PICKUPLOCATION";
            columnExpression3.Table = table1;
            column3.Expression = columnExpression3;
            columnExpression4.ColumnName = "BASEPAY";
            columnExpression4.Table = table1;
            column4.Expression = columnExpression4;
            columnExpression5.ColumnName = "TIPPAY";
            columnExpression5.Table = table1;
            column5.Expression = columnExpression5;
            columnExpression6.ColumnName = "TOTALPAY";
            columnExpression6.Table = table1;
            column6.Expression = columnExpression6;
            selectQuery1.Columns.Add(column1);
            selectQuery1.Columns.Add(column2);
            selectQuery1.Columns.Add(column3);
            selectQuery1.Columns.Add(column4);
            selectQuery1.Columns.Add(column5);
            selectQuery1.Columns.Add(column6);
            selectQuery1.Name = "Doordash_Trip_Activity";
            selectQuery1.Tables.Add(table1);
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            selectQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gridControl1.DataMember = "Doordash_Trip_Activity";
            this.gridControl1.DataSource = this.sqlDataSource1;
            this.gridControl1.Location = new System.Drawing.Point(3, 3);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(710, 970);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colDATE,
            this.colPICKUPLOCATION,
            this.colBASEPAY,
            this.colTIPPAY,
            this.colTOTALPAY});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDATE, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colDATE
            // 
            this.colDATE.FieldName = "DATE";
            this.colDATE.Name = "colDATE";
            this.colDATE.Visible = true;
            this.colDATE.VisibleIndex = 0;
            // 
            // colPICKUPLOCATION
            // 
            this.colPICKUPLOCATION.FieldName = "PICKUPLOCATION";
            this.colPICKUPLOCATION.Name = "colPICKUPLOCATION";
            this.colPICKUPLOCATION.Visible = true;
            this.colPICKUPLOCATION.VisibleIndex = 0;
            // 
            // colBASEPAY
            // 
            this.colBASEPAY.DisplayFormat.FormatString = "c2";
            this.colBASEPAY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBASEPAY.FieldName = "BASEPAY";
            this.colBASEPAY.Name = "colBASEPAY";
            this.colBASEPAY.Visible = true;
            this.colBASEPAY.VisibleIndex = 1;
            // 
            // colTIPPAY
            // 
            this.colTIPPAY.DisplayFormat.FormatString = "c2";
            this.colTIPPAY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTIPPAY.FieldName = "TIPPAY";
            this.colTIPPAY.Name = "colTIPPAY";
            this.colTIPPAY.Visible = true;
            this.colTIPPAY.VisibleIndex = 2;
            // 
            // colTOTALPAY
            // 
            this.colTOTALPAY.DisplayFormat.FormatString = "c2";
            this.colTOTALPAY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTALPAY.FieldName = "TOTALPAY";
            this.colTOTALPAY.Name = "colTOTALPAY";
            this.colTOTALPAY.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTALPAY", "{0:c2}")});
            this.colTOTALPAY.Visible = true;
            this.colTOTALPAY.VisibleIndex = 3;
            // 
            // DoorDashViewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "DoorDashViewData";
            this.Size = new System.Drawing.Size(719, 976);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colDATE;
        private DevExpress.XtraGrid.Columns.GridColumn colPICKUPLOCATION;
        private DevExpress.XtraGrid.Columns.GridColumn colBASEPAY;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPPAY;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTALPAY;
    }
}
