using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace EGOTeino.Framework.UI
{
    public class TeinoNotify
    {
        public NotifyIcon NotifyIcon { get; set; }
        private MainForm _mainForm;
        private SettingForm _settingForm;
        private SettingProvider _settingProvider;
        public TeinoNotify(MainForm main, SettingForm setting, SettingProvider settingProvider)
        {
            _mainForm = main;
            _settingForm = setting;
            _settingProvider = settingProvider;
            NotifyIcon = new NotifyIcon();
            NotifyIcon.Visible = true;
            NotifyIcon.Icon = Properties.Resources.MainIcon;
            NotifyIcon.MouseDown += Notify_MouseDown;
        }


        private void Notify_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowMain();
            }
            else if (e.Button == MouseButtons.Right)
            {

                using (SelectorPopup selectorPopup = new SelectorPopup())
                {
                    var show = API.GenerateInterfaceForPopUp("Show");
                    var setting = API.GenerateInterfaceForPopUp("Setting");
                    var exit = API.GenerateInterfaceForPopUp("Exit");

                    show.Click += Show_Click;
                    setting.Click += Setting_Click;
                    exit.Click += Exit_Click;
                    selectorPopup.ShowDialog(show, setting, exit);
                }
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Setting_Click(object sender, EventArgs e)
        {
            ShowSetting();
        }

        private void ShowMain()
        {
            _mainForm.Show();
            _mainForm.TopMost = true;
            if (!_settingProvider.TopMost)
                _mainForm.TopMost = false;
        }
        private void ShowSetting()
        {
            _settingForm.Show();
            _settingForm.TopMost = true;
            _settingForm.TopMost = false;
        }

        private void Show_Click(object sender, EventArgs e)
        {
            ShowMain();
        }
    }
}
