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

namespace UrbanTransportionSystem
{
    public partial class FrmRemoveData : Form
    {

        private ILayer m_layer = null;
        private IFeatureLayer m_ipSelectedLyr = null;
        private IFeatureLayer m_ipPolygonLyr = null;
        private IHookHelper m_hookHelper = null;
        IMap map = null;
        List<string> selectedLayerNames = new List<string>();
            
    public FrmRemoveData(object hook)
        {
            InitializeComponent();
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
            map = m_hookHelper.FocusMap;
        }
        public ILayer lyr
        {
            get
            {
                return m_layer;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (map == null)
            {
                MessageBox.Show("地图对象为空，无法执行移除操作");
                return;
            }
            for (int i = 0; i < listData.CheckedItems.Count; i++)
            {
                selectedLayerNames.Add(listData.CheckedItems[i].ToString());
            }

            for (int i = map.LayerCount - 1; i >= 0; i--)
            {
                ILayer lyr = map.get_Layer(i);
                if (selectedLayerNames.Contains(lyr.Name))
                {
                    map.DeleteLayer(lyr);
                }
            }
            this.Close();
        }


        private void FrmRemoveData_Load(object sender, EventArgs e)
        {
            if (m_hookHelper == null)
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = this; // 根据实际情况设置正确的Hook对象，这里假设当前Form作为Hook对象
            }
            map = m_hookHelper.FocusMap;

            if (map != null)
            {
                for (int i = 0; i < map.LayerCount; i++)
                {
                    ILayer lyr = map.get_Layer(i);
                    IFeatureLayer fLyr = lyr as IFeatureLayer;
                    if (fLyr != null)
                    {
                        listData.Items.Add(lyr.Name);
                    }
                }
            }
        }

    }
}
