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
namespace UrbanTransportionSystem
{

    public partial class FrmAdminMap : Form
    {

        private IMapControl3 m_mapControl = null;
        private ILayer selectedLayer;
        private string m_mapDocumentName = string.Empty;
        IEnvelope limitEnvelope = new EnvelopeClass();
        public ILayer editItemsLayer;
        private IGeometry selectGeometry = null;
        private bool isLineSelect = false;
        private IFeatureLayer editChooseLayer = null;
        private BarToggleSwitchItem checkEdit = null;
        public IFeatureLayer ToolsLayer
        {
            get
            {
                return editChooseLayer;
            }
        }

        public FrmAdminMap()
        {
            InitializeComponent();
            limitEnvelope.PutCoords(109.5, 31, 111, 32);
            axMapControl.Extent = limitEnvelope;
            axMapControl.OnMouseDown += AxMapControl_OnMouseDown;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_mapControl = (IMapControl3)axMapControl.Object;

        }

        private void btnOpenMXD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();

        }

        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            axMapControl2.AutoMouseWheel = false;
            CopyAndOverwriteMap();
        }

        private void CopyAndOverwriteMap()
        {
            IObjectCopy objCopy = new ObjectCopyClass();
            object fromMap = axMapControl.Map;
            object objMap = axMapControl2.Map;
            objCopy.Overwrite(fromMap, ref objMap);
            axMapControl2.Extent = axMapControl2.FullExtent;
            axMapControl2.Refresh();
        }

        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            IElement ele = new RectangleElementClass();
            ele.Geometry = axMapControl.Extent;
            IFillSymbol symbol = new SimpleFillSymbolClass();
            IRgbColor clr = new RgbColorClass();
            clr.NullColor = true;
            clr.Transparency = 0;
            symbol.Color = clr;
            ILineSymbol linSymbol = new SimpleLineSymbolClass();
            IRgbColor linClr = new RgbColorClass();
            linClr.Red = 255;
            linSymbol.Color = linClr;
            symbol.Outline = linSymbol;
            ((IFillShapeElement)ele).Symbol = symbol;
            axMapControl2.ActiveView.GraphicsContainer.DeleteAllElements();
            axMapControl2.ActiveView.GraphicsContainer.AddElement(ele, 0);
            axMapControl2.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void axMapControl2_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {

        }

        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IEnvelope env = axMapControl2.TrackRectangle();
            axMapControl.Extent = env;
            axMapControl2.Refresh();
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            // 判断当前是否处于选择要素工具激活状态（即点击了btnPtSelect按钮后）
            if (isLineSelect)
            {
                selectGeometry = axMapControl.TrackLine();
                axMapControl.Map.SelectByShape(selectGeometry, null, false);
                axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                isLineSelect = false;
            }
        }

        private void axMapControl1_OnFullExtentUpdated(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)
        {
            CopyAndOverwriteMap();
        }

        private void btnSaveMXD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                mapDoc.Save(mapDoc.UsesRelativePaths, false);
                MessageBox.Show("已保存");
                mapDoc.Close();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap map = new MapClass();
                editItemsLayer = new FeatureLayerClass();
                object other = new object();
                object index = new object();
                axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref editItemsLayer, ref other, ref index);
                if (editItemsLayer != null)
                {
                    selectedLayer = editItemsLayer;
                }
                if (e.button == 2)
                {
                    tocMenu.ShowPopup(Control.MousePosition);
                }
            }

        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ICommand addData = new ControlsAddDataCommandClass();
            addData.OnCreate(axMapControl.Object);
            addData.OnClick();


        }

        private void btnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmRemoveData frmRemoveData = new FrmRemoveData(m_mapControl.Object);
            frmRemoveData.Show();
            axMapControl.Refresh();

        }

        private void btnGlobalMap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl.CurrentTool = null;
            ICommand command = new ControlsMapFullExtentCommand();
            command.OnCreate(axMapControl.Object);
            command.OnClick();
        }

        private void btnMapRoam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ICommand command = new ControlsMapPanTool();
            command.OnCreate(axMapControl.Object);
            axMapControl.CurrentTool = command as ITool;
        }

        private void btnMousePt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            isLineSelect = false;
            axMapControl.CurrentTool = null;
        }
        private void AxMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ICommand selectCommand = new ControlsSelectFeaturesTool();
                selectCommand.OnCreate(axMapControl.Object);
                axMapControl.CurrentTool = selectCommand as ITool;
            }

            if (e.Button == MouseButtons.Right)
            {
                ICommand clearCommand = new ControlsClearSelectionCommand();
                clearCommand.OnCreate(axMapControl.Object);
                clearCommand.OnClick();
            }
        }

        private void btnMapIdentifierElements_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ICommand command = new ControlsMapIdentifyTool();
            command.OnCreate(axMapControl.Object);
            axMapControl.CurrentTool = command as ITool;
        }

        private void btnMapMeasurement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ICommand command = new ControlsMapMeasureTool();
            command.OnCreate(axMapControl.Object);
            axMapControl.CurrentTool = command as ITool;
        }

        private void btnMagnifier_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl.CurrentTool = null;
            IEnvelope pEnvelope = axMapControl.Extent;
            pEnvelope.Expand(0.5, 0.5, true);
            axMapControl.Extent = pEnvelope;
            axMapControl.Refresh();
        }

        private void btnShrink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axMapControl.CurrentTool = null;
            IEnvelope pEnvelope = axMapControl.Extent;
            pEnvelope.Expand(2, 2, true);
            axMapControl.Extent = pEnvelope;
            axMapControl.Refresh();

        }

        private void btnPropertyTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPropertyTable frmPropertyTable = new FrmPropertyTable(m_mapControl.Object);
            frmPropertyTable.Show();
        }

        private void axMapControl_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {

        }

        private void btnSearchByAttribute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPropFind frmCalculate = new FrmPropFind(m_mapControl.Object);
            frmCalculate.Show();
        }

        private void FrmAdminMap_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnCrateLayer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCreateFeatureClass frmCreateFeatureClass = new FrmCreateFeatureClass(m_mapControl.Object);
            frmCreateFeatureClass.Show();
        }

        private void btnDelFeature_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ICommand command = new ControlsEditingStartCommand();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();


            CmdDelFeature cmdDelFeature = new CmdDelFeature();
            cmdDelFeature.OnCreate(m_mapControl.Object);
            cmdDelFeature.OnClick();

            ICommand clearSelectionCommand = new ControlsClearSelectionCommand();
            clearSelectionCommand.OnCreate(m_mapControl.Object);
            clearSelectionCommand.OnClick();
        }

        private void btnAttributeTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPropertyTable frmPropertyTable = new FrmPropertyTable(axMapControl.Object, selectedLayer);
            frmPropertyTable.Show();
        }

        private void barBoxSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ICommand command = new ControlsSelectFeaturesTool();
            command.OnCreate(axMapControl.Object);
            axMapControl.CurrentTool = command as ITool;
        }

        private void btnLineSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            isLineSelect = true;
        }

        private void AxMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {

        }




        private void btnAddData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "加载Shapefile数据";
            openFileDialog.Filter = "Shapefile(*.shp)|*.shp|asa|*.*";
            openFileDialog.Multiselect = false;  // 不允许选择多个文件

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string shapefile_path = openFileDialog.FileName;
                try
                {
                    string shapefile_dir = System.IO.Path.GetDirectoryName(shapefile_path);
                    string shapefile_name = System.IO.Path.GetFileName(shapefile_path);
                    axMapControl.AddShapeFile(shapefile_dir, shapefile_name);
                    axMapControl.Refresh();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"加载Shapefile数据成失败: {ex.Message}", "");
                    MessageBox.Show(string.Format("加载Shapefile数据失败:\n {0}", ex.Message), "加载失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        private void btnAddGdbData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "打开GDB文件夹";
            if (DialogResult.OK == folderBrowserDialog.ShowDialog())
            {
                string openFullPath = "";
                if (Directory.Exists(folderBrowserDialog.SelectedPath))
                {
                    if (folderBrowserDialog.SelectedPath.ToUpper().Contains(".GDB"))
                    {
                        openFullPath = folderBrowserDialog.SelectedPath;
                    }
                }

                if (!string.IsNullOrEmpty(openFullPath))
                {
                    IFeatureWorkspace featureWorkspace;
                    IWorkspaceFactory workspaceFactory;
                    try
                    {
                        IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                        IWorkspace pWorkspace;

                        workspaceFactory = new FileGDBWorkspaceFactoryClass();
                        pWorkspace = workspaceFactory.OpenFromFile(openFullPath, 0);
                        featureWorkspace = pWorkspace as IFeatureWorkspace;

                        IEnumDataset Temp_AllIndependentFeatureClass = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureClass);
                        IFeatureClass Each_IndependentFeatureClass = Temp_AllIndependentFeatureClass.Next() as IFeatureClass;
                        while (Each_IndependentFeatureClass != null)
                        {
                            pFeatureLayer = new FeatureLayerClass();
                            pFeatureLayer.FeatureClass = featureWorkspace.OpenFeatureClass(Each_IndependentFeatureClass.AliasName);
                            pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                            axMapControl.AddLayer(pFeatureLayer as ILayer);
                            axMapControl.Refresh();
                            Each_IndependentFeatureClass = Temp_AllIndependentFeatureClass.Next() as IFeatureClass;
                        }


                        pWorkspace = workspaceFactory.OpenFromFile(openFullPath, 0);
                        IEnumDataset pEnumDataset = pWorkspace.get_Datasets(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTFeatureDataset);
                        pEnumDataset.Reset();
                        IDataset pDataset = pEnumDataset.Next();
                        if (pDataset is IFeatureDataset)
                        {
                            IFeatureDataset pFeatureDataset = featureWorkspace.OpenFeatureDataset(pDataset.Name);
                            IEnumDataset pEnumDataset1 = pFeatureDataset.Subsets;
                            pEnumDataset1.Reset();
                            IDataset pDataset1 = pEnumDataset1.Next();
                            if (pDataset1 is IFeatureClass)
                            {
                                while (pDataset1 != null)
                                {
                                    pFeatureLayer = new FeatureLayerClass();
                                    pFeatureLayer.FeatureClass = featureWorkspace.OpenFeatureClass(pDataset1.Name);
                                    pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                                    axMapControl.Map.AddLayer(pFeatureLayer);
                                    axMapControl.ActiveView.Refresh();
                                    pDataset1 = pEnumDataset1.Next();
                                }
                            }
                            else
                            {
                                MessageBox.Show("No FeatureLayer!");
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("加载GDB失败！", "提示");
                    }

                }
            }
        }


        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            menuEdit1.Visible = true;
            updateCbxLayerItems();
            btnEditCanUse(false);
        }
        private void btnEditCanUse(bool use)
        {
            btnOpenMXD.Enabled = use;
            btnAdd.Enabled = use;
            btnRemove.Enabled = use;
            btnCreateLayer.Enabled = use;
            btnAddData.Enabled = use;
            btnAddGdbData.Enabled = use;

        }
        private void updateCbxLayerItems()
        {
            cbxLayerItems.Items.Clear();
            IMap map = axMapControl.Map;
            IEnumLayer layers = map.Layers;
            layers.Reset();
            ILayer layer = layers.Next();

            while (layer != null)
            {
                cbxLayerItems.Items.Add(layer.Name);
                layer = layers.Next();
            }
        }

        private void beaginEdit()
        {
            ICommand command = new ToolEditFeature(editChooseLayer);
            command.OnCreate(m_mapControl.Object);
            axMapControl.CurrentTool = (ITool)command;
        }


        private void switchEditFeature_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            checkEdit = (BarToggleSwitchItem)sender;
            if (checkEdit.Checked)
            {
                cbxLyr.Enabled = false;
                IMap map = axMapControl.Map;
                IEnumLayer layers = map.Layers;
                layers.Reset();
                ILayer layer = layers.Next();
                while (layer != null)
                {
                    if (layer is IFeatureLayer)
                    {
                        if (layer.Name == cbxLyr.EditValue.ToString())
                        {
                            editChooseLayer = (IFeatureLayer)layer;
                            break;
                        }
                    }
                    layer = layers.Next();
                }
                beaginEdit();
            }
            else
            {
                cbxLyr.Enabled = true;
                axMapControl.CurrentTool = null;
            }
        }

        private void btnExitEdit_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            menuEdit1.Visible = false;
            btnEditCanUse(true);
            editChooseLayer = null;
            if (checkEdit != null)
            {
                checkEdit.Checked = false;
            }
        }
    }
}
