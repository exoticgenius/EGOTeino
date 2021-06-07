namespace EGOTeino.Framework.UI
{
    partial class AboutPanel
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
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutPanel));
            this.Confirm = new EGO.SolidUI.FirstButton();
            this.firstFlowPanel1 = new EGO.SolidUI.FirstFlowPanel();
            this.lbl_lang_count = new EGO.SolidUI.FirstLabel();
            this.firstLabel1 = new EGO.SolidUI.FirstLabel();
            this.firstFlowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Confirm
            // 
            this.Confirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Confirm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Confirm.FlatAppearance.BorderSize = 0;
            this.Confirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.Confirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.Confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Confirm.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.Confirm.FontSize = 12F;
            this.Confirm.Location = new System.Drawing.Point(485, 547);
            this.Confirm.ManualDarkTheme = false;
            this.Confirm.ManualInvert = false;
            this.Confirm.ManualThemeColor = false;
            this.Confirm.Margin = new System.Windows.Forms.Padding(5);
            this.Confirm.Name = "Confirm";
            this.Confirm.ReverseDarkTheme = false;
            this.Confirm.ReverseInvertAction = false;
            this.Confirm.Size = new System.Drawing.Size(100, 38);
            this.Confirm.TabIndex = 14;
            this.Confirm.Tag = "";
            this.Confirm.Text = "Confirm";
            this.Confirm.UseVisualStyleBackColor = false;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // firstFlowPanel1
            // 
            this.firstFlowPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.firstFlowPanel1.Controls.Add(this.lbl_lang_count);
            this.firstFlowPanel1.Controls.Add(this.firstLabel1);
            this.firstFlowPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.firstFlowPanel1.Location = new System.Drawing.Point(15, 25);
            this.firstFlowPanel1.ManualDarkTheme = false;
            this.firstFlowPanel1.ManualInvert = false;
            this.firstFlowPanel1.ManualThemeColor = false;
            this.firstFlowPanel1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.firstFlowPanel1.Name = "firstFlowPanel1";
            this.firstFlowPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.firstFlowPanel1.ReverseDarkTheme = false;
            this.firstFlowPanel1.ReverseInvertAction = false;
            this.firstFlowPanel1.Size = new System.Drawing.Size(570, 517);
            this.firstFlowPanel1.TabIndex = 13;
            // 
            // lbl_lang_count
            // 
            this.lbl_lang_count.AutoSize = true;
            this.lbl_lang_count.Font = new System.Drawing.Font("Segoe UI Light", 18F);
            this.lbl_lang_count.FontSize = 18F;
            this.lbl_lang_count.Location = new System.Drawing.Point(10, 10);
            this.lbl_lang_count.ManualDarkTheme = false;
            this.lbl_lang_count.ManualInvert = false;
            this.lbl_lang_count.ManualThemeColor = false;
            this.lbl_lang_count.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_lang_count.Name = "lbl_lang_count";
            this.lbl_lang_count.ReverseDarkTheme = false;
            this.lbl_lang_count.ReverseInvertAction = false;
            this.lbl_lang_count.Size = new System.Drawing.Size(219, 32);
            this.lbl_lang_count.TabIndex = 0;
            this.lbl_lang_count.Text = "Exotic Genius TEINO";
            // 
            // firstLabel1
            // 
            this.firstLabel1.AutoSize = true;
            this.firstLabel1.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.firstLabel1.FontSize = 14F;
            this.firstLabel1.Location = new System.Drawing.Point(15, 47);
            this.firstLabel1.ManualDarkTheme = false;
            this.firstLabel1.ManualInvert = false;
            this.firstLabel1.ManualThemeColor = false;
            this.firstLabel1.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.firstLabel1.Name = "firstLabel1";
            this.firstLabel1.ReverseDarkTheme = false;
            this.firstLabel1.ReverseInvertAction = false;
            this.firstLabel1.Size = new System.Drawing.Size(504, 200);
            this.firstLabel1.TabIndex = 0;
            this.firstLabel1.Text = resources.GetString("firstLabel1.Text");
            // 
            // AboutPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.firstFlowPanel1);
            this.Name = "AboutPanel";
            this.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.Size = new System.Drawing.Size(600, 600);
            this.Tag = "About";
            this.firstFlowPanel1.ResumeLayout(false);
            this.firstFlowPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private EGO.SolidUI.FirstButton Confirm;
        private EGO.SolidUI.FirstFlowPanel firstFlowPanel1;
        private EGO.SolidUI.FirstLabel lbl_lang_count;
        private EGO.SolidUI.FirstLabel firstLabel1;
    }
}
