

using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;

using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using QA;

namespace Shell.Core
{
    internal class ClientServices : IClientServices
    {
        private Config Config { get; set; }

        public Control Container { get; set; }

        private HttpDataServices DataService { get; set; }

        private DataTable Localization { get; set; }

        private DataTable Setting { get; set; }

        private List<string> Permissions { get; set; }

        private List<string> UserPermissions { get; set; }

        public string LastError { get; private set; }

        private Dictionary<string, object> SharedInfo = new Dictionary<string, object>();

        public ClientServices()
        {
            this.Config = Shell.Core.Config.Load();
            this.DataService = new HttpDataServices(Config["WebService"]);
            this.Permissions = new List<string>();
            this.UserPermissions = new List<string>();
        }

        public void Initialize()
        {
            var query = DataQuery.Cached("Application", "ws_Localization_Get", new { Culture = Config["Culture"] });
            query += DataQuery.Create("Application", "ws_Settings_List");
            var ds = this.Execute(query);
            this.Localization = ds.Tables(0);
            this.Setting = ds.Tables(1);
        }

        public void LoadPermissions(string FacID)
        {
            var query = DataQuery.Create("Security", "ws_Permissions_List", new
            {
                FacID=FacID
            });
            query += DataQuery.Create("Security", "ws_UserPermissions_List", new
            {
                ID=this.GetInformation("UserID"),
                FacID=FacID
            });

            var ds = this.Execute(query);
            if(ds==null)
            {
                QA.UI.ShowError(this.LastError);
                return;
            }
            Permissions.Clear();
            // ws_Permissions_List
            var table = ds.Tables(0);
            foreach (DataRow row in table.Rows)
            {
                Permissions.Add(row["Key"].ToString().ToUpper());
            }
            UserPermissions.Clear();
            // ws_UserPermissions_List
            table = ds.Tables(1);
            foreach (DataRow row in table.Rows)
            {
                UserPermissions.Add(row["Key"].ToString().ToUpper());
            }
        }

        public void LoadSettings()
        {
            var query = DataQuery.Create("Application", "ws_Settings_ListByFacID", new
            {
                
            });

            var ds = this.Execute(query);

            this.Setting = ds.Tables(0);
        }

        public string Localize(string name)
        {
            var rows = Localization.Select(String.Format("Name='{0}'", name));

            foreach (var row in rows)
            {
                return row["Message"] + "";
            }

            return "";
        }

        public string Localize(string category, string name)
        {
            var rows = Localization.Select(String.Format("Category='{0}' and Name='{1}'", category, name));

            foreach (var row in rows)
            {
                return row["Message"] + "";
            }

            return "";
        }

        public string GetSetting(string name)
        {
            var rows = Setting.Select(String.Format("Name='{0}'", name));

            foreach (var row in rows)
            {
                return row["Value"] + "";
            }

            return "";
        }

        public string GetSetting(string category, string name)
        {
            var rows = Setting.Select(String.Format("Category='{0}' and Name='{1}'", category, name));

            foreach (var row in rows)
            {
                return row["Value"] + "";
            }

            return "";
        }

        public DataSet Execute(RequestCollection requests)
        {
            this.LastError = "";
            var ds = DataService.Execute(requests);

            var table = ds.FirstTable();
            if (table != null)
            {
                if (table.TableName == "Error")
                {
                    var row = table.FirstRow();

                    var message = row["Message"].ToString();
                    var source = row["Source"].ToString();
                    var stackTrace = row["StackTrace"].ToString();
                    var helpLink = row["HelpLink"].ToString();

                    this.LastError = message;
                    ds = null;
                }
            }

            return ds;
        }

        public void SetInformation(string key, object value)
        {
            if (SharedInfo.ContainsKey(key))
                SharedInfo[key] = value;
            else
                SharedInfo.Add(key, value);
        }

        public object GetInformation(string key)
        {
            if (SharedInfo.ContainsKey(key))
                return SharedInfo[key];

            return null;
        }

        public object this[string key]
        {
            get { return GetInformation(key); }
            set { SetInformation(key, value); }
        }

