
namespace UrbanTransportionSystem
{
    partial class FrmCreateFeatureClass
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
            this.group = new DevExpress.XtraEditors.GroupControl();
            this.rBtnLine = new System.Windows.Forms.RadioButton();
            this.rBtnPoint = new System.Windows.Forms.RadioButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl4 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnAddFields = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbxFieldType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtFiledName = new DevExpress.XtraEditors.TextEdit();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.txtFeatureClassName = new DevExpress.XtraEditors.TextEdit();
            this.gridFieldsTable = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.editPath = new DevExpress.XtraEditors.ButtonEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.group)).BeginInit();
            this.group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).BeginInit();
            this.splitContainerControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxFieldType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFiledName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFeatureClassName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFieldsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // group
            // 
            this.group.Controls.Add(this.rBtnLine);
            this.group.Controls.Add(this.rBtnPoint);
            this.group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.group.Location = new System.Drawing.Point(0, 0);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(121, 100);
            this.group.TabIndex = 0;
            this.group.Text = "类型";
            // 
            // rBtnLine
            // 
            this.rBtnLine.AutoSize = true;
            this.rBtnLine.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rBtnLine.Location = new System.Drawing.Point(20, 37);
            this.rBtnLine.Name = "rBtnLine";
            this.rBtnLine.Size = new System.Drawing.Size(59, 22);
            this.rBtnLine.TabIndex = 2;
            this.rBtnLine.TabStop = true;
            this.rBtnLine.Text = "线路";
            this.rBtnLine.UseVisualStyleBackColor = true;
            // 
            // rBtnPoint
            // 
            this.rBtnPoint.AutoSize = true;
            this.rBtnPoint.Location = new System.Drawing.Point(20, 65);
            this.rBtnPoint.Name = "rBtnPoint";
            this.rBtnPoint.Size = new System.Drawing.Size(59, 22);
            this.rBtnPoint.TabIndex = 3;
            this.rBtnPoint.TabStop = true;
            this.rBtnPoint.Text = "站点";
            this.rBtnPoint.UseVisualStyleBackColor = true;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.group);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl4);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1035, 100);
            this.splitContainerControl1.SplitterPosition = 121;
            this.splitContainerControl1.TabIndex = 4;
            // 
            // splitContainerControl4
            // 
            this.splitContainerControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl4.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl4.Name = "splitContainerControl4";
            this.splitContainerControl4.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl4.Panel1.Text = "Panel1";
            this.splitContainerControl4.Panel2.Controls.Add(this.groupControl4);
            this.splitContainerControl4.Panel2.Text = "Panel2";
            this.splitContainerControl4.Size = new System.Drawing.Size(902, 100);
            this.splitContainerControl4.SplitterPosition = 652;
            this.splitContainerControl4.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnAddFields);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.cbxFieldType);
            this.groupControl1.Controls.Add(this.txtFiledName);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(652, 100);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "字段编辑器";
            // 
            // btnAddFields
            // 
            this.btnAddFields.Location = new System.Drawing.Point(509, 44);
            this.btnAddFields.Name = "btnAddFields";
            this.btnAddFields.Size = new System.Drawing.Size(136, 29);
            this.btnAddFields.TabIndex = 8;
            this.btnAddFields.Text = "添加字段";
            this.btnAddFields.Click += new System.EventHandler(this.btnAddFields_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(245, 48);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 21);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "字段类型：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(29, 48);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 21);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "字段名：";
            // 
            // cbxFieldType
            // 
            this.cbxFieldType.Location = new System.Drawing.Point(336, 45);
            this.cbxFieldType.Name = "cbxFieldType";
            this.cbxFieldType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cbxFieldType.Properties.Appearance.Options.UseFont = true;
            this.cbxFieldType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxFieldType.Properties.Items.AddRange(new object[] {
            "Int",
            "Double",
            "Text"});
            this.cbxFieldType.Size = new System.Drawing.Size(136, 28);
            this.cbxFieldType.TabIndex = 5;
            // 
            // txtFiledName
            // 
            this.txtFiledName.EditValue = "";
            this.txtFiledName.Location = new System.Drawing.Point(103, 47);
            this.txtFiledName.Name = "txtFiledName";
            this.txtFiledName.Size = new System.Drawing.Size(136, 24);
            this.txtFiledName.TabIndex = 0;
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.txtFeatureClassName);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl4.Location = new System.Drawing.Point(0, 0);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(238, 100);
            this.groupControl4.TabIndex = 0;
            this.groupControl4.Text = "要素名";
            // 
            // txtFeatureClassName
            // 
            this.txtFeatureClassName.Location = new System.Drawing.Point(19, 47);
            this.txtFeatureClassName.Name = "txtFeatureClassName";
            this.txtFeatureClassName.Size = new System.Drawing.Size(197, 24);
            this.txtFeatureClassName.TabIndex = 0;
            // 
            // gridFieldsTable
            // 
            this.gridFieldsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFieldsTable.Location = new System.Drawing.Point(2, 28);
            this.gridFieldsTable.MainView = this.gridView1;
            this.gridFieldsTable.Name = "gridFieldsTable";
            this.gridFieldsTable.Size = new System.Drawing.Size(1031, 246);
            this.gridFieldsTable.TabIndex = 5;
            this.gridFieldsTable.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridFieldsTable;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 100);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.splitContainerControl3);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1035, 354);
            this.splitContainerControl2.SplitterPosition = 276;
            this.splitContainerControl2.TabIndex = 6;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridFieldsTable);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1035, 276);
            this.groupControl2.TabIndex = 6;
            this.groupControl2.Text = "字段表";
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl3.Name = "splitContainerControl3";
            this.splitContainerControl3.Panel1.Controls.Add(this.groupControl3);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            this.splitContainerControl3.Panel2.Controls.Add(this.btnOK);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(1035, 66);
            this.splitContainerControl3.SplitterPosition = 871;
            this.splitContainerControl3.TabIndex = 0;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.editPath);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(871, 66);
            this.groupControl3.TabIndex = 1;
            this.groupControl3.Text = "保存路径";
            // 
            // editPath
            // 
            this.editPath.Location = new System.Drawing.Point(20, 33);
            this.editPath.Name = "editPath";
            this.editPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.editPath.Size = new System.Drawing.Size(846, 24);
            this.editPath.TabIndex = 0;
            
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(4, 14);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(126, 40);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = " 确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmCreateFeatureClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 454);
            this.Controls.Add(this.splitContainerControl2);
            this.Controls.Add(this.splitContainerControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmCreateFeatureClass";
            this.Text = "FrmCreateFeatureClass";
            this.Load += new System.EventHandler(this.FrmCreateFeatureClass_Load);
            ((System.ComponentModel.ISupportInitialize)(this.group)).EndInit();
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).EndInit();
            this.splitContainerControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxFieldType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFiledName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFeatureClassName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFieldsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editPath.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl group;
        private System.Windows.Forms.RadioButton rBtnLine;
        private System.Windows.Forms.RadioButton rBtnPoint;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtFiledName;
        private DevExpress.XtraEditors.ComboBoxEdit cbxFieldType;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAddFields;
        private DevExpress.XtraGrid.GridControl gridFieldsTable;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
        private DevExpress.XtraEditors.ButtonEdit editPath;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl4;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.TextEdit txtFeatureClassName;
    }
}