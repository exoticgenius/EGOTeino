using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EGOTeino.Framework.Core;
using EGO.SolidUI;

namespace EGOTeino.Framework.UI
{
    public partial class MainForm : IForm
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

        /// <summary>
        /// dependency field
        /// </summary>
        SettingForm _setting;
        /// <summary>
        /// dependency field
        /// </summary>
        MainCore _mainCore;

        /// <summary>
        /// creates a main form with dependencies
        /// </summary>
        /// <param name="setting">setting form dependency</param>
        /// <param name="mainCore">main core dependency</param>
        public MainForm(SettingForm setting, MainCore mainCore)
        {
            _setting = setting;
            _mainCore = mainCore;
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            lbl_head.MouseDown += this.MouseKeyDown;
            FormClosing += Main_FormClosing;
            this.Icon = Properties.Resources.MainIcon;
            Sync();
        }

        /// <summary>
        /// keeping main thread alive until user not attempted to exit program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing  && _setting._settingProvider.ExitToTray)
            {
                Hide();
                e.Cancel = true;
                return;
            }
            Application.Exit();
        }

        private void btn_Setting_Click(object sender, EventArgs e)
        {
            _setting.Show();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Currect_Click(object sender, EventArgs e)
        {
            if (!_mainCore.WrongLanguage())
                txt_Content.Textes = _mainCore.Action(txt_Content.Textes);
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
            this.lbl_head = new EGO.SolidUI.FirstLabel();
            this.btn_Close = new EGO.SolidUI.FirstButton();
            this.txt_Content = new EGO.SolidUI.FirstTextbox();
            this.btn_Correct = new EGO.SolidUI.FirstButton();
            this.btn_Setting = new EGO.SolidUI.FirstButton();
            this.SuspendLayout();
            // 
            // lbl_head
            // 
            this.lbl_head.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.lbl_head.DarkTheme = false;
            this.lbl_head.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_head.Font = new System.Drawing.Font("Segoe UI Light", 16.5F);
            this.lbl_head.FontSize = 16.5F;
            this.lbl_head.ForeColor = System.Drawing.Color.Teal;
            this.lbl_head.Inverted = false;
            this.lbl_head.Location = new System.Drawing.Point(5, 5);
            this.lbl_head.ManualDarkTheme = false;
            this.lbl_head.ManualInvert = false;
            this.lbl_head.ManualThemeColor = false;
            this.lbl_head.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_head.Name = "lbl_head";
            this.lbl_head.ReverseDarkTheme = false;
            this.lbl_head.ReverseInvertAction = false;
            this.lbl_head.Size = new System.Drawing.Size(390, 30);
            this.lbl_head.TabIndex = 0;
            this.lbl_head.Text = "EGO TEINO";
            this.lbl_head.ThemeColor = System.Drawing.Color.Teal;
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Close.DarkTheme = true;
            this.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.btn_Close.FontSize = 12F;
            this.btn_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btn_Close.Inverted = false;
            this.btn_Close.Location = new System.Drawing.Point(365, 5);
            this.btn_Close.ManualDarkTheme = true;
            this.btn_Close.ManualInvert = true;
            this.btn_Close.ManualThemeColor = true;
            this.btn_Close.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.ReverseDarkTheme = false;
            this.btn_Close.ReverseInvertAction = false;
            this.btn_Close.Size = new System.Drawing.Size(30, 30);
            this.btn_Close.TabIndex = 1;
            this.btn_Close.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_Content
            // 
            this.txt_Content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Content.AutoScroll = true;
            this.txt_Content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_Content.BorderBottom = true;
            this.txt_Content.BorderLeft = true;
            this.txt_Content.BorderRight = true;
            this.txt_Content.BorderTop = true;
            this.txt_Content.DarkTheme = false;
            this.txt_Content.EnableResponsiveStyle = true;
            this.txt_Content.ForeColor = System.Drawing.Color.Teal;
            this.txt_Content.HideSelection = true;
            this.txt_Content.Inverted = false;
            this.txt_Content.Location = new System.Drawing.Point(10, 45);
            this.txt_Content.ManualDarkTheme = false;
            this.txt_Content.ManualInvert = false;
            this.txt_Content.ManualThemeColor = false;
            this.txt_Content.Margin = new System.Windows.Forms.Padding(5);
            this.txt_Content.MouseOn = false;
            this.txt_Content.MultiLine = true;
            this.txt_Content.Name = "txt_Content";
            this.txt_Content.PasswordChar = '\0';
            this.txt_Content.ReverseDarkTheme = false;
            this.txt_Content.ReverseInvertAction = false;
            this.txt_Content.SelectionStart = 0;
            this.txt_Content.Size = new System.Drawing.Size(380, 197);
            this.txt_Content.TabIndex = 2;
            this.txt_Content.Textes = "";
            this.txt_Content.ThemeColor = System.Drawing.Color.Teal;
            // 
            // btn_Currect
            // 
            this.btn_Correct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Correct.BackColor = System.Drawing.Color.Teal;
            this.btn_Correct.DarkTheme = false;
            this.btn_Correct.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btn_Correct.FlatAppearance.BorderSize = 0;
            this.btn_Correct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.btn_Correct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btn_Correct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Correct.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.btn_Correct.FontSize = 12F;
            this.btn_Correct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btn_Correct.Inverted = false;
            this.btn_Correct.Location = new System.Drawing.Point(10, 252);
            this.btn_Correct.ManualDarkTheme = false;
            this.btn_Correct.ManualInvert = false;
            this.btn_Correct.ManualThemeColor = false;
            this.btn_Correct.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Correct.Name = "btn_Correct";
            this.btn_Correct.ReverseDarkTheme = false;
            this.btn_Correct.ReverseInvertAction = false;
            this.btn_Correct.Size = new System.Drawing.Size(100, 38);
            this.btn_Correct.TabIndex = 5;
            this.btn_Correct.Text = "Correct";
            this.btn_Correct.ThemeColor = System.Drawing.Color.Teal;
            this.btn_Correct.UseVisualStyleBackColor = false;
            this.btn_Correct.Click += new System.EventHandler(this.Btn_Currect_Click);
            // 
            // btn_Setting
            // 
            this.btn_Setting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Setting.BackColor = System.Drawing.Color.Teal;
            this.btn_Setting.DarkTheme = false;
            this.btn_Setting.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btn_Setting.FlatAppearance.BorderSize = 0;
            this.btn_Setting.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.btn_Setting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btn_Setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Setting.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.btn_Setting.FontSize = 12F;
            this.btn_Setting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btn_Setting.Inverted = false;
            this.btn_Setting.Location = new System.Drawing.Point(290, 252);
            this.btn_Setting.ManualDarkTheme = false;
            this.btn_Setting.ManualInvert = false;
            this.btn_Setting.ManualThemeColor = false;
            this.btn_Setting.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.ReverseDarkTheme = false;
            this.btn_Setting.ReverseInvertAction = false;
            this.btn_Setting.Size = new System.Drawing.Size(100, 38);
            this.btn_Setting.TabIndex = 6;
            this.btn_Setting.Text = "Setting ...";
            this.btn_Setting.ThemeColor = System.Drawing.Color.Teal;
            this.btn_Setting.UseVisualStyleBackColor = false;
            this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.btn_Setting);
            this.Controls.Add(this.btn_Correct);
            this.Controls.Add(this.txt_Content);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.lbl_head);
            this.ForeColor = System.Drawing.Color.Teal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 500);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowInTaskbar = false;
            this.Text = "Main";
            this.ThemeColor = System.Drawing.Color.Teal;
            this.ResumeLayout(false);

        }

        #endregion

        private EGO.SolidUI.FirstLabel lbl_head;
        private EGO.SolidUI.FirstButton btn_Close;
        private EGO.SolidUI.FirstTextbox txt_Content;
        private EGO.SolidUI.FirstButton btn_Correct;
        private EGO.SolidUI.FirstButton btn_Setting;
        #endregion
    }
}

