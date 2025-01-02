
namespace UrbanTransportionSystem
{
    partial class FrmPropFind
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.listBoxFields = new DevExpress.XtraEditors.ListBoxControl();
            this.listBoxValues = new DevExpress.XtraEditors.ListBoxControl();
            this.textBoxWhereClause = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnInputEqual = new DevExpress.XtraEditors.SimpleButton();
            this.btnInputNeqval = new DevExpress.XtraEditors.SimpleButton();
            this.btnLike = new DevExpress.XtraEditors.SimpleButton();
            this.btnGreaterThan = new DevExpress.XtraEditors.SimpleButton();
            this.btnGreaterEqual = new DevExpress.XtraEditors.SimpleButton();
            this.btnAND = new DevExpress.XtraEditors.SimpleButton();
            this.btnModulo = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuotient = new DevExpress.XtraEditors.SimpleButton();
            this.btnNot = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetValue = new DevExpress.XtraEditors.SimpleButton();
            this.btnLeftBracket = new DevExpress.XtraEditors.SimpleButton();
            this.btnIS = new DevExpress.XtraEditors.SimpleButton();
            this.btnLessThan = new DevExpress.XtraEditors.SimpleButton();
            this.btnLessEqual = new DevExpress.XtraEditors.SimpleButton();
            this.btnOR = new DevExpress.XtraEditors.SimpleButton();
            this.checkValue = new DevExpress.XtraEditors.CheckEdit();
            this.btnRightBracket = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cbLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxWhereClause.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(78, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(100, 24);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "图层名称：";
            // 
            // cbLayer
            // 
            this.cbLayer.Location = new System.Drawing.Point(209, 35);
            this.cbLayer.Name = "cbLayer";
            this.cbLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbLayer.Size = new System.Drawing.Size(495, 24);
            this.cbLayer.TabIndex = 1;
            this.cbLayer.SelectedIndexChanged += new System.EventHandler(this.cbLayer_SelectedIndexChanged);
            // 
            // listBoxFields
            // 
            this.listBoxFields.Location = new System.Drawing.Point(46, 128);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(199, 252);
            this.listBoxFields.TabIndex = 3;
            this.listBoxFields.DoubleClick += new System.EventHandler(this.listBoxField_DoubleClick);
            // 
            // listBoxValues
            // 
            this.listBoxValues.Location = new System.Drawing.Point(595, 122);
            this.listBoxValues.Name = "listBoxValues";
            this.listBoxValues.Size = new System.Drawing.Size(199, 252);
            this.listBoxValues.TabIndex = 4;
            this.listBoxValues.DoubleClick += new System.EventHandler(this.listBoxValues_DoubleClick);
            // 
            // textBoxWhereClause
            // 
            this.textBoxWhereClause.Location = new System.Drawing.Point(37, 426);
            this.textBoxWhereClause.Name = "textBoxWhereClause";
            this.textBoxWhereClause.Size = new System.Drawing.Size(757, 24);
            this.textBoxWhereClause.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(46, 98);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(100, 24);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "属性字段：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(595, 92);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(80, 24);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "属性值：";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(46, 396);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(174, 24);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Select*From Where";
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.Location = new System.Drawing.Point(64, 469);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 29);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(357, 469);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 29);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(623, 469);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 29);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnInputEqual
            // 
            this.btnInputEqual.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnInputEqual.Appearance.Options.UseFont = true;
            this.btnInputEqual.Location = new System.Drawing.Point(274, 143);
            this.btnInputEqual.Name = "btnInputEqual";
            this.btnInputEqual.Size = new System.Drawing.Size(63, 29);
            this.btnInputEqual.TabIndex = 14;
            this.btnInputEqual.Text = "=";
            this.btnInputEqual.Click += new System.EventHandler(this.btnInputEqual_Click);
            // 
            // btnInputNeqval
            // 
            this.btnInputNeqval.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnInputNeqval.Appearance.Options.UseFont = true;
            this.btnInputNeqval.Location = new System.Drawing.Point(388, 143);
            this.btnInputNeqval.Name = "btnInputNeqval";
            this.btnInputNeqval.Size = new System.Drawing.Size(63, 29);
            this.btnInputNeqval.TabIndex = 15;
            this.btnInputNeqval.Text = "<>";
            this.btnInputNeqval.Click += new System.EventHandler(this.btnInputNeqval_Click);
            // 
            // btnLike
            // 
            this.btnLike.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnLike.Appearance.Options.UseFont = true;
            this.btnLike.Location = new System.Drawing.Point(503, 143);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(63, 29);
            this.btnLike.TabIndex = 16;
            this.btnLike.Text = "Like";
            this.btnLike.Click += new System.EventHandler(this.btnLike_Click);
            // 
            // btnGreaterThan
            // 
            this.btnGreaterThan.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGreaterThan.Appearance.Options.UseFont = true;
            this.btnGreaterThan.Location = new System.Drawing.Point(274, 189);
            this.btnGreaterThan.Name = "btnGreaterThan";
            this.btnGreaterThan.Size = new System.Drawing.Size(63, 29);
            this.btnGreaterThan.TabIndex = 17;
            this.btnGreaterThan.Text = ">";
            this.btnGreaterThan.Click += new System.EventHandler(this.btnGreaterThan_Click);
            // 
            // btnGreaterEqual
            // 
            this.btnGreaterEqual.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGreaterEqual.Appearance.Options.UseFont = true;
            this.btnGreaterEqual.Location = new System.Drawing.Point(388, 189);
            this.btnGreaterEqual.Name = "btnGreaterEqual";
            this.btnGreaterEqual.Size = new System.Drawing.Size(63, 29);
            this.btnGreaterEqual.TabIndex = 18;
            this.btnGreaterEqual.Text = "≥";
            this.btnGreaterEqual.Click += new System.EventHandler(this.btnGreaterEqual_Click);
            // 
            // btnAND
            // 
            this.btnAND.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnAND.Appearance.Options.UseFont = true;
            this.btnAND.Location = new System.Drawing.Point(503, 189);
            this.btnAND.Name = "btnAND";
            this.btnAND.Size = new System.Drawing.Size(63, 29);
            this.btnAND.TabIndex = 19;
            this.btnAND.Text = "AND";
            this.btnAND.Click += new System.EventHandler(this.btnAND_Click);
            // 
            // btnModulo
            // 
            this.btnModulo.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnModulo.Appearance.Options.UseFont = true;
            this.btnModulo.Location = new System.Drawing.Point(275, 280);
            this.btnModulo.Name = "btnModulo";
            this.btnModulo.Size = new System.Drawing.Size(28, 29);
            this.btnModulo.TabIndex = 20;
            this.btnModulo.Text = "%";
            this.btnModulo.Click += new System.EventHandler(this.btnModulo_Click);
            // 
            // btnQuotient
            // 
            this.btnQuotient.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnQuotient.Appearance.Options.UseFont = true;
            this.btnQuotient.Location = new System.Drawing.Point(309, 280);
            this.btnQuotient.Name = "btnQuotient";
            this.btnQuotient.Size = new System.Drawing.Size(28, 29);
            this.btnQuotient.TabIndex = 21;
            this.btnQuotient.Text = "/";
            this.btnQuotient.Click += new System.EventHandler(this.btnQuotient_Click);
            // 
            // btnNot
            // 
            this.btnNot.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnNot.Appearance.Options.UseFont = true;
            this.btnNot.Location = new System.Drawing.Point(503, 280);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new System.Drawing.Size(63, 29);
            this.btnNot.TabIndex = 22;
            this.btnNot.Text = "Not";
            this.btnNot.Click += new System.EventHandler(this.btnNot_Click);
            // 
            // btnGetValue
            // 
            this.btnGetValue.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGetValue.Appearance.Options.UseFont = true;
            this.btnGetValue.Location = new System.Drawing.Point(376, 331);
            this.btnGetValue.Name = "btnGetValue";
            this.btnGetValue.Size = new System.Drawing.Size(190, 29);
            this.btnGetValue.TabIndex = 23;
            this.btnGetValue.Text = "获得属性值";
            this.btnGetValue.Click += new System.EventHandler(this.btnGetValue_Click);
            // 
            // btnLeftBracket
            // 
            this.btnLeftBracket.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnLeftBracket.Appearance.Options.UseFont = true;
            this.btnLeftBracket.Location = new System.Drawing.Point(388, 280);
            this.btnLeftBracket.Name = "btnLeftBracket";
            this.btnLeftBracket.Size = new System.Drawing.Size(28, 29);
            this.btnLeftBracket.TabIndex = 24;
            this.btnLeftBracket.Text = "(";
            this.btnLeftBracket.Click += new System.EventHandler(this.btnLeftBracket_Click);
            // 
            // btnIS
            // 
            this.btnIS.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnIS.Appearance.Options.UseFont = true;
            this.btnIS.Location = new System.Drawing.Point(274, 331);
            this.btnIS.Name = "btnIS";
            this.btnIS.Size = new System.Drawing.Size(63, 29);
            this.btnIS.TabIndex = 25;
            this.btnIS.Text = "IS";
            this.btnIS.Click += new System.EventHandler(this.btnIS_Click);
            // 
            // btnLessThan
            // 
            this.btnLessThan.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnLessThan.Appearance.Options.UseFont = true;
            this.btnLessThan.Location = new System.Drawing.Point(275, 233);
            this.btnLessThan.Name = "btnLessThan";
            this.btnLessThan.Size = new System.Drawing.Size(63, 29);
            this.btnLessThan.TabIndex = 26;
            this.btnLessThan.Text = "<";
            this.btnLessThan.Click += new System.EventHandler(this.btnLessThan_Click);
            // 
            // btnLessEqual
            // 
            this.btnLessEqual.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnLessEqual.Appearance.Options.UseFont = true;
            this.btnLessEqual.Location = new System.Drawing.Point(388, 233);
            this.btnLessEqual.Name = "btnLessEqual";
            this.btnLessEqual.Size = new System.Drawing.Size(63, 29);
            this.btnLessEqual.TabIndex = 27;
            this.btnLessEqual.Text = "≤";
            this.btnLessEqual.Click += new System.EventHandler(this.btnLessEqual_Click);
            // 
            // btnOR
            // 
            this.btnOR.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnOR.Appearance.Options.UseFont = true;
            this.btnOR.Location = new System.Drawing.Point(503, 233);
            this.btnOR.Name = "btnOR";
            this.btnOR.Size = new System.Drawing.Size(63, 29);
            this.btnOR.TabIndex = 28;
            this.btnOR.Text = "OR";
            this.btnOR.Click += new System.EventHandler(this.btnOR_Click);
            // 
            // checkValue
            // 
            this.checkValue.Location = new System.Drawing.Point(604, 392);
            this.checkValue.Name = "checkValue";
            this.checkValue.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.checkValue.Properties.Appearance.Options.UseFont = true;
            this.checkValue.Properties.Caption = "去掉重复的属性值";
            this.checkValue.Size = new System.Drawing.Size(199, 28);
            this.checkValue.TabIndex = 6;
            this.checkValue.CheckedChanged += new System.EventHandler(this.checkValue_CheckedChanged);
            // 
            // btnRightBracket
            // 
            this.btnRightBracket.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnRightBracket.Appearance.Options.UseFont = true;
            this.btnRightBracket.Location = new System.Drawing.Point(423, 280);
            this.btnRightBracket.Name = "btnRightBracket";
            this.btnRightBracket.Size = new System.Drawing.Size(28, 29);
            this.btnRightBracket.TabIndex = 29;
            this.btnRightBracket.Text = ")";
            this.btnRightBracket.Click += new System.EventHandler(this.btnRightBracket_Click);
            // 
            // FrmPropFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 522);
            this.Controls.Add(this.btnRightBracket);
            this.Controls.Add(this.btnOR);
            this.Controls.Add(this.btnLessEqual);
            this.Controls.Add(this.btnLessThan);
            this.Controls.Add(this.btnIS);
            this.Controls.Add(this.btnLeftBracket);
            this.Controls.Add(this.btnGetValue);
            this.Controls.Add(this.btnNot);
            this.Controls.Add(this.btnQuotient);
            this.Controls.Add(this.btnModulo);
            this.Controls.Add(this.btnAND);
            this.Controls.Add(this.btnGreaterEqual);
            this.Controls.Add(this.btnGreaterThan);
            this.Controls.Add(this.btnLike);
            this.Controls.Add(this.btnInputNeqval);
            this.Controls.Add(this.btnInputEqual);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.checkValue);
            this.Controls.Add(this.textBoxWhereClause);
            this.Controls.Add(this.listBoxValues);
            this.Controls.Add(this.listBoxFields);
            this.Controls.Add(this.cbLayer);
            this.Controls.Add(this.labelControl1);
            this.Name = "FrmPropFind";
            this.Text = "FrmPropFind";
            this.Load += new System.EventHandler(this.FrmCalculate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxWhereClause.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkValue.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbLayer;
        private DevExpress.XtraEditors.ListBoxControl listBoxFields;
        private DevExpress.XtraEditors.ListBoxControl listBoxValues;
        private DevExpress.XtraEditors.TextEdit textBoxWhereClause;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnInputEqual;
        private DevExpress.XtraEditors.SimpleButton btnInputNeqval;
        private DevExpress.XtraEditors.SimpleButton btnLike;
        private DevExpress.XtraEditors.SimpleButton btnGreaterThan;
        private DevExpress.XtraEditors.SimpleButton btnGreaterEqual;
        private DevExpress.XtraEditors.SimpleButton btnAND;
        private DevExpress.XtraEditors.SimpleButton btnModulo;
        private DevExpress.XtraEditors.SimpleButton btnQuotient;
        private DevExpress.XtraEditors.SimpleButton btnNot;
        private DevExpress.XtraEditors.SimpleButton btnGetValue;
        private DevExpress.XtraEditors.SimpleButton btnLeftBracket;
        private DevExpress.XtraEditors.SimpleButton btnIS;
        private DevExpress.XtraEditors.SimpleButton btnLessThan;
        private DevExpress.XtraEditors.SimpleButton btnLessEqual;
        private DevExpress.XtraEditors.SimpleButton btnOR;
        private DevExpress.XtraEditors.CheckEdit checkValue;
        private DevExpress.XtraEditors.SimpleButton btnRightBracket;
    }
}