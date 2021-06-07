using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EGOTeino.Framework.Core;
using Fractal.Extensions;
using Hook;
using EGO.SolidUI;

namespace EGOTeino.Framework.UI
{
    public class Teino
    {
        private string DBName;
        public DataSet DataSet { get; set; }
        public SettingProvider SettingProvider { get; set; }
        public HookManager HookManager { get; set; }
        public MainCore MainCore { get; set; }
        public MainForm MainForm { get; set; }
        public SettingForm Setting { get; set; }
        public TeinoNotify Notify { get; set; }

        public void InitialDatabase()
        {
            DBName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "EGO", "Teino", "Database");
            DataSet = new DataSet();

            if (File.Exists(DBName))
            {
                using (StreamReader sr = new StreamReader(File.OpenRead(DBName)))
                {
                    string data = sr.ReadToEnd();
                    DataSet.Emitter(ref data);
                }
            }

            if (DataSet.Dictionary == null)
            {
                DataSet.AddChild(new Dictionary());
            }
            if (DataSet.GearBox == null)
            {
                DataSet.AddChild(new GearBox());
            }

            SettingProvider = new SettingProvider(DataSet.GearBox);
        }
        public void FinalDatabase()
        {
            using (StreamWriter sw = new StreamWriter(File.Create(DBName)))
            {
                DataSet.Serialize(sw);
            }

            SettingProvider = null;
        }
        public void InitialUI()
        {
            InitialTheme();
            HookManager = new HookManager();
            Setting = new SettingForm(DataSet.Dictionary, SettingProvider, HookManager);
            MainCore = new MainCore(DataSet.Dictionary, HookManager, SettingProvider);
            MainForm = new MainForm(Setting, MainCore);
            Notify = new TeinoNotify(MainForm, Setting, SettingProvider);
        }
        public void FinalUI()
        {
            HookManager.TryUnsubscribeFromGlobalMouseEvents();
            HookManager.TryUnsubscribeFromGlobalKeyboardEvents();
            Notify.NotifyIcon.Dispose();
            MainForm.Dispose();
            MainCore = null;
            Setting.Dispose();
            HookManager = null;
        }
        public void InitialEvents()
        {
            DataSet.AttributeChangedStatic += DataSet_AttributeChangedStatic;
            SolidSettings.SetStyleCalled += SolidSettings_SetStyleCalled;
            HookManager.KeyDown += MainCore.GlobalEventProvider_KeyDown;
            HookManager.KeyUp += MainCore.GlobalEventProvider_KeyUp;
        }
        public void FinalEvents()
        {
            DataSet.AttributeChangedStatic -= DataSet_AttributeChangedStatic;
            SolidSettings.SetStyleCalled -= SolidSettings_SetStyleCalled;
            HookManager.KeyDown -= MainCore.GlobalEventProvider_KeyDown;
            HookManager.KeyUp -= MainCore.GlobalEventProvider_KeyUp;
        }

        public void SolidSettings_SetStyleCalled()
        {
            SettingProvider.ThemeColor = SolidSettings.ThemeColor;
            SettingProvider.ReverseDarkColor = SolidSettings.ReverseColorDark;
            SettingProvider.ReverseLightColor = SolidSettings.ReverseColorLight;
            SettingProvider.DarkTheme = SolidSettings.DarkTheme;
            SettingProvider.InvertedTheme = SolidSettings.Inverted;
        }
        public void InitialTheme()
        {
            SolidSettings.ThemeColor_NoTrigger = SettingProvider.ThemeColor;
            SolidSettings.ReverseColorDark_NoTrigger = SettingProvider.ReverseDarkColor;
            SolidSettings.ReverseColorLight_NoTrigger = SettingProvider.ReverseLightColor;
            SolidSettings.DarkTheme_NoTrigger = SettingProvider.DarkTheme;
            SolidSettings.Inverted_NoTrigger = SettingProvider.InvertedTheme;
        }
        public void DataSet_AttributeChangedStatic(Fractal.INode sender, string parameter1, int parameter2)
        {
            if (sender is Gear g)
            {
                switch (g.Name)
                {
                    case SettingProvider.TopMost_AccessName:
                        MainForm.TopMost = SettingProvider.TopMost;
                        break;
                    case SettingProvider.Capture_AccessName:
                        HookManager.Suspended = !SettingProvider.Capture;
                        break;
                }
            }

        }

    }
}
