using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EGO.SolidUI;

namespace EGOTeino.Framework.UI
{
    public partial class GuidePanel : IControlPanel
    {
        SettingForm _setting;
        public GuidePanel(SettingForm setting)
        {
            _setting = setting;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            ReverseInvertAction = true;
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
