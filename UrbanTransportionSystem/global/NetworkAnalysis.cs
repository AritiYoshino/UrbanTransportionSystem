using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
    class NetworkAnalysis
    {
        public static bool open3dform = false;
        public static IMap p2DMap = null;
        public static ISceneGraph p3DMap = null;
        public static bool scenePan = false;
        public static IPoint scenePoint = new PointClass();
        public static IPoint mapPoint = new PointClass();
        public static IPoint currentPoint = new PointClass();
        public static IActiveView pActiveView = null;
        public static int MouseDown = 0;
        public static int MapMouseDown = 0;
        public static ICamera pSceneCamera = null;
        public static List<string>CoorPoint = null;//存储路径分析坐标位置
        
        public static int PropertyID = -1;
        public static List<string> map2list = new List<string>();// 从axmap到属性表


        public static INAContext m_NAContext;//网络分析上下文
        public static INetworkDataset networkDataset;//网络数据集
        public static IFeatureWorkspace pFWorkspace;
        public static IFeatureClass inputFClass;//打开stops数据集
        public static IFeatureDataset featureDataset;
        public static int clickedcount = 0;//mapcontrol加点显示点数
        public static IGraphicsContainer PGC;
        public static bool networkAnalysis = false;


        public static void Refresh()
        {
            open3dform = false;
            p2DMap = null;
            p3DMap = null;
            scenePan = false;
            scenePoint = new PointClass();
            mapPoint = new PointClass();
            currentPoint = new PointClass();
            pActiveView = null;
            MouseDown = 0;
            MapMouseDown = 0;
            pSceneCamera = null;
            CoorPoint = null;

            PropertyID = -1;
            map2list = new List<string>();

            m_NAContext = null;
            networkDataset = null;
            pFWorkspace = null;
            inputFClass = null;
            featureDataset = null;
            clickedcount = 0;
            PGC = null;
            networkAnalysis = false;
        }



        public static IWorkspace OpenWorkspace(string strGDBName)
        {
            IWorkspaceFactory workspaceFactory;
            workspaceFactory = new FileGDBWorkspaceFactoryClass();
            return workspaceFactory.OpenFromFile(strGDBName, 0);
        }

        public static INetworkDataset OpenNetworkDataset(IWorkspace networkDatasetWorkspace, System.String networkDatasetName, System.String featureDatasetName)
        {
            if (networkDatasetWorkspace == null || networkDatasetName == "" || featureDatasetName == null)
            {
                return null;
            }
            IDatasetContainer3 datasetContainer3 = null;

            switch (networkDatasetWorkspace.Type)
            {
                case ESRI.ArcGIS.Geodatabase.esriWorkspaceType.esriFileSystemWorkspace:

                    
                    IWorkspaceExtensionManager workspaceExtensionManager = networkDatasetWorkspace as ESRI.ArcGIS.Geodatabase.IWorkspaceExtensionManager; // Dynamic Cast
                    ESRI.ArcGIS.esriSystem.UID networkID = new ESRI.ArcGIS.esriSystem.UIDClass();

                    networkID.Value = "esriGeoDatabase.NetworkDatasetWorkspaceExtension";
                    ESRI.ArcGIS.Geodatabase.IWorkspaceExtension workspaceExtension = workspaceExtensionManager.FindExtension(networkID);
                    datasetContainer3 = workspaceExtension as IDatasetContainer3; // Dynamic Cast
                    break;

                case ESRI.ArcGIS.Geodatabase.esriWorkspaceType.esriLocalDatabaseWorkspace:

                

                case ESRI.ArcGIS.Geodatabase.esriWorkspaceType.esriRemoteDatabaseWorkspace:

                    
                    ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featureWorkspace = networkDatasetWorkspace as ESRI.ArcGIS.Geodatabase.IFeatureWorkspace; // Dynamic Cast
                    featureDataset = featureWorkspace.OpenFeatureDataset(featureDatasetName);
                    ESRI.ArcGIS.Geodatabase.IFeatureDatasetExtensionContainer featureDatasetExtensionContainer = featureDataset as ESRI.ArcGIS.Geodatabase.IFeatureDatasetExtensionContainer; // Dynamic Cast
                    ESRI.ArcGIS.Geodatabase.IFeatureDatasetExtension featureDatasetExtension = featureDatasetExtensionContainer.FindExtension(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset);
                    datasetContainer3 = featureDatasetExtension as ESRI.ArcGIS.Geodatabase.IDatasetContainer3; // Dynamic Cast
                    break;
            }

            if (datasetContainer3 == null)
                return null;

            ESRI.ArcGIS.Geodatabase.IDataset dataset = datasetContainer3.get_DatasetByName(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset, networkDatasetName);

            return dataset as ESRI.ArcGIS.Geodatabase.INetworkDataset; // Dynamic Cast
        }

        public static INAContext CreateSolverContext(INetworkDataset networkDataset)
        {
            //获取创建网络分析上下文所需的IDENETWORKDATASET类型参数
            IDENetworkDataset deNDS = GetDENetworkDataset(networkDataset);
            INASolver naSolver;
            naSolver = new NARouteSolver();
            INAContextEdit contextEdit = naSolver.CreateContext(deNDS, naSolver.Name) as INAContextEdit;
            contextEdit.Bind(networkDataset, new GPMessagesClass());
            return contextEdit as INAContext;
        }

        public static IDENetworkDataset GetDENetworkDataset(INetworkDataset networkDataset)
        {
            //将网络分析数据集QI添加到DATASETCOMPOENT
            IDatasetComponent dstComponent;
            dstComponent = networkDataset as IDatasetComponent;
            //获得数据元素
            return dstComponent.DataElement as IDENetworkDataset;
        }

        public static void CreateFeature(IFeatureClass featureClass, IPointCollection PointCollection)
        {
            //是否为点图层
            if (featureClass.ShapeType != esriGeometryType.esriGeometryPoint)
            {
                return;
            }
            //创建点要素
            for (int i = 0; i < PointCollection.PointCount; i++)
            {
                IFeature feature = featureClass.CreateFeature();
                feature.Shape = PointCollection.get_Point(i);
                IRowSubtypes rowSubtypes = (IRowSubtypes)feature;
                feature.Store();
            }
        }

    }
    class NewEventArgs:EventArgs
    {
        public List <string> Text
        {
            get;
            set;
        }
    }

}