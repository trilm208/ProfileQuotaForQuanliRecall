using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

public static class DevExpressGridControl
{
    public static void Populate(this GridControl grid, DataView view)
    {
        GridControl_Populate(grid, view);
    }

    public static void Populate(this GridControl grid, DataTable table)
    {
        GridControl_Populate(grid, table);
    }

    private static void GridControl_Populate(GridControl grid, object dataSource)
    {
        string last = "";

        var view = grid.FocusedView as GridView;
        if (view.RowCount > 0)
        {
            last = view.GetDataRow(0).Item(0);
        }

        grid.DataSource = dataSource;
        grid.BestFitColumns();

        for (int i = 0; i < view.RowCount; i++)
        {
            var id = view.GetDataRow(i).Item(0);
            if (id == last)
            {
                view.SelectRow(i);
                view.FocusedRowHandle = i;
                view.MakeRowVisible(i);
                return;
            }
        }
    }

    public static void RecursExpand(this GridView masterView, int masterRowHandle)
    {
        // Prevent excessive visual updates.
        masterView.BeginUpdate();
        try
        {
            // Get the number of master-detail relationships.
            int relationCount = masterView.GetRelationCount(masterRowHandle);
            // Iterate through relationships.
            for (int index = relationCount - 1; index >= 0; index--)
            {
                // Open the detail View for the current relationship.
                masterView.ExpandMasterRow(masterRowHandle, index);
                // Get the detail View.
                var childView = masterView.GetDetailView(masterRowHandle, index) as GridView;
                if (childView != null)
                {
                    // Get the number of rows in the detail View.
                    int childRowCount = childView.DataRowCount;
                    // Expand child rows recursively.
                    for (int handle = 0; handle < childRowCount; handle++)
                        RecursExpand(childView, handle);
                }
            }
        }
        finally
        {
            // Enable visual updates.
            masterView.EndUpdate();
        }
    }

    public static void BestFitColumns(this GridControl grid)
    {
        var gridView = grid.DefaultView as GridView;
        if (gridView != null)
        {
            foreach (GridColumn column in gridView.Columns)
            {
                column.BestFit();
            }

            gridView.OptionsView.ColumnAutoWidth = true;
        }

        var bandedGridView = grid.DefaultView as BandedGridView;
        if (bandedGridView != null)
        {
            foreach (BandedGridColumn column in bandedGridView.Columns)
            {
                column.BestFit();
            }

            gridView.OptionsView.ColumnAutoWidth = true;
        }
    }

    public static DataRow GetSelectedDataRow(this GridControl grid)
    {
        var view = grid.FocusedView as GridView;
        var selected = view.GetSelectedRows();

        if (selected.Length > 0)
            return view.GetDataRow(selected.First());

        return null;
    }

