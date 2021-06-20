using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EGO.SolidUI;
using EGOTeino.Framework.Core;

namespace EGOTeino.Framework.UI
{
    public partial class GeneralSettingPanel : IControlPanel
    {
        SettingForm _setting;
        public GeneralSettingPanel(SettingForm setting)
        {
            _setting = setting;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            ReverseInvertAction = true;
            swb_topMost.Enable = _setting._settingProvider.TopMost;
            swb_capture.Enable = _setting._settingProvider.Capture;
            swb_tray.Enable = _setting._settingProvider.ExitToTray;
            swb_operationConfirm.Enable = _setting._settingProvider.ConfirmOperation;
            ValidateLabels();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            _setting.Hide();
        }
        /// <summary>
        /// navigate to language setting panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Extra_Click(object sender, EventArgs e)
        {
            _setting.PanelStack.Push(this.GetType());
            _setting.PanelStack.Push(typeof(LanguageSettingPanel));
            this.Dispose();
            GC.Collect();
        }
        /// <summary>
        /// opens theme setting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_theme_Click(object sender, EventArgs e)
        {
            SolidSettings.ThemeSelector.Show();
        }

        private void swb_topMost_Click(object sender, EventArgs e)
        {
            _setting._settingProvider.TopMost = swb_topMost.Enable;
        }

        private void swb_capture_Click(object sender, EventArgs e)
        {
            _setting._settingProvider.Capture = swb_capture.Enable;
        }

        private void swb_tray_Click(object sender, EventArgs e)
        {
            _setting._settingProvider.ExitToTray = swb_tray.Enable;
        }

        private void swb_operationConfirm_Click(object sender, EventArgs e)
        {
            _setting._settingProvider.ConfirmOperation = swb_operationConfirm.Enable;
        }
        /// <summary>
        /// create popup of languages to show to user for primary language
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_first_Click(object sender, EventArgs e)
        {
            List<Control> lst = new List<Control>();
            foreach (var item in _setting._dictionary.Getlanguages())
            {
                lst.Add(item.GenerateInterfaceForPopUp());
            }
            using (SelectorPopup sp = new SelectorPopup())
            {
                if (sp.ShowDialog(lst.ToArray()) == DialogResult.OK)
                {
                    _setting._dictionary.FirstLanguage = (Language)sp.Tag;
                }
            }
            ValidateLabels();
        }
        /// <summary>
        /// set language labels to current state
        /// </summary>
        private void ValidateLabels()
        {
            lbl_first.Text = $"First language: {_setting._dictionary.FirstLanguage?.Name ?? string.Empty}";
            lbl_second.Text = $"Second language: {_setting._dictionary.SecondLanguage?.Name ?? string.Empty}";
        }
        /// <summary>
        /// create popup of languages to show to user for secondary language
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_second_Click(object sender, EventArgs e)
        {
            List<Control> lst = new List<Control>();
            foreach (var item in _setting._dictionary.Getlanguages())
            {
                lst.Add(item.GenerateInterfaceForPopUp());
            }
            using (SelectorPopup sp = new SelectorPopup())
            {
                if (sp.ShowDialog(lst.ToArray()) == DialogResult.OK)
                {
                    _setting._dictionary.SecondLanguage = (Language)sp.Tag;
                }
            }
            ValidateLabels();
        }
        /// <summary>
        /// navigate to guide panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_advanced_Click(object sender, EventArgs e)
        {
            _setting.PanelStack.Push(this.GetType());
            _setting.PanelStack.Push(typeof(GuidePanel));
            this.Dispose();
            GC.Collect();
        }
        /// <summary>
        /// navigate to about panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Extra3_Click(object sender, EventArgs e)
        {
            _setting.PanelStack.Push(this.GetType());
            _setting.PanelStack.Push(typeof(AboutPanel));
            this.Dispose();
            GC.Collect();
        }
    }
}
