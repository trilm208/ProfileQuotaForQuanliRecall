using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QA;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.ProjectOxford.Face;
using Shell.Core;
using Microsoft.ProjectOxford.Face.Contract;
using DevExpress.XtraSplashScreen;

namespace Shell
{
    public partial class RespondentProfileInformation : ClientControl
    {
  
        public IFaceServiceClient faceServiceClient
        {
            get
            {
                return new FaceServiceClient(Properties.Settings.Default.SubscriptionKey);
            }
        }

        public string ProjectID { get;  set; }

        double Confidence = 0;
       
        private DataTable dtFaceList;
        private DataTable tblStreet;
        private DataTable tblQuotaControl;
        private DataTable dataHanhChanh;
      
        private DataTable facelist;
      

        public RespondentProfileInformation()
        {
            InitializeComponent();

            DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit lookUpProperties2;
            lookUpProperties2 = txtDistrict.Properties;
            lookUpProperties2.ImmediatePopup = true;

            DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit lookUpProperties3;
            lookUpProperties3 = txtWard.Properties;
            lookUpProperties3.ImmediatePopup = true;

            DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit lookUpProperties5;
            lookUpProperties5 = txtRecruit.Properties;
            lookUpProperties5.ImmediatePopup = true;

            DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit lookUpProperties6;
            lookUpProperties6 = rCity.Properties;
            lookUpProperties6.ImmediatePopup = true;

        }

        public string AnswerID { get; set; }
        public string ProjectNo { get; internal set; }

        public  override void Process()
        {
            base.Process();
            

            var query = DataAccess.DataQuery.Create("KadenceDB", "ws_L_FaceListID_List");

            query += DataAccess.DataQuery.Create("KadenceDB", "ws_MDM_Street_ListAll");

            query += DataAccess.DataQuery.Create("Docs", "ws_DOC_QuotaControl_Get", new
            {
                ProjectID
            });

            query += DataAccess.DataQuery.Create("KadenceDB", "ws_L_WardDistrictCity_ListAllV2");
           
            query += DataAccess.DataQuery.Create("KadenceDB", "ws_L_FaceListID_List");
            query += DataAccess.DataQuery.Create("KadenceDB", "ws_HR_PTEProject_List", new
            {
                ProjectID,
                IsFWRecuit = 1,
                IsFWInterview = 0
            });

            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                return;
            }

           
            dtFaceList = ds.FirstTable();
            tblStreet = ds.SecondTable();
            rCity.Properties.DataSource = ds.Tables[3].Distinct("City").OrderBy("City");
            tblQuotaControl = ds.Tables[2];
            dataHanhChanh = ds.Tables[3];      
            facelist = ds.Tables[4];
            txtRecruit.Properties.DataSource = ds.Tables[5];

            createQuotaInterface();

        }

     

        private void createQuotaInterface()
        {
            foreach (DataRow row in tblQuotaControl.Rows)
            {
                var c_quota = new QuotaGroup(row);
                c_quota.Height = flowQuota.Height - 20;
                c_quota.Width = c_quota.Height+50;
                flowQuota.Controls.Add(c_quota);
            }
        }
        private string _pathImage = "";
        private async void btnFD_Click(object sender, EventArgs e)
        {

           

            await NhanDien();
  
           

        }

