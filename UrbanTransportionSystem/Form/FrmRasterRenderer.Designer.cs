
namespace UrbanTransportionSystem
{
    partial class FrmRasterRenderer
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
            this.xtraRenderer = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbClassifyMethod = new System.Windows.Forms.ComboBox();
            this.nudClassCount = new System.Windows.Forms.NumericUpDown();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.colorComboBox1 = new System.Windows.Forms.ComboBox();
            this.xtraStretch = new DevExpress.XtraTab.XtraTabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbRasterBands = new System.Windows.Forms.ComboBox();
            this.colorComboBox = new System.Windows.Forms.ComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraRenderer)).BeginInit();
            this.xtraRenderer.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClassCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.xtraStretch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraRenderer
            // 
            this.xtraRenderer.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.xtraRenderer.Appearance.Options.UseForeColor = true;
            this.xtraRenderer.Location = new System.Drawing.Point(0, 0);
            this.xtraRenderer.Name = "xtraRenderer";
            this.xtraRenderer.SelectedTabPage = this.xtraTabPage2;
            this.xtraRenderer.Size = new System.Drawing.Size(438, 165);
            this.xtraRenderer.TabIndex = 0;
            this.xtraRenderer.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraStretch,
            this.xtraTabPage2});
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.label2);
            this.xtraTabPage2.Controls.Add(this.label4);
            this.xtraTabPage2.Controls.Add(this.label5);
            this.xtraTabPage2.Controls.Add(this.cmbClassifyMethod);
            this.xtraTabPage2.Controls.Add(this.nudClassCount);
            this.xtraTabPage2.Controls.Add(this.pictureBox2);
            this.xtraTabPage2.Controls.Add(this.colorComboBox1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(436, 133);
            this.xtraTabPage2.Text = "分级渲染";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 18);
            this.label2.TabIndex = 25;
            this.label2.Text = "分级方法：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 99);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 24;
            this.label4.Text = "选择颜色带：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 18);
            this.label5.TabIndex = 23;
            this.label5.Text = "分级数量：";
            // 
            // cmbClassifyMethod
            // 
            this.cmbClassifyMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClassifyMethod.FormattingEnabled = true;
            this.cmbClassifyMethod.Location = new System.Drawing.Point(122, 15);
            this.cmbClassifyMethod.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClassifyMethod.Name = "cmbClassifyMethod";
            this.cmbClassifyMethod.Size = new System.Drawing.Size(132, 26);
            this.cmbClassifyMethod.TabIndex = 22;
            // 
            // nudClassCount
            // 
            this.nudClassCount.Location = new System.Drawing.Point(122, 50);
            this.nudClassCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudClassCount.Name = "nudClassCount";
            this.nudClassCount.Size = new System.Drawing.Size(133, 26);
            this.nudClassCount.TabIndex = 21;
            this.nudClassCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Location = new System.Drawing.Point(122, 87);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(257, 38);
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            // 
            // colorComboBox1
            // 
            this.colorComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorComboBox1.FormattingEnabled = true;
            this.colorComboBox1.ItemHeight = 22;
            this.colorComboBox1.Location = new System.Drawing.Point(122, 89);
            this.colorComboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.colorComboBox1.Name = "colorComboBox1";
            this.colorComboBox1.Size = new System.Drawing.Size(279, 28);
            this.colorComboBox1.TabIndex = 19;
            this.colorComboBox1.SelectedIndexChanged += new System.EventHandler(this.colorComboBox1_SelectedIndexChanged);
            // 
            // xtraStretch
            // 
            this.xtraStretch.Controls.Add(this.label1);
            this.xtraStretch.Controls.Add(this.label3);
            this.xtraStretch.Controls.Add(this.pictureBox1);
            this.xtraStretch.Controls.Add(this.cmbRasterBands);
            this.xtraStretch.Controls.Add(this.colorComboBox);
            this.xtraStretch.Name = "xtraStretch";
            this.xtraStretch.Size = new System.Drawing.Size(436, 133);
            this.xtraStretch.Text = "拉伸渲染";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "选择颜色带：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "选择波段：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(111, 78);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(257, 38);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // cmbRasterBands
            // 
            this.cmbRasterBands.FormattingEnabled = true;
            this.cmbRasterBands.Location = new System.Drawing.Point(111, 25);
            this.cmbRasterBands.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRasterBands.Name = "cmbRasterBands";
            this.cmbRasterBands.Size = new System.Drawing.Size(160, 26);
            this.cmbRasterBands.TabIndex = 5;
            // 
            // colorComboBox
            // 
            this.colorComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorComboBox.FormattingEnabled = true;
            this.colorComboBox.ItemHeight = 22;
            this.colorComboBox.Location = new System.Drawing.Point(111, 78);
            this.colorComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.colorComboBox.Name = "colorComboBox";
            this.colorComboBox.Size = new System.Drawing.Size(279, 28);
            this.colorComboBox.TabIndex = 10;
            this.colorComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.colorComboBox_DrawItem);
            this.colorComboBox.SelectedIndexChanged += new System.EventHandler(this.colorComboBox_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(66, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 29);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(285, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 29);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmRasterRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 211);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.xtraRenderer);
            this.Name = "FrmRasterRenderer";
            this.Text = "FrmRasterRenderer";
            this.Load += new System.EventHandler(this.FrmRasterRenderer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraRenderer)).EndInit();
            this.xtraRenderer.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClassCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.xtraStretch.ResumeLayout(false);
            this.xtraStretch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraRenderer;
        private DevExpress.XtraTab.XtraTabPage xtraStretch;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbRasterBands;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbClassifyMethod;
        private System.Windows.Forms.NumericUpDown nudClassCount;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox colorComboBox1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.ComboBox colorComboBox;
    }
}