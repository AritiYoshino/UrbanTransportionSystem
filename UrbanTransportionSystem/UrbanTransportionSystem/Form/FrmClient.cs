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
    public partial class FrmClient : System.Windows.Forms. Form
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
        private IRasterLayer rasterlayer = null;
        private ITOCControl mTOCControl;
        private ILayer pMoveLayer;
        private int toIndex;
        private IActiveView m_ipActiveView;
        public event EventHandler SendMsgEvent;

        public FrmClient()
        {
            InitializeComponent();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void FrmClient_Load(object sender, EventArgs e)
        {
            m_mapControl = (IMapControl3)axMapControl.Object;
            mTOCControl = axTOCControl1.Object as ITOCControl;
            m_ipActiveView = axMapControl.ActiveView;
        }

        private void btnMapRoam_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand command = new ControlsMapPanTool();
            command.OnCreate(axMapControl.Object);
            axMapControl.CurrentTool = command as ITool;
        }

        private void btnMousePt_ItemClick(object sender, ItemClickEventArgs e)
        {
            isLineSelect = false;
            axMapControl.CurrentTool = null;
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand command = new ControlsMapIdentifyTool();
            command.OnCreate(axMapControl.Object);
            axMapControl.CurrentTool = command as ITool;
        }

        private void btnMapMeasurement_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand command = new ControlsMapMeasureTool();
            command.OnCreate(axMapControl.Object);
            axMapControl.CurrentTool = command as ITool;
        }

        private void btnMagnifier_ItemClick(object sender, ItemClickEventArgs e)
        {
            axMapControl.CurrentTool = null;
            IEnvelope pEnvelope = axMapControl.Extent;
            pEnvelope.Expand(0.5, 0.5, true);
            axMapControl.Extent = pEnvelope;
            axMapControl.Refresh();
        }

        private void btnZoom_ItemClick(object sender, ItemClickEventArgs e)
        {
            axMapControl.CurrentTool = null;
            IEnvelope pEnvelope = axMapControl.Extent;
            pEnvelope.Expand(2, 2, true);
            axMapControl.Extent = pEnvelope;
            axMapControl.Refresh();
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmSearch frmSearch = new FrmSearch(axMapControl.Object,axMapControl);
            frmSearch.Show();
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            NetworkAnalysis.CoorPoint = new List<string>();
            //NetworkAnalysis.CoorPoint = new string[50];
            FrmNetwork frmNetWork = new FrmNetwork(axMapControl.Object, axMapControl);
            frmNetWork.Show();
            SendMsgEvent += frmNetWork.MainFormTxtChanged;
        }

        private void axMapControl_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
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
        }

        private void axMapControl_OnFullExtentUpdated(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)
        {

        }
    }
}