        public void LoadControl(string typeName)
        {
            var parts = typeName.Split(',');
            var fileName = parts[0].Trim();
            var className = parts[1].Trim();

            fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            var assembly = Assembly.LoadFile(fileName);
            var control = assembly.CreateInstance(className) as QA.ClientControl;

            Container.Controls.Clear();
            Container.Controls.Add(control);

            if (control != null)
            {
                control.Dock = DockStyle.Fill;
                control.Initialize(this);
                control.Process();
            }
        }

        public void RunReport(string reportID, object param)
        {
            SplashScreenManager.ShowForm(typeof(RunReportWait));

            var reportParams = new StringBuilder();
            var stringWriter = new StringWriter(reportParams);

            using (JsonWriter json = new JsonTextWriter(stringWriter))
            {
                json.WriteStartObject();

                var type = param.GetType();
                var properties = type.GetProperties();

                for (int n = 0; n < properties.Length; n++)
                {
                    var property = properties[n];
                    var name = property.Name;
                    var value = property.GetValue(param, null);

                    json.WritePropertyName(name);
                    json.WriteValue(value);
                }

                json.WriteEndObject();
            }

            var query = DataQuery.Create("Reports", "ws_ReportQueue_Push", new
            {
                ReportID = reportID,
                ReportParams = reportParams.ToString(),
                UserID = Guid.Empty
            });

            var ds = this.Execute(query);
            var row = ds.FirstRow();

            if (row != null)
            {
                var seq = row["Seq"].ToString();

                Stopwatch sw = new Stopwatch();
                sw.Start();

                while (sw.Elapsed.TotalSeconds < 60)
                {
                    Thread.Sleep(500);

                    query = DataQuery.Create("Reports", "ws_ReportOutput_Get", new { QueueSeq = seq });
                    ds = this.Execute(query);
                    row = ds.FirstRow();

                    if (row != null)
                    {
                        var fileData = row["FileData"].ToString();
                        var fileExtension = row["FileExtension"].ToString();
                        ReportUtility.CreateAndOpen(fileData, fileExtension);
                        break;
                    }
                }
            }

            SplashScreenManager.CloseForm();
        }

        public void RunReport(string reportID, object param, string newguid)
        {
            SplashScreenManager.ShowForm(typeof(RunReportWait));

            var reportParams = new StringBuilder();
            var stringWriter = new StringWriter(reportParams);

            using (JsonWriter json = new JsonTextWriter(stringWriter))
            {
                json.WriteStartObject();

                var type = param.GetType();
                var properties = type.GetProperties();

                for (int n = 0; n < properties.Length; n++)
                {
                    var property = properties[n];
                    var name = property.Name;
                    var value = property.GetValue(param, null);

                    json.WritePropertyName(name);
                    json.WriteValue(value);
                }

                json.WriteEndObject();
            }

            var query = DataQuery.Create("Reports", "ws_ReportQueue_Push", new
            {
                ReportID = reportID,
                ReportParams = reportParams.ToString(),
                UserID = Guid.Empty
            });

            var ds = this.Execute(query);
            var row = ds.FirstRow();

            if (row != null)
            {
                var seq = row["Seq"].ToString();

                Stopwatch sw = new Stopwatch();
                sw.Start();

                while (sw.Elapsed.TotalSeconds < 60)
                {
                    Thread.Sleep(500);

                    query = DataQuery.Create("Reports", "ws_ReportOutput_Get", new { QueueSeq = seq });
                    ds = this.Execute(query);
                    row = ds.FirstRow();

                    if (row != null)
                    {
                        var fileData = row["FileData"].ToString();
                        var fileExtension = row["FileExtension"].ToString();
                        ReportUtility.Create(fileData, fileExtension, newguid);
                        break;
                    }
                }
            }

            SplashScreenManager.CloseForm();
        }

        public bool HasPermission(string key)
        {
            key = key.ToUpper();
            if (UserPermissions.Contains(key))
                return true;

            if (Permissions.Contains(key))
                return false;

            var query = DataQuery.Create("Security", "ws_Permissions_Create", new { Key = key });

            
           var ds= this.Execute(query);

           if (ds == null)
           {
               UI.ShowError(this.LastError);
               
           }
            
            Permissions.Add(key);

            return false;
        }
    }
}