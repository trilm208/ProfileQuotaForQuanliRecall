using DataAccess;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QA
{
    public static class QAFunction
    {

        public static string ReadFromConfigXMlFile(string fieldName)
        {
            string result = "";

            var doc = new System.Xml.XmlDocument();

            string path = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            if (System.IO.File.Exists(path))
            {
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config.xml");

                var entities = doc.SelectNodes("//Config/Entry");

                foreach (System.Xml.XmlNode node in entities)
                {
                    var name = node.Attributes.GetNamedItem("Name");
                    var value = node.Attributes.GetNamedItem("Value");

                    if (name == null || value == null)
                        continue;

                    if (name.Value == fieldName.Trim())
                        result = value.Value;
                }
            }

            return result.Trim();
        }
        public static string GetSPDateTimeServer()
        {
            return "ws_GetDatetimeServer";
        }

        public static string GetUserLogin(IClientServices services)
        {
            return services.GetInformation("UserID") == null ? "" : services.GetInformation("UserID").ToString();
        }

        public static DateTime GetTodayDate(IClientServices services)
        {
            return services.GetInformation("DateToday") == null ? DateTime.Today : Convert.ToDateTime(services.GetInformation("DateToday"));
        }

        public static string GetBacSiNoiTruKhamBenh(IClientServices services)
        {
            return services.GetInformation("UserID") == null ? QAFunction.DefaultDatabase() : services.GetInformation("UserID").ToString();
        }

        public static string DateTimeToString(this DateTime date)
        {
            return date.ToString("MM/dd/yyyy hh:mm:ss");
        }

        public static string GetServerIP()
        {
            string result = "";

            var doc = new System.Xml.XmlDocument();

            string path = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            if (System.IO.File.Exists(path))
            {
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config.xml");

                var entities = doc.SelectNodes("//Config/Entry");

                foreach (System.Xml.XmlNode node in entities)
                {
                    var name = node.Attributes.GetNamedItem("Name");
                    var value = node.Attributes.GetNamedItem("Value");

                    if (name == null || value == null)
                        continue;

                    if (name.Value == "WebService")
                        result = value.Value;
                }
            }

            return result.Split(':')[1].Replace('/', ' ').TrimStart();
        }

        public static string DefaultDatabase()
        {
            return "QAHosGenericDB";
        }

        public static string GetFacID(IClientServices services)
        {
            return services.GetInformation("FacID") == null ? "1" : services.GetInformation("FacID").ToString();
        }

        public static double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = QAFunction.ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        public static int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        public static string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public static string DefaultGuid()
        {
            return "00000000-0000-0000-0000-000000000000";
        }

        public static string GetValue(this DevExpress.XtraEditors.TextEdit textedit)
        {
            return textedit.Text.Trim();
        }

        public static void SetValue(this DevExpress.XtraEditors.TextEdit textedit, object value)
        {
            textedit.EditValue = value;
        }

        public static string ImageToStringBase64(this Image image)
        {
            var converter = new ImageConverter();
            var imgArray = (byte[])converter.ConvertTo(image, typeof(byte[]));
            return Convert.ToBase64String(imgArray);
        }

        public static Image StringBase64ToImage(this string base64String)
        {
            // Convert Base64 String to byte[]

            if (base64String.IsEmpty())
                return null;
            byte[] imageBytes = Convert.FromBase64String(base64String);
            var ms = new MemoryStream(imageBytes, 0,
                imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        public static void RemoveByValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue someValue)
        {
            var itemsToRemove = new List<TKey>();

            foreach (var pair in dictionary)
            {
                if (pair.Value.Equals(someValue))
                    itemsToRemove.Add(pair.Key);
            }

            foreach (TKey item in itemsToRemove)
            {
                dictionary.Remove(item);
            }
        }

        public static string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
    public static LayoutControlItem GetFindButtonLayoutItemClear(PopupBaseForm Form)
        {
            foreach (Control FormC in Form.Controls)
            {
                if (FormC is SearchEditLookUpPopup)
                {
                    SearchEditLookUpPopup SearchPopup = FormC as SearchEditLookUpPopup;
                    foreach (Control SearchPopupC in SearchPopup.Controls)
                    {
                        if (SearchPopupC is LayoutControl)
                        {
                            LayoutControl FormLayout = SearchPopupC as LayoutControl;
                            Control Button = FormLayout.GetControlByName("btClear");
                            if (Button != null)
                            {
                                return FormLayout.GetItemByControl(Button);
                            }
                        }
                    }
                }
            }
            return null;
        }
   public  static LayoutControlItem GetFindButtonLayoutItemFind(PopupBaseForm Form)
        {
            foreach (Control FormC in Form.Controls)
            {
                if (FormC is SearchEditLookUpPopup)
                {
                    SearchEditLookUpPopup SearchPopup = FormC as SearchEditLookUpPopup;
                    foreach (Control SearchPopupC in SearchPopup.Controls)
                    {
                        if (SearchPopupC is LayoutControl)
                        {
                            LayoutControl FormLayout = SearchPopupC as LayoutControl;
                            Control Button = FormLayout.GetControlByName("btFind");
                            if (Button != null)
                            {
                                return FormLayout.GetItemByControl(Button);
                            }
                        }
                    }
                }
            }
            return null;
        }
        public static void saveError(IClientServices Services, string spName, string paramQuery)
        {
            RequestCollection _query = DataQuery.Create("FW", "ws_Error_Save", new
            {
                ID = "0",
                Error = Services.LastError,
                LocalIP = LocalIPAddress(),
                sPName = spName,
                spParameterQuery = paramQuery,
                StackTrade = "",
                StackMessage = "",
                StackSource = ""
            });
            DataSet _ds = Services.Execute(_query);
            if (_ds == null)
            {
                UI.ShowError(Services.LastError);
            }
        }

        public static void saveError(IClientServices Services,
            Exception exception)
        {
            UI.ShowError(exception.Message);
            RequestCollection _query = DataQuery.Create("FW", "ws_Error_Save", new
            {
                ID = "0",
                Error = Services.LastError,
                LocalIP = LocalIPAddress(),
                sPName = "",
                spParameterQuery = "",
                StackTrade = exception.StackTrace,
                StackMessage = exception.Message,
                StackSource = exception.Source
            });
            DataSet _ds = Services.Execute(_query);
            if (_ds == null)
            {
                UI.ShowError(Services.LastError);
            }
        }

        public static void saveError(IClientServices Services, Exception exception, string FormName)
        {
            UI.ShowError(exception.Message);
            RequestCollection _query = DataQuery.Create("FW", "ws_Error_Save", new
            {
                ID = "0",
                Error = Services.LastError + "(" + FormName + ")",
                LocalIP = LocalIPAddress(),
                sPName = "",
                spParameterQuery = "",
                exception.StackTrace,
                exception.Message,
                exception.Source
            }); DataSet _ds = Services.Execute(_query);
            if (_ds == null)
            {
                UI.ShowError(Services.LastError);
            }
        }

        public static void getListGridControl(this Control.ControlCollection controls, List<GridControl> list)
        {
            foreach (Control ctl in controls)
            {
                if (ctl is GridControl)
                {
                    list.Add((GridControl)ctl);
                }
                if (ctl.HasChildren)
                {
                    ctl.Controls.getListGridControl(list);
                }
            }
        }

        public static void AddNewRow(this GridView gridView, Dictionary<String, String> _dictionary, int index)
        {
            int currentIndex = index;
            if (index == -1) //thêm mới
            {
                gridView.AddNewRow();
                currentIndex = GridControl.NewItemRowHandle;
            }
            else
            {
                if (index >= gridView.RowCount)
                {
                    return;
                }
            }
            foreach (var dic in _dictionary)
            {
                gridView.SetRowCellValue(currentIndex, gridView.Columns[dic.Key], dic.Value);
            }

            gridView.UpdateCurrentRow();
        }

        public static int findIndex(this GridView gridView, string columnName, string value)
        {
            if (gridView == null)
                return -1;
            for (int i = 0; i < gridView.DataRowCount; i++)
            {
                if (gridView.GetDataRow(i).Item(columnName) == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string TableCellValue(IClientServices Services, string TableName, string ColumnNameShow, string ColumnNameExpression, string Expression)
        {
            string value = "";
            RequestCollection _query = DataQuery.Create("QAHosGenericDB", "ws_GetFieldlValueOfTable", new
            {
                TableName = TableName,
                FieldNameShow = ColumnNameShow,
                FieldNameExpression = ColumnNameExpression,
                Expression = Expression
            });
            DataSet _ds = Services.Execute(_query);
            if (_ds == null)
            {
                UI.ShowError(Services.LastError);
            }

            value = _ds.FirstValue();

            return value;
        }

        //public static int findIndex(this GridView gridView, List<String> columnNames, List<String> values)
        //{
        //    if (gridView == null)
        //        return -1;
        //    for (int i = 0; i < gridView.DataRowCount; i++)
        //    {
        //        bool check = true;
        //        var row = gridView.GetDataRow(i);
        //        for (int index = 0; index < columnNames.Count; index++)
        //        {
        //            if (row.Item(columnNames[index]) != values[index])
        //            {
        //                check = false;
        //                continue;
        //            }
        //        }
        //        if (check == true)
        //        {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}

        public static void ClearAllTextEdit(this Control.ControlCollection controlCollection)
        {
            foreach (Control control in controlCollection)
            {
                if (control is DevExpress.XtraEditors.TextEdit)
                {
                    ((DevExpress.XtraEditors.TextEdit)control).Text = String.Empty;
                }
                else
                {
                    if (control.HasChildren == true)
                    {
                        control.Controls.ClearAllTextEdit();
                    }
                }
            }
        }

        public static void _EnabledControl(this Control.ControlCollection controls, Type type, bool status)
        {
            foreach (Control ctr in controls)
            {
                if (ctr.GetType().Name == type.Name)
                {
                    ctr.Enabled = status;
                }
                if (ctr.HasChildren == true)
                {
                    ctr.Controls._EnabledControl(type, status);
                }
            }
        }

        public static void _ClearControl(this Control.ControlCollection controls, Type type)
        {
            foreach (Control ctr in controls)
            {
                if (ctr.GetType().Name == type.Name)
                {
                    ctr.Text = String.Empty;
                }
                if (ctr.HasChildren == true)
                {
                    ctr.Controls._ClearControl(type);
                }
            }
        }

        public static void _EnableAll(this Control.ControlCollection controlCollection, string TypeControl, bool statusEnable)
        {
            if (TypeControl == "TextEdit")
            {
                foreach (Control control in controlCollection)
                {
                    if (control is DevExpress.XtraEditors.TextEdit)
                    {
                        ((DevExpress.XtraEditors.TextEdit)control).Enabled = statusEnable;
                    }
                    else
                    {
                        if (control.HasChildren == true)
                        {
                            control.Controls._EnableAll(TypeControl, statusEnable);
                        }
                    }
                }
                return;
            }
            if (TypeControl == "SimpleButton")
            {
                foreach (Control control in controlCollection)
                {
                    if (control is DevExpress.XtraEditors.SimpleButton)
                    {
                        ((DevExpress.XtraEditors.SimpleButton)control).Enabled = statusEnable;
                    }
                    else
                    {
                        if (control.HasChildren == true)
                        {
                            control.Controls._EnableAll(TypeControl, statusEnable);
                        }
                    }
                }
                return;
            }
            if (TypeControl == "QACombo")
            {
                foreach (Control control in controlCollection)
                {
                    if (control is QA.Controls.QACombo)
                    {
                        ((QA.Controls.QACombo)control).Enabled = statusEnable;
                    }
                    else
                    {
                        if (control.HasChildren == true)
                        {
                            control.Controls._EnableAll(TypeControl, statusEnable);
                        }
                    }
                }
                return;
            }
            if (TypeControl == "Combox")
            {
                foreach (Control control in controlCollection)
                {
                    if (control is ComboBox)
                    {
                        ((ComboBox)control).Enabled = statusEnable;
                    }
                    else
                    {
                        if (control.HasChildren == true)
                        {
                            control.Controls._EnableAll(TypeControl, statusEnable);
                        }
                    }
                }
                return;
            }
        }

        public static DataTable _CopyFromTableToTable(DataTable _srcTable, List<String> ColumnsCopy, List<String> ColumnsBonus)
        {
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

        public static int findIndex(this GridView view, List<string> columnsName, List<string> columnsValue)
        {
            for (int i = 0; i < view.DataRowCount; i++)
            {
                bool check = true;
                for (int j = 0; j < columnsName.Count; j++)
                {
                    if (view.GetDataRow(i).Item(columnsName[j]) != columnsValue[j])
                    {
                        check = false;
                    }
                }
                if (check == true)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string RemoveNote(this String Sentence)
        {
            bool innot = false;
            string newstring = "";
            for (int t = 0; t < Sentence.Count(); t++)
            {
                if (Sentence[t] == '(')
                {
                    innot = true;
                }
                if ((Sentence[t] == '(' || Sentence[t] == ')') || innot)
                {
                    if (Sentence[t] == ')')
                    {
                        innot = false;
                    }
                }
                else
                {
                    newstring += Sentence[t];
                }
            }

            return newstring;
        }

        public static string UpperSentence(this string senten)
        {
            bool needupdate = true;
            String newstring = "";
            for (int t = 0; t < senten.Count(); t++)
            {
                // tìm kí tự để update lên thông tin
                if (char.IsLetter(senten[t]) && needupdate)
                {
                    string a = char.ToUpper(senten[t]).ToString().ToUpper();
                    newstring += a;
                    needupdate = false;
                }
                else
                {
                    string a = (senten[t]).ToString().ToLower();
                    newstring += a;
                }

                if (senten[t] == '.' || senten[t] == '-')
                {
                    needupdate = true;
                }
            }
            return newstring;
        }

        /// <summary>
        /// Documentations
        /// </summary>
        /// <param name="IDinsurance"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        ///

       

       
        public static T LoadObject<T>(string typeName)
        {
            try
            {
                var parts = typeName.Split(',');
                var fileName = "QA.Shared.dll";//parts[0].Trim();
                var className = "QA.DocGenericForm2";//parts[1].Trim();

                fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                var assembly = Assembly.LoadFile(fileName);
                return (T)assembly.CreateInstance(className);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return default(T);
        }

        public static T LoadObject<T>(string fileName, string className)
        {
            try
            {
                fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                var assembly = Assembly.LoadFile(fileName);
                return (T)assembly.CreateInstance(className);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return default(T);
        }

        public static string RemoveUnicode(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(stFormD[ich]);
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        public static string[] GetThongTin(String IDinsurance, string type)
        {
            string[] insurance = IDinsurance.Split('|');

            if (type == "1")
            {
                insurance[1] = new string(DecodeHextoUft8(insurance[1]));
                var obj = insurance[1].Substring(1, 2);
                insurance[4] = new string(DecodeHextoUft8(insurance[4]));
                insurance[9] = new string(DecodeHextoUft8(insurance[9]));
                if (obj.ToString() == "TE") // Đối tượng là Trẻ Em
                    insurance[10] = new string(DecodeHextoUft8(insurance[10]));
            }
            else if (type == "2") // Lấy thông tin từ barcode trên toa thuốc PatientID|PatientHospitalID|FullName|Field4(Mã quân nhân)|RXID
            {
            }
            return insurance;
        }

        public static char[] DecodeHextoUft8(string m_enc)
        {
            Regex regex = new Regex(@"(?<hex>[0-9A-F]{2})", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            byte[] bytes = regex.Matches(m_enc).OfType<Match>().Select(m => Convert.ToByte(m.Groups["hex"].Value, 16)).ToArray();
            char[] chars = Encoding.UTF8.GetChars(bytes);
            return chars;
        }

        public static void LoadGridMasterDetail(DataTable master, DataTable detail, string columnName, GridControl gridControl, List<String> listVisible, Dictionary<string, string> dicCaption)
        {
            try
            {
                if (master == null)
                    return;
                var ds = new DataSet();

                ds.Tables.Add(master.Copy());

                if (master.Rows.Count > 0)
                {
                    if (detail != null)
                    {
                        ds.Tables.Add(detail.Copy());
                        DataColumn invId = ds.Tables(0).Columns[columnName];
                        DataColumn FK_invId = ds.Tables(1).Columns[columnName];
                        ds.Relations.Add(new DataRelation("Detail", invId, FK_invId, false));
                        gridControl.DataSource = ds.Tables(0);
                        //  gridLab.ForceInitialize();
                        var view = new GridView(gridControl);

                        #region đổi style view

                        view.Appearance.HeaderPanel.Options.UseBackColor = true;

                        view.Appearance.HeaderPanel.BackColor = Color.DeepSkyBlue;
                        ;
                        view.Appearance.HeaderPanel.BackColor2 = Color.RoyalBlue;
                        view.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
                        view.Appearance.HeaderPanel.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                        view.Appearance.HeaderPanel.ForeColor = Color.White;

                        //Set color for Grid selected
                        Color backcolor = Color.SpringGreen;
                        Color backcolor2 = Color.SpringGreen;
                        Color forecolor = Color.Black;
                        //Color bordercolor = Color.Transparent;

                        view.Appearance.SelectedRow.BackColor = backcolor;
                        view.Appearance.SelectedRow.BackColor2 = backcolor2;
                        view.Appearance.SelectedRow.ForeColor = forecolor;
                        //view.Appearance.SelectedRow.BorderColor = bordercolor;
                        view.Appearance.FocusedRow.BackColor = backcolor;
                        view.Appearance.FocusedRow.BackColor2 = backcolor2;
                        view.Appearance.FocusedRow.ForeColor = forecolor;
                        //view.Appearance.FocusedRow.BorderColor = bordercolor;
                        view.Appearance.HideSelectionRow.BackColor = backcolor;
                        view.Appearance.HideSelectionRow.BackColor2 = backcolor2;
                        view.Appearance.HideSelectionRow.ForeColor = forecolor;

                        view.Appearance.FocusedCell.BackColor = backcolor;
                        view.Appearance.FocusedCell.BackColor2 = backcolor2;
                        view.Appearance.FocusedCell.ForeColor = forecolor;

                        #endregion đổi style view

                        view.OptionsBehavior.Editable = false;
                        view.OptionsBehavior.ReadOnly = false;
                        view.OptionsView.ShowIndicator = false;
                        view.OptionsView.ShowGroupPanel = false;
                        view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
                        gridControl.LevelTree.Nodes.Add("Detail", view);
                        view.PopulateColumns(ds.Tables(1));

                        // #region Modify designer of child gird
                        view.OptionsBehavior.Editable = false;
                        view.OptionsView.ShowGroupPanel = false;

                        foreach (string s in listVisible)
                        {
                            view.Columns["s"].VisibleIndex = -1;
                        }

                        foreach (var pair in dicCaption)
                        {
                            view.Columns[pair.Key].Caption = pair.Value;
                        }

                        //expandlist[0] = 1;

                        // #endregion
                    }
                    else
                    {
                        gridControl.DataSource = master;
                    }
                }
                else
                {
                    gridControl.DataSource = null;
                }
            }
            catch (Exception exception)
            {
                return;
            }
        }

        public static void LoadGridMasterDetail(DataTable master, DataTable detail, string columnName, GridControl gridControl)
        {
            try
            {
                if (master == null)
                    return;
                var ds = new DataSet();

                ds.Tables.Add(master.Copy());

                if (master.Rows.Count > 0)
                {
                    if (detail != null)
                    {
                        ds.Tables.Add(detail.Copy());
                        DataColumn invId = ds.Tables(0).Columns[columnName];
                        DataColumn FK_invId = ds.Tables(1).Columns[columnName];
                        ds.Relations.Add(new DataRelation("Detail", invId, FK_invId, false));
                        gridControl.DataSource = ds.Tables(0);
                        //  gridLab.ForceInitialize();
                        var view = new GridView(gridControl);

                        #region đổi style view

                        view.Appearance.HeaderPanel.Options.UseBackColor = true;

                        view.Appearance.HeaderPanel.BackColor = Color.DeepSkyBlue;
                        ;
                        view.Appearance.HeaderPanel.BackColor2 = Color.RoyalBlue;
                        view.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
                        view.Appearance.HeaderPanel.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                        view.Appearance.HeaderPanel.ForeColor = Color.White;

                        //Set color for Grid selected
                        Color backcolor = Color.SpringGreen;
                        Color backcolor2 = Color.SpringGreen;
                        Color forecolor = Color.Black;
                        //Color bordercolor = Color.Transparent;

                        view.Appearance.SelectedRow.BackColor = backcolor;
                        view.Appearance.SelectedRow.BackColor2 = backcolor2;
                        view.Appearance.SelectedRow.ForeColor = forecolor;
                        //view.Appearance.SelectedRow.BorderColor = bordercolor;
                        view.Appearance.FocusedRow.BackColor = backcolor;
                        view.Appearance.FocusedRow.BackColor2 = backcolor2;
                        view.Appearance.FocusedRow.ForeColor = forecolor;
                        //view.Appearance.FocusedRow.BorderColor = bordercolor;
                        view.Appearance.HideSelectionRow.BackColor = backcolor;
                        view.Appearance.HideSelectionRow.BackColor2 = backcolor2;
                        view.Appearance.HideSelectionRow.ForeColor = forecolor;

                        view.Appearance.FocusedCell.BackColor = backcolor;
                        view.Appearance.FocusedCell.BackColor2 = backcolor2;
                        view.Appearance.FocusedCell.ForeColor = forecolor;

                        #endregion đổi style view

                        view.OptionsBehavior.Editable = false;
                        view.OptionsBehavior.ReadOnly = false;
                        view.OptionsView.ShowIndicator = false;
                        view.OptionsView.ShowGroupPanel = false;
                        view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
                        gridControl.LevelTree.Nodes.Add("Detail", view);
                        view.PopulateColumns(ds.Tables(1));

                        // #region Modify designer of child gird
                        view.OptionsBehavior.Editable = false;
                        view.OptionsView.ShowGroupPanel = false;

                        //foreach (string s in listVisible)
                        //{
                        //    view.Columns["s"].VisibleIndex = -1;
                        //}

                        //foreach (var pair in dicCaption)
                        //{
                        //    view.Columns[pair.Key].Caption = pair.Value;
                        //}

                        //expandlist[0] = 1;

                        // #endregion
                    }
                    else
                    {
                        gridControl.DataSource = master;
                    }
                }
                else
                {
                    gridControl.DataSource = null;
                }
            }
            catch (Exception exception)
            {
                return;
            }
        }

        public static int DiffDateExceptWeekEnd(DateTime startDate, DateTime endDate)
        {
            TimeSpan diff = endDate - startDate;
            int days = diff.Days;
            int j = 0;
            for (var i = 0; i <= days; i++)
            {
                var testDate = startDate.AddDays(i);
                if (testDate.DayOfWeek != DayOfWeek.Saturday && testDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    j = j + 1;
                }
            }
            return j;
        }

      

     

        public static void ClearDataPanel(DevExpress.XtraEditors.PanelControl ClearPanel)
        {
            foreach (Control a in ClearPanel.Controls)
            {
                if (a.GetType().ToString() == "DevExpress.XtraEditors.TextEdit" || a.GetType().ToString() == "DevExpress.XtraEditors.MemoEdit")
                {
                    a.Text = "";
                }
            }
        }

        public static Double LoadPriceBuy(Double gia)
        {
            Double TiGia = 1.5;
            if (gia <= 1000)
            {
                return 1.15 * gia;
            }
            if (gia <= 5000)
            {
                return 1.1 * gia;
            }
            if (gia <= 100000)
            {
                return 1.07 * gia;
            }
            if (gia <= 1000000)
            {
                return 1.05 * gia;
            }
            else
            {
                return 1.02 * gia;
            }
        }
    }
}