using EGOTeino.Framework.Core;

using Hook;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EGOTeino.Framework.UI
{
    public class MainCore
    {
        public OperationCore _core;
        private HookManager _hookmanager;
        private SettingProvider _settingProvider;
        bool LCtrl = false;
        bool RCtrl = false;
        public MainCore(Dictionary dictionary, HookManager hookManager, SettingProvider settingProvider)
        {
            _core = new OperationCore(dictionary);
            _hookmanager = hookManager;
            _settingProvider = settingProvider;
        }
        /// <summary>
        /// detemine usage of both control keys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GlobalEventProvider_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LControlKey)
            {
                LCtrl = true;
            }
            else if (e.KeyCode == Keys.RControlKey)
            {
                RCtrl = true;
            }
        }
        /// <summary>
        /// take action when both control key are pressed down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GlobalEventProvider_KeyUp(object sender, KeyEventArgs e)
        {
            bool tr = RCtrl;
            bool tl = LCtrl;

            if (e.KeyCode == Keys.LControlKey)
            {
                LCtrl = false;
            }
            else if (e.KeyCode == Keys.RControlKey)
            {
                RCtrl = false;
            }

            if (tr && tl)
            {
                lock (_core)
                {
                    if (!WrongLanguage())
                    {
                        Perform();
                    }
                    else
                    {
                        Task.Run(() => MessageBox.Show(
                    "one or both of main languages may be not set or duplicated",
                    "fix language settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error));

                    }
                }
            }
        }
        /// <summary>
        /// check for first and second language in setting to make sure they are configured correctly
        /// </summary>
        /// <returns></returns>
        public bool WrongLanguage()
        {
            return _core.Dictionary.FirstLanguage == null ||
                   _core.Dictionary.SecondLanguage == null ||
                   _core.Dictionary.FirstLanguage == _core.Dictionary.SecondLanguage;
        }
        /// <summary>
        /// suspend hook manager until the action gets done and get content from clipboard
        /// </summary>
        private void Perform()
        {
            lock (_core)
            {
                _hookmanager.Suspended = true;
                string data = GetClipboardContent();
                data = Action(data);
                SetClipboardContent(data);
                _hookmanager.Suspended = false;
            }
        }
        /// <summary>
        /// check system language and decide to choose which languages to perform action
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Action(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return "ERROR";

            Language ConvertFrom = null;
            Language ConvertTo = null;

            if (_settingProvider.ConfirmOperation)
            {
                using (SelectorPopup sp = new SelectorPopup())
                {
                    if (sp.ShowDialog(_core.Dictionary.PullChildren<Language>().ToArray()) == DialogResult.OK)
                    {
                        ConvertTo = sp.Tag as Language;
                    }
                    else
                    {
                        ConvertFrom = _core.Dictionary.Getlanguage(API.GetKeyboardLanguage());
                    }
                }
            }
            else
            {
                ConvertFrom = _core.Dictionary.Getlanguage(API.GetKeyboardLanguage());
            }

            if (ConvertTo == null && ConvertFrom == null)
            {
                ConvertFrom = _core.Dictionary.FirstLanguage;
                ConvertTo = _core.Dictionary.SecondLanguage;
            }
            else if (ConvertTo == null)
            {
                if (ConvertFrom == _core.Dictionary.FirstLanguage)
                {
                    ConvertTo = _core.Dictionary.SecondLanguage;
                }
                else if (ConvertFrom == _core.Dictionary.SecondLanguage)
                {
                    ConvertTo = _core.Dictionary.FirstLanguage;
                }
                else
                {
                    ConvertTo = _core.Dictionary.FirstLanguage;
                }
            }
            else if (ConvertFrom == null)
            {
                if (ConvertTo == _core.Dictionary.FirstLanguage)
                {
                    ConvertFrom = _core.Dictionary.SecondLanguage;
                }
                else if (ConvertTo == _core.Dictionary.SecondLanguage)
                {
                    ConvertFrom = _core.Dictionary.FirstLanguage;
                }
                else
                {
                    ConvertFrom = _core.Dictionary.FirstLanguage;
                }
            }

            data = _core.InsideOut(data, ConvertFrom, ConvertTo);

            return data;
        }

        private void SetClipboardContent(string data)
        {
            try
            {
                Clipboard.SetText(data);
            }
            catch { }
        }
        private string GetClipboardContent()
        {
            string data = null;
            try
            {
                if (Clipboard.ContainsText()) data = Clipboard.GetText();
            }
            catch { }

            return data;
        }
    }
}






