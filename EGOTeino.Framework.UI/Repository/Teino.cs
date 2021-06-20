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
        /// <summary>
        /// root database location
        /// </summary>
        private string DBName;
        /// <summary>
        /// main app database
        /// </summary>
        public DataSet DataSet { get; set; }
        /// <summary>
        /// main ap setting
        /// </summary>
        public SettingProvider SettingProvider { get; set; }
        /// <summary>
        /// main hook manager
        /// </summary>
        public HookManager HookManager { get; set; }
        /// <summary>
        /// main action core
        /// </summary>
        public MainCore MainCore { get; set; }
        /// <summary>
        /// very first form of app
        /// </summary>
        public MainForm MainForm { get; set; }
        /// <summary>
        /// setting form instance
        /// </summary>
        public SettingForm Setting { get; set; }
        /// <summary>
        /// tray icon
        /// </summary>
        public TeinoNotify Notify { get; set; }

        /// <summary>
        /// read database file and initialize it
        /// </summary>
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
        /// <summary>
        /// save all datatabse changes and write to database file
        /// </summary>
        public void FinalDatabase()
        {
            using (StreamWriter sw = new StreamWriter(File.Create(DBName)))
            {
                DataSet.Serialize(sw);
            }

            SettingProvider = null;
            GC.Collect();
        }
        /// <summary>
        /// create all needed form and classes for launching teino
        /// </summary>
        public void InitialUI()
        {
            InitialTheme();
            HookManager = new HookManager();
            Setting = new SettingForm(DataSet.Dictionary, SettingProvider, HookManager);
            MainCore = new MainCore(DataSet.Dictionary, HookManager, SettingProvider);
            MainForm = new MainForm(Setting, MainCore);
            Notify = new TeinoNotify(MainForm, Setting, SettingProvider);
        }
        /// <summary>
        /// dispose forms and set classes to null to free memory
        /// </summary>
        public void FinalUI()
        {
            HookManager.TryUnsubscribeFromGlobalMouseEvents();
            HookManager.TryUnsubscribeFromGlobalKeyboardEvents();
            Notify.NotifyIcon.Dispose();
            MainForm.Dispose();
            MainCore = null;
            Setting.Dispose();
            HookManager = null;
            GC.Collect();
        }
        /// <summary>
        /// bind events to objects to control app flow
        /// </summary>
        public void InitialEvents()
        {
            DataSet.AttributeChangedStatic += DataSet_AttributeChangedStatic;
            SolidSettings.SetStyleCalled += SolidSettings_SetStyleCalled;
            HookManager.KeyDown += MainCore.GlobalEventProvider_KeyDown;
            HookManager.KeyUp += MainCore.GlobalEventProvider_KeyUp;
        }
        /// <summary>
        /// unbind events to objects to prevent throw and GC deadlock
        /// </summary>
        public void FinalEvents()
        {
            DataSet.AttributeChangedStatic -= DataSet_AttributeChangedStatic;
            SolidSettings.SetStyleCalled -= SolidSettings_SetStyleCalled;
            HookManager.KeyDown -= MainCore.GlobalEventProvider_KeyDown;
            HookManager.KeyUp -= MainCore.GlobalEventProvider_KeyUp;
        }
        /// <summary>
        /// saves the theme when user changes the values
        /// </summary>
        public void SolidSettings_SetStyleCalled()
        {
            SettingProvider.ThemeColor = SolidSettings.ThemeColor;
            SettingProvider.ReverseDarkColor = SolidSettings.ReverseColorDark;
            SettingProvider.ReverseLightColor = SolidSettings.ReverseColorLight;
            SettingProvider.DarkTheme = SolidSettings.DarkTheme;
            SettingProvider.InvertedTheme = SolidSettings.Inverted;
        }
        /// <summary>
        /// get theme setting from provider and apply to app
        /// </summary>
        public void InitialTheme()
        {
            SolidSettings.ThemeColor_NoTrigger = SettingProvider.ThemeColor;
            SolidSettings.ReverseColorDark_NoTrigger = SettingProvider.ReverseDarkColor;
            SolidSettings.ReverseColorLight_NoTrigger = SettingProvider.ReverseLightColor;
            SolidSettings.DarkTheme_NoTrigger = SettingProvider.DarkTheme;
            SolidSettings.Inverted_NoTrigger = SettingProvider.InvertedTheme;
        }
        /// <summary>
        /// top most events in app to control top most state and capturing state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
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
