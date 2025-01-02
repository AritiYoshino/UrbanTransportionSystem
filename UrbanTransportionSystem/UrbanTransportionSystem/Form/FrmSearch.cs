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
    public partial class FrmSearch : System.Windows.Forms.Form
    {
        private IHookHelper m_hookHelper = null;
        private IFeatureLayer busStopLayer=null;
        private IFeatureLayer metroStopLayer = null;
        private AxMapControl axMapControl = null;
        public FrmSearch(object hook, AxMapControl mapControl)
        {
            InitializeComponent();
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
            axMapControl = mapControl;
        }

        private void FrmSearch_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < m_hookHelper.FocusMap.LayerCount; i++)
            {
                ILayer lyr = m_hookHelper.FocusMap.get_Layer(i);     
                if (lyr is IFeatureLayer  && lyr.Name == "公交车站")
                {
                    busStopLayer = lyr as IFeatureLayer;

                } 
                if(lyr is IFeatureLayer && lyr.Name == "地铁站")
                {
                    metroStopLayer = lyr as IFeatureLayer;
                }
            }
            LoadBusStopData();
            LoadMetroStopData();
        }

        private void ZoomToGeometry(ESRI.ArcGIS.Geometry.IGeometry geometry, IActiveView activeView)
        {
            IEnvelope env = geometry.Envelope;
            IGeometry5 geo = geometry as IGeometry5;

            if (env.Width == 0 || env.Height == 0)
            {
                ESRI.ArcGIS.Geometry.IPoint centerPoint = new PointClass();
                centerPoint.X = geo.CentroidEx.X;
                centerPoint.Y = geo.CentroidEx.Y;

                //IDisplayTransformation displayTrans = activeView.ScreenDisplay.DisplayTransformation;
                //displayTrans.SpatialReference = activeView.FocusMap.SpatialReference;

                env.XMin = centerPoint.X - 0.001;
                env.XMax = centerPoint.X + 0.001;
                env.YMin = centerPoint.Y - 0.001;
                env.YMax = centerPoint.Y + 0.001;

                env.CenterAt(centerPoint);
            }
            env.Expand(3, 3, true);
            activeView.Extent = env;
            activeView.Refresh();
            activeView.ScreenDisplay.UpdateWindow();
        }


        private void LoadBusStopData()
        {
            if (busStopLayer == null) return;
            IFeatureCursor featureCursor = busStopLayer.Search(null, false);
            IFeature feature;
            List<string> busStopNames = new List<string>();
            while ((feature = featureCursor.NextFeature()) != null)
            {
                // 获取 Name 字段的值
                string name = feature.get_Value(feature.Fields.FindField("name_st")).ToString();
                listBusStops.Items.Add(name);
                busStopNames.Add(name);
            }
            listBusStops.DataSource = busStopNames;
        }

        private void LoadMetroStopData()
        {
            if (metroStopLayer == null) return;
            IFeatureCursor featureCursor = metroStopLayer.Search(null, false);
            IFeature feature;
            List<string> metroStopNames = new List<string>();
            while ((feature = featureCursor.NextFeature()) != null)
            {
                // 获取 Name 字段的值
                string name = feature.get_Value(feature.Fields.FindField("name")).ToString();
                listMetroStops.Items.Add(name);
                metroStopNames.Add(name);
            }
            listMetroStops.DataSource = metroStopNames;
        }

        private void searchBusStops_EditValueChanged(object sender, EventArgs e)
        {
            try
            {              
                if (searchBusStops.EditValue != null)
                {
                    string searchText = searchBusStops.EditValue.ToString();
                    List<string> filteredNames = new List<string>();
                    if (string.IsNullOrEmpty(searchText))
                    {
                        LoadBusStopData();
                    }
                    else
                    {
                        // 过滤数据
                        List<string> allNames = (List<string>)listBusStops.DataSource;
                        foreach (string name in allNames)
                        {
                            if (name.Contains(searchText))
                            {
                                filteredNames.Add(name);
                            }
                        }
                        listBusStops.DataSource = filteredNames;
                    }
                }
            }

            catch
            {

            }

        }

        private void listBusStops_DoubleClick(object sender, EventArgs e)
        {
            string selectedItem = listBusStops.SelectedItem.ToString();
            searchBusStops.EditValue = selectedItem;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            //“确认”按钮实现的功能【查询地点位置并缩放到该地点】
            string text = searchBusStops.Text.Trim();

            IFeatureLayer pFeatureLayer = busStopLayer;

            IQueryFilter queryFilter = new QueryFilterClass();

            queryFilter.WhereClause = "name_st='" + text + "'";

            IActiveView activeView = m_hookHelper.ActiveView;

            try
            {
                IFeatureCursor featureCursor = pFeatureLayer.Search(queryFilter, false);
                IFeature pFeature;
                while ((pFeature = featureCursor.NextFeature()) != null)
                {
                    ZoomToGeometry(pFeature.Shape, activeView);
                    axMapControl.FlashShape(pFeature.Shape);
                }
            }
            catch (Exception pException)
            {
                MessageBox.Show(pException.Message);
            }

        }

        private void searchMetro_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (searchMetroStops.EditValue != null)
                {
                    string searchText = searchMetroStops.EditValue.ToString();
                    List<string> filteredNames = new List<string>();
                    if (string.IsNullOrEmpty(searchText))
                    {
                        LoadMetroStopData();
                    }
                    else
                    {
                        // 过滤数据
                        List<string> allNames = (List<string>)listMetroStops.DataSource;
                        foreach (string name in allNames)
                        {
                            if (name.Contains(searchText))
                            {
                                filteredNames.Add(name);
                            }
                        }
                        listMetroStops.DataSource = filteredNames;
                    }
                }
            }

            catch
            {

            }
        }

        private void btnSearchMetro_Click(object sender, EventArgs e)
        {
            //“确认”按钮实现的功能【查询地点位置并缩放到该地点】
            string text = searchMetroStops.Text;

            IFeatureLayer pFeatureLayer = metroStopLayer;

            IQueryFilter queryFilter = new QueryFilterClass();

            queryFilter.WhereClause = "name='" + text + "'";

            IActiveView activeView = m_hookHelper.ActiveView;

            try
            {
                IFeatureCursor featureCursor = pFeatureLayer.Search(queryFilter, false);
                IFeature pFeature;
                while ((pFeature = featureCursor.NextFeature()) != null)
                {
                    ZoomToGeometry(pFeature.Shape, activeView);
                    axMapControl.FlashShape(pFeature.Shape);
                }
            }
            catch (Exception pException)
            {
                MessageBox.Show(pException.Message);
            }
        }

        private void listMetroStops_DoubleClick(object sender, EventArgs e)
        {
            string selectedItem = listMetroStops.SelectedItem.ToString();
            searchMetroStops.EditValue = selectedItem;
        }
    }
}
