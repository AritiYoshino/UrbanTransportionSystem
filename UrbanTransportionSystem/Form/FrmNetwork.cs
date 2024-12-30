using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using ESRI.ArcGIS;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Data.OleDb;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.NetworkAnalysis;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;

namespace UrbanTransportionSystem
{
    public partial class FrmNetwork : System.Windows.Forms.Form
    {
        private INASolver naSolver = null;
        private IHookHelper m_hookHelper = null;
        private AxMapControl axMapControl;
        private IActiveView m_ipActiveView=null;
        public FrmNetwork(object hook,AxMapControl mapControl)
        {
            InitializeComponent();
            if (m_hookHelper == null) m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
            this.TopMost = true;
            axMapControl = mapControl;
        }

        internal void MainFormTxtChanged(object sender, EventArgs e)
        {
            NewEventArgs arg = e as NewEventArgs;
            listCoordinate.Items.Add(arg.Text[arg.Text.Count-1]);
        }
        private void Analysis()
        {
            if (NetworkAnalysis.networkAnalysis && NetworkAnalysis.clickedcount > 1)
            {
                NetworkAnalysis.networkAnalysis = false;
                listInfoNavigation.Items.Add("开始进行路径分析求解...");

                IGPMessages gpMessages = new GPMessagesClass();
                loadNANetworkLocations("stops", NetworkAnalysis.inputFClass, 50000000);
                naSolver = NetworkAnalysis.m_NAContext.Solver;

                try
                {
                    bool flag = naSolver.Solve(NetworkAnalysis.m_NAContext, gpMessages, null);
                }
                catch
                {
                    MessageBox.Show("未找到路径");
                    return;
                }
                listInfoNavigation.Items.Add("done!");

                //获取路径结果
                INAStreetDirectionsAgent2 streetAgent;
                streetAgent = NetworkAnalysis.m_NAContext.Agents.get_ItemByName("StreetDirectionsAgent") as INAStreetDirectionsAgent2;
                //设置处理结果为中文
                //streetAgent.Language = "zh-CN";
                
                streetAgent.Execute(null, null);

                INAStreetDirectionsContainer directionsContainer;
                directionsContainer = streetAgent.DirectionsContainer as INAStreetDirectionsContainer;
                //directionsContainer.SaveAsXML("route.xml");

                int directionsCount = directionsContainer.DirectionsCount;
                string routeIdName = string.Empty;
                string routeString;
                for (int index = 0; index < directionsCount; index++)
                {
                    INAStreetDirections naStreetDirections = directionsContainer.get_Directions(index);
                    routeString = getDirectionsString(naStreetDirections);
                    getDataTableFromRouteString(routeString);
                }

                //解决完后，删除图层内容
                ITable pTable_inputFClass = NetworkAnalysis.inputFClass as ITable;
                pTable_inputFClass.DeleteSearchedRows(null);
                (NetworkAnalysis.p2DMap as IActiveView).Refresh();

                if (gpMessages != null)
                {
                    for (int i = 0; i < gpMessages.Count; i++)
                    {
                        switch (gpMessages.GetMessage(i).Type)
                        {

                            case esriGPMessageType.esriGPMessageTypeError:
                                listInfoNavigation.Items.Add("错误 " + gpMessages.GetMessage(i).ErrorCode.ToString() + " " + gpMessages.GetMessage(i).Description);
                                break;
                            case esriGPMessageType.esriGPMessageTypeWarning:
                                listInfoNavigation.Items.Add("警告 " + gpMessages.GetMessage(i).Description);
                                break;
                            default:
                                listInfoNavigation.Items.Add("信息 " + gpMessages.GetMessage(i).Description);
                                break;
                        }
                    }
                }

                (NetworkAnalysis.p2DMap as IActiveView).Refresh();

            }
            else if (NetworkAnalysis.clickedcount == 1)
            {
                MessageBox.Show("未选取终点，请继续定位！");
            }
            else
            {
                MessageBox.Show("未选取或更新起点和终点，请重新定位！");
            }
        }

        private void loadNANetworkLocations(string strNAClassName, IFeatureClass inputFC, double snapTolerance)
        {
            INAClass naClass;
            INamedSet classes;
            classes = NetworkAnalysis.m_NAContext.NAClasses;
            naClass = classes.get_ItemByName(strNAClassName) as INAClass;
            //删除naClasses中添加的项
            naClass.DeleteAllRows();
            //加载网络分析对象，设置容差值
            INAClassLoader classLoader = new NAClassLoader();
            classLoader.Locator = NetworkAnalysis.m_NAContext.Locator;
            if (snapTolerance > 0) classLoader.Locator.SnapTolerance = snapTolerance;
            classLoader.NAClass = naClass;
            //创建INAclassFieldMap,用于字段映射
            INAClassFieldMap fieldMap;
            fieldMap = new NAClassFieldMap();
            //加载网络分析类
            int rowsln = 0;
            int rowsLocated = 0;
            IFeatureCursor featureCursor = inputFC.Search(null, true);
            classLoader.Load((ICursor)featureCursor, null, ref rowsln, ref rowsLocated);
            ((INAContextEdit)NetworkAnalysis.m_NAContext).ContextChanged();
        }