    public static int GetRowsHeight(this GridControl grid)
    {
        var graphics = grid.CreateGraphics();
        var view = grid.MainView;

        PropertyInfo pi = view.GetType().GetProperty("ViewInfo", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        var viewInfo = pi.GetValue(view, null) as GridViewInfo;

        int height = 0;

        for (int rowHandle = 0; rowHandle < view.RowCount; rowHandle++)
        {
            height += viewInfo.CalcRowHeight(graphics, rowHandle, 0);
        }

        return height + 2;
    }

    public static int GetHeightFull(this GridControl grid)
    {
        var graphics = grid.CreateGraphics();
        var view = grid.MainView;

        PropertyInfo pi = view.GetType().GetProperty("ViewInfo", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        var viewInfo = pi.GetValue(view, null) as GridViewInfo;

        int height = viewInfo.ColumnRowHeight;

        for (int rowHandle = 0; rowHandle < view.RowCount; rowHandle++)
        {
            height += viewInfo.CalcRowHeight(graphics, rowHandle, 0);
        }

        return height + 2 + 20;
    }

    public static void FilterContain(this GridControl grid, string criteria)
    {
        FilterContain(grid, criteria, string.Empty);
    }

    public static void Filter(this GridControl grid, string criteria)
    {
        Filter(grid, criteria, string.Empty);
    }

    public static void FilterGrid(this GridControl grid, System.Collections.Generic.List<string> columnNames, string criteria)
    {
        FilterGrid(grid, columnNames, criteria, string.Empty);
    }

    public static void FilterGrid(this GridControl grid, System.Collections.Generic.List<string> columnNames, string criteria, string query_add)
    {
        GridView view = grid.MainView as GridView;
        string query = "";

        criteria = criteria.Trim().ToLower();

        foreach (var item in criteria.Split(' '))
        {
            string subquery = "";

            if (item.Length == 0) continue;

            foreach (GridColumn col in view.Columns) // foreach (GridColumn col in view.VisibleColumns)
            {
                if (col.FieldName.Length == 0) continue;
                if (columnNames.Contains(col.FieldName) == false) continue;

                if (subquery.Length > 0)
                    subquery += " OR ";

                subquery += String.Format("[{0}] LIKE '%{1}%'", col.FieldName, EscapeLikeValue(item));
            }

            if (subquery.Length > 0)
            {
                if (query.Length > 0)
                    query += " AND ";

                query += String.Format("({0})", subquery);
            }
        }

        if (query_add.Length > 0 && query.Length > 0)
        {
            query += String.Format(" OR ({0})", query_add);
        }

        view.ActiveFilterString = query;
    }

    public static void FilterContain(this GridControl grid, string criteria, string query_add)
    {
        GridView view = grid.MainView as GridView;
        string query = "";

        criteria = criteria.Trim().ToLower();

        //foreach (var item in criteria.Split(' '))
        //{
        string subquery = "";

        //if (item.Length == 0) continue;

        foreach (GridColumn col in view.Columns) // foreach (GridColumn col in view.VisibleColumns)
        {
            if (col.FieldName.Length == 0) continue;

            if (subquery.Length > 0)
                subquery += " OR ";

            // subquery += String.Format("[{0}] CONTAINS '%{1}%'", col.FieldName, EscapeLikeValue(criteria));
            subquery += String.Format("CONTAINS ({0},'{1}')", col.FieldName, EscapeLikeValue(criteria)); //'%{1}%'", col.FieldName, EscapeLikeValue(criteria));
        }

        if (subquery.Length > 0)
        {
            if (query.Length > 0)
                query += " AND ";

            query += String.Format("({0})", subquery);
        }
        //}

        if (query_add.Length > 0 && query.Length > 0)
        {
            query += String.Format(" OR ({0})", query_add);
        }

        view.ActiveFilterString = query;
    }

    public static void Filter(this GridControl grid, string criteria, string query_add)
    {
        GridView view = grid.MainView as GridView;
        string query = "";

        criteria = criteria.Trim().ToLower();

        //foreach (var item in criteria.Split(' '))
        //{
        string subquery = "";

        //if (item.Length == 0) continue;

        foreach (GridColumn col in view.Columns) // foreach (GridColumn col in view.VisibleColumns)
        {
            if (col.FieldName.Length == 0) continue;

            if (subquery.Length > 0)
                subquery += " OR ";

            subquery += String.Format("[{0}] LIKE '%{1}%'", col.FieldName, EscapeLikeValue(criteria));
        }

        if (subquery.Length > 0)
        {
            if (query.Length > 0)
                query += " AND ";

            query += String.Format("({0})", subquery);
        }
        //}

        if (query_add.Length > 0 && query.Length > 0)
        {
            query += String.Format(" OR ({0})", query_add);
        }

        view.ActiveFilterString = query;
    }

    public static void Filter(this GridControl grid, string colName, DateTime FromDate, DateTime ToDate)
    {
        GridView view = grid.MainView as GridView;
        DevExpress.Data.Filtering.CriteriaOperator filter = new DevExpress.Data.Filtering.GroupOperator(DevExpress.Data.Filtering.GroupOperatorType.And,

                new DevExpress.Data.Filtering.BinaryOperator("Date", FromDate, DevExpress.Data.Filtering.BinaryOperatorType.GreaterOrEqual),

                new DevExpress.Data.Filtering.BinaryOperator("Date", ToDate, DevExpress.Data.Filtering.BinaryOperatorType.Less));

        string filterDisplayText = String.Format(colName + " is between {0:d} and {1:d}", FromDate, ToDate);

        ColumnFilterInfo dateFilter = new ColumnFilterInfo(filter.ToString(), filterDisplayText);

        view.Columns[colName].FilterInfo = dateFilter;
    }

    public static void Filter(this GridControl grid, string colName, string criteria, string query_add)
    {
        GridView view = grid.MainView as GridView;
        string query = "";

        criteria = criteria.Trim().ToLower();

        foreach (var item in criteria.Split(' '))
        {
            string subquery = "";

            if (item.Length == 0) continue;

            foreach (GridColumn col in view.Columns)
            {
                if (col.FieldName == colName)
                {
                    if (col.FieldName.Length == 0) continue;

                    if (subquery.Length > 0)
                        subquery += " OR ";

                    subquery += String.Format("[{0}] LIKE '%{1}%'", col.FieldName, EscapeLikeValue(item));
                }
            }

            if (subquery.Length > 0)
            {
                if (query.Length > 0)
                    query += " AND ";

                query += String.Format("({0})", subquery);
            }
        }

        if (query_add.Length > 0 && query.Length > 0)
        {
            query += String.Format(" OR ({0})", query_add);
        }

        view.ActiveFilterString = query;
    }

    private static string EscapeLikeValue(string valueWithoutWildcards)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < valueWithoutWildcards.Length; i++)
        {
            char c = valueWithoutWildcards[i];
            if (c == '*' || c == '%' || c == '[' || c == ']')
                sb.Append("[").Append(c).Append("]");
            else if (c == '\'')
                sb.Append("''");
            else
                sb.Append(c);
        }
        return sb.ToString();
    }

