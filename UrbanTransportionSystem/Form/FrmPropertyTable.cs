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

namespace UrbanTransportionSystem
{
    public partial class FrmPropertyTable : System.Windows.Forms.Form
    {
        // 实现类的成员变量定义
        #region 成员变量
        // 用于存储Shape文件的要素类
        private IFeatureClass shpFeatureClass;
        // 存储状态列表（具体用途需结合更多上下文确定）
        private List<string> stateList = new List<string>();
        // 存储状态数量列表（具体用途需结合更多上下文确定）
        private List<int> stateCountList = new List<int>();
        // 定义一个容差常量，用于一些精度相关的比较等操作（这里是float类型转换后的表示）
        private const double tolerance = 0.00001f;
        // 用于辅助获取地图相关对象的帮助类实例，初始化为null
        private IHookHelper m_hookHelper = null;
        // 地图控件对象，用于操作地图显示等相关功能
        private IMapControl3 m_mapControl;
        // 存储已修改单元格信息的列表，每个元素包含行、列和修改后的值
        private List<RowAndCol> modifiedCells = new List<RowAndCol>();
        // 初始的图层对象（可能在初始化窗口等场景下传入使用）
        private ILayer initLyr = null;
        // 存储要删除的行索引的列表
        private List<int> rowsToDelete = new List<int>();

        public RowAndCol[] pRowAndCol = new RowAndCol[10000];
        public System.Data.DataTable dt2;
        int count = 0;

        // 定义一个结构体，用于表示表格中的行、列以及对应单元格的值
        public struct RowAndCol
        {
            private int row;
            private int column;
            private string _value;
            // 结构体的构造函数，用于初始化行、列和值
            public int Row
            {
                get
                {
                    return row;
                }
                set
                {
                    row = value;
                }
            }
            // 获取列索引的属性
            public int Column
            {
                get
                {
                    return column;
                }
                set
                {
                    column = value;
                }
            }
            // 获取单元格值的属性
            public string Value
            {
                get
                {
                    return _value;
                }
                set
                {
                    _value = value;
                }
            }


        }
        #endregion

        // 实现类的构造函数（包括重载）      
        #region 构造函数
        // 构造函数，用于初始化窗口，传入一个hook对象，用于关联相关地图操作对象
        public FrmPropertyTable(object hook)
        {
            InitializeComponent();

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
            m_mapControl = (IMapControl3)m_hookHelper.Hook;
        }

        // 重载的构造函数，除了hook对象外，还传入一个初始的图层对象
        public FrmPropertyTable(object hook, ILayer Lyr)
        {
            InitializeComponent();

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;
            m_mapControl = (IMapControl3)m_hookHelper.Hook;

            initLyr = Lyr;
        }
        #endregion