        private string getDirectionsString(INAStreetDirections serverDirections)
        {
            // 得到总的距离和时间
            INAStreetDirection direction = serverDirections.Summary;
            string totallength = null, totaltime = null;
            for (int k = 0; k < direction.StringCount; k++)
            {
                if (direction.get_StringType(k) == esriDirectionsStringType.esriDSTLength)
                    totallength = direction.get_String(k);
                if (direction.get_StringType(k) == esriDirectionsStringType.esriDSTTime)
                    totaltime = direction.get_String(k);
            }
            //MessageBox.Show("Directions for CFRoute [" + (1) + "] - Total Distance: " + totallength + " Total Time: " + totaltime);            
            string directionString = string.Empty;
            // 加节点到方向
            for (int directionIndex = 0; directionIndex < serverDirections.DirectionCount; directionIndex++)
            {
                direction = serverDirections.get_Direction(directionIndex);
                for (int stringIndex = 0; stringIndex < direction.StringCount; stringIndex++)
                {
                    if (direction.get_StringType(stringIndex) == esriDirectionsStringType.esriDSTGeneral ||
                        direction.get_StringType(stringIndex) == esriDirectionsStringType.esriDSTDepart ||
                        direction.get_StringType(stringIndex) == esriDirectionsStringType.esriDSTArrive ||
                        direction.get_StringType(stringIndex) == esriDirectionsStringType.esriDSTSummary
                        )
                    {
                        if (stringIndex == 0)
                        {
                            directionString += direction.get_String(stringIndex).ToString() + "|";
                        }
                        else if (stringIndex == 2)
                        {
                            directionString += direction.get_String(stringIndex).ToString() + "@";
                        }
                    }
                }
            }
            return directionString;

        }

        //导航信息显示
        private void getDataTableFromRouteString(string routesString)
        {
            routesString = routesString.Substring(0, routesString.Length - 1);
            string[] routeArray = routesString.Split(new Char[] { '@' });
            string firstSecond = routeArray[0];
            string[] firstSeconds = firstSecond.Split(new Char[] { '|' });
            string firstString = firstSeconds[0];
            string secondDirection = firstSeconds[1];
            string secondLength = firstSeconds[2];
            string endString = routeArray[routeArray.Length - 1];
            listInfoNavigation.Items.Add("");
            listInfoNavigation.Items.Add("导航信息");
            listInfoNavigation.Items.Add("");
            listInfoNavigation.Items.Add(firstString);
            listInfoNavigation.Items.Add("");
            listInfoNavigation.Items.Add(secondDirection + "   " + secondLength);
            listInfoNavigation.Items.Add("");
            for (int i = 1; i < routeArray.Length - 1; i++)
            {
                //MessageBox.Show(routeArray[i]);   
                string[] temps = routeArray[i].Split(new Char[] { '|' });
                string direction = temps[0];
                string length = temps[1];
                listInfoNavigation.Items.Add(direction + "   " + length);
                listInfoNavigation.Items.Add("");
            }
            listInfoNavigation.Items.Add(endString);
        }

