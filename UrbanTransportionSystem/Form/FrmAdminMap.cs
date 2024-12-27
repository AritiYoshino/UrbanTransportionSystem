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
    public partial class FrmAdminMap : System.Windows.Forms.Form
    {
        #region 成员变量
        private IMapControl3 m_mapControl = null;
        private ILayer selectedLayer;
        private string m_mapDocumentName = string.Empty;
        IEnvelope limitEnvelope = new EnvelopeClass();
        public ILayer editItemsLayer;
        private IGeometry selectGeometry = null;
        private bool isLineSelect = false;
        private IFeatureLayer editChooseLayer = null;
        private BarToggleSwitchItem checkEdit = null;
        private IRasterLayer rasterlayer = null;
        private ITOCControl mTOCControl;
        private ILayer pMoveLayer;
        private int toIndex;
        private IActiveView m_ipActiveView;
        public  event EventHandler SendMsgEvent;//定义事件


        public IFeatureLayer ToolsLayer
        {
            get
            {
                return editChooseLayer;
            }
        }
        #endregion

        #region 构造函数
        public FrmAdminMap()
        {
            InitializeComponent();
            limitEnvelope.PutCoords(109.5, 31, 111, 32);
            axMapControl.Extent = limitEnvelope;

        }
        #endregion

        #region 窗口加载
        private void Form1_Load(object sender, EventArgs e)
        {
            m_mapControl = (IMapControl3)axMapControl.Object;
            mTOCControl = axTOCControl1.Object as ITOCControl;
            m_ipActiveView = axMapControl.ActiveView;

        }
        #endregion

        #region 菜单栏-文件
        private void btnOpenMXD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();

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

        #endregion

        #region 鹰眼地图
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

        private void axMapControl1_OnFullExtentUpdated(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)
        {
            CopyAndOverwriteMap();
        }
        #endregion

        #region 选择要素
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
            NetworkAnalysis.MapMouseDown = 1;
            NetworkAnalysis.mapPoint.PutCoords(e.x, e.y);
            if (NetworkAnalysis.networkAnalysis == true)
            {


                //this.Cursor = new System.Windows.Forms.Cursor("..\\..\\Resources\\locate.cur");
                IPointCollection points;//输入点集合
                ESRI.ArcGIS.Geometry.IPoint point;
                points = new MultipointClass();
                point = axMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                object o = Type.Missing;
                points.AddPoint(point, ref o, ref o);

                NetworkAnalysis.CreateFeature(NetworkAnalysis.inputFClass, points);//或者用鼠标点击最近点

                //把最近的点显示出来
                IElement element;
                ITextElement textelement = new TextElementClass();
                element = textelement as IElement;
                ITextSymbol textSymbol = new TextSymbol();

                textelement.Symbol = textSymbol;
                NetworkAnalysis.clickedcount++;
                textelement.Text = NetworkAnalysis.clickedcount.ToString();
                element.Geometry = m_ipActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                NetworkAnalysis.PGC.AddElement(element, 0);
                m_ipActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
     
                NetworkAnalysis.CoorPoint.Add(e.x.ToString() + " , " + e.y.ToString());
                SendMsgEvent(this, new NewEventArgs() { Text = NetworkAnalysis.CoorPoint });
            }
        }


        #endregion

        #region 图例功能
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {

            //左键移动图层顺序
            esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap pBasMap = null;
            ILayer ydLayer = null;
            object pOther = null;
            object pIndex = null;
            try
            {


                if (e.button == 1)
                {
                    mTOCControl.HitTest(e.x, e.y, ref pItem, ref pBasMap, ref ydLayer, ref pOther, ref pIndex);
                    if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                    {
                        if (ydLayer is IAnnotationSublayer) return;
                        else
                            pMoveLayer = ydLayer;

                    }
                }

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
                        if (editItemsLayer is IFeatureLayer)
                        {
                            btnAttributeTable.Enabled = true;
                            btnRemoveLayer.Enabled = true;


                        }
                        else if (editItemsLayer is IRasterLayer)
                        {
                            btnRendererTiff.Enabled = true;
                            btnRemoveLayer.Enabled = true;
                            rasterlayer = editItemsLayer as IRasterLayer;
                        }
                        else
                        {

                        }
                        selectedLayer = editItemsLayer;
                    }
                    else
                    {
                        btnAddData.Enabled = true;
                        btnAddGdbData.Enabled = true;
                        btnAddTiff.Enabled = true;
                    }

                    if (e.button == 2)
                    {
                        tocMenu.ShowPopup(Control.MousePosition);
                    }

                }
            }

            catch
            {

            }


        }

        private void axTOCControl1_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap pBasMap = null;
            ILayer pLayer = null;
            object pOther = null;
            object pIndex = null;
            
            if (e.button == 1)
            {
                mTOCControl.HitTest(e.x, e.y, ref pItem, ref pBasMap, ref pLayer, ref pOther, ref pIndex);
                if (pMoveLayer != pLayer)
                {
                    IMap pMap = axMapControl.Map;
                    ILayer pTempLayer;
                    for (int i = 0; i < pMap.LayerCount; i++)
                    {
                        pTempLayer = pMap.get_Layer(i);
                        if (pTempLayer == pLayer)
                            toIndex = i;
                    }
                    pMap.MoveLayer(pMoveLayer, toIndex);
                    axMapControl.ActiveView.Refresh();
                    mTOCControl.Update();
                }
            }

        }

        #endregion

        #region 工具条
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

        #endregion

        #region 菜单-空间查询
        private void btnSearchByAttribute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPropFind frmCalculate = new FrmPropFind(m_mapControl.Object);
            frmCalculate.Show();
        }

        private void btnPropertyTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPropertyTable frmPropertyTable = new FrmPropertyTable(m_mapControl.Object);
            frmPropertyTable.Show();
        }
        #endregion

        #region 菜单-路点规划
        private void btnCrateLayer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCreateFeatureClass frmCreateFeatureClass = new FrmCreateFeatureClass(m_mapControl.Object);
            frmCreateFeatureClass.Show();
        }

       

        private void btnAttributeTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPropertyTable frmPropertyTable = new FrmPropertyTable(axMapControl.Object, selectedLayer);
            frmPropertyTable.Show();
        }
        #endregion

        #region TocMenu 添加数据
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

        private void btnAddTiff_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择栅格文件";
            openFileDialog.Filter = "TIFF(*.tif)|*.tif";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AddRasterFile(openFileDialog.FileName);
            }
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

            // 刷新地图
            axMapControl.AddLayer(pLayer, 0);
            axMapControl.Refresh();
        }

        #endregion

        #region 矢量编辑器
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
            try
            {
                checkEdit = (BarToggleSwitchItem)sender;
                if (cbxLyr.EditValue == null)
                {
                    checkEdit.CheckedChanged -= switchEditFeature_CheckedChanged;
                    checkEdit.Checked = false;
                    MessageBox.Show("请选择图层");
                    checkEdit.CheckedChanged += switchEditFeature_CheckedChanged;
                    return;
                }

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
            catch
            {
                MessageBox.Show("编辑状态异常");
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
        #endregion

        #region TocMenu 移除数据与栅格渲染器
        private void btnRemoveLayer_ItemClick(object sender, ItemClickEventArgs e)
        {
            IMap map = axMapControl.Map;
            if (selectedLayer != null)
            {
                map.DeleteLayer(selectedLayer);
                axMapControl.Refresh();
            }
            
        }

        private void btnRendererTiff_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmRasterRenderer frmRasterRenderer = new FrmRasterRenderer(axMapControl, axTOCControl1, rasterlayer);
            frmRasterRenderer.Show();
        }



        #endregion

        #region 空间分析
        private void btnSpatialNoise_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmNoiseAnalysis frmNoiseAnalysis = new FrmNoiseAnalysis(axMapControl.Object);
            frmNoiseAnalysis.Show();
        }

        #endregion

        #region Toc按钮可用状态
        private void tocMenu_CloseUp(object sender, EventArgs e)
        {
            btnAttributeTable.Enabled = false;
            btnRemoveLayer.Enabled = false;
            btnAddData.Enabled = false;
            btnAddGdbData.Enabled = false;
            btnAddTiff.Enabled = false;
            btnRendererTiff.Enabled = false;
        }
        #endregion


        #region  路径规划

        private void btnPathPlanning_ItemClick(object sender, ItemClickEventArgs e)
        {
            NetworkAnalysis.CoorPoint = new List<string>();
            //NetworkAnalysis.CoorPoint = new string[50];
            FrmNetwork frmNetWork = new FrmNetwork(axMapControl.Object,axMapControl);
            frmNetWork.Show();
            SendMsgEvent += frmNetWork.MainFormTxtChanged;
        }
        #endregion

        #region 退出程序
        private void FrmAdminMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("您确定要退出程序吗？", "关闭提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }


        #endregion

        private void btnSpatialStatistics_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmSpatialStatistics frmSpatialStatistics = new FrmSpatialStatistics(axMapControl.Object);
            frmSpatialStatistics.Show();
        }
    }
}