        // 实现窗口的初始化
        #region 窗口初始化
        // 窗口加载事件处理函数，用于在窗口加载时填充图层选择下拉框的选项
        private void FrmPropertyTable1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < m_hookHelper.FocusMap.LayerCount; i++)
            {
                ILayer lyr = m_hookHelper.FocusMap.get_Layer(i);
                IFeatureLayer fLyr = lyr as IFeatureLayer;
                // 将图层名称添加到下拉框的选项列表中
                cbxLayerSelect.Properties.Items.Add(lyr.Name);
                if (initLyr != null)
                {
                    if (lyr.Name == initLyr.Name)
                    {
                        // 如果当前图层名称与传入的初始图层名称相同，则设置下拉框的选中项为当前图层
                        cbxLayerSelect.SelectedIndex = i;
                    }
                }
            }
        }
        #endregion

        // 实现属性表的显示
        #region 图层属性表显示
        // 根据图层选择来处理表格数据的函数，例如获取对应图层的属性数据并显示在表格中
        private void ProcessTableDataBasedOnLayerSelection()
        {
            int selectedIndex = cbxLayerSelect.SelectedIndex;
            gridControl1.DataSource = null;
            if (selectedIndex >= 0)
            {
                ILayer pLayer = m_hookHelper.FocusMap.get_Layer(selectedIndex);
                IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                IFeatureClass pFeatureClass = pFLayer.FeatureClass;

                if (pFeatureClass == null) return;

                System.Data.DataTable dt = new System.Data.DataTable();
                DataColumn dc = null;

                for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
                {

                    dc = new DataColumn(pFeatureClass.Fields.get_Field(i).Name);

                    dt.Columns.Add(dc);

                }

                IFeatureCursor pFeatureCuror = pFeatureClass.Search(null, false);
                IFeature pFeature = pFeatureCuror.NextFeature();

                DataRow dr = null;
                while (pFeature != null)
                {
                    dr = dt.NewRow();
                    for (int j = 0; j < pFeatureClass.Fields.FieldCount; j++)
                    {
                        if (pFeatureClass.FindField(pFeatureClass.ShapeFieldName) == j)
                        {

                            dr[j] = pFeatureClass.ShapeType.ToString();
                        }
                        else
                        {
                            dr[j] = pFeature.get_Value(j).ToString();

                        }
                    }

                    dt.Rows.Add(dr);
                    pFeature = pFeatureCuror.NextFeature();
                }
                gridControl1.DataSource = dt;
                dt2 = dt;

            }
        }
     
        #endregion

        // 实现属性表的编辑（单元格修改、要素的删除、增加字段等）
        #region 属性表编辑



        // 表格单元格值改变事件处理函数，将修改的单元格信息记录到modifiedCells列表中
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
        
            if (gridView1 != null)
            {
                // 获取发生变化的单元格所在的行和列的索引
                int rowIndex = e.RowHandle;
                int colIndex = e.Column.AbsoluteIndex;
                // 获取新值
                string newValue = e.Value.ToString();
                // 将信息存储到 pRowAndCol 数组中
                pRowAndCol[count].Row = rowIndex;
                pRowAndCol[count].Column = colIndex;
                pRowAndCol[count].Value = newValue;
                // 增加 count 的值，为下一次存储做准备
                count++;
            }
        }

        // 切换编辑开关状态改变事件处理函数，用于控制表格是否可编辑以及相关按钮的可用性
        private void switchEdit_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BarToggleSwitchItem checkEdit = (BarToggleSwitchItem)sender;
            gridView1.OptionsBehavior.Editable = checkEdit.Checked;
            btnDelFeature.Enabled = checkEdit.Checked;
            btnAddFields.Enabled = checkEdit.Checked;
            btnSave.Enabled = !checkEdit.Checked;
            cbxLayerSelect.Enabled = !checkEdit.Checked;
        }

        // 图层选择下拉框的选中项改变事件处理函数，用于重新加载对应图层的属性表数据到表格中，并清除之前的高亮显示
        private void cbxLayerSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearHighlight();
            if (cbxLayerSelect.SelectedIndex >= 0)
            {
                gridView1.Columns.Clear();
                modifiedCells.Clear();
                rowsToDelete.Clear();
                try
                {
                    ProcessTableDataBasedOnLayerSelection();
                    //TableLoad();
                }

                catch(Exception ex)
                {
                    MessageBox.Show("发生错误" +ex);
                    return;
                }

            }
        }


        // 删除要素按钮点击事件处理函数，获取表格中选中的行，将对应行索引添加到要删除的行列表中，并从表格中删除对应行
        private void btnDelFeature_ItemClick(object sender, ItemClickEventArgs e)
        {
            GridView gridView = gridControl1.FocusedView as GridView;
            if (gridView != null)
            {
                int[] selectedRows = gridView.GetSelectedRows();
                if (selectedRows.Length > 0)
                {
                    for (int i = selectedRows.Length - 1; i >= 0; i--)
                    {
                        int rowHandle = selectedRows[i];
                        var row = gridView.GetRow(rowHandle);

                        if (row != null)
                        {              
                            string strInt=gridView.GetRowCellValue(rowHandle, gridView.Columns[0]).ToString();
                            int firstColumnValue = Convert.ToInt32(strInt);
                            rowsToDelete.Add(firstColumnValue);
                        }
                        gridView.DeleteRow(rowHandle);
                    }
                }
            }
        }

        private void SaveDelete()
        {
            int selectedIndex = cbxLayerSelect.SelectedIndex;
            ILayer pLayer = m_hookHelper.FocusMap.get_Layer(selectedIndex);
            IFeatureLayer pFLayer = pLayer as IFeatureLayer;
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "";
            IFeatureClass pFeatureClass = pFLayer.FeatureClass;
            ITable pTable;
            pTable = pFLayer as ITable;
            if (rowsToDelete.Count > 0)
            {
                IWorkspaceEdit workspaceEdit = (pTable as IDataset).Workspace as IWorkspaceEdit;
               // try
              //  {
                    workspaceEdit.StartEditing(true);
                    workspaceEdit.StartEditOperation();

                    foreach (int rowHandle in rowsToDelete)
                    {
                        IRow pRow = pTable.GetRow(rowHandle);
                        if (pRow != null)
                        {
                            pRow.Delete();
                        }
                    }
                    workspaceEdit.StopEditOperation();
                    workspaceEdit.StopEditing(true);
                    rowsToDelete.Clear();
               // }
              //  catch (Exception ex)
              //  {
                 //   workspaceEdit.StopEditOperation();
               //     workspaceEdit.StopEditing(false);
                 //   MessageBox.Show($"删除操作出现错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //  }
            }
        }


        // 保存按钮点击事件处理函数，用于将表格中修改的数据保存回对应的图层数据源，并处理删除行等操作
        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SaveEdit();
                SaveDelete();
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK);
            }

            catch (Exception ex)
            {
                MessageBox.Show("保存失败" + ex);
            }
        }

        private void SaveEdit()
        {
        
                int selectIndex = cbxLayerSelect.SelectedIndex;
                ILayer pLayer = m_hookHelper.FocusMap.get_Layer(selectIndex);
                IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                IFeatureClass pFeatureClass = pFLayer.FeatureClass;
                ITable pTable;
                //pTable = pFeatureClass.CreateFeature().Table;//很重要的一种获取shp表格的一种方式         
                pTable = pFLayer as ITable;
                //将改变的记录值传给shp中的表
                int i = 0;
                while (pRowAndCol[i].Column != 0 || pRowAndCol[i].Row != 0)
                {
                    IRow pRow;
                    pRow = pTable.GetRow(pRowAndCol[i].Row+1);
                    pRow.set_Value(pRowAndCol[i].Column, pRowAndCol[i].Value);
                    pRow.Store();
                    i++;
                }
                count = 0;
                for (int j = 0; j < i; j++)
                {
                    pRowAndCol[j].Row = 0;
                    pRowAndCol[j].Column = 0;
                    pRowAndCol[j].Value = null;
                }
               

            m_mapControl.Refresh();

        }

        #endregion

        // 实验将属性表导出为xls
        #region 属性表导出
        // 将表格数据导出到Excel文件的按钮点击事件处理函数
        private void btnToExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Data.DataTable dataTable = gridControl1.DataSource as System.Data.DataTable;
            if (dataTable == null)
            {
                MessageBox.Show("没有可导出的数据，请先选择图层并加载属性表数据。");
                return;
            }

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
            Worksheet worksheet = (Worksheet)workbook.ActiveSheet;

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
            }

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                for (int colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
                {
                    worksheet.Cells[rowIndex + 2, colIndex + 1] = dataTable.Rows[rowIndex][colIndex].ToString();
                }
            }

            worksheet.Columns.AutoFit();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel文件 (*.xls)|*.xls";
            saveFileDialog.Title = "保存为Excel文件";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog.FileName);
            }

            workbook.Close(false);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        #endregion

        //实验要素选择并高亮
        #region 要素选择
        // 根据查询条件在地图上高亮显示符合条件的要素的函数
        private void HighlightFeaturesByQuery(IQueryFilter queryFilter)
        {
            ILayer mLayer;
            // 获取当前在图层选择下拉框中选中的图层
            mLayer = m_hookHelper.FocusMap.get_Layer(cbxLayerSelect.SelectedIndex);
            IFeatureLayer pFLayer = mLayer as IFeatureLayer;
            if (pFLayer != null)
            {
                IFeatureSelection pFeatSelection;
                // 将要素图层转换为要素选择对象，用于后续根据条件选择要素
                pFeatSelection = pFLayer as IFeatureSelection;
                // 根据传入的查询条件选择要素，这里选择的是新建选择集（替换之前的选择结果），并且不进行扩展选择
                pFeatSelection.SelectFeatures(queryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

                IViewRefresh viewRefresh = m_mapControl.Map as IViewRefresh;
                // 设置渐进式绘制为真，可能用于优化地图刷新显示效果，尤其是在处理大量要素时
                viewRefresh.ProgressiveDrawing = true;
                // 刷新指定的图层，使得地图上对应的要素高亮显示效果生效
                viewRefresh.RefreshItem(mLayer);
            }
        }

        // 表格行点击事件处理函数，根据点击行获取相关条件，构建查询条件并在地图上高亮显示对应要素，或者清除高亮显示（如果没有有效条件）
        private void GridViewRowClickHandler(object sender, RowClickEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView != null)
            {
                IQueryFilter pQuery = new QueryFilterClass();
                int count = gridView.SelectedRowsCount;

                // 获取第一列的字段名
                string firstColumnName = gridView.Columns[0].FieldName;
                string[] possibleColumns = { firstColumnName };
                string val = "";

                // 遍历可能用于构建查询条件的列（这里是OBJECTID相关列），尝试获取对应单元格的值作为查询条件
                foreach (string col in possibleColumns)
                {
                    try
                    {
                        if (count == 1)
                        {
                            val = gridView.GetRowCellValue(gridView.GetSelectedRows()[0], col).ToString();
                            if (!string.IsNullOrEmpty(val))
                            {
                                break;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < count; i++)
                            {
                                string tempVal = gridView.GetRowCellValue(gridView.GetSelectedRows()[i], col).ToString();
                                if (!string.IsNullOrEmpty(tempVal))
                                {
                                    val = tempVal;
                                    break;
                                }
                            }
                            if (!string.IsNullOrEmpty(val))
                            {
                                break;
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

                if (!string.IsNullOrEmpty(val))
                {
                    if (count == 1)
                    {
                        // 如果只选中了一行，构建简单的相等查询条件（例如 OBJECTID = 具体值）
                        pQuery.WhereClause = possibleColumns[0] + "=" + val;
                        HighlightFeaturesByQuery(pQuery);
                    }
                    else
                    {
                        string condition = "";
                        // 如果选中了多行，构建 OR 连接的多个相等查询条件（例如 OBJECTID = 值1 OR OBJECTID = 值2 等）
                        foreach (int rowHandle in gridView.GetSelectedRows())
                        {
                            string rowVal = gridView.GetRowCellValue(rowHandle, possibleColumns[0]).ToString();
                            condition += possibleColumns[0] + "=" + rowVal + " OR ";
                        }
                        if (condition.Length > 0)
                        {
                            // 移除最后多余的 " OR " 字符串
                            condition = condition.Remove(condition.Length - 4);
                        }
                        pQuery.WhereClause = condition;
                        HighlightFeaturesByQuery(pQuery);
                    }
                }
                else
                {
                    // 如果没有获取到有效的查询条件值，则清除地图上的高亮显示
                    ClearHighlight();
                }
            }
        }

        // 清除地图上所有要素的高亮显示的函数
        private void ClearHighlight()
        {
            IEnumLayer enumLayer = m_mapControl.Map.get_Layers(null, true);
            enumLayer.Reset();
            ILayer layer = enumLayer.Next();
            while (layer != null)
            {
                IFeatureLayer pFLayer = layer as IFeatureLayer;
                if (pFLayer != null)
                {
                    IFeatureSelection pFeatSelection = pFLayer as IFeatureSelection;
                    if (pFeatSelection != null)
                    {
                        // 清除当前要素图层上的要素选择（即清除高亮显示）
                        pFeatSelection.Clear();
                    }
                    IViewRefresh viewRefresh = m_mapControl.Map as IViewRefresh;
                    if (viewRefresh != null)
                    {
                        // 设置渐进式绘制为真，可能用于优化地图刷新显示效果，尤其是在处理大量要素时
                        viewRefresh.ProgressiveDrawing = true;
                        // 刷新当前图层，使得清除高亮显示的效果生效
                        viewRefresh.RefreshItem(layer);
                    }
                }
                layer = enumLayer.Next();
            }
        }
        #endregion

        #region 添加字段
        private void btnAddFields_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                if (cbxLayerSelect.SelectedIndex >= 0)
                {
                    int selectedIndex = cbxLayerSelect.SelectedIndex;
                    ILayer lyr = m_hookHelper.FocusMap.get_Layer(selectedIndex);
                    IFeatureLayer fLyr = lyr as IFeatureLayer;
                    FrmAddField frmAddField = new FrmAddField(m_hookHelper, fLyr);
                    frmAddField.Show();
                    MessageBox.Show("创建成功");
                    ProcessTableDataBasedOnLayerSelection();
                }
                else
                {
                    MessageBox.Show("请选择图层");
                }
            }

            catch
            {
                MessageBox.Show("异常错误");
            }

        }
        #endregion


        //实现窗口关闭清空选择
        #region 窗口关闭事件
        // 窗口关闭事件处理函数，在用户尝试关闭窗口时，弹出确认提示框，若用户选择不关闭则取消关闭操作，并清除地图上的高亮显示
        private void FrmPropertyTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("您确定要关闭属性表吗？", "关闭提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            ClearHighlight();
        }
        #endregion

  
    }

}