        private void ExportFeature(IFeatureClass apFeatureClass, string ExportFilePath, string ExportFileShortName)
        {

            if (apFeatureClass == null)
            {

                MessageBox.Show("分析出错，请检查路径分析结果", "系统提示");
                return;

            }

            //设置导出要素类的参数
            IFeatureClassName pOutFeatureClassName = new FeatureClassNameClass();
            IDataset pOutDataset = (IDataset)apFeatureClass;
            pOutFeatureClassName = (IFeatureClassName)pOutDataset.FullName;
            //创建一个输出shp文件的工作空间
            IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactory();
            IWorkspaceName pInWorkspaceName = new WorkspaceNameClass();
            pInWorkspaceName = pShpWorkspaceFactory.Create(ExportFilePath, ExportFileShortName, null, 0);

            //创建一个要素集合
            IFeatureDatasetName pInFeatureDatasetName = null;
            //创建一个要素类
            IFeatureClassName pInFeatureClassName = new FeatureClassNameClass();
            IDatasetName pInDatasetClassName;
            pInDatasetClassName = (IDatasetName)pInFeatureClassName;
            pInDatasetClassName.Name = ExportFileShortName;//作为输出参数
            pInDatasetClassName.WorkspaceName = pInWorkspaceName;
            //自定义字段
            AddField(apFeatureClass, "Elevation", "", esriFieldType.esriFieldTypeInteger);
            //通过FIELDCHECKER检查字段的合法性，为输出SHP获得字段集合
            long iCounter;
            IFields pOutFields, pInFields;
            IFieldChecker pFieldChecker;
            IField pGeoField;
            IEnumFieldError pEnumFieldError = null;
            pInFields = apFeatureClass.Fields;
            pFieldChecker = new FieldChecker();
            pFieldChecker.Validate(pInFields, out pEnumFieldError, out pOutFields);
            //通过循环查找几何字段
            pGeoField = null;
            for (iCounter = 0; iCounter < pOutFields.FieldCount; iCounter++)
            {
                if (pOutFields.get_Field((int)iCounter).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    pGeoField = pOutFields.get_Field((int)iCounter);
                    break;
                }
            }
            //得到几何字段的几何定义
            IGeometryDef pOutGeometryDef;
            IGeometryDefEdit pOutGeometryDefEdit;
            pOutGeometryDef = pGeoField.GeometryDef;
            //设置几何字段的空间参考和网格
            pOutGeometryDefEdit = (IGeometryDefEdit)pOutGeometryDef;
            pOutGeometryDefEdit.GridCount_2 = 1;
            pOutGeometryDefEdit.set_GridSize(0, 1500000);
            try
            {
                //开始导入
                IFeatureDataConverter pShpToClsConverter = new FeatureDataConverterClass();
                pShpToClsConverter.ConvertFeatureClass(pOutFeatureClassName, null, pInFeatureDatasetName, pInFeatureClassName, pOutGeometryDef, pOutFields, "", 1000, 0);
                MessageBox.Show("导出成功！");
            }
            catch
            {
            }
        }

        private void AddField(IFeatureClass pFeatureClass, string name, string aliasName, esriFieldType FieldType)
        {
            //若存在，则不需添加
            if (pFeatureClass.Fields.FindField(name) > -1) return;
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.AliasName_2 = aliasName;
            pFieldEdit.Name_2 = name;
            pFieldEdit.Type_2 = FieldType;

            IClass pClass = pFeatureClass as IClass;
            pClass.AddField(pField);
        }

        private void OutputShp()
        {
            saveFileDialog1.Filter = "shapefile（*.shp）|*.shp";
            saveFileDialog1.FileName = "route_export";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog1.AddExtension = true;
                string file = saveFileDialog1.FileName.Substring(0, saveFileDialog1.FileName.LastIndexOf('\\'));
                string name = System.IO.Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                if (!System.IO.Directory.Exists(file))
                {
                    System.IO.Directory.CreateDirectory(file);
                }
                try
                {
                    IFeatureClass routesFC;
                    routesFC = NetworkAnalysis.m_NAContext.NAClasses.get_ItemByName("Routes") as IFeatureClass;
                    ExportFeature(routesFC, file, name);
                }
                catch
                {
                    MessageBox.Show("导出失败！");
                }
            }
        }


        private void btnClearItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
           
