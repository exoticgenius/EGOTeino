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
                }
            }
        }

        public bool WrongLanguage()
        {
            return _core.Dictionary.FirstLanguage == null ||
                   _core.Dictionary.SecondLanguage == null ||
                   _core.Dictionary.FirstLanguage == _core.Dictionary.SecondLanguage;
        }
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
        public string Action(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return "ERROR";

            Language dest = null;
            Language src = null;

            if (_settingProvider.ConfirmOperation)
            {
                using (SelectorPopup sp = new SelectorPopup())
                {
                    if (sp.ShowDialog(_core.Dictionary.PullChildren<Language>().ToArray()) == DialogResult.OK)
                    {
                        src = sp.Tag as Language;
                    }
                    else
                    {
                        dest = _core.Dictionary.Getlanguage(API.GetKeyboardLanguage());
                    }
                }
            }
            else
            {
                dest = _core.Dictionary.Getlanguage(API.GetKeyboardLanguage());
            }

            if (src == null && dest == null)
            {
                dest = _core.Dictionary.FirstLanguage;
                src = _core.Dictionary.SecondLanguage;
            }
            else if(src == null && dest != null)
            {
                if(dest == _core.Dictionary.FirstLanguage)
                {
                    src = _core.Dictionary.SecondLanguage;
                }
                else if(dest == _core.Dictionary.SecondLanguage)
                {
                    src = _core.Dictionary.FirstLanguage;
                }
                else
                {
                    src = _core.Dictionary.FirstLanguage;
                }
            }
            else if (src != null && dest == null)
            {
                if (src == _core.Dictionary.FirstLanguage)
                {
                    dest = _core.Dictionary.SecondLanguage;
                }
                else if (src == _core.Dictionary.SecondLanguage)
                {
                    dest = _core.Dictionary.FirstLanguage;
                }
                else
                {
                    dest = _core.Dictionary.FirstLanguage;
                }
            }

            data = _core.InsideOut(data, dest, src);

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






