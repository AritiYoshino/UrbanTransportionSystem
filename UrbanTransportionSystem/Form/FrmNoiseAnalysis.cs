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
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Server;
using System.Diagnostics;

namespace UrbanTransportionSystem
{
    public partial class FrmNoiseAnalysis : System.Windows.Forms.Form
    {
        private IHookHelper m_hookHelper = null;
        private string rasterPath1 = null;
        private string rasterPath2 = null;
        private string raster1Coefficient=null;
        private string raster2Coefficient=null;
        private string rasterName = null;
        private string rasterPath = null;
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
                IRasterLayer rLyr =  lyr as IRasterLayer;
                if (fLyr != null)
                {
                    cbxLayerSelect.Properties.Items.Add(lyr.Name);
                    FieldLayerLoad();
                }
                if(rLyr!=null)
                {
                    cbxTiffSelect1.Properties.Items.Add(lyr.Name);
                    cbxTiffSelect2.Properties.Items.Add(lyr.Name);
                }
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



        private void TiffPush()
        {
            string path = txtPushPath.Text;
            path = path.Replace("/", "\\");  
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

            //设置空间处理范围         
            IEnvelope fullExtent =m_hookHelper.ActiveView.FullExtent;
            object extentProObj = fullExtent;
            rasterEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extentProObj);

            //设置要素数据
            IFeatureClassDescriptor feaDes;
            feaDes = new FeatureClassDescriptorClass();
            feaDes.Create(fClass, null, cbxFieldSelect.SelectedItem.ToString());
            IGeoDataset inGeodataset;
            inGeodataset = feaDes as IGeoDataset;

            //设置输出栅格
            IRaster outraster;
            IGeoDataset outGeoDataset;

            IDensityOp densityOp = rasterEnv as IDensityOp;
            outGeoDataset = densityOp.KernelDensity(inGeodataset, r);
            outraster = (IRaster)outGeoDataset;
            try
            {
                IRaster2 outputRaster = outraster as IRaster2;
                IRasterDataset rasterDataset = outputRaster.RasterDataset;
                IWorkspaceFactory pWKSF = new RasterWorkspaceFactoryClass();
                //path = path + "\\"+ txtTiffName1.Text;
                IWorkspace pWorkspace = pWKSF.OpenFromFile(path, 0);
                ISaveAs2 pSaveAs = rasterDataset as ISaveAs2;
                string name = txtTiffName1.Text + ".tif";
                string fullPath = System.IO.Path.Combine(path, name);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                pSaveAs.SaveAs(name, pWorkspace, "TIFF");
                IRasterLayer pRlayer = new RasterLayer();
                pRlayer.CreateFromRaster(outputRaster as IRaster);
                pRlayer.Name = txtTiffName1.Text;
                map.AddLayer(pRlayer);
                cbxTiffSelect1.Properties.Items.Add(pRlayer.Name);
                cbxTiffSelect2.Properties.Items.Add(pRlayer.Name);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        
        private void txtPushPath_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtPushPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnAnalyse_Click(object sender, EventArgs e)
        {

            try
            {
                rasterName = txtTiffName2.Text;
                rasterPath = txtAnPath.Text;
                rasterPath = rasterPath.Replace("/", "\\");
                raster1Coefficient = txtCoefficient1.Text;
                raster2Coefficient = txtCoefficient2.Text;
                

                
                RasterAnalyse();
            }
            catch
            {
                MessageBox.Show("发生异常错误或栅格参数不完整");
                return;
            }

            AddRasterFile(System.IO.Path.Combine(rasterPath, rasterName)+".tif");
        }

        private void AddRasterFile(string filePath)
        {
            IWorkspaceFactory pWorkspaceFactory = new RasterWorkspaceFactory();
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(filePath), 0);
            IRasterWorkspace pRasterWorkspace = pWorkspace as IRasterWorkspace;
            IRasterDataset pRasterDataset = pRasterWorkspace.OpenRasterDataset(System.IO.Path.GetFileName(filePath));

            // 创建金字塔
            IRasterPyramid pRasterPyramid = pRasterDataset as IRasterPyramid;
            if (!pRasterPyramid.Present)
            {
                pRasterPyramid.Create();
            }

            // 栅格图层
            IRasterLayer pRasterLayer = new RasterLayer();
            pRasterLayer.CreateFromDataset(pRasterDataset);
            ILayer pLayer = pRasterLayer as ILayer;

            IMap map = m_hookHelper.FocusMap;
            IActiveView activeView= m_hookHelper.ActiveView;
            // 刷新地图
            map.AddLayer(pLayer);
            activeView.Refresh();
        }

