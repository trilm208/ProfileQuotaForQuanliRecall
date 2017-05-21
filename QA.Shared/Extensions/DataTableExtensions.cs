using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace System
{
    public static class DataTableExtensions
    {

        public static DataTable PivotTable(this DataTable dt,DataTable colDt)
        {
            
            var result = new DataTable();

            result.Columns.Add("QCNo",typeof(string));

            foreach (DataRow colName in colDt.Rows)
            {
                result.Columns.Add(colName.Item(0), typeof(string));
            }

            foreach (DataRow row in dt.Rows)
            {
                // kiem tra ton tai ID chua
                if (result.IsContainValue(row.Item("QCNo"), "QCNo") == false)
                {
                    var NewRow = result.NewRow();

                    NewRow["QCNo"] = row.Item("QCNo");
                    result.Rows.Add(NewRow);
                }

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (result.Rows[i].Item("QCNo") == row.Item("QCNo"))
                    {
                        try
                        {
                            result.Rows[i][row.Item("FieldName")] = row.Item("FieldValue");
                        }
                        catch
                        {
                        }
                    }
                }
                result.AcceptChanges();
            }
            
            return result;
        }
        public static DataTable GetInversedDataTable(this DataTable table, string columnX,
                                             params string[] columnsToIgnore)
        {
            //Create a DataTable to Return
            DataTable returnTable = new DataTable();

            if (columnX == "")
                columnX = table.Columns[0].ColumnName;

            //Add a Column at the beginning of the table

            returnTable.Columns.Add(columnX);

            //Read all DISTINCT values from columnX Column in the provided DataTale
            List<string> columnXValues = new List<string>();

            //Creates list of columns to ignore
            List<string> listColumnsToIgnore = new List<string>();
            if (columnsToIgnore.Length > 0)
                listColumnsToIgnore.AddRange(columnsToIgnore);

            if (!listColumnsToIgnore.Contains(columnX))
                listColumnsToIgnore.Add(columnX);

            foreach (DataRow dr in table.Rows)
            {
                string columnXTemp = dr[columnX].ToString();
                //Verify if the value was already listed
                if (!columnXValues.Contains(columnXTemp) && listColumnsToIgnore.Contains(columnXTemp)==false)
                {
                    //if the value id different from others provided, add to the list of 
                    //values and creates a new Column with its value.
                    columnXValues.Add(columnXTemp);
                    returnTable.Columns.Add(columnXTemp);
                }
                else
                {
                    //Throw exception for a repeated value
                    throw new Exception("The inversion used must have " +
                                        "unique values for column " + columnX);
                }
            }

            //Add a line for each column of the DataTable

            foreach (DataColumn dc in table.Columns)
            {
                if (!columnXValues.Contains(dc.ColumnName) &&
                    !listColumnsToIgnore.Contains(dc.ColumnName))
                {
                    DataRow dr = returnTable.NewRow();
                    dr[0] = dc.ColumnName;
                    returnTable.Rows.Add(dr);
                }
            }

            //Complete the datatable with the values
            for (int i = 0; i < returnTable.Rows.Count; i++)
            {
                for (int j = 1; j < returnTable.Columns.Count; j++)
                {
                    returnTable.Rows[i][j] =
                      table.Rows[j - 1][returnTable.Rows[i][0].ToString()].ToString();
                }
            }

            return returnTable;
        }
        public static DataTable Delete(this DataTable table, string filter)
        {
            table.Select(filter).Delete();
            return table;
        }
        public static void Delete(this IEnumerable<DataRow> rows)
        {
            foreach (var row in rows)
                row.Delete();
        }
        public static IEnumerable<T> Convert<T>(this DataTable table)
        {
            List<T> data = new List<T>();

            var type = typeof(T);
            var fields = type.GetFields();

            foreach (DataRow row in table.Rows)
            {
                var item = (T)type.GetConstructor(Type.EmptyTypes).Invoke(null);

                foreach (var field in fields)
                {
                    var value = row[field.Name];
                    if (value == DBNull.Value)
                        value = null;

                    if (field.FieldType == typeof(int))
                    {
                        field.SetValue(item, System.Convert.ToInt32(value));
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        field.SetValue(item, System.Convert.ToBoolean(value));
                    }
                    else
                    {
                        field.SetValue(item, value);
                    }
                }

                data.Add(item);
            }

            return data;
        }
      
        public static List<DataRow> GetDifferenceRow(this DataTable dt1, DataTable dt2, string keyName)
        {
            //tim nhung row khác biệt có trong dt2 nhưng ko có trong dt1
            var results = new List<DataRow>();

            foreach (DataRow row in dt1.Rows)
            {
                if (dt2._FindIndex(keyName, row.Item(keyName)) < 0)
                {
                    results.Add(row);
                }
            }
            return results;
        }
        public static void ExportToExcel(this DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
               
            }
        }
        public static string GetSplit(this DataTable dt, string colName, string Delimiter, string criteria)
        {
            string s = "";

            var dr = dt.Select(criteria);
            if (dr.Length == 0)
            {
                return s;
            }

            for (int i = 0; i < dr.Length; i++)
            {
                var row = dr[i];
                if (row != null)
                {
                    if (i == 0)
                    {
                        s += row.Item(colName);
                    }
                    else
                    {
                        s += Delimiter + row.Item(colName);
                    }
                }
            }
            return s;
        }
       
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static DataTable MergeTable(this List<DataTable> listTable)
        {
            if (listTable.Count == 0)
            {
                return null;
            }
            var table_final = listTable[0];

            for (int i = 1; i < listTable.Count; i++)
            {
                if (listTable[i].Rows.Count == 0)
                    break;
                foreach (DataRow dr in listTable[i].Rows)
                {
                    DataRow newRow = table_final.NewRow();
                    newRow.ItemArray = dr.ItemArray;

                    table_final.Rows.Add(newRow);
                }
            }
            return table_final;
        }
        public static bool IsContainValue(this DataTable dt, string value, string columnname)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row.Item(columnname) == value)
                {
                    return true;
                }
            }
            return false;
        }
        public static string DataTableToJSON(this DataTable Dt)
        {
            if (Dt == null || Dt.Rows.Count == 0)
                return "";
            string[] StrDc = new string[Dt.Columns.Count];

            string HeadStr = string.Empty;
            for (int i = 0; i < Dt.Columns.Count; i++)
            {
                StrDc[i] = Dt.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\":\"" + StrDc[i] + i.ToString() + "¾" + "\",";
            }

            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

            StringBuilder Sb = new StringBuilder();

            Sb.Append("[");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                string TempStr = HeadStr;

                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString().Trim());
                }
                //Sb.AppendFormat("{{{0}}},",TempStr);

                Sb.Append("{" + TempStr + "},");
            }

            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

            if (Sb.ToString().Length > 0)
                Sb.Append("]");

            return Sb.ToString();
            return StripControlChars(Sb.ToString());
        }
         public static string GetJSONString(this DataTable table)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented); 
        }
       
        public static string DataTableToJSON(this DataTable Dt, List<string> list)
        {
            string[] StrDc = new string[Dt.Columns.Count];

            string HeadStr = string.Empty;
            for (int i = 0; i < Dt.Columns.Count; i++)
            {
                StrDc[i] = Dt.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\":\"" + StrDc[i] + i.ToString() + "¾" + "\",";
            }

            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

            StringBuilder Sb = new StringBuilder();

            Sb.Append("[");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                string TempStr = HeadStr;

                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    //TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString().Trim());

                    TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString().Trim());
                }
                //Sb.AppendFormat("{{{0}}},",TempStr);

                Sb.Append("{" + TempStr + "},");
            }

            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

            if (Sb.ToString().Length > 0)
                Sb.Append("]");

            return Sb.ToString();
            return StripControlChars(Sb.ToString());
        }

        //To strip control characters:

        //A character that does not represent a printable character but //serves to initiate a particular action.

        public static string StripControlChars(string s)
        {
            return Regex.Replace(s, @"[^\x20-\x7F]", "");
        }

        public static DataTable MergeAll(this IList<DataTable> tables, String primaryKeyColumn)
        {
            if (!tables.Any())
                throw new ArgumentException("Tables must not be empty", "tables");
            if (primaryKeyColumn != null)
                foreach (DataTable t in tables)
                    if (!t.Columns.Contains(primaryKeyColumn))
                        throw new ArgumentException("All tables must have the specified primarykey column " + primaryKeyColumn, "primaryKeyColumn");

            if (tables.Count == 1)
                return tables[0];

            DataTable table = new DataTable("TblUnion");
            table.BeginLoadData(); // Turns off notifications, index maintenance, and constraints while loading data
            foreach (DataTable t in tables)
            {
                table.Merge(t); // same as table.Merge(t, false, MissingSchemaAction.Add);
            }
            table.EndLoadData();

            if (primaryKeyColumn != null)
            {
                // since we might have no real primary keys defined, the rows now might have repeating fields
                // so now we're going to "join" these rows ...
                var pkGroups = table.AsEnumerable()
                    .GroupBy(r => r[primaryKeyColumn]);
                var dupGroups = pkGroups.Where(g => g.Count() > 1);
                foreach (var grpDup in dupGroups)
                {
                    // use first row and modify it
                    DataRow firstRow = grpDup.First();
                    foreach (DataColumn c in table.Columns)
                    {
                        if (firstRow.IsNull(c))
                        {
                            DataRow firstNotNullRow = grpDup.Skip(1).FirstOrDefault(r => !r.IsNull(c));
                            if (firstNotNullRow != null)
                                firstRow[c] = firstNotNullRow[c];
                        }
                    }
                    // remove all but first row
                    var rowsToRemove = grpDup.Skip(1);
                    foreach (DataRow rowToRemove in rowsToRemove)
                        table.Rows.Remove(rowToRemove);
                }
            }

            return table;
        }

        public static DataTable _CopyFromTableToTable(this DataTable _srcTable, List<String> ColumnsCopy, List<String> ColumnsBonus)
        {
            _srcTable.AcceptChanges();
            DataTable table = new DataTable();

            foreach (var columnName in ColumnsCopy)
            {
                table.Columns.Add(columnName, typeof(String));
            }
            foreach (var columnName in ColumnsBonus)
            {
                table.Columns.Add(columnName, typeof(String));
            }

            foreach (DataRow row in _srcTable.Rows)
            {
                DataRow _newRow = table.NewRow();
                foreach (var col in ColumnsCopy)
                {
                    _newRow[col] = row.Item(col);
                }
                table.Rows.Add(_newRow);
            }

            return table;
        }

        public static int _FindIndex(this DataTable _table, string columnName, string value)
        {
            if (_table == null || _table.Rows.Count == 0)
                return -2;
            for (int i = 0; i < _table.Rows.Count; i++)
            {
                if (_table.Rows[i].Item(columnName).Trim() == value.Trim())
                {
                    return i;
                }
            }
            return -1;
        }

        public static string _FindValue(this DataTable _table, string OutputColumnName, string InputcolumnName, string InputValue)
        {
            if (_table == null)
                return string.Empty;
            foreach (DataRow row in _table.Rows)
            {
                if (row.Item(InputcolumnName) == InputValue)
                {
                    return row.Item(OutputColumnName);
                }
            }
            return String.Empty;
        }
    }
}