    public static bool ExportToFile(this GridControl gridTemplate)
    {
        try
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter =
                    "Excel (2010) (.xlsx)|*.xlsx |Excel (2003)(.xls)|*.xls|RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;

                    //gridTemplate.ExportToXlsx(exportFilePath);
                   
                    switch (fileExtenstion)
                    {
                        case ".xls":
                            gridTemplate.ExportToXls(exportFilePath);
                            return true;
                            break;

                        case ".xlsx":                        
                            gridTemplate.ExportToXlsx(exportFilePath);
                            return true;
                            break;

                        case ".rtf":
                            gridTemplate.ExportToRtf(exportFilePath);
                            return true;
                            break;

                        case ".pdf":

                            gridTemplate.ExportToPdf(exportFilePath);
                            return true;
                            break;

                        case ".html":
                            gridTemplate.ExportToHtml(exportFilePath);
                            return true;
                            break;

                        case ".mht":
                            gridTemplate.ExportToMht(exportFilePath);
                            return true;
#pragma warning disable CS0162 // Unreachable code detected
                            break;
#pragma warning restore CS0162 // Unreachable code detected

                        default:
                            return false;
                            break;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return false;
        }
    }

    public static bool SelectARowInGridView(this GridView gridView1, string columnName, string value)
    {
        if (value == "0" || value == QA.QAFunction.DefaultGuid())
        {
            gridView1.SelectRow(0);
            gridView1.FocusedRowHandle = 0; gridView1.MakeRowVisible(0);
            return false;
        }
        else
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                string id = gridView1.GetDataRow(i).Item(columnName).ToUpper();
                if (id == value.ToUpper())
                {
                    gridView1.SelectRow(i);
                    gridView1.FocusedRowHandle = i;
                    gridView1.MakeRowVisible(i);
                    return true;
                }
            }
        }
        return false;
    }

    public static string GetSplit(this GridView gv, string colName, string Delimiter)
    {
        string s = "";

        for (int i = 0; i < gv.DataRowCount; i++)
        {
            if (i == 0)
            {
                s += gv.GetDataRow(i).Item(colName);
            }
            else
            {
                s += Delimiter + gv.GetDataRow(i).Item(colName);
            }
        }
        return s;
    }

   

    public static void ExpandAllRows(this GridView View)
    {
        View.BeginUpdate();
        try
        {
            int dataRowCount = View.DataRowCount;
            for (int rHandle = 0; rHandle < dataRowCount; rHandle++)
                View.SetMasterRowExpanded(rHandle, true);
        }
        finally
        {
            View.EndUpdate();
        }
    }
}