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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace UrbanTransportionSystem
{
    public partial class FrmPropFind : System.Windows.Forms.Form
    {
        private CmdLayerCalculate calculateCommand;
        private IHookHelper m_hookHelper = null;
      
        public FrmPropFind(object hook)
        {
            InitializeComponent();
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
       
        }

        private void FrmCalculate_Load(object sender, EventArgs e)
        {
            calculateCommand = new CmdLayerCalculate
            {
                cbLayer = this.cbLayer,
                listBoxField = this.listBoxFields,
                listBoxValues = this.listBoxValues
            };
            calculateCommand.OnCreate(m_hookHelper.Hook);
 
        }

       

        private void listBoxField_DoubleClick(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + listBoxFields.SelectedItem.ToString() + " ";
        }

        private void listBoxValues_DoubleClick(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + listBoxValues.SelectedItem.ToString() + " ";
        }
        private void checkValue_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            btnGetValue_Click(sender,e);
        }

        private void btnGetValue_Click(object sender, EventArgs e)
        {
            if (listBoxFields.Text == "")
            {
                MessageBox.Show("请选择一个属性字段！");
            }
            else
            {
                try
                {
                    // 获取选中的属性字段名称
                    string strSelectedFieldName = listBoxFields.Text;

                    // 清空列表框及重置标签文本
                    listBoxValues.Items.Clear();
                    labelControl1.Text = "";

                    IFeatureCursor pFeatureCursor;
                    IFeatureClass pFeatureClass;
                    IFeature pFeature;

                    if (strSelectedFieldName != null)
                    {
                       
                        pFeatureClass = (GetLayerByName(cbLayer.Text) as IFeatureLayer).FeatureClass;

                      
                        pFeatureCursor = pFeatureClass.Search(null, true);

                        
                        pFeature = pFeatureCursor.NextFeature();

                     
                        int index = pFeatureClass.FindField(strSelectedFieldName);

                        while (pFeature != null)
                        {
                           
                            string strValue = pFeature.get_Value(index).ToString();

                           
                            if (checkValue.Checked)
                            {
                                if (pFeature.Fields.get_Field(index).Type == esriFieldType.esriFieldTypeString)
                                {
                                    strValue = "'" + strValue + "'";
                                }

                                if (listBoxValues.FindStringExact(strValue) == ListBox.NoMatches)
                                {
                                   
                                    listBoxValues.Items.Add(strValue);
                                }
                            }
                       
                            else
                            {
                                if (pFeature.Fields.get_Field(index).Type == esriFieldType.esriFieldTypeString)
                                {
                                    strValue = "'" + strValue + "'";
                                }

                                listBoxValues.Items.Add(strValue);
                            }

                            // 获取下一个要素
                            pFeature = pFeatureCursor.NextFeature();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

        }
        private ILayer GetLayerByName(string strLayerName)
        {
            ILayer pLayer = null;

            for (int i = 0; i <= m_hookHelper.FocusMap.LayerCount - 1; i++)
            {

                if (strLayerName == m_hookHelper.FocusMap.get_Layer(i).Name)
                {
 
                    pLayer = m_hookHelper.FocusMap.get_Layer(i);

                    break;
                }
            }
            return pLayer;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (textBoxWhereClause.Text == "")
            {
                MessageBox.Show("请生成查询语句！");
                return;
            }

            
            this.WindowState = FormWindowState.Minimized;

            
            PerformAttributeFilter();

            this.WindowState = FormWindowState.Normal;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
          
            PerformAttributeFilter();
            this.Dispose();
        }
        private void PerformAttributeFilter()
        {
            try
            {
                IFeatureLayer pFeatureLayer;
                pFeatureLayer = GetLayerByName(cbLayer.SelectedItem.ToString()) as IFeatureLayer;

                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;

              
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = textBoxWhereClause.Text;

                IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, true);

                IFeature pFeature;
                pFeature = pFeatureCursor.NextFeature();
                IFeatureSelection fSelection = pFeatureLayer as IFeatureSelection;
                fSelection.Clear();
                while (pFeature!= null)
                {
                    fSelection.Add(pFeature);
                    pFeature = pFeatureCursor.NextFeature();
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);

                m_hookHelper.ActiveView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("您的查询语句可能有误,请检查| " + ex.Message);
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.Text = "";
        }

        private void btnInputEqual_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "=";
        }

        private void cbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculateCommand.SelectedIndexChanged();
        }

        private void btnInputNeqval_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "<>";
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "LIKE";
        }

        private void btnGreaterThan_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + ">";
        }

        private void btnGreaterEqual_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + ">=";
        }

        private void btnLessThan_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "<";
        }

        private void btnLessEqual_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "<=";
        }

        private void btnAND_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "AND";
        }

        private void btnOR_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "OR";
        }

        private void btnModulo_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "%";
        }

        private void btnQuotient_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "/";
        }

        private void btnLeftBracket_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "(";
        }

        private void btnRightBracket_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + ")";
        }

        private void btnNot_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "NOT";
        }

        private void btnIS_Click(object sender, EventArgs e)
        {
            string currentText = textBoxWhereClause.Text;
            textBoxWhereClause.Text = currentText + "IS";
        }
    }
}
