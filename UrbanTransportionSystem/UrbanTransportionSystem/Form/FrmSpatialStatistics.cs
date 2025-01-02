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

using ESRI.ArcGIS.Geodatabase;

using Microsoft.Office.Interop.Excel;

using DevExpress.XtraCharts;


namespace UrbanTransportionSystem
{
    public partial class FrmSpatialStatistics : System.Windows.Forms.Form
    {
        private IHookHelper m_hookHelper = null;
        public FrmSpatialStatistics(object hook)
        {
            InitializeComponent();
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
        }

        private void FrmSpatialStatistics_Load(object sender, EventArgs e)
        {

            IMap map = m_hookHelper.FocusMap;
            IEnumLayer layers = map.Layers;
            layers.Reset();
            ILayer layer = layers.Next();

            while (layer != null)
            {
                cbxLayer.Properties.Items.Add(layer.Name);
                cbxLayer1.Properties.Items.Add(layer.Name);
                cbxLayer2.Properties.Items.Add(layer.Name);
                layer = layers.Next();
            }
        }

        private void cbxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 清空原有的选项
            cbxLength.Properties.Items.Clear();
            cbxSpeed.Properties.Items.Clear();
            cbxLength.Text = "";
            cbxSpeed.Text = "";
            // 获取当前选中的图层名称
            string selectedLayerName = cbxLayer.Text;
            IMap map = m_hookHelper.FocusMap;
            IEnumLayer layers = map.Layers;
            layers.Reset();
            ILayer selectedLayer = null;
            ILayer layer = layers.Next();
            while (layer != null)
            {
                if (layer.Name == selectedLayerName)
                {
                    selectedLayer = layer;
                    break;
                }
                layer = layers.Next();
            }