        private void RasterAnalyse()
        {
            string pythonPath = null;
            string scriptPath = null;
            PathPython(ref pythonPath, ref scriptPath);

            Process process = new Process();
            process.StartInfo.FileName = pythonPath;
            process.StartInfo.Arguments = scriptPath;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            string arguments = $"\"{scriptPath}\" \"{rasterPath1}\" \"{rasterPath2}\" \"{raster1Coefficient}\" \"{raster2Coefficient}\" \"{rasterName}\" \"{rasterPath}\"";

            process.StartInfo.Arguments = arguments;
            try
            {
                process.Start();
                process.WaitForExit(); // 先等待进程退出
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(output))
                {
                    MessageBox.Show("Python 输出: " + output);
                }
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show("Python 输出: " + error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("调用 Python 时出错: " + ex.Message);
            }
        }


        private void PathPython(ref string pythonPath, ref string scriptPath)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = System.IO.Path.GetDirectoryName(baseDirectory);
            string grandParentDirectory = System.IO.Path.GetDirectoryName(parentDirectory);
            string greatGrandParentDirectory = System.IO.Path.GetDirectoryName(grandParentDirectory);
            greatGrandParentDirectory = greatGrandParentDirectory.Replace(" / ", "\\");
            pythonPath = System.IO.Path.Combine(greatGrandParentDirectory, "Python27\\ArcGIS10.8\\python.exe");
            scriptPath = System.IO.Path.Combine(greatGrandParentDirectory, "python\\RasterCompute.py");

        }
       

        public ILayer GetLayerByName(string name)
        {
            IMap map = m_hookHelper.FocusMap;
            IEnumLayer Temp_AllLayer = map.Layers;
            ILayer Each_Layer = Temp_AllLayer.Next();
            while (Each_Layer != null)
            {
                if (Each_Layer.Name.Contains(name))
                    return Each_Layer;
                Each_Layer = Temp_AllLayer.Next();
            }
            return null;
        }

        
        private void txtAnPath_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtAnPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void cbxTiffSelect1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ILayer layer = GetLayerByName(cbxTiffSelect1.SelectedItem.ToString());
            IRasterLayer rasterLayer = layer as IRasterLayer;   
            IRaster raster = rasterLayer.Raster;
            IRaster2 raster2 = raster as IRaster2;
            IRasterDataset rasterDataset = raster2.RasterDataset;
            IDataset dataset = rasterDataset as IDataset;
            string layerPath = dataset.Workspace.PathName;
            string dataSourceName = dataset.BrowseName;
            rasterPath1 = System.IO.Path.Combine(layerPath, dataSourceName);
            rasterPath1=rasterPath1.Replace("/", "\\");
        }

        private void cbxTiffSelect2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ILayer layer = GetLayerByName(cbxTiffSelect2.SelectedItem.ToString());
            IRasterLayer rasterLayer = layer as IRasterLayer;
            IRaster raster = rasterLayer.Raster;
            IRaster2 raster2 = raster as IRaster2;
            IRasterDataset rasterDataset = raster2.RasterDataset;
            IDataset dataset = rasterDataset as IDataset;
            string layerPath = dataset.Workspace.PathName;
            string dataSourceName = dataset.BrowseName;
            rasterPath2 = System.IO.Path.Combine(layerPath, dataSourceName);
            rasterPath2 = rasterPath2.Replace("/", "\\");
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            TiffPush();
        }
    }
}
