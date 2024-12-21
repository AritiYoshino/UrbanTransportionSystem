using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.DisplayUI;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Office.Interop.Excel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.SpatialAnalyst;

namespace UrbanTransportionSystem
{
    public partial class FrmNoiseAnalysis : System.Windows.Forms.Form
    {
        private IHookHelper m_hookHelper = null;
        private double searchRadius = 100; // 搜索半径，单位根据实际数据坐标系等确定
        private double cellSize = 10; // 输出结果像元大小

        public FrmNoiseAnalysis(object hook)
        {
            InitializeComponent();
            if (m_hookHelper == null) m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
        }

        private void FrmNoiseAnalysis_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < m_hookHelper.FocusMap.LayerCount; i++)
            {
                ILayer lyr = m_hookHelper.FocusMap.get_Layer(i);
                IFeatureLayer fLyr = lyr as IFeatureLayer;
                // 将图层名称添加到下拉框的选项列表中
                cbxLayerSelect.Properties.Items.Add(lyr.Name);
                FieldLayerLoad();
            }
        }
        private void FieldLayerLoad()
        {
            int selectedLayerIndex = cbxLayerSelect.SelectedIndex;
            if (selectedLayerIndex >= 0)
            {
                ILayer selectedLayer = m_hookHelper.FocusMap.get_Layer(selectedLayerIndex);
                IFeatureLayer featureLayer = selectedLayer as IFeatureLayer;
                if (featureLayer != null)
                {
                    IFields fields = featureLayer.FeatureClass.Fields;
                    for (int i = 0; i < fields.FieldCount; i++)
                    {
                        IField field = fields.get_Field(i);
                        cbxFieldSelect.Properties.Items.Add(field.Name);
                    }
                }
            }
        }

        private void cbxLayerSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxFieldSelect.Properties.Items.Clear();
            FieldLayerLoad();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TiffPush();
        }

        private void TiffPush()
        {
            string path = txtPushPath.Text;
            path = path.Replace("/", "\\");  //修正路径替换赋值问题，确保替换生效
            IMap map = m_hookHelper.FocusMap;
            IActiveView activeView = map as IActiveView;
            IEnvelope extent = activeView.Extent;
            ILayer selectedLayer = map.get_Layer(cbxLayerSelect.SelectedIndex);
            IFeatureLayer selectedFeatureLayer = selectedLayer as IFeatureLayer;
            IFeatureClass selectedFeatureClass = selectedFeatureLayer.FeatureClass;

            IRasterAnalysisEnvironment rasterEnv = new RasterDensityOp();

            //设置半径
            double r = Convert.ToDouble(txtRadius.Text);
            rasterEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, r);

            //设置输出栅格大小
            double cellSize = Convert.ToDouble(txtPixel.Text);
            object cellSizeObj = cellSize;
            rasterEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref cellSizeObj);

            //获得图层
            ILayer layer = null;
            IMap mapTemp = map;
            for (int i = 0; i < mapTemp.LayerCount; i++)
            {
                ILayer temp = mapTemp.Layer[i];
                if (temp.Name == cbxLayerSelect.SelectedItem.ToString())
                    layer = temp;
            }
            IFeatureLayer fLayer = layer as IFeatureLayer;
            IFeatureClass fClass = fLayer.FeatureClass;

            //设置空间处理范围（此处假设原代码传递layer对象设置范围的逻辑正确，如果有误需进一步修正）
            object extentProObj = layer;
            rasterEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extentProObj);

            //设置要素数据
            IFeatureClassDescriptor feaDes;
            feaDes = new FeatureClassDescriptorClass();
            feaDes.Create(fClass, null, cbxFieldSelect.SelectedItem.ToString());
            IGeoDataset inGeodataset;
            inGeodataset = feaDes as IGeoDataset;

            //设置输出栅格
            //IRaster outraster;
            IGeoDataset outGeoDataset;

            IDensityOp densityOp = rasterEnv as IDensityOp;
            outGeoDataset = densityOp.KernelDensity(inGeodataset, r);

            try
            {
                IWorkspaceFactory pWKSF = new RasterWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWKSF.OpenFromFile(path, 0);
                ISaveAs pSaveAs = outGeoDataset as ISaveAs;
                pSaveAs.SaveAs(path, pWorkspace, "TIFF");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            IRasterLayer pRlayer = new RasterLayer();
            pRlayer.CreateFromRaster((IRaster)outGeoDataset);
            pRlayer.Name = System.IO.Path.GetFileName(path);
            map.AddLayer(pRlayer);
        }
        
        private void txtPushPath_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtPushPath.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
