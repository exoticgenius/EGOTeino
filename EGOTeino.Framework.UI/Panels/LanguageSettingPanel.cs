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
using Fractal;

namespace EGOTeino.Framework.UI
{
    public partial class LanguageSettingPanel : IControlPanel 
    {
        SettingForm _setting;
        public LanguageSettingPanel(SettingForm setting)
        {
            _setting = setting;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            ReverseInvertAction = true;
            _setting._dictionary.AddedChild += Lng_AddedChild;
            _setting._dictionary.RemovedChild += Lng_RemovedChild;
            lst_langs.ControlAdded += Lst_langs_ControlChanged;
            lst_langs.ControlRemoved += Lst_langs_ControlChanged;

            FillList();
            validateLabels();
        }

        private void FillList()
        {
            foreach (var item in _setting._dictionary.Getlanguages())
            {
                lst_langs.Controls.Add(API.GenerateInterfaceForLanguagePanel(item));
            }
        }

        private void Lst_langs_ControlChanged(object sender, ControlEventArgs e)
        {
            lst_langs.FixScroll();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Extra_Click(object sender, EventArgs e)
        {
            foreach (SelectableLabel item in lst_langs.Controls)
            {
                if (item.Selected)
                {
                    _setting.CurrentLanguage =(Language) item.Tag;

                    _setting.PanelStack.Push(this.GetType());
                    _setting.PanelStack.Push(typeof(AdvancedLanguageSettingPanel));
                    this.Dispose();
                    return;
                }
            }
            
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Language language = new Language() { Name = API.GetKeyboardLanguage() };
            _setting._dictionary.AddLanguage(language);
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            foreach (SelectableLabel item in lst_langs.Controls)
            {
                if (item.Selected)
                {
                    _setting.CurrentLanguage = (Language)item.Tag;

                   _setting.PanelStack.Push(this.GetType());
                    _setting.PanelStack.Push(typeof(LanguageAdditionPanel));
                    this.Dispose();
                    return;
                }
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            foreach (SelectableLabel item in lst_langs.Controls)
            {
                if (item.Selected)
                {
                    _setting._dictionary.RemoveChild(x => x.Name == ((INode)item.Tag).Name);

                    return;
                }
            }
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            _setting._dictionary.AddedChild -= Lng_AddedChild;
            _setting._dictionary.RemovedChild -= Lng_RemovedChild;
            base.OnHandleDestroyed(e);
        }

        private void Lng_RemovedChild(INode sender, INode parameter1)
        {
            foreach (SelectableLabel sl in lst_langs.Controls)
            {
                if ( sl.Tag == parameter1)
                {
                    sl.Dispose();
                    validateLabels();
                }
                if(_setting.CurrentLanguage ==(Language)sl.Tag)
                {
                    _setting.CurrentLanguage = null;
                }
            }
        }

        private void Lng_AddedChild(INode sender, INode parameter1)
        {
            var res = API.GenerateInterfaceForLanguagePanel((Language)parameter1);
            lst_langs.Controls.Add(res);
            res.Selected = true;
            lst_langs.PerformStyle();
            validateLabels();
        }
        public void validateLabels()
        {
            lbl_lang_count.Text = $"Languages {_setting._dictionary.ChildrenCount}";
            lbl_key_count.Text = $"All keys {_setting._dictionary.AllChildrenCount - _setting._dictionary.ChildrenCount}";
        }
    }
}
