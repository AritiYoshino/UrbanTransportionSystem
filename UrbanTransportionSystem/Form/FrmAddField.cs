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
using Microsoft.VisualBasic;
using structSet;

namespace UrbanTransportionSystem
{
    public partial class FrmAddField : System.Windows.Forms.Form
    {
        private IHookHelper m_hookHelper = null;
        private IFeatureLayer featureLayer = null;
        private fieldInfo fieldAdd;
        private IFeatureClass featureClass=null;
        public FrmAddField(IHookHelper hook, IFeatureLayer lyr)
        {
            InitializeComponent();
            m_hookHelper = hook;
            featureLayer = lyr;
        }
        private void FrmAddField_Load(object sender, EventArgs e)
        {
            featureClass = featureLayer.FeatureClass;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            fieldAdd.FieldName = txtFiledName.Text;
            fieldAdd.FieldType = cbxFieldType.SelectedItem.ToString();
            IFieldEdit customField = (IFieldEdit)new Field();
            customField.Name_2 = fieldAdd.FieldName;
            customField.AliasName_2 = fieldAdd.FieldName;
            switch (fieldAdd.FieldType)
            {
                case "Int":
                    customField.Type_2 = esriFieldType.esriFieldTypeInteger;
                    break;
                case "Text":
                    customField.Type_2 = esriFieldType.esriFieldTypeString;
                    break;
                case "Double":
                    customField.Type_2 = esriFieldType.esriFieldTypeDouble;
                    break;
                case "date":
                    customField.Type_2 = esriFieldType.esriFieldTypeDate;
                    break;
                default:
                    MessageBox.Show($"不识别的字段类型: {fieldAdd.FieldType}");
                    break;
            }
            featureClass.AddField(customField);
            MessageBox.Show("创建成功");
        }

    }
}
