using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
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
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout
    /// </summary>
    [Guid("dc67eacd-648f-43eb-b460-82ba28e39ca1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("UrbanTransportionSystem.cmdCalculate")]
    public sealed class CmdLayerCalculate : ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
    { 
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        private IMapControl3 m_mapControl;
        // 添加以下属性来接收窗口控件
        public ComboBoxEdit cbLayer { get; set; }
        public ListBoxControl listBoxField { get; set; }
        public ListBoxControl listBoxValues { get; set; }

        public CmdLayerCalculate()
        {
            base.m_category = "YourCategory";
            base.m_caption = "Calculate";
            base.m_message = "Perform calculation operations";
            base.m_toolTip = "Calculate";
            base.m_name = "YourCommandName";

            try
            {
                string bitmapResourceName = "YourBitmapResourceName";
                base.m_bitmap = new System.Drawing.Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            base.m_enabled = true;
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;

            // 获取地图控件对象（假设是IMapControl3类型）
            m_mapControl = hook as IMapControl3;
            LayersToComboBox();
            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
     
        private void LoadLayersToComboBox()
        {
            // 获取地图的所有图层，并将图层名称添加到组合框中
            IMap map = m_hookHelper.FocusMap;
            IEnumLayer layers = map.Layers;
            layers.Reset();
            ILayer layer = layers.Next();

            while (layer != null)
            {
                // 使用接收的cbLayer控件属性，将图层名称添加到其中
                cbLayer.Properties.Items.Add(layer.Name);
                layer = layers.Next();
            }
        }

        private void UpdateFieldAndValueLists()
        {

            listBoxField.Items.Clear();
            listBoxValues.Items.Clear();


            string strSelectedLayerName = cbLayer.Text;

            IFeatureLayer pFeatureLayer = null;
            IDisplayTable pDisPlayTable = null;

            try
            {

                for (int i = 0; i <= m_hookHelper.FocusMap.LayerCount - 1; i++)
                {

                    if (m_hookHelper.FocusMap.get_Layer(i).Name == strSelectedLayerName)
                    {

                        if (m_hookHelper.FocusMap.get_Layer(i) is IFeatureLayer)
                        {
                            pFeatureLayer = m_hookHelper.FocusMap.get_Layer(i) as IFeatureLayer;

                           
                            pDisPlayTable = pFeatureLayer as IDisplayTable;

                            
                            for (int j = 0; j <= pDisPlayTable.DisplayTable.Fields.FieldCount - 1; j++)
                            {
                                listBoxField.Items.Add(pDisPlayTable.DisplayTable.Fields.get_Field(j).Name);
                            }
                        }
                        else
                        {
                            
                            MessageBox.Show("您选择的图层不能进行属性查询!" + "请重新选择");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void LayersToComboBox()
        {
            
            LoadLayersToComboBox();
        }

        public void SelectedIndexChanged()
        {
            UpdateFieldAndValueLists();
        }
    }
    #endregion
}
