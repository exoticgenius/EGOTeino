namespace EGOTeino.Framework.UI
{
    partial class SelectorPopup
    {
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
            GlobalEventProvider.MouseDown -= GlobalEventProvider_MouseDown;
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lst_langs = new EGO.SolidUI.FirstFlowPanel();
            this.SuspendLayout();
            // 
            // lst_langs
            // 
            this.lst_langs.AutoScroll = true;
            this.lst_langs.AutoSize = true;
            this.lst_langs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lst_langs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lst_langs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_langs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.lst_langs.Location = new System.Drawing.Point(0, 0);
            this.lst_langs.ManualDarkTheme = false;
            this.lst_langs.ManualInvert = false;
            this.lst_langs.ManualThemeColor = false;
            this.lst_langs.Margin = new System.Windows.Forms.Padding(0);
            this.lst_langs.Name = "lst_langs";
            this.lst_langs.ReverseDarkTheme = false;
            this.lst_langs.ReverseInvertAction = false;
            this.lst_langs.Size = new System.Drawing.Size(500, 500);
            this.lst_langs.TabIndex = 0;
            this.lst_langs.WrapContents = false;
            // 
            // SelectorPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.lst_langs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(500, 500);
            this.Name = "SelectorPopup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SelectorPopup";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EGO.SolidUI.FirstFlowPanel lst_langs;
    }
}