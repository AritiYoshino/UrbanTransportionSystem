
namespace UrbanTransportionSystem
{
    partial class FrmNetwork
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNetwork));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnLoadNetWork = new DevExpress.XtraBars.BarButtonItem();
            this.btnLocation = new DevExpress.XtraBars.BarButtonItem();
            this.btnClear = new DevExpress.XtraBars.BarButtonItem();
            this.btnAnalyse = new DevExpress.XtraBars.BarButtonItem();
            this.btnOutput = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.listCoordinate = new DevExpress.XtraEditors.ListBoxControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.listInfoNavigation = new DevExpress.XtraEditors.ListBoxControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listCoordinate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listInfoNavigation)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnClear,
            this.btnAnalyse,
            this.btnOutput,
            this.btnLocation,
            this.btnLoadNetWork});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 6;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnLoadNetWork, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnLocation, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnClear, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnAnalyse, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnOutput, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnLoadNetWork
            // 
            this.btnLoadNetWork.Caption = "载入网络数据集";
            this.btnLoadNetWork.Id = 5;
            this.btnLoadNetWork.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadNetWork.ImageOptions.Image")));
            this.btnLoadNetWork.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnLoadNetWork.ImageOptions.LargeImage")));
            this.btnLoadNetWork.Name = "btnLoadNetWork";
            this.btnLoadNetWork.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLoadNetWork_ItemClick);
            // 
            // btnLocation
            // 
            this.btnLocation.Caption = "定位";
            this.btnLocation.Id = 4;
            this.btnLocation.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLocation.ImageOptions.Image")));
            this.btnLocation.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnLocation.ImageOptions.LargeImage")));
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLocation_ItemClick);
            // 
            // btnClear
            // 
            this.btnClear.Caption = "清除";
            this.btnClear.Id = 1;
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.Image")));
            this.btnClear.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.LargeImage")));
            this.btnClear.Name = "btnClear";
            this.btnClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClearItem_ItemClick);
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Caption = "路径分析";
            this.btnAnalyse.Id = 2;
            this.btnAnalyse.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAnalyse.ImageOptions.Image")));
            this.btnAnalyse.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAnalyse.ImageOptions.LargeImage")));
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAnalyse_ItemClick);
            // 
            // btnOutput
            // 
            this.btnOutput.Caption = "导出路径";
            this.btnOutput.Id = 3;
            this.btnOutput.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOutput.ImageOptions.Image")));
            this.btnOutput.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnOutput.ImageOptions.LargeImage")));
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOutput_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(828, 30);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 382);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(828, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 30);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 352);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(828, 30);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 352);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.listCoordinate);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(828, 164);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "坐标信息";
            // 
            // listCoordinate
            // 
            this.listCoordinate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCoordinate.Location = new System.Drawing.Point(2, 28);
            this.listCoordinate.Name = "listCoordinate";
            this.listCoordinate.Size = new System.Drawing.Size(824, 134);
            this.listCoordinate.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 30);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(828, 352);
            this.splitContainerControl1.SplitterPosition = 164;
            this.splitContainerControl1.TabIndex = 7;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.listInfoNavigation);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(828, 176);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "导航信息";
            // 
            // listInfoNavigation
            // 
            this.listInfoNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listInfoNavigation.Location = new System.Drawing.Point(2, 28);
            this.listInfoNavigation.Name = "listInfoNavigation";
            this.listInfoNavigation.Size = new System.Drawing.Size(824, 146);
            this.listInfoNavigation.TabIndex = 0;
            // 
            // FrmNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 382);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNetwork";
            this.Text = "路径规划";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNetwork_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNetwork_FormClosed);
            this.Load += new System.EventHandler(this.FrmNetWork_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listCoordinate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listInfoNavigation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnClear;
        private DevExpress.XtraBars.BarButtonItem btnAnalyse;
        private DevExpress.XtraBars.BarButtonItem btnOutput;
        private DevExpress.XtraBars.BarButtonItem btnLocation;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ListBoxControl listCoordinate;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.ListBoxControl listInfoNavigation;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraBars.BarButtonItem btnLoadNetWork;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}