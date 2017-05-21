using QA.Controls;
using System;
using System.ComponentModel;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QA
{
    public partial class ClientControl : UserControl
    {
        [Browsable(false)]
        public IClientServices Services { get; set; }

        public delegate void OnResidentChangeHandler();

        public delegate void OnProcessHandler();

        public delegate void OnFacilityChangeHandler(string FacilityID, string FacilityName);

        public delegate void OnGenericActionHandler(string Action, object ActionParam);

        public delegate bool OnInitializeHandler(IClientServices Services, bool ReadOnly);

        public delegate void BeforeApplyStyleHandler(object Sender, CancelEventArgs e);

        public delegate void OnNavigateHandler(string SubMenu);

        public event OnResidentChangeHandler OnResidentChange;

        public event OnFacilityChangeHandler OnFacilityChange;

        public event OnGenericActionHandler OnGenericAction;

        public event OnProcessHandler OnProcess;

        public event OnInitializeHandler OnInitialize;

        public event CancelEventHandler OnClientClose;

        public event OnNavigateHandler OnNavigate;

        public event EventHandler CloseForm;

        public event CancelEventHandler OnBeforeResidentChange;

        public event BeforeApplyStyleHandler BeforeApplyStyle;

        public ClientControl()
        {
            InitializeComponent();
        }

        public virtual void Initialize(IClientServices services)
        {
            this.Services = services;

            var children = this.GetChildren();
            foreach (var memoEdit in children.OfType<QA.Shared.Controls.MemoEdit>())
            {
                memoEdit.Initialize(services);
                memoEdit.Process();
            }

            foreach (var comboBox in children.OfType<QA.Shared.Controls.ComboBox>())
            {
                comboBox.Initialize(services);
                comboBox.Process();
            }

          

          

            //foreach (var control in children.OfType<QA.Controls.QAPractice.ICDCode>())
            //{
            //    control.Initialize(services);
            //    control.Process();
            //}

            //foreach (var control in children.OfType<QA.Controls.QAPractice.ICDName>())
            //{
            //    control.Initialize(services);
            //    control.Process();
            //}

           

        }

        public virtual void Process()
        {
            System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#DADADA");
            this.BackColor = col;

            //PrivateFontCollection pfc = new PrivateFontCollection();

            //pfc.AddFontFile("D:\\QA Solutions SVN\\Client\\@Font\\utm-avo.ttf");
            //pfc.AddFontFile(Path.Combine(Application.StartupPath, "@Font\\utm-avo.ttf"));
            //pfc.AddFontFile("D:\\QA Solutions SVN\\Client\\@Fontboxvn-UTM-Avo\\utm-avobold.ttf");
            //var style = System.Drawing.FontStyle.Regular;
            //this.Font = new System.Drawing.Font("Arial", 7.5F, style);
            //var ff = new System.Drawing.Font(pfc.Families[0], 7.5F, style);
            //var f = new System.Drawing.Font("Arial", 7.5F, style);
            //var font = this.Font;
            //this.Font = ff;

            bool cancelStyle = false;
            if (BeforeApplyStyle != null)
            {
                CancelEventArgs e = new CancelEventArgs(false);
                BeforeApplyStyle(this, e);
                cancelStyle = e.Cancel;
                e = null;
            }
            if (cancelStyle == false)
                QAPaint();
            if (OnProcess != null) OnProcess();
        }

        public void QAPaint()
        {
            this.Controls.SetControlColor();
            //this.Controls
        }

        private void SetControlColor()
        {
        }
    }
}