            // 检查是否为 IFeatureLayer
            if (selectedLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = (IFeatureLayer)selectedLayer;
                IFeatureClass featureClass = featureLayer.FeatureClass;
                IFields fields = featureClass.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    // 将字段名添加到组合框中
                    cbxLength.Properties.Items.Add(field.Name);
                    cbxSpeed.Properties.Items.Add(field.Name);
                }
            }
        }

        private void btnOk1_Click(object sender, EventArgs e)
        {
            try 
            { 
           
            // 确保用户已经选择了所需字段
            if (cbxLength.Text == "" || cbxSpeed.Text == "")
            {
                MessageBox.Show("请选择分析数据");
                return;
            }
            chartControl1.Series.Clear();
            Analysis1();
            }

            catch
            {
                MessageBox.Show("发生错误");
            }
        }

        private void Analysis1()
        {
            string selectedLayerName = cbxLayer.Text;
            IMap map = m_hookHelper.FocusMap;
            IEnumLayer layers = map.Layers;
            layers.Reset();
            ILayer selectedLayer = null;
            ILayer layer = layers.Next();
            while (layer != null)
            {
                if (layer.Name == selectedLayerName)
                {
                    selectedLayer = layer;
                    break;
                }
                layer = layers.Next();
            }

            if (selectedLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = (IFeatureLayer)selectedLayer;
                IFeatureClass featureClass = featureLayer.FeatureClass;
                int lengthFieldIndex = featureClass.FindField(cbxLength.Text);
                int speedFieldIndex = featureClass.FindField(cbxSpeed.Text);

                // 速度区间边界
                double[] speedRanges = new double[] { 20, 40, 60, 80, 100 };
                double[] totalLengths = new double[4];

                // 统计每个速度区间的总长度
                IQueryFilter queryFilter = new QueryFilterClass();
                IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
                IFeature feature = featureCursor.NextFeature();
                while (feature != null)
                {
                    double speed = Convert.ToDouble(feature.get_Value(speedFieldIndex));
                    double length = Convert.ToDouble(feature.get_Value(lengthFieldIndex));
                    for (int i = 0; i < speedRanges.Length - 1; i++)
                    {
                        if (speed >= speedRanges[i] && speed < speedRanges[i + 1])
                        {
                            totalLengths[i] += length;
                            break;
                        }
                        else if (speed >= speedRanges[speedRanges.Length - 1])
                        {
                            totalLengths[speedRanges.Length - 2] += length;
                            break;
                        }
                    }
                    feature = featureCursor.NextFeature();
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);

                // 计算总长度
                double totalLength = totalLengths.Sum();
                DrawBarChart(speedRanges, totalLengths);

            }
           
        }

        private void DrawBarChart(double[] speedRanges, double[] totalLengths)
        {
            // 创建一个新的 Series
            DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("Speed Ranges", ViewType.Bar);

            // 清空之前的 Series
            chartControl1.Series.Clear();

            // 将数据添加到 Series 中
            for (int i = 0; i < speedRanges.Length - 1; i++)
            {
                // 构建柱状图的数据点
                SeriesPoint point = new SeriesPoint($"{speedRanges[i]}-{speedRanges[i + 1]}", totalLengths[i]);
                series.Points.Add(point);
            }

            // 将 Series 添加到 chartControl1 中
            chartControl1.Series.Add(series);

            // 设置图表的一些属性
            chartControl1.Titles.Add(new DevExpress.XtraCharts.ChartTitle());
            chartControl1.Titles[0].Text = "Speed Range vs Total Length";
            chartControl1.Legend.Visible = true;

            // 设置 X 轴和 Y 轴的标题
            ((XYDiagram)chartControl1.Diagram).AxisX.Title.Text = "时速";
            ((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "里程（图上单位）";
        }

        private void cbxLayer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxType.Properties.Items.Clear();
            cbxLength1.Properties.Items.Clear();
            cbxType.Text = "";
            cbxLength1.Text = "";
            // 获取当前选中的图层名称
            string selectedLayerName = cbxLayer1.Text;
            IMap map = m_hookHelper.FocusMap;
            IEnumLayer layers = map.Layers;
            layers.Reset();
            ILayer selectedLayer = null;
            ILayer layer = layers.Next();
            while (layer != null)
            {
                if (layer.Name == selectedLayerName)
                {
                    selectedLayer = layer;
                    break;
                }
                layer = layers.Next();
            }

            // 检查是否为 IFeatureLayer
            if (selectedLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = (IFeatureLayer)selectedLayer;
                IFeatureClass featureClass = featureLayer.FeatureClass;
                IFields fields = featureClass.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                  
                    cbxType.Properties.Items.Add(field.Name);
                    cbxLength1.Properties.Items.Add(field.Name);
                }
            }
        }

        [Obsolete]
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                // 确保用户已经选择了所需字段
                if (cbxLength1.Text == "" || cbxType.Text == "")
                {
                    MessageBox.Show("请选择分析数据");
                    return;
                }
                chartControl1.Series.Clear();
                Analysis2();
                chartControl2.Legend.Visible = true;
            }

            catch
            {
                MessageBox.Show("发生错误");
            }
        }

        [Obsolete]
        private void Analysis2()
        {
            string selectedTypeField = cbxType.Text;
            string selectedLengthField = cbxLength1.Text;

            // 获取当前选中的图层名称
            string selectedLayerName = cbxLayer1.Text;
            IMap map = m_hookHelper.FocusMap;
            IEnumLayer layers = map.Layers;
            layers.Reset();
            ILayer selectedLayer = null;
            ILayer layer = layers.Next();
            while (layer != null)
            {
                if (layer.Name == selectedLayerName)
                {
                    selectedLayer = layer;
                    break;
                }
                layer = layers.Next();
            }

            if (selectedLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = (IFeatureLayer)selectedLayer;
                IFeatureClass featureClass = featureLayer.FeatureClass;
                IQueryFilter queryFilter = new QueryFilterClass();
                IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
                IFeature feature = featureCursor.NextFeature();

                // 创建一个新的 DataTable 对象
                System.Data.DataTable dataTable = new System.Data.DataTable();
                // 添加列
                dataTable.Columns.Add(selectedTypeField, typeof(string));
                dataTable.Columns.Add(selectedLengthField, typeof(double));

                while (feature != null)
                {
                    string typeValue = feature.get_Value(feature.Fields.FindField(selectedTypeField)).ToString();
                    double lengthValue = Convert.ToDouble(feature.get_Value(feature.Fields.FindField(selectedLengthField)));
                    // 将数据添加到 DataTable 中
                    dataTable.Rows.Add(typeValue, lengthValue);
                    feature = featureCursor.NextFeature();
                }

                // 创建一个新的 Series 对象
                DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("Road Lengths", ViewType.Pie);

                // 对 DataTable 中的数据进行分组并计算总长度
                var groupedData = from row in dataTable.AsEnumerable()
                                  group row by row.Field<string>(selectedTypeField) into grp
                                  select new
                                  {
                                      Type = grp.Key,
                                      TotalLength = grp.Sum(r => r.Field<double>(selectedLengthField))
                                  };

                // 遍历分组后的数据添加数据点
                foreach (var item in groupedData)
                {
                    SeriesPoint point = new SeriesPoint(item.Type, item.TotalLength);          
                    series.Points.Add(point);
                }

                // 将 Series 添加到 ChartControl 中
                chartControl2.Series.Clear();
                series.PointOptions.PointView = PointView.ArgumentAndValues;
                series.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                chartControl2.Series.Add(series);
            }

        }

        [Obsolete]
        private void btnOK2_Click(object sender, EventArgs e)
        {
            try
            {

                // 确保用户已经选择了所需字段
                if (cbxSpeed1.Text == "" || cbxType1.Text == "")
                {
                    MessageBox.Show("请选择分析数据");
                    return;
                }
                chartControl1.Series.Clear();
                Analysis3();
                chartControl3.Legend.Visible = true;
            }

            catch
            {
                MessageBox.Show("发生错误");
            }
        }
        private void Analysis3()
        {
            // 获取用户选择的信息
            string selectedLayerName = cbxLayer2.Text;
            string selectedSpeedField = cbxSpeed1.Text;
            string selectedTypeField = cbxType1.Text;

            IMap map = m_hookHelper.FocusMap;
            IEnumLayer layers = map.Layers;
            layers.Reset();
            ILayer selectedLayer = null;
            ILayer layer = layers.Next();
            while (layer != null)
            {
                if (layer.Name == selectedLayerName)
                {
                    selectedLayer = layer;
                    break;
                }
                layer = layers.Next();
            }

            if (selectedLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = (IFeatureLayer)selectedLayer;
                IFeatureClass featureClass = featureLayer.FeatureClass;
                IQueryFilter queryFilter = new QueryFilterClass();
                IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
                IFeature feature = featureCursor.NextFeature();

                // 创建一个新的 DataTable 对象
                System.Data.DataTable dataTable = new System.Data.DataTable();
                // 添加列
                dataTable.Columns.Add(selectedTypeField, typeof(string));
                dataTable.Columns.Add(selectedSpeedField, typeof(double));

                while (feature != null)
                {
                    string typeValue = feature.get_Value(feature.Fields.FindField(selectedTypeField)).ToString();
                    double speedValue = Convert.ToDouble(feature.get_Value(feature.Fields.FindField(selectedSpeedField)));
                    // 将数据添加到 DataTable 中
                    dataTable.Rows.Add(typeValue, speedValue);
                    feature = featureCursor.NextFeature();
                }

                // 创建一个新的 Series 对象
                DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("Road Speeds", ViewType.RadarLine);

                // 对 DataTable 中的数据进行分组
                var groupedData = from row in dataTable.AsEnumerable()
                                  group row by row.Field<string>(selectedTypeField) into grp
                                  select new
                                  {
                                      Type = grp.Key,
                                      Speeds = grp.Select(r => r.Field<double>(selectedSpeedField)).ToList()
                                  };

                // 遍历分组后的数据添加数据点
                foreach (var item in groupedData)
                {
                    SeriesPoint point = new SeriesPoint(item.Type, item.Speeds.ToArray());
                    series.Points.Add(point);
                }

                // 将 Series 添加到 ChartControl 中
                chartControl3.Series.Clear();
                chartControl3.Series.Add(series);
            }
        }

        private void cbxLayer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 清空原有的选项
            cbxType1.Properties.Items.Clear();
            cbxSpeed1.Properties.Items.Clear();
            cbxLength.Text = "";
            cbxType1.Text = "";
            // 获取当前选中的图层名称
            string selectedLayerName = cbxLayer2.Text;
            IMap map = m_hookHelper.FocusMap;
            IEnumLayer layers = map.Layers;
            layers.Reset();
            ILayer selectedLayer = null;
            ILayer layer = layers.Next();
            while (layer != null)
            {
                if (layer.Name == selectedLayerName)
                {
                    selectedLayer = layer;
                    break;
                }
                layer = layers.Next();
            }

            // 检查是否为 IFeatureLayer
            if (selectedLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = (IFeatureLayer)selectedLayer;
                IFeatureClass featureClass = featureLayer.FeatureClass;
                IFields fields = featureClass.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    // 将字段名添加到组合框中
                    cbxType1.Properties.Items.Add(field.Name);
                    cbxSpeed1.Properties.Items.Add(field.Name);
                }
            }
        }


    }
}
