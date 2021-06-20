using EGOTeino.Framework.Core;
using Hook;
using EGO.SolidUI;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EGOTeino.Framework.UI
{
    public class SettingForm : IForm
    {
        #region fields used to defind sizeable feature for form
        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int THICKNESS = 5;
        #endregion fields
        #region props used to defind sizeable feature for form
        Rectangle R_Top { get => new Rectangle(0, 0, this.ClientSize.Width, THICKNESS); }
        Rectangle R_Left { get => new Rectangle(0, 0, THICKNESS, this.ClientSize.Height); }
        Rectangle R_Bottom { get => new Rectangle(0, this.ClientSize.Height - THICKNESS, this.ClientSize.Width, THICKNESS); }
        Rectangle R_Right { get => new Rectangle(this.ClientSize.Width - THICKNESS, 0, THICKNESS, this.ClientSize.Height); }
        Rectangle TopLeft { get => new Rectangle(0, 0, THICKNESS, THICKNESS); }
        Rectangle TopRight { get => new Rectangle(this.ClientSize.Width - THICKNESS, 0, THICKNESS, THICKNESS); }
        Rectangle BottomLeft { get => new Rectangle(0, this.ClientSize.Height - THICKNESS, THICKNESS, THICKNESS); }
        Rectangle BottomRight { get => new Rectangle(this.ClientSize.Width - THICKNESS, this.ClientSize.Height - THICKNESS, THICKNESS, THICKNESS); }

        #endregion props

        public Language CurrentLanguage { get; set; }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// keeping order of setting panels for backward navgation
        /// </summary>
        public Stack<Type> PanelStack = new Stack<Type>();
        /// <summary>
        /// languages store
        /// </summary>
        public Dictionary _dictionary;
        /// <summary>
        /// setting items store
        /// </summary>
        public SettingProvider _settingProvider;
        /// <summary>
        /// hook for shortcut keys
        /// </summary>
        public HookManager _hookManager;
        public SettingForm(Dictionary dataSet, SettingProvider settingProvider, HookManager hookManager)
        {
            _dictionary = dataSet;
            _settingProvider = settingProvider;
            _hookManager = hookManager;
            InitializeComponent();
            lbl_head.MouseDown += MouseKeyDown;
            PanelContainer.ControlRemoved += PanelContainer_ControlRemoved;
            FormClosing += Setting_FormClosing;
            VisibleChanged += SettingForm_VisibleChanged;
            this.Icon = Properties.Resources.MainIcon;
            PanelStack.Push(typeof(GeneralSettingPanel));
            backbtn(false);
            Sync();
        }

        /// <summary>
        /// keep the state of last showing panel in setting window
        /// create on opening
        /// stack and dispose on closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                CreatePanel();
            }
            else
            {
                PanelStack.Push(PanelContainer.Controls[0].GetType());
                PanelContainer.Controls[0].Dispose();
            }
        }
        /// <summary>
        /// prevent destroy window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                Hide();
            }
        }
        /// <summary>
        /// dispose top most panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_back_Click(object sender, EventArgs e)
        {
            foreach (IControlPanel item in PanelContainer.Controls)
            {
                item.Dispose();
            }
        }
        /// <summary>
        /// when one panel get disposed this method fires
        /// peek the last panel in stack to change the state of back button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelContainer_ControlRemoved(object sender, ControlEventArgs e)
        {
            backbtn(true);
            if (PanelContainer.Controls.Count > 0) return;
            if (PanelStack.Peek() == typeof(GeneralSettingPanel))
            {
                backbtn(false);
            }
            else
            {
                backbtn(true);
            }
            if (Visible)
                CreatePanel();
        }
        /// <summary>
        /// pops the last panel in stack and construct and show in window as top most panel
        /// and sets the title of setting window
        /// </summary>
        private void CreatePanel()
        {
            Control c = (Control)Activator.CreateInstance(PanelStack.Pop(), this);
            PanelContainer.Controls.Add(c);
            lbl_head.Text = c.Tag.ToString();
        }

        /// <summary>
        /// back button state changer and push window title
        /// </summary>
        /// <param name="show"></param>
        private void backbtn(bool show)
        {
            btn_back.Visible = show;
            if (show) lbl_head.Padding = new Padding(35, 0, 0, 0);
            else lbl_head.Padding = new Padding(0);
        }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (R_Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (R_Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (R_Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (R_Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }


        #region designer
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Close = new EGO.SolidUI.FirstButton();
            this.lbl_head = new EGO.SolidUI.FirstLabel();
            this.btn_back = new EGO.SolidUI.FirstButton();
            this.PanelContainer = new EGO.SolidUI.FirstPanel();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.btn_Close.FontSize = 12F;
            this.btn_Close.Location = new System.Drawing.Point(575, 5);
            this.btn_Close.ManualDarkTheme = true;
            this.btn_Close.ManualInvert = true;
            this.btn_Close.ManualThemeColor = true;
            this.btn_Close.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.ReverseDarkTheme = false;
            this.btn_Close.ReverseInvertAction = false;
            this.btn_Close.Size = new System.Drawing.Size(30, 30);
            this.btn_Close.TabIndex = 3;
            this.btn_Close.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbl_head
            // 
            this.lbl_head.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_head.Font = new System.Drawing.Font("Segoe UI Light", 16.5F);
            this.lbl_head.FontSize = 16.5F;
            this.lbl_head.Location = new System.Drawing.Point(5, 5);
            this.lbl_head.ManualDarkTheme = false;
            this.lbl_head.ManualInvert = false;
            this.lbl_head.ManualThemeColor = false;
            this.lbl_head.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_head.Name = "lbl_head";
            this.lbl_head.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.lbl_head.ReverseDarkTheme = false;
            this.lbl_head.ReverseInvertAction = false;
            this.lbl_head.Size = new System.Drawing.Size(600, 30);
            this.lbl_head.TabIndex = 2;
            this.lbl_head.Text = "General";
            // 
            // btn_back
            // 
            this.btn_back.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_back.FlatAppearance.BorderSize = 0;
            this.btn_back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_back.Font = new System.Drawing.Font("Segoe UI Light", 10F);
            this.btn_back.FontSize = 10F;
            this.btn_back.Location = new System.Drawing.Point(5, 5);
            this.btn_back.ManualDarkTheme = false;
            this.btn_back.ManualInvert = false;
            this.btn_back.ManualThemeColor = false;
            this.btn_back.Margin = new System.Windows.Forms.Padding(5);
            this.btn_back.Name = "btn_back";
            this.btn_back.ReverseDarkTheme = false;
            this.btn_back.ReverseInvertAction = false;
            this.btn_back.Size = new System.Drawing.Size(30, 30);
            this.btn_back.TabIndex = 4;
            this.btn_back.Text = "←";
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // PanelContainer
            // 
            this.PanelContainer.AlwaysThemeToBack = false;
            this.PanelContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PanelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContainer.Location = new System.Drawing.Point(5, 35);
            this.PanelContainer.ManualDarkTheme = false;
            this.PanelContainer.ManualInvert = false;
            this.PanelContainer.ManualThemeColor = false;
            this.PanelContainer.Margin = new System.Windows.Forms.Padding(5);
            this.PanelContainer.Name = "PanelContainer";
            this.PanelContainer.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.PanelContainer.ReverseDarkTheme = false;
            this.PanelContainer.ReverseInvertAction = false;
            this.PanelContainer.Size = new System.Drawing.Size(600, 605);
            this.PanelContainer.TabIndex = 5;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 645);
            this.Controls.Add(this.PanelContainer);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.lbl_head);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(610, 645);
            this.Name = "Setting";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ReverseInvertAction = true;
            this.Text = "Setting";
            this.ResumeLayout(false);

        }

        #endregion

        private EGO.SolidUI.FirstButton btn_Close;
        public EGO.SolidUI.FirstLabel lbl_head;
        public EGO.SolidUI.FirstButton btn_back;
        public EGO.SolidUI.FirstPanel PanelContainer;
        #endregion
    }
}