            clear();
        }

        private void clear()
        {
            listCoordinate.Items.Clear();
            listInfoNavigation.Items.Clear();
            clearAll();
        }

        private void clearAll()
        {
            NetworkAnalysis.networkAnalysis = false;
           // m_hookHelper.Hook.CurrentTool = null;
            ITable pTable = NetworkAnalysis.inputFClass as ITable;
            pTable.DeleteSearchedRows(null);
            //提取路径前，删除上一次路径route网络上下文
            IFeatureClass routesFC;
            routesFC = NetworkAnalysis.m_NAContext.NAClasses.get_ItemByName("Routes") as IFeatureClass;
            ITable pTable1 = routesFC as ITable;
            pTable1.DeleteSearchedRows(null);
            //提取路径前，删除上一次路径Stops网络上下文
            INAClass stopsNAClass = NetworkAnalysis.m_NAContext.NAClasses.get_ItemByName("Stops") as INAClass;
            ITable ptable2 = stopsNAClass as ITable;
            ptable2.DeleteSearchedRows(null);
            //提取路径前，删除上一次barries网络上下文
            INAClass barriesNAClass = NetworkAnalysis.m_NAContext.NAClasses.get_ItemByName("Barriers") as INAClass;
            ITable pTable3 = barriesNAClass as ITable;
            pTable3.DeleteSearchedRows(null);
            NetworkAnalysis.PGC.DeleteAllElements();
            NetworkAnalysis.clickedcount = 0;
            (NetworkAnalysis.p2DMap as IActiveView).Refresh();
        }

        private void btnOutput_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OutputShp();
        }

        private void FrmNetWork_Load(object sender, EventArgs e)
        {
            m_ipActiveView = m_hookHelper.ActiveView;
        }

        private void btnLoadNetWork_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                LoadNetwork();
            }

            catch
            {
                MessageBox.Show("打开失败，请检查路网的文件型数据库");
            }

        }
        private void LoadNetwork()
        {
            folderBrowserDialog1.SelectedPath = Application.StartupPath; ;
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;

            string folderPath = folderBrowserDialog1.SelectedPath;

            //打开工作区间
            NetworkAnalysis.pFWorkspace = NetworkAnalysis.OpenWorkspace(folderPath) as IFeatureWorkspace;
            //打开网络数据集
            NetworkAnalysis.networkDataset = NetworkAnalysis.OpenNetworkDataset(NetworkAnalysis.pFWorkspace as IWorkspace, "路网_ND", "路网");
            //NetworkAnalysis.networkDataset = NetworkAnalysis.OpenNetworkDataset(NetworkAnalysis.pFWorkspace as IWorkspace, "同济新村道路_ND", "同济新村道路");
            //创建网络分析上下文，建立一种解决关系
            NetworkAnalysis.m_NAContext = NetworkAnalysis.CreateSolverContext(NetworkAnalysis.networkDataset);

            //打开停靠点数据集
            NetworkAnalysis.inputFClass = NetworkAnalysis.pFWorkspace.OpenFeatureClass("stops");
            
            //TEST_ND_JUNCTIONS图层
            IFeatureLayer vertex = new FeatureLayerClass();
            vertex.FeatureClass = NetworkAnalysis.pFWorkspace.OpenFeatureClass("路网_ND_Junctions");
            //vertex.FeatureClass = NetworkAnalysis.pFWorkspace.OpenFeatureClass("同济新村道路_ND_Junctions");
            vertex.Name = vertex.FeatureClass.AliasName;
            //axMapControl.AddLayer(vertex, 0);
            //道路图层
            IFeatureLayer road;
            road = new FeatureLayerClass();
            road.FeatureClass = NetworkAnalysis.pFWorkspace.OpenFeatureClass("路网");
            //road.FeatureClass = NetworkAnalysis.pFWorkspace.OpenFeatureClass("同济新村道路");

            road.Name = road.FeatureClass.AliasName;
            //axMapControl.AddLayer(road, 0);

            //为networkdataset生成一个图层，并将该图层添加到axmapcontrol中
            ILayer pLayer;//网络图层
            INetworkLayer pNetworkLayer;
            pNetworkLayer = new NetworkLayerClass();
            pNetworkLayer.NetworkDataset = NetworkAnalysis.networkDataset;
            pLayer = pNetworkLayer as ILayer;
            pLayer.Name = "成都市路网";
            axMapControl.AddLayer(pLayer, 0);
            //生成一个网络分析图层并添加到axmaptrol1中
            ILayer layer1;
            INALayer nalayer = NetworkAnalysis.m_NAContext.Solver.CreateLayer(NetworkAnalysis.m_NAContext);
          
            
            layer1 = nalayer as ILayer;
            layer1.Name = NetworkAnalysis.m_NAContext.Solver.DisplayName;
            axMapControl.AddLayer(layer1, 0);
            m_ipActiveView = axMapControl.ActiveView;
            NetworkAnalysis.p2DMap = m_ipActiveView.FocusMap;
            NetworkAnalysis.PGC = NetworkAnalysis.p2DMap as IGraphicsContainer;
        }

        private void btnLocation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NetworkAnalysis.networkAnalysis = true;
        }

        private void btnAnalyse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Analysis();

        }

        private void FrmNetwork_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("您确定要退出吗？", "关闭提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void FrmNetwork_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (NetworkAnalysis.networkDataset!=null)
                {
                    clear();
                    IMap map = m_hookHelper.FocusMap;
                    for (int i = map.LayerCount - 1; i >= 0; i--)
                    {
                        ILayer lyr = map.get_Layer(i);
                        if (lyr.Name == "成都市路网" || lyr.Name == "路径")
                        {
                            map.DeleteLayer(lyr);
                        }
                    }
                    IActiveView activeView = m_hookHelper.ActiveView;
                    activeView.Refresh();
                    NetworkAnalysis.Refresh();
                }
            }
            catch
            {
                
            }
        }
    }
}
