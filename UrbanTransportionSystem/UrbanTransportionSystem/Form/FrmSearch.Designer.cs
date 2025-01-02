
namespace UrbanTransportionSystem
{
    partial class FrmSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearch));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnSearchBUS = new DevExpress.XtraEditors.SimpleButton();
            this.searchBusStops = new DevExpress.XtraEditors.SearchControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.listBusStops = new DevExpress.XtraEditors.ListBoxControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.searchMetroStops = new DevExpress.XtraEditors.TextEdit();
            this.btnSearchMetro = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.listMetroStops = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchBusStops.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBusStops)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchMetroStops.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listMetroStops)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(433, 410);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(431, 378);
            this.xtraTabPage1.Text = "公交站点查询";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(431, 378);
            this.splitContainerControl1.SplitterPosition = 92;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnSearchBUS);
            this.groupControl1.Controls.Add(this.searchBusStops);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(431, 92);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "搜索框";
            // 
            // btnSearchBUS
            // 
            this.btnSearchBUS.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSearchBUS.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSearchBUS.Appearance.Options.UseFont = true;
            this.btnSearchBUS.Appearance.Options.UseForeColor = true;
            this.btnSearchBUS.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.btnSearchBUS.Location = new System.Drawing.Point(317, 39);
            this.btnSearchBUS.Name = "btnSearchBUS";
            this.btnSearchBUS.Size = new System.Drawing.Size(103, 34);
            this.btnSearchBUS.TabIndex = 1;
            this.btnSearchBUS.Text = "搜索";
            this.btnSearchBUS.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // searchBusStops
            // 
            this.searchBusStops.Location = new System.Drawing.Point(11, 47);
            this.searchBusStops.Name = "searchBusStops";
            this.searchBusStops.Properties.ShowClearButton = false;
            this.searchBusStops.Properties.ShowSearchButton = false;
            this.searchBusStops.Size = new System.Drawing.Size(281, 24);
            this.searchBusStops.TabIndex = 0;
            this.searchBusStops.EditValueChanged += new System.EventHandler(this.searchBusStops_EditValueChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.listBusStops);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(431, 274);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "公交站点列表";
            // 
            // listBusStops
            // 
            this.listBusStops.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBusStops.Location = new System.Drawing.Point(2, 28);
            this.listBusStops.Name = "listBusStops";
            this.listBusStops.Size = new System.Drawing.Size(427, 244);
            this.listBusStops.TabIndex = 0;
            this.listBusStops.DoubleClick += new System.EventHandler(this.listBusStops_DoubleClick);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.splitContainerControl2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(431, 378);
            this.xtraTabPage2.Text = "地铁站点查询";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControl3);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.groupControl4);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(431, 378);
            this.splitContainerControl2.SplitterPosition = 92;
            this.splitContainerControl2.TabIndex = 1;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.searchMetroStops);
            this.groupControl3.Controls.Add(this.btnSearchMetro);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(431, 92);
            this.groupControl3.TabIndex = 0;
            this.groupControl3.Text = "搜索框";
            // 
            // searchMetroStops
            // 
            this.searchMetroStops.Location = new System.Drawing.Point(11, 45);
            this.searchMetroStops.Name = "searchMetroStops";
            this.searchMetroStops.Size = new System.Drawing.Size(281, 24);
            this.searchMetroStops.TabIndex = 3;
            this.searchMetroStops.EditValueChanged += new System.EventHandler(this.searchMetro_EditValueChanged);
            // 
            // btnSearchMetro
            // 
            this.btnSearchMetro.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSearchMetro.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSearchMetro.Appearance.Options.UseFont = true;
            this.btnSearchMetro.Appearance.Options.UseForeColor = true;
            this.btnSearchMetro.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton2.ImageOptions.SvgImage")));
            this.btnSearchMetro.Location = new System.Drawing.Point(317, 37);
            this.btnSearchMetro.Name = "btnSearchMetro";
            this.btnSearchMetro.Size = new System.Drawing.Size(103, 34);
            this.btnSearchMetro.TabIndex = 2;
            this.btnSearchMetro.Text = "搜索";
            this.btnSearchMetro.Click += new System.EventHandler(this.btnSearchMetro_Click);
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.listMetroStops);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl4.Location = new System.Drawing.Point(0, 0);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(431, 274);
            this.groupControl4.TabIndex = 0;
            this.groupControl4.Text = "地铁站点列表";
            // 
            // listMetroStops
            // 
            this.listMetroStops.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMetroStops.Location = new System.Drawing.Point(2, 28);
            this.listMetroStops.Name = "listMetroStops";
            this.listMetroStops.Size = new System.Drawing.Size(427, 244);
            this.listMetroStops.TabIndex = 0;
            this.listMetroStops.DoubleClick += new System.EventHandler(this.listMetroStops_DoubleClick);
            // 
            // FrmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 410);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FrmSearch";
            this.Text = "FrmSearch";
            this.Load += new System.EventHandler(this.FrmSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchBusStops.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBusStops)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchMetroStops.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listMetroStops)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SearchControl searchBusStops;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.ListBoxControl listBusStops;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.ListBoxControl listMetroStops;
        private DevExpress.XtraEditors.SimpleButton btnSearchBUS;
        private DevExpress.XtraEditors.TextEdit searchMetroStops;
        private DevExpress.XtraEditors.SimpleButton btnSearchMetro;
    }
}