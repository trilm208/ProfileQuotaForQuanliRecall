using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QA.Shared.LogicCheck
{
    public static class LogicCheck
    {
        private static object GetFieldValue(DataTable data, string FieldName)
        {
            var type = data._FindValue("FieldType", "FieldName", FieldName);
            if (type == "string")
            {
                return ConvertSafe.ToString(data._FindValue("FieldValue", "FieldName", FieldName));
            }

            if (type == "double")
            {
                return Convert.ToDouble((data._FindValue("FieldValue", "FieldName", FieldName)));
            }
            return null;

        }
        private static Double CountTExpression(string query_check, DataTable data)
        {
            query_check = query_check.Replace("COUNT", "").Replace("(", "").Replace(")", "");

            string[] parts = System.Text.RegularExpressions.Regex.Split(query_check, ";");

            int count = 0;
            foreach (string part in parts)
            {
                if (checkLogicAND(part, data) == true)
                {
                    count++;
                }
            }

            return count;

        }
        public static bool checkLogicAND(string query_check, DataTable data)
        {
            string[] lines = System.Text.RegularExpressions.Regex.Split(query_check, "AND");
            bool result = true;
            foreach (string c_line in lines)
            {
                #region #moi line
                // TIM MAY THANG COUNT MA REPLACE
                string line = c_line;

                //if (line.Contains("COUNT"))
                //{ 
                //    line.Replace("COUNT(", "");
                //    line.Replace(")", "");
                //    var s_count = System.Text.RegularExpressions.Regex.Split(line, ")");

                //    string[] lines_count = System.Text.RegularExpressions.Regex.Split(query_check, ";");

                //}
                char pheptinhsosanh = ' ';
                if (line.Contains("="))
                {
                    pheptinhsosanh = '=';
                }
                else
                {
                    if (line.Contains(">"))
                    {
                        pheptinhsosanh = '>';
                    }
                    else
                    {
                        if (line.Contains("<"))
                        {
                            pheptinhsosanh = '<';
                        }
                        else
                        {
                            if (line.Contains("#"))
                            {
                                pheptinhsosanh = '#';
                            }
                            else
                            {

                            }
                        }
                    }
                }
                #region chung
                if (line.Contains(pheptinhsosanh.ToString()))
                {
                    string[] parts = System.Text.RegularExpressions.Regex.Split(line, pheptinhsosanh.ToString());


                    var leftparts = parts[0].Split('+');

                    object leftpartvalue;

                    string templeftString = "UNKNOWN";

                    Double templeftDecimal = -99999;

                    foreach (string leftpart in leftparts)
                    {


                        var leftpartname = leftpart;
                        if (leftpart.Contains("COUNT"))
                        {
                            if (templeftDecimal == -99999)
                                templeftDecimal = 0;
                            templeftDecimal += CountTExpression(leftpart, data);
                        }


                        else
                        {
                            if (leftpart.Contains('@'))
                            {
                                leftpartname = leftpart.Replace('@', ' ').Trim();
                                //leftpartvalue=

                                var sub_leftpartvalue = GetFieldValue(data, leftpartname);
                                if (sub_leftpartvalue.GetType() == typeof(string))
                                {
                                    if (templeftString == "UNKNOWN")
                                        templeftString = "";
                                    templeftString += sub_leftpartvalue.ToString();
                                }


                                if (sub_leftpartvalue.GetType() == typeof(Double))
                                {
                                    if (templeftDecimal == -99999)
                                        templeftDecimal = 0;
                                    templeftDecimal += (Double)sub_leftpartvalue;
                                }

                                else
                                {
                                    if (leftpart.Contains('"'))
                                    {
                                        if (templeftString == "UNKNOWN")
                                            templeftString = "";
                                        templeftString += leftpart.Replace('"', ' ').Trim();
                                    }
                                    else
                                    {
                                        if (templeftDecimal == -99999)
                                            templeftDecimal = 0;
                                        templeftDecimal += Convert.ToDouble(leftpart);
                                    }
                                }
                            }
                        }
                    }
                    if (templeftDecimal != -99999)
                    {
                        leftpartvalue = templeftDecimal;
                    }
                    else
                    {
                        if (templeftString == "UNKNOWN")
                        {
                            leftpartvalue = templeftString;
                        }
                        else
                        {
                            leftpartvalue = "";
                        }
                    }

                    var rightparts = parts[1].Split('+');

                    object rightpartvalue;

                    string temprightString = "UNKNOWN";

                    Double temprightDecimal = -99999;

                    foreach (string rightpart in rightparts)
                    {


                        var rightpartname = rightpart;
                        if (rightpart.Contains('@'))
                        {
                            rightpartname = rightpart.Replace('@', ' ').Trim();
                            //rightpartvalue=

                            var sub_rightpartvalue = GetFieldValue(data, rightpartname);
                            if (sub_rightpartvalue.GetType() == typeof(string))
                            {
                                if (temprightString == "UNKNOWN")
                                    temprightString = "";
                                temprightString += sub_rightpartvalue.ToString();
                            }


                            if (sub_rightpartvalue.GetType() == typeof(Double))
                            {
                                if (temprightDecimal == -99999)
                                    temprightDecimal = 0;
                                temprightDecimal += (Double)sub_rightpartvalue;
                            }

                        }
                        else
                        {
                            if (rightpart.Contains("COUNT"))
                            {
                                if (temprightDecimal == -99999)
                                    temprightDecimal = 0;
                                temprightDecimal += CountTExpression(rightpart, data);
                            }
                            else
                            {
                                if (rightpart.Contains('"'))
                                {
                                    if (temprightString == "UNKNOWN")
                                        temprightString = "";
                                    temprightString += rightpart.Replace('"', ' ').Trim();
                                }
                                else
                                {
                                    if (temprightDecimal == -99999)
                                        temprightDecimal = 0;
                                    temprightDecimal += Convert.ToDouble(rightpart);
                                }
                            }
                        }

                    }
                    if (temprightDecimal != -99999)
                    {
                        rightpartvalue = temprightDecimal;
                    }
                    else
                    {
                        if (temprightString == "UNKNOWN")
                        {
                            rightpartvalue = temprightString;
                        }
                        else
                        {
                            rightpartvalue = "";
                        }
                    }

                    if (leftpartvalue.GetType() != rightpartvalue.GetType())
                    {
                        return false;
                    }

                    if (pheptinhsosanh == '=')
                    {

                        if (leftpartvalue.GetType() == typeof(Double) && rightpartvalue.GetType() == typeof(Double))
                        {
                            if ((Double)(leftpartvalue) != (Double)rightpartvalue)
                                return false;
                        }
                        if (leftpartvalue.GetType() == typeof(String) && rightpartvalue.GetType() == typeof(String))
                        {
                            if (leftpartvalue.ToString() != rightpartvalue.ToString())
                                return false;
                        }

                    }
                    if (pheptinhsosanh == '>')
                    {

                        if (leftpartvalue.GetType() == typeof(String))
                            return false;

                        if (leftpartvalue.GetType() == typeof(Double))
                        {
                            if ((Double)leftpartvalue <= (Double)rightpartvalue)
                                return false;
                        }

                    }
                    if (pheptinhsosanh == '<')
                    {

                        if (leftpartvalue.GetType() == typeof(String))
                            return false;

                        if (leftpartvalue.GetType() == typeof(Double))
                        {
                            if ((Double)leftpartvalue >= (Double)rightpartvalue)
                                return false;
                        }

                    }
                    if (pheptinhsosanh == '#')
                    {

                        if (leftpartvalue.GetType() == typeof(Double))
                        {
                            if ((Double)leftpartvalue == (Double)rightpartvalue)
                                return false;


                        }
                        if (leftpartvalue.GetType() == typeof(String))
                        {
                            if ((String)leftpartvalue == (String)rightpartvalue)
                                return false;
                        }

                    }
                }
                else
                {

                }
                #endregion
                #endregion
            }
            return result;

        }
        public static bool checkLogic(DataTable filterTable,  DataTable dataTable, string _variableName)
        {

            if (filterTable == null || dataTable == null)
                return true;
            DataRow[] logicRows = filterTable.Select(string.Format("VariableName='{0}'", _variableName));
            if (logicRows.Count() == 0)
            {
                //nextx page
                return true;
                // ko hien len
            }
            else
            {
                var dtValue = PivotTable(dataTable);
                foreach (DataRow row in logicRows)
                {
                    try
                    {
                        var resultlogic = dtValue.Select(row.Item("FilterCondition"));

                        if (resultlogic.Count() > 0)
                        {
                            return false;

                            //logic OK go to next page
                        }
                        else
                        {

                        }
                    }
                    catch(Exception E)
                    {
                        UI.ShowError(E.Message);
                        return false;
                    }
                 

                }
            }
            return true;

        }

        private static DataTable PivotTable(DataTable dt)
        {
            DataTable result = new DataTable();

            foreach (DataRow row in dt.Rows)
            {
                if (result.Columns.Contains(row.Item("FieldName")) == false)
                {
                    Type fieldType;
                    fieldType = typeof(String);
                    if (row.Item("FieldType") == "string")
                    {
                        fieldType = typeof(String);
                    }
                    if (row.Item("FieldType") == "double")
                    {
                        fieldType = typeof(Double);
                    }
                    DataColumn dc = new DataColumn(row.Item("FieldName"), fieldType);
                    result.Columns.Add(dc);
                }
            }
            DataRow new_row = result.NewRow();
            foreach (DataRow row in dt.Rows)
            {
                DataColumn dc = result.Columns[row.Item("FieldName")];
                if (dc.DataType == typeof(Double))
                {
                    if (row.Item("FieldValue").IsEmpty())
                    {
                        new_row[row.Item("FieldName")] = 0;
                    }
                    else
                    {
                        new_row[row.Item("FieldName")] = Convert.ToDouble(row.Item("FieldValue"));
                    }
                }
                else
                {
                    new_row[row.Item("FieldName")] = row.Item("FieldValue");
                }
            }
            result.Rows.Add(new_row);
            result.AcceptChanges();
            return result;
        }

    }
}
