using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Office.Interop.Excel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesGDB;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using structSet;

namespace UrbanTransportionSystem
{
    public partial class FrmCreateFeatureClass : System.Windows.Forms.Form
    {

        private System.Data.DataTable  dataTable;
        private IHookHelper m_hookHelper = null;
        private IMapControl3 m_mapControl;
        private List<FieldInfo> fieldInfos = new List<FieldInfo>();

        public esriGeometryType type
        {
            get
            {
                if (rBtnPoint.Checked)
                    return esriGeometryType.esriGeometryPoint;
                else if (rBtnLine.Checked)
                    return esriGeometryType.esriGeometryPolyline;
                else
                    return esriGeometryType.esriGeometryNull;
            }
        }
        public FrmCreateFeatureClass(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
            m_mapControl = (IMapControl3)m_hookHelper.Hook;
            InitializeComponent();
            editPath.ButtonClick += EditPath_ButtonClick;
        }

        private void EditPath_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            string defaultPath = @"D:\\成都市";
            folderBrowserDialog.SelectedPath = defaultPath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                editPath.Text = folderBrowserDialog.SelectedPath;
            }
        }


        private void btnAddFields_Click(object sender, EventArgs e)
        {
            string nameValue = txtFiledName.Text;
            string selectedItem = cbxFieldType.SelectedItem.ToString();
            if (nameValue == ""|| selectedItem == null) MessageBox.Show("请输入完整字段");
            DataRow newRow = dataTable.NewRow();
            newRow["FieldName"] = nameValue;
            newRow["FieldType"] = selectedItem;
            dataTable.Rows.Add(newRow);

            GridView gridView = gridView1;
            if (gridView == null)
            {
                MessageBox.Show("无法获取有效的GridView");
                return;
            }
            gridFieldsTable.DataSource = dataTable;
            gridFieldsTable.Refresh();
            txtFiledName.Text = "";
        }


        private void FrmCreateFeatureClass_Load(object sender, EventArgs e)
        {
            dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("FieldName", typeof(string));
            dataTable.Columns["FieldName"].Caption = "字段名称";
            dataTable.Columns.Add("FieldType", typeof(string));
            dataTable.Columns["FieldType"].Caption = "字段类型";
            gridFieldsTable.DataSource = dataTable;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (editPath.Text=="")
            {
                MessageBox.Show("请输入保存路径");
                return;
            }
            List<fieldInfo> additionalFields = new List<fieldInfo>();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    fieldInfo fieldInfo = new fieldInfo();
                    fieldInfo.FieldName = row["FieldName"].ToString();
                    fieldInfo.FieldType = row["FieldType"].ToString();
                    additionalFields.Add(fieldInfo);
                }
            }

            CmdCreateFeatureClass createFeatureClassCommand = new CmdCreateFeatureClass(editPath.Text, txtFeatureClassName.Text, type,ref additionalFields);
            createFeatureClassCommand.OnCreate(m_hookHelper);
            createFeatureClassCommand.OnClick(); 
        }


    }
}


