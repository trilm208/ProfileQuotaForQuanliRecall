using DevExpress.XtraSplashScreen;
using DataAccess;
using Shell.Core;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using QA;

namespace Shell
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.QASkinV1).Assembly);

            MessageBoxManager.Register();

            //Initialize();



            if (Debugger.IsAttached)
            {
                Run(args);
            }
            else
            {
                try
                {
                    Run(args);
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
            }
        }

        // In Visual Studio 2012 and newer versions of Visual Studio, to ensure that your custom skin assembly is loaded and that the custom skin is registered at design time, please add the following code to your project
        //public class SkinManager : Component
        //{
        //    public SkinManager()
        //    {
        //        DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.QASkinV1).Assembly);
        //    }
        //}

        private static void ShowException(Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        private static void Run(string[] args)
        {
            SplashScreenManager.ShowForm(typeof(SplashScreen));

            var services = new ClientServices();

            services.SetInformation("FacID", QA.QAFunction.ReadFromConfigXMlFile("FacID"));
            services.Initialize();
            
            using (var frm = new frmLogin())
            {
                frm.Initialize(services);
                frm.Process();

                SplashScreenManager.CloseForm();
                if (frm.ShowDialog() != DialogResult.OK)
                {
                 
                    return;
                }
                //UserID = frm.UserID;
                //UserName = frm.UserName;
            }

            services.LoadPermissions(services.GetInformation("FacID").ToString());


            string ProjectID="", ProjectNo="";
            var query = DataAccess.DataQuery.Create("KadenceDB", "ws_HR_UserProjectPermissions_Get", new
            {
                UserID = services.GetInformation("UserID"),
                IsFWSupInterview = 0,
                IsFWSupRecuit = 1,
                IsQC = 0,
                Type = 1
            });


            var ds = services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(services.LastError);
                return;
            }
            if (ds.FirstTable().Rows.Count == 1)
            {
                ProjectID = ds.FirstRow().Item("ProjectID");
                ProjectNo = ds.FirstRow().Item("ProjectNo");
            }
            else
            {
                if (ds.FirstTable().Rows.Count < 1)
                {
                    UI.ShowError("Bạn không có dự án nào");
                    return;
                }
                else
                {
                    using (var frm = new frmProjectList())
                    {
                        frm.Initialize(services);
                        frm.InitializeDataSource = ds.FirstTable();
                        frm.Process();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            ProjectID = frm.SelectedProjectID;
                            ProjectNo = frm.SelectedProjectNo;
                        }
                    }
                }
            }
          

            //SplashScreenManager.ShowForm(typeof(SplashScreen));
            // services.LoadSettings();

            // Debug.WriteLine("Shell #4 : ClearReport");



            // ReportUtility.ClearReports();

            // Debug.WriteLine("Shell #5 : Opening Main form");

            using (var frm = new frmQCRecall())
            {
                frm.Text = String.Format("FW Assignment Bạn đang làm việc với dự án : {0}", ProjectNo);
                frm.ProjectID = ProjectID;
                frm.ProjectNo = ProjectNo;
                frm.Initialize(services);
                frm.Process();

                //SplashScreenManager.CloseForm();
                Application.Run(frm);
            }

           
        }
    }
}