        private async Task NhanDien()
        {
            gFaceDetection.Text = "Face Detection ( FD ) đang nhận diện";

            flowLayoutPanel2.Controls.Clear();

            Confidence = Properties.Settings.Default.Confidence;
            if (FacePhoto.Image == null)
            {
                OpenFileDialogToPickImage();
            }
            if (FacePhoto.Image == null)
            {
                gFaceDetection.Text = "Face Detection ( FD )";
                return ;
            }
            try
            {
                var ms = new MemoryStream();
                FacePhoto.Image.Save(ms, ImageFormat.Png);
                var m = Serializer.Compress(ms);
                // If you're going to read from the stream, you may need to reset the position to the start
                ms.Position = 0;

                var watch = System.Diagnostics.Stopwatch.StartNew();
                // the code that you want to measure comes here
                WriteLog(string.Format("Resquest DetectAsync"));
                var faces = await faceServiceClient.DetectAsync(m);

                watch.Stop();

                WriteLog(string.Format("Response: Success. Detected {0} face(s) in {1} seconds", faces.Length, watch.Elapsed.TotalSeconds));

                var personSimilarResultFace = new PersonSimilarResults();

                flowLayoutPanel2.Controls.Clear();

                foreach (var f in faces)
                {
                    var faceId = f.FaceId;
                    WriteLog(String.Format("Request: Finding similar faces in Face Match Mode for face {0}", faceId));

                    foreach (DataRow row in dtFaceList.Rows)
                    {
                        try
                        {
                            var personSimilarResults = await faceServiceClient.FindSimilarAsync(faceId, row.Item("faceListId"), FindSimilarMatchMode.matchFace, 4);
                            foreach (var personSimilarResult in personSimilarResults)
                            {
                                if (personSimilarResult.Confidence >= Confidence)
                                {
                                    personSimilarResultFace.Add(personSimilarResult.PersistedFaceId, personSimilarResult.Confidence);

                                }
                            }
                            WriteLog(String.Format("Response: Success FindSimilarAsync {0}- {1}", faceId, row.Item("faceListId")));
                        }
                        catch (FaceAPIException ex)
                        {
                            WriteLog(String.Format("Error FindSimilarAsync {0}- {1}:{2}", faceId, row.Item("faceListId"), ex.ErrorMessage));
                        }

                    }


                }
                if (personSimilarResultFace.Data.Count == 0)
                {
                    WriteLog(String.Format("Không tìm thấy khuôn mặt giống khuôn mặt đáp viên {0}", txtName.Text));
                    MessageBox.Show(String.Format("Không tìm thấy khuôn mặt giống khuôn mặt đáp viên {0}", txtName.Text));
                    gFaceDetection.Text = "Face Detection ( FD )";
                    return ;
                }
                else
                {
                    //MessageBox.Show(
                }

                var items = from pair in personSimilarResultFace.Data
                            orderby pair.Value ascending
                            select pair;

                var query = new DataAccess.RequestCollection();

                string ids = "";
                foreach (KeyValuePair<string, double> item in items)
                {
                    ids += item.Key + ",";

                }

                query = DataAccess.DataQuery.Create("KadenceDB", "ws_FaceList_List", new
                {

                    FaceIDs = ids
                });
                var ds = this.Services.Execute(query);
                if (ds == null)
                {
                    MessageBox.Show(Services.LastError);
                    return ;
                }

                foreach (DataRow row in ds.FirstTable().Rows)
                {
                    var faceRespondent = new FaceFaceSimilarUnit();
                    faceRespondent.Conf = personSimilarResultFace.Data[row.Item("FaceID")];

                    faceRespondent.ProjectNameFullName = row.Item("ProjectNo") + "_" + row.Item("FullName");
                    byte[] byteImage = Convert.FromBase64String(row.Item("Image"));

                    Image imageProfile;
                    using (MemoryStream memorysteam = new MemoryStream(byteImage))
                    {
                        imageProfile = Image.FromStream(memorysteam);
                    }
                    faceRespondent.ImageRespondent = imageProfile;


                    faceRespondent.Height = flowLayoutPanel2.Height - 10;
                    faceRespondent.Width = faceRespondent.Height;
                    flowLayoutPanel2.Controls.Add(faceRespondent);

                }

                UI.ShowError(String.Format("Đã tìm thấy {1} khuôn mặt giống khuôn mặt đáp viên {0}", txtName.Text, ds.FirstTable().Rows.Count));


            }
            catch (FaceAPIException ex)
            {
                MessageBox.Show(ex.Message);
                gFaceDetection.Text = "Face Detection ( FD )";
                return ;
            }

            gFaceDetection.Text = "Face Detection ( FD )";
         
        }

        private void OpenFileDialogToPickImage()
        {
         
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                _pathImage = openFileDialog1.FileName;
                FacePhoto.Image = Image.FromFile(_pathImage);
                
                //txtFullName.Text = Path.GetFileName(openFileDialog1.FileName).Split('.')[0];
                //Text = "Detecting...";
                //faceRects = await UploadAndDetectFaces(filePath);

            }
        }

        void WriteLog(string logMessage)
        {

            LogWriter.LogWrite(logMessage);
            if (String.IsNullOrEmpty(logMessage) || logMessage == "\n")
            {

               
            }
            else
            {
                string timeStr = DateTime.Now.ToString("HH:mm:ss");
                string messaage = "[" + timeStr + "]: " + logMessage + Environment.NewLine;
                
                LogWriter.LogWrite(messaage);
            }


        }

        private void FacePhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialogToPickImage();
        }

        private void btnRD_Click(object sender, EventArgs e)
        {
            var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormValues_SearchMultiRespondentCurrentProject", new
            {
                STT = "1@",  
                ProjectID,
                Name = txtName.Text.Trim().ToUpper().RemoveSign4VietnameseString().Replace("  ", String.Empty) + "@",
                Telephone = txtTelephone.Text.Trim().ToUpper().RemoveSign4VietnameseString().Replace("  ", String.Empty) + "@",
                Address = txtAddress.Text.Trim().ToUpper().RemoveSign4VietnameseString().Replace("  ", String.Empty) + "@",
                Street = txtStreet.Text.Trim().ToUpper().RemoveSign4VietnameseString().Replace("  ", String.Empty) + "@",
                MonthCheck = txtSoThang.Text.Trim(),
                ProjectType = "1@2"
            });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                Cursor.Current = Cursors.Default;
                return;
            }

            gICMA.Populate(ds.FirstTable());

            if (ds.FirstTable().Rows.Count == 0)
            {
                MessageBox.Show("Đã kiểm tra xong ICMA Data Detection: không phát hiện trùng data");
                return;
            }
            else
            {
                MessageBox.Show(String.Format("Đã kiểm tra xong ICMA Data Detection: phát hiện {0}", ds.FirstTable().Rows.Count));
                return;
            }
        }

        private void txtRespondentNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtSearchID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                // binding data
                var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_Answers_FWSearchByGreenID", new
                {
                    ProjectID,
                    GreenID = txtSearchID.Text.Trim(),
                    UserID = Services.GetInformation("UserID")
                });

               

                var ds = Services.Execute(query);
                if (ds == null)
                {
                    ClearOldData();
                    UI.ShowError(Services.LastError);
                    return;
                }
                if (ds.FirstTable().Rows.Count == 0)
                {
                    UI.ShowError("Không tìm thấy dữ liệu mã đáp viên này");
                  
                    ClearOldData();
                    return;
                }
                AnswerID = ds.FirstRow().Item("AnswerID");

                //map data
                txtRespondentNo.Text = ds.FirstRow().Item("GreenID");
                txtName.Text= ds.FirstRow().Item("RespondentFullName");
                dateDOB.Text = ds.FirstRow().Item("RespondentYoB");
                txtStreet.Text= ds.FirstRow().Item("RespondentStreet");
                txtAddress.Text= ds.FirstRow().Item("RespondentAddressLandmark");
                txtTelephone.Text= ds.FirstRow().Item("RespondentTelephone");
                rGender.EditValue = ds.FirstRow().Item("RespondentGender");
                rCity.EditValue= ds.FirstRow().Item("RespondentCity");
                txtDistrict.EditValue= ds.FirstRow().Item("RespondentDistrict");
                txtWard.EditValue= ds.FirstRow().Item("RespondentWard");
                if (ds.FirstRow().Item("RespondentInterviewStatus").IsEmpty())
                {
                    rStatus.EditValue = ds.FirstRow().Item("RespondentStatus");
                }
                else
                {
                    rStatus.EditValue = ds.FirstRow().Item("RespondentInterviewStatus");
                }
                rQCStatus.EditValue = ds.FirstRow().Item("QCCheckedStatus");
                txtCancelReason.Text= ds.FirstRow().Item("QCCancelReason");
                txtRecruit.EditValue = ds.FirstRow().Item("RecruitCode");
                txtEmail.Text = ds.FirstRow().Item("EmailAddress");
                btnSave.Enabled = btnFD.Enabled = btnRD.Enabled = true;

                FacePhoto.Image = ds.Tables[2].FirstRow().Item("Image").StringBase64ToImage();
                _pathImage = "";

                #region  MapDataQuota

                foreach(var control in flowQuota.Controls)
                {
                    if(control.GetType()==typeof(QuotaGroup))
                    {
                        string value = ds.SecondTable()._FindValue("FieldValue", "FieldName", ((QuotaGroup)control).QuotaFieldName);
                        ((QuotaGroup)control).QuotaFieldValue = value;
                    }
                }


                #endregion
                e.Handled = true;
            }
        }

        private void ClearOldData()
        {
            _pathImage = "";
            AnswerID = "";

            txtRespondentNo.Text = txtName.Text = dateDOB.Text = txtAddress.Text = txtTelephone.Text = txtStreet.Text = txtEmail.Text =  txtCancelReason.Text = "";
            rCity.EditValue = rGender.EditValue = txtDistrict.EditValue = txtWard.EditValue = "";
            rQCStatus.EditValue = rStatus.EditValue = "";
            txtRecruit.EditValue = "";
            btnSave.Enabled = btnFD.Enabled = btnRD.Enabled = false;
            txtSearchID.Text = string.Empty;

            foreach (var control in flowQuota.Controls)
            {
                if (control.GetType() == typeof(QuotaGroup))
                {
                    ((QuotaGroup)control).QuotaFieldValue = "";
                }
            }
            gICMA.DataSource = null;

            FacePhoto.Image = null;
            flowLayoutPanel2.Controls.Clear();

            gFaceDetection.Text = "Face Detection ( FD )";

            txtSearchID.Focus();

        }

        private void rCity_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtDistrict.Properties.DataSource = dataHanhChanh.Select(string.Format("City='{0}'", rCity.EditValue.ToString())).CopyToDataTable().Distinct("District");
                txtDistrict.EditValue = null;
            }
            catch
            {

            }
            FindFaceListID();
        }

        private string FindFaceListID()
        {
            if(rCity.EditValue.ToString()=="HỒ CHÍ MINH")
                 return "kadencehcm4";
            if (rCity.EditValue.ToString() == "HÀ NỘI")
                return "kadencehn4";
            return "kadencehn4";
               

            if (rGender.EditValue != null && rCity.EditValue != null && dateDOB.Text.IsNotEmpty())
            {
                string find_value = "";

                if (rCity.EditValue.ToString() == "HỒ CHÍ MINH")
                    find_value += "hcm";
                if (rCity.EditValue.ToString() == "HÀ NỘI")
                    find_value += "hn";

                if (rGender.EditValue.ToString() == "Nam")
                    find_value += "_nam";
                if (rGender.EditValue.ToString() == "Nữ")
                    find_value += "_nu";

                int currentyear = DateTime.Now.Year;

                string currentmonth = DateTime.Now.Month.ToString();

                if (currentmonth.Length < 2)
                    currentmonth = "0" + currentmonth;

                int namsinh = 0;
                if (int.TryParse(dateDOB.Text.ToString(), out namsinh))
                {
                    if (namsinh == 0)
                    {
                        return "";
                     
                    }
                    if (namsinh > currentyear)
                    {
                        return "";
                        
                    }
                    if (currentyear - namsinh >= 18)
                    {
                        find_value += "_nguoilon";
                    }
                    else
                    {
                        find_value += "_treem";
                    }
                }
                find_value += "_" + currentmonth + currentyear.ToString();
                return find_value;
            }
            else
            {
                return "";
            }
        }

        private void txtDistrict_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDistrict.EditValue == null || txtDistrict.Text.IsEmpty())
                {
                    txtWard.Properties.DataSource = dataHanhChanh.Select(string.Format("City='{0}'", rCity.EditValue.ToString())).CopyToDataTable().Distinct("Ward");
                }

                else
                {
                    txtWard.Properties.DataSource = dataHanhChanh.Select(string.Format("City='{0}' AND District='{1}'", rCity.EditValue.ToString(), (txtDistrict.EditValue == null || txtDistrict.EditValue.ToString().IsEmpty()) ? "" : txtDistrict.EditValue.ToString())).CopyToDataTable().Distinct("Ward");

                }
                txtWard.EditValue = null;
            }
            catch
            {

            }
        }

        private void txtDistrict_Popup(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            FilterLookupDistrict(sender);
        }

        private void FilterLookupDistrict(object sender)
        {
            DevExpress.XtraEditors.GridLookUpEdit edit = sender as DevExpress.XtraEditors.GridLookUpEdit;
            DevExpress.XtraGrid.Views.Grid.GridView gridView = edit.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
            System.Reflection.FieldInfo fi = gridView.GetType().GetField("extraFilter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //Text = edit.AutoSearchText;
            DevExpress.Data.Filtering.BinaryOperator op1 = new DevExpress.Data.Filtering.BinaryOperator("District", "%" + edit.AutoSearchText + "%", DevExpress.Data.Filtering.BinaryOperatorType.Like);

            string filterCondition = new DevExpress.Data.Filtering.GroupOperator(DevExpress.Data.Filtering.GroupOperatorType.Or, new DevExpress.Data.Filtering.CriteriaOperator[] { op1 }).ToString();
            fi.SetValue(gridView, filterCondition);

            System.Reflection.MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            mi.Invoke(gridView, null);
        }

        private void txtDistrict_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtWard.Focus();
                // SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }
        
        private void txtDistrict_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                FilterLookupDistrict(sender);
            }));
        }

        private void txtWard_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtWard_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                FilterLookupWard(sender);
            }));
        }

        private void txtWard_Popup(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }

            FilterLookupWard(sender);
        }
        private void FilterLookupWard(object sender)
        {
            DevExpress.XtraEditors.GridLookUpEdit edit = sender as DevExpress.XtraEditors.GridLookUpEdit;
            DevExpress.XtraGrid.Views.Grid.GridView gridView = edit.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
            System.Reflection.FieldInfo fi = gridView.GetType().GetField("extraFilter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //Text = edit.AutoSearchText;
            DevExpress.Data.Filtering.BinaryOperator op1 = new DevExpress.Data.Filtering.BinaryOperator("Ward", "%" + edit.AutoSearchText + "%", DevExpress.Data.Filtering.BinaryOperatorType.Like);

            string filterCondition = new DevExpress.Data.Filtering.GroupOperator(DevExpress.Data.Filtering.GroupOperatorType.Or, new DevExpress.Data.Filtering.CriteriaOperator[] { op1 }).ToString();
            fi.SetValue(gridView, filterCondition);

            System.Reflection.MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            mi.Invoke(gridView, null);
        }
        private void txtWard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtStreet.Focus();
                // SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void rGender_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void dateDOB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void rCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void txtStreet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void rQCStatus_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            //save
            SplashScreenManager.ShowForm(typeof(RunReportWait));

        
                await SaveData();
              

            SplashScreenManager.CloseForm();

          
        }

        private async Task SaveData()
        {
            try
            {

                var query = new DataAccess.RequestCollection();

                if (CheckValidateInput() == false)
                    return;

                string selectedfacelistID = FindFaceListID();
                if (dtFaceList._FindIndex("faceListId", selectedfacelistID) < 0)
                {
                    WriteLog("Vui lòng liên hệ tạo face list trước khi đồng bộ hình");
                    UI.ShowError("Vui lòng liên hệ tạo face list trước khi đồng bộ hình");
                    return;
                }

                AddPersistedFaceResult ressultaddFacetoList;

                query += DataAccess.DataQuery.Create("Docs", "ws_DOC_Answers_SupQuanlReUpdate", new
                {
                    AnswerID,
                    GreenID = txtRespondentNo.Text,
                    ProjectID,
                    RespondentFullName = txtName.Text.Trim().ToUpper(),
                    RespondentAddressLandmark = txtAddress.Text.Trim().ToUpper(),
                    RespondentStreet = txtStreet.Text.Trim().ToUpper(),
                    RespondentWard = (txtWard.EditValue == null || txtWard.EditValue.ToString().IsEmpty()) ? "" : txtWard.Text.ToString().Trim(),
                    RespondentDistrict = (txtDistrict.EditValue == null || txtDistrict.EditValue.ToString().IsEmpty()) ? "" : txtDistrict.Text.ToString(),
                    RespondentCity = rCity.EditValue.ToString(),
                    RespondentTelephone = txtTelephone.Text.Trim(),
                    RespondentGender = rGender.EditValue == null ? "" : rGender.EditValue.ToString(),
                    RespondentStatus = rStatus.EditValue.ToString(),
                    RespondentYoB = dateDOB.Text.Trim(),
                    RecruitCode = txtRecruit.EditValue.ToString(),
                    EmailAddress = txtEmail.Text.Trim(),
                    UserID = Services.GetInformation("UserID")

                });

                foreach (Control ctr in flowQuota.Controls)
                {
                    if (ctr.GetType() == typeof(QuotaGroup))
                    {
                        if ((ctr as QuotaGroup).QuotaFieldValue == null || (ctr as QuotaGroup).QuotaFieldValue.IsEmpty())
                        {
                            UI.ShowError("Vui lòng chọn đầy đủ thông tin quota của đáp viên");
                            return;
                        }
                        query += DataAccess.DataQuery.Create("Docs", "ws_DOC_QuotaControlValues_Save", new
                        {
                            AnswerID,
                            ProjectID,
                            FieldName = (ctr as QuotaGroup).QuotaFieldName,
                            FieldValue = (ctr as QuotaGroup).QuotaFieldValue,
                            UserID = Services.GetInformation("UserID")
                        });
                    }
                }

                Image img = FacePhoto.Image;
                string name = txtName.Text.Trim();
                string temp_Path = _pathImage;
                string ID = AnswerID;

                //ClearOldData();

                if (temp_Path.IsNotEmpty())
                {

                    try
                    {
                        WriteLog(String.Format("Đã gửi yêu cầu lưu thông tin của đáp viên {0}", name));
                        FileStream streamfile = File.OpenRead(temp_Path);

                        MessageBox.Show(String.Format("Đã gửi yêu cầu lưu thông tin của đáp viên {0}", name));
                        WriteLog(String.Format("Resquest AddtoFaceList "));
                        ressultaddFacetoList = await faceServiceClient.AddFaceToFaceListAsync(selectedfacelistID, streamfile, ProjectNo + "_" + name);
                        WriteLog(String.Format("Complted Resquest AddtoFaceList "));
                        var Data = img.ImageToStringBase64();

                        query = DataAccess.DataQuery.Create("KadenceDB", "ws_FaceList_Save", new
                        {
                            ID = ID,
                            FaceID = ressultaddFacetoList.PersistedFaceId,
                            FaceListId = selectedfacelistID,
                            Image = Data,
                            FullName = name,
                            ProjectNo = ProjectNo,
                            UserID = Services.GetInformation("UserID")
                        }) + query;
                    }
                    catch (FaceAPIException ex)
                    {
                        WriteLog(String.Format("Lỗi {0}", ex.ErrorMessage));
                        UI.ShowError(String.Format("Lỗi {0}", ex.ErrorMessage));
                        return;
                    }
                }


                var ds = Services.Execute(query);
                if (ds == null)
                {
                    WriteLog(Services.LastError);
                    MessageBox.Show(Services.LastError);
                    return;
                }
                WriteLog(String.Format("Đã lưu thành công thông tin của đáp viên {0} {1}", name, ID));
                MessageBox.Show(String.Format("Đã lưu thành công thông tin của đáp viên {0}", name));

            }

            catch (Exception ex)
            {
                WriteLog(string.Format(ex.Message));
                UI.ShowError(ex.Message);
                return;
            }
        }

        private bool CheckValidateInput()
        {
            
            if (txtName.Text.IsEmpty())
            {
                UI.ShowError("Vui lòng nhập Họ và tên");
                txtName.Focus();
                return false;

            }
            if (dateDOB.Text.IsEmpty())
            {
                UI.ShowError("Vui lòng nhập năm sinh");
                dateDOB.Focus();
                return false;
            }
            if (txtStreet.Text.IsEmpty())
            {
                UI.ShowError("Vui lòng nhập tên đường");
                txtStreet.Focus();
                return false;
            }
            if (txtAddress.Text.IsEmpty())
            {
                UI.ShowError("Vui lòng nhập địa chỉ");
                txtAddress.Focus();
                return false;
            }

            if (txtTelephone.Text.IsEmpty())
            {
                UI.ShowError("Vui lòng nhập điện thoại");
                txtTelephone.Focus();
                return false;
            }

            if (rGender.EditValue == null || rGender.EditValue.ToString().IsEmpty())
            {
                UI.ShowError("Vui lòng chọn giới tính");
                rGender.Focus();
                return false;
            }

            if (rCity.EditValue == null || rCity.EditValue.ToString().IsEmpty())
            {
                UI.ShowError("Vui lòng thành phố");
                rCity.Focus();
                return false;
            }

            if (txtDistrict.EditValue == null || txtDistrict.EditValue.ToString().IsEmpty())
            {
                UI.ShowError("Vui lòng quận");
                txtDistrict.Focus();
                return false;
            }

            if (txtWard.EditValue == null || txtWard.EditValue.ToString().IsEmpty())
            {
                UI.ShowError("Vui lòng nhập phường");
                txtWard.Focus();
                return false;
            }

            if (txtRecruit.EditValue == null || txtRecruit.EditValue.ToString().IsEmpty())
            {
                UI.ShowError("Vui lòng nhập PVV");
                txtRecruit.Focus();
                return false;
            }

            foreach (Control ctr in flowQuota.Controls)
            {
                if (ctr.GetType() == typeof(QuotaGroup))
                {
                    if ((ctr as QuotaGroup).QuotaFieldValue == null || (ctr as QuotaGroup).QuotaFieldValue.IsEmpty())
                    {
                        UI.ShowError("Vui lòng chọn đầy đủ thông tin quota của đáp viên");
                        return false;
                    }

                }
            }

            return true;
        }

        private void txtRecruit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                FilterLookupStaff(sender);
            }));
        }
        private void FilterLookupStaff(object sender)
        {
            DevExpress.XtraEditors.GridLookUpEdit edit = sender as DevExpress.XtraEditors.GridLookUpEdit;
            DevExpress.XtraGrid.Views.Grid.GridView gridView = edit.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
            System.Reflection.FieldInfo fi = gridView.GetType().GetField("extraFilter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //Text = edit.AutoSearchText;
            DevExpress.Data.Filtering.BinaryOperator op1 = new DevExpress.Data.Filtering.BinaryOperator("StaffID", "%" + edit.AutoSearchText + "%", DevExpress.Data.Filtering.BinaryOperatorType.Like);
            DevExpress.Data.Filtering.BinaryOperator op2 = new DevExpress.Data.Filtering.BinaryOperator("FullName", "%" + edit.AutoSearchText + "%", DevExpress.Data.Filtering.BinaryOperatorType.Like);
            string filterCondition = new DevExpress.Data.Filtering.GroupOperator(DevExpress.Data.Filtering.GroupOperatorType.Or, new DevExpress.Data.Filtering.CriteriaOperator[] { op1, op2 }).ToString();
            fi.SetValue(gridView, filterCondition);

            System.Reflection.MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            mi.Invoke(gridView, null);
        }
        private void luStaffID_Popup(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }

            FilterLookupStaff(sender);
        }

        private void rCity_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                txtDistrict.Properties.DataSource = dataHanhChanh.Select(string.Format("City='{0}'", rCity.EditValue.ToString())).CopyToDataTable().Distinct("District");

                
                txtDistrict.EditValue = null;
            }
            catch
            {

            }
        }

        private void rCity_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                FilterLookupCity(sender);
            }));
        }

        private void rCity_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtDistrict.Focus();
                // SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void rCity_Popup(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }

            FilterLookupCity(sender);
        }
        private void FilterLookupCity(object sender)
        {
            DevExpress.XtraEditors.GridLookUpEdit edit = sender as DevExpress.XtraEditors.GridLookUpEdit;
            DevExpress.XtraGrid.Views.Grid.GridView gridView = edit.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
            System.Reflection.FieldInfo fi = gridView.GetType().GetField("extraFilter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //Text = edit.AutoSearchText;
            DevExpress.Data.Filtering.BinaryOperator op1 = new DevExpress.Data.Filtering.BinaryOperator("City", "%" + edit.AutoSearchText + "%", DevExpress.Data.Filtering.BinaryOperatorType.Like);
         
            string filterCondition = new DevExpress.Data.Filtering.GroupOperator(DevExpress.Data.Filtering.GroupOperatorType.Or, new DevExpress.Data.Filtering.CriteriaOperator[] { op1 }).ToString();
            fi.SetValue(gridView, filterCondition);

            System.Reflection.MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            mi.Invoke(gridView, null);
        }
    }
}
