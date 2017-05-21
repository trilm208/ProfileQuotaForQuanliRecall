using DevExpress.XtraEditors;

//using Infragistics.Win.UltraWinGrid;
//using Infragistics.Win.UltraWinTabControl;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Design;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QA
{
    public static class QAPaint
    {
        public static Color SetControlColor(this DevExpress.LookAndFeel.UserLookAndFeel LookAndFeel, string ColorName)
        {
            Color color = Color.Transparent;

            DevExpress.Skins.Skin activeSkin;
            activeSkin = DevExpress.Skins.CommonSkins.GetSkin(LookAndFeel);

            Object property = activeSkin.Properties[ColorName];
            if (property != null && property is Color)
            {
                color = (Color)property;
            }

            return color;
        }

        public static void SetControlColor(this System.Windows.Forms.Control.ControlCollection controls)
        {
            controls.SetControlColor(DevExpress.LookAndFeel.UserLookAndFeel.Default);
        }

        public static void SetControlColor(this System.Windows.Forms.Control.ControlCollection controls, string SkinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(SkinName);
            controls.SetControlColor(DevExpress.LookAndFeel.UserLookAndFeel.Default);
        }

        public static void SetControlColor(this System.Windows.Forms.Control.ControlCollection controls, DevExpress.LookAndFeel.UserLookAndFeel LookAndFeel)
        {
            foreach (Control ctl in controls)
            {
                ctl.SetControlColor(LookAndFeel);
                if (ctl.HasChildren == true)
                {
                    ctl.Controls.SetControlColor(LookAndFeel);
                }
            }
        }

        public static void SetControlColor(this Control ctl)
        {
            ctl.SetControlColor(DevExpress.LookAndFeel.UserLookAndFeel.Default);
        }

        public static void SetControlColor(this Control ctl, DevExpress.LookAndFeel.UserLookAndFeel LookAndFeel)
        {
            switch (ctl.GetType().Name)
            {
                case "GroupControl":
                    (ctl as GroupControl).LookAndFeel.UseDefaultLookAndFeel = false;
                    if ((ctl as GroupControl).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("QA_Caramel");
                        (ctl as GroupControl).LookAndFeel.SkinName = "QA_Caramel_Smart";
                        //(ctl as GroupControl).LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003 ;

                        //(ctl as GroupControl).AppearanceCaption.BackColor = System.Drawing.Color.DeepSkyBlue;
                        //(ctl as GroupControl).AppearanceCaption.BackColor2 = System.Drawing.Color.RoyalBlue;

                      //  (ctl as GroupControl).AppearanceCaption.ForeColor = System.Drawing.Color.White;

                        //(ctl as GroupControl).AppearanceCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;

                        //(ctl as GroupControl).AppearanceCaption.ForeColor = System.Drawing.Color.Brown;

                        //đổi caption panel của group control
                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#F0EEEE");
                        //(ctl as GroupControl).AppearanceCaption.BackColor = col;
                        //(ctl as GroupControl).AppearanceCaption.BackColor2 = col;

                        //Font
                        //(ctl as GroupControl).AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);

                        // đổi màu nền của group control
                        (ctl as GroupControl).Appearance.BackColor = col;// System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
                        (ctl as GroupControl).Appearance.BackColor2 = col; //System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
                         (ctl as GroupControl).AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                         (ctl as GroupControl).AppearanceCaption.Options.UseFont = true;
                        //(ctl as GroupControl).BackColor = col;

                        //(ctl as GroupControl).Appearance.Options.UseBorderColor = true;
                        //(ctl as GroupControl).Appearance.BorderColor = System.Drawing.Color.Transparent;

                        //thay  đổi font chữ của groupcontrol Caption
                        //(ctl as GroupControl).AppearanceCaption.Font=new Font("Times New Roman", 12.0f);
                    }
                    break;

                case "GridControl":
                    GridView view = (GridView)(ctl as GridControl).MainView;
                    // Cells
                    if ((ctl as GridControl).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as GridControl).LookAndFeel.SkinName = "QA_Caramel_Smart";
                        //(ctl as GridControl).LookAndFeel.SkinName = "";
                        //(ctl as GridControl).LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
                        (ctl as GridControl).LookAndFeel.UseDefaultLookAndFeel = false;
                        //view.OptionsBehavior.Editable = false;
                        //view.OptionsBehavior.ReadOnly = true;
                      
                        //Set color for Grid HeaderPanel
                        view.Appearance.HeaderPanel.Options.UseBackColor = true;

                        //view.Appearance.HeaderPanel.BackColor = System.Drawing.Color.DeepSkyBlue;//DeepSkyBlue
                        //view.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.RoyalBlue;//RoyalBlue;
                        //view.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
                        view.OptionsView.ShowIndicator = false;
                        view.RowHeight = 26;

                        view.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
                        view.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
                        //// Font header
                        //view.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
                        view.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);

                        // Font row
                        view.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);

                        //Set color for Grid selected #C3D3F1
                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#A3D867");
                        Color backcolor = col;
                        Color backcolor2 = col;
                        Color forecolor = Color.Black;
                        Color bordercolor = Color.Transparent;

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
                        //  view.Appearance.HideSelectionRow.BorderColor = bordercolor;

                      
                    }
                    break;

                case "PanelControl":
                    (ctl as PanelControl).LookAndFeel.UseDefaultLookAndFeel = false;
                    if ((ctl as PanelControl).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as PanelControl).LookAndFeel.SkinName = "QA_Caramel_Smart";
                        //(ctl as PanelControl).LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                        //đổi caption panel của group control
                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#F0EEEE");
                        //(ctl as GroupControl).AppearanceCaption.BackColor = System.Drawing.Color.Silver;
                        //(ctl as GroupControl).AppearanceCaption.BackColor2 = System.Drawing.Color.Silver;

                        // đổi màu nền của group control
                        (ctl as PanelControl).Appearance.BackColor = col; // System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                        (ctl as PanelControl).Appearance.BackColor2 = col;// System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));

                        (ctl as PanelControl).Appearance.Options.UseBorderColor = true;
                        (ctl as PanelControl).Appearance.BorderColor = System.Drawing.Color.Black;

                        //thay  đổi font chữ của groupcontrol Caption
                        //(ctl as GroupControl).AppearanceCaption.Font=new Font("Times New Roman", 12.0f);
                    }
                    break;

                case "XtraTabControl":
                    (ctl as DevExpress.XtraTab.XtraTabControl).LookAndFeel.UseDefaultLookAndFeel = false;
                    if ((ctl as DevExpress.XtraTab.XtraTabControl).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as DevExpress.XtraTab.XtraTabControl).LookAndFeel.SkinName = "QA_Caramel_Smart";

                        (ctl as DevExpress.XtraTab.XtraTabControl).AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 9.5F);
                        //(ctl as DevExpress.XtraTab.XtraTabControl).AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        //(ctl as DevExpress.XtraTab.XtraTabControl).SelectedTabPage.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 10F);

                        //(ctl as DevExpress.XtraTab.XtraTabControl).SelectedTabPage.Appearance.HeaderDisabled.Font = new System.Drawing.Font("Tahoma", 10F);

                        //(ctl as DevExpress.XtraTab.XtraTabControl).SelectedTabPage.Appearance.HeaderActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //(ctl as DevExpress.XtraTab.XtraTabControl).SelectedTabPage.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 8.5F);
                    }
                    break;

                case "SimpleButton":
                    (ctl as SimpleButton).LookAndFeel.UseDefaultLookAndFeel = false;
                    if ((ctl as SimpleButton).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as SimpleButton).LookAndFeel.SkinName = "QA_Caramel_Smart";

                        (ctl as SimpleButton).Appearance.Font = new System.Drawing.Font("", 8F, System.Drawing.FontStyle.Bold);
                        (ctl as SimpleButton).Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;//(ctl as SimpleButton).Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
                        (ctl as SimpleButton).ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

                        (ctl as SimpleButton).Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                        (ctl as SimpleButton).Appearance.ForeColor = System.Drawing.Color.White;

                        #region none

                        (ctl as SimpleButton).LookAndFeel.SkinName = "Office 2013";
                        (ctl as SimpleButton).LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                        (ctl as SimpleButton).Appearance.BackColor = System.Drawing.Color.DeepSkyBlue;
                        (ctl as SimpleButton).Appearance.BackColor2 = System.Drawing.Color.RoyalBlue;

                        #endregion none
                    }
                    break;

                case "LookUpEdit":
                    (ctl as LookUpEdit).LookAndFeel.UseDefaultLookAndFeel = false;

                    if ((ctl as LookUpEdit).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as LookUpEdit).LookAndFeel.SkinName = "QA_Caramel_Smart";

                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#A3D867");
                        Color backcolor = col;
                        Color backcolor2 = col;

                        //(ctl as LookUpEdit).Properties.Appearance.BackColor = backcolor;
                        //(ctl as LookUpEdit).Properties.Appearance.ForeColor = System.Drawing.Color.Black;

                        //(ctl as LookUpEdit).Properties.AppearanceFocused.BackColor = backcolor;
                        //(ctl as LookUpEdit).Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black;

                        //(ctl as LookUpEdit).LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                        //(ctl as LookUpEdit).Properties.Appearance.BackColor = System.Drawing.Color.LightBlue;
                        //(ctl as LookUpEdit).Properties.Appearance.ForeColor = System.Drawing.Color.Black;

                        //(ctl as LookUpEdit).Properties.AppearanceFocused.BackColor = System.Drawing.Color.SpringGreen;
                        //(ctl as LookUpEdit).Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black;

                        //(ctl as LookUpEdit).Appearance.BackColor = System.Drawing.Color.DeepSkyBlue;
                        //(ctl as LookUpEdit).Appearance.BackColor2 = System.Drawing.Color.RoyalBlue;
                        //(ctl as LookUpEdit).Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
                        //(ctl as LookUpEdit).Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                        //(ctl as LookUpEdit).Appearance.ForeColor = System.Drawing.Color.White;
                    }
                    break;

                case "MyLookUpEdit":
                    (ctl as LookUpEdit).LookAndFeel.UseDefaultLookAndFeel = false;

                    if ((ctl as LookUpEdit).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as LookUpEdit).LookAndFeel.SkinName = "QA_Caramel_Smart";

                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#A3D867");
                        Color backcolor = col;
                        Color backcolor2 = col;

                        //(ctl as LookUpEdit).Properties.Appearance.BackColor = backcolor;
                        //(ctl as LookUpEdit).Properties.Appearance.ForeColor = System.Drawing.Color.Black;

                        //(ctl as LookUpEdit).Properties.AppearanceFocused.BackColor = backcolor;
                        //(ctl as LookUpEdit).Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black;

                        //(ctl as LookUpEdit).LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                        //(ctl as LookUpEdit).Properties.Appearance.BackColor = System.Drawing.Color.LightBlue;
                        //(ctl as LookUpEdit).Properties.Appearance.ForeColor = System.Drawing.Color.Black;

                        //(ctl as LookUpEdit).Properties.AppearanceFocused.BackColor = System.Drawing.Color.SpringGreen;
                        //(ctl as LookUpEdit).Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black;

                        //(ctl as LookUpEdit).Appearance.BackColor = System.Drawing.Color.DeepSkyBlue;
                        //(ctl as LookUpEdit).Appearance.BackColor2 = System.Drawing.Color.RoyalBlue;
                        //(ctl as LookUpEdit).Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
                        //(ctl as LookUpEdit).Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                        //(ctl as LookUpEdit).Appearance.ForeColor = System.Drawing.Color.White;
                    }
                    break;

                case "SearchLookUpEdit":
                    (ctl as SearchLookUpEdit).LookAndFeel.UseDefaultLookAndFeel = false;

                    GridView viewSearch = (GridView)(ctl as SearchLookUpEdit).Properties.View;

                    if ((ctl as SearchLookUpEdit).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as SearchLookUpEdit).LookAndFeel.SkinName = "QA_Caramel_Smart";

                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#A3D867");
                        Color backcolor = col;
                        Color backcolor2 = col;
                        Color forecolor = Color.Black;
                        Color bordercolor = Color.Transparent;
                        // searchLookUpEdit1.Properties.PopupFindMode =
                        //(ctl as SearchLookUpEdit).Properties.PopupFindMode = DevExpress.XtraEditors.FindMode.Always;
                        //(ctl as SearchLookUpEdit).Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                        viewSearch.Appearance.SelectedRow.BackColor = backcolor;
                        viewSearch.Appearance.SelectedRow.BackColor2 = backcolor2;
                        viewSearch.Appearance.SelectedRow.ForeColor = forecolor;
                        //view.Appearance.SelectedRow.BorderColor = bordercolor;
                        viewSearch.Appearance.FocusedRow.BackColor = backcolor;
                        viewSearch.Appearance.FocusedRow.BackColor2 = backcolor2;
                        viewSearch.Appearance.FocusedRow.ForeColor = forecolor;
                        //view.Appearance.FocusedRow.BorderColor = bordercolor;
                        viewSearch.Appearance.HideSelectionRow.BackColor = backcolor;
                        viewSearch.Appearance.HideSelectionRow.BackColor2 = backcolor2;
                        viewSearch.Appearance.HideSelectionRow.ForeColor = forecolor;

                        viewSearch.Appearance.FocusedCell.BackColor = backcolor;
                        viewSearch.Appearance.FocusedCell.BackColor2 = backcolor2;
                        viewSearch.Appearance.FocusedCell.ForeColor = forecolor;
                        viewSearch.Appearance.HideSelectionRow.BorderColor = bordercolor;
                    }
                    break;

                case "ComboBoxEdit":
                    (ctl as ComboBoxEdit).LookAndFeel.UseDefaultLookAndFeel = false;
                    if ((ctl as ComboBoxEdit).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as ComboBoxEdit).LookAndFeel.SkinName = "QA_Caramel_Smart";

                        //System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#A3D867");
                        //Color backcolor = col;
                        //Color backcolor2 = col;
                        //Color forecolor = Color.Black;
                        //Color bordercolor = Color.Transparent;

                        //(ctl as ComboBoxEdit).Properties.AppearanceFocused.BackColor = col;
                        //(ctl as ComboBoxEdit).Properties.AppearanceFocused.BackColor2 = col;

                        //System.Drawing.Color col = Color.Transparent; //System.Drawing.ColorTranslator.FromHtml("#DADADA");
                        //(ctl as ComboBoxEdit).BackColor = col;
                    }
                    break;

                case "LabelControl":
                    (ctl as LabelControl).LookAndFeel.UseDefaultLookAndFeel = false;
                    if ((ctl as LabelControl).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as LabelControl).LookAndFeel.SkinName = "QA_Caramel_Smart";

                        System.Drawing.Color col = Color.Transparent; //System.Drawing.ColorTranslator.FromHtml("#DADADA");
                        (ctl as LabelControl).BackColor = col;
                    }
                    break;

                case "CheckEdit":
                    (ctl as CheckEdit).LookAndFeel.UseDefaultLookAndFeel = false;
                    if ((ctl as CheckEdit).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as CheckEdit).LookAndFeel.SkinName = "QA_Caramel_Smart";
                    }
                    break;

                case "RadioGroup":
                    (ctl as RadioGroup).LookAndFeel.UseDefaultLookAndFeel = false;
                    if ((ctl as RadioGroup).LookAndFeel.SkinName != "QA_Caramel_Smart")
                    {
                        (ctl as RadioGroup).LookAndFeel.SkinName = "QA_Caramel_Smart";
                    }
                    break;

                //case "MRUEdit":
                //    (ctl as MRUEdit).LookAndFeel.UseDefaultLookAndFeel = false;
                //    if ((ctl as MRUEdit).LookAndFeel.SkinName != "QA_Caramel_Smart")
                //    {
                //        (ctl as MRUEdit).LookAndFeel.SkinName = "Office 2013";

                //        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#A3D867");
                //        Color backcolor = col;
                //        Color backcolor2 = col;
                //        Color forecolor = Color.Black;
                //        Color bordercolor = Color.Transparent;

                //        (ctl as MRUEdit).Properties.AppearanceDropDown.BackColor = col;
                //        (ctl as MRUEdit).Properties.AppearanceDropDown.BackColor2 = col;
                //        // (ctl as MRUEdit).Properties.AppearanceDropDown.ForeColor = col;
                //    }
                //    break;

                //    break;
            }
        }
    }
}