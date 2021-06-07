using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EGO.SolidUI;
using EGOTeino.Framework.Core;
using Fractal;
using Fractal.Extensions;

namespace EGOTeino.Framework.UI
{
    public partial class AdvancedLanguageSettingPanel : IControlPanel
    {
        Language lng;
        SettingForm _setting;
        public AdvancedLanguageSettingPanel(SettingForm setting)
        {
            _setting = setting;
            lng = setting.CurrentLanguage;
            InitializeComponent();
            lng.AddedChild += Lng_AddedChild;
            lng.RemovedChild += Lng_RemovedChild;
            lst_keys.ControlAdded += Lst_keys_ControlChanged;
            lst_keys.ControlRemoved += Lst_keys_ControlChanged;
            this.Dock = DockStyle.Fill;
            ReverseInvertAction = true;
            lbl_lang_name.Text = $"Language: {lng.Name}";
            FillList();
        }

        private void FillList()
        {
            foreach (var item in lng.PullChildren<KeyItem>())
            {
                var x = API.GenerateInterfaceForAdditionPanel(item);
                x.MouseClick += X_Click;
                lst_keys.Controls.Add(x);
            }
        }
        private void Lst_keys_ControlChanged(object sender, ControlEventArgs e)
        {
            lst_keys.FixScroll();
        }

        private void X_Click(object sender, MouseEventArgs e)
        {
            KeyItem key = (KeyItem)((SelectableLabel)sender).Tag;
            txt_keycode.Textes = (key.KeyCode > 1000 ? key.KeyCode - 1000 : key.KeyCode).ToString();
            txt_keychar.Textes = key.KeyChar;
            swb_shift.Enable = key.KeyCode > 1000 ? true : false;
        }

        private void firstButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (!Tools.IsNum(txt_keycode.Textes) || string.IsNullOrWhiteSpace(txt_keychar.Textes)) return;
            KeyItem key = new KeyItem();
            key.KeyCode = swb_shift.Enable ? int.Parse(txt_keycode.Textes) + 1000 : int.Parse(txt_keycode.Textes);
            key.KeyChar = txt_keychar.Textes;
            lng.AddKey(key);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            _setting._dictionary.AddedChild -= Lng_AddedChild;
            _setting._dictionary.RemovedChild -= Lng_RemovedChild;
            lng.SortAll();
            base.OnHandleDestroyed(e);
        }

        private void Lng_RemovedChild(INode sender, INode parameter1)
        {
            foreach (var item in lst_keys.Controls)
            {
                if (item is SelectableLabel sl && sl.Tag == parameter1)
                {
                    sl.Dispose();
                }
            }
        }

        private void Lng_AddedChild(INode sender, INode parameter1)
        {
            var x = API.GenerateInterfaceForAdditionPanel((KeyItem)parameter1);
            x.MouseClick += X_Click;
            lst_keys.Controls.Add(x);
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            foreach (SelectableLabel item in lst_keys.Controls)
            {
                if (item.Selected)
                {
                    KeyItem key = (KeyItem)item.Tag;
                    key.KeyCode = swb_shift.Enable ? int.Parse(txt_keycode.Textes) + 1000 : int.Parse(txt_keycode.Textes);
                    key.KeyChar = txt_keychar.Textes;
                    item.Text = $"{key.KeyChar}: {key.KeyCode}";
                    API.FixScroll(lst_keys);
                    break;
                }
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            foreach (var item in lst_keys.Controls)
            {
                if (item is SelectableLabel sl && sl.Selected && sl.Tag != null)
                {
                    lng.RemoveChild((INode)sl.Tag);
                }
            }
        }
    }
}
