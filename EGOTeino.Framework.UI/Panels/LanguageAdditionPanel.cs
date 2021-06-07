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
using Hook;
using Fractal;
using Fractal.Extensions;

namespace EGOTeino.Framework.UI
{
    public partial class LanguageAdditionPanel : IControlPanel
    {
        HookManager eventProvider =new HookManager();
        private Language lng;
        bool shiftDown = false;
        SettingForm _setting;
        public LanguageAdditionPanel(SettingForm setting)
        {
            _setting = setting;
            lng = _setting.CurrentLanguage;
            InitializeComponent();
            this.txt_current_keyChar.ActualTextBox.Margin = new System.Windows.Forms.Padding(50);
            this.txt_current_keyChar.ActualTextBox.Font = new Font("Microsoft Sans Serif", 90F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.Dock = DockStyle.Fill;
            ReverseInvertAction = true;
            this.txt_current_keyChar.Focus();

            eventProvider.KeyDown += EventProvider_KeyDown;
            eventProvider.KeyUp += EventProvider_KeyUp;
            lng.AddedChild += Lng_AddedChild;
            lng.RemovedChild += Lng_RemovedChild;
            lst_keys.ControlAdded += Lst_keys_ControlChanged;
            lst_keys.ControlRemoved += Lst_keys_ControlChanged;
            VisibleChanged += LanguageAdditionPanel_VisibleChanged;

            FillList();
        }

        private void FillList()
        {
            foreach (var item in lng.PullChildren())
            {
                lst_keys.Controls.Add(API.GenerateInterfaceForAdditionPanel((KeyItem)item));
            }
        }

        private void LanguageAdditionPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible) _setting._hookManager.Suspended = true;
            else _setting._hookManager.Suspended = false;
        }

        private void Lst_keys_ControlChanged(object sender, ControlEventArgs e)
        {
            lst_keys.FixScroll();
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
           lst_keys.Controls.Add( API.GenerateInterfaceForAdditionPanel((KeyItem)parameter1));
        }

        private void EventProvider_KeyDown(object sender, KeyEventArgs e)
        {
            lock (lng)
            {
                if (e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.LShiftKey)
                {
                    shiftDown = true;
                }
                if (e.KeyCode.IsWritable()) txt_current_keyChar.Textes = "";
            }
        }

        private void EventProvider_KeyUp(object sender, KeyEventArgs e)
        {
            lock (lng)
            {
                if (e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.LShiftKey)
                {
                    shiftDown = false;
                }
                if (e.KeyCode.IsWritable())
                {
                    if (txt_current_keyChar.Focused && !string.IsNullOrWhiteSpace(txt_current_keyChar.Textes) && API.GetKeyboardLanguage() == lng.Name)
                    {
                        int keyCode = (int)e.KeyCode;
                        if (shiftDown) keyCode += 1000;
                        KeyItem keyItem = new KeyItem(keyCode, txt_current_keyChar.Textes);

                        if (!lng.AddKey(keyItem))
                        {
                            lbl_keyCode.Text = $"Key already Exists, Code: {keyCode}";
                            lbl_keyChar.Text = $"Key char: {txt_current_keyChar.Textes}";
                            lbl_shiftUsed.Text = $"Try enter another key";
                        }
                        else
                        {
                            lbl_keyCode.Text = $"Key code: {keyCode}";
                            lbl_keyChar.Text = $"Key char: {txt_current_keyChar.Textes}";
                            lbl_shiftUsed.Text = $"Shift used: {(shiftDown ? "yes" : "no")}";
                        }
                    }
                    else if(API.GetKeyboardLanguage() != lng.Name)
                    {
                        lbl_keyCode.Text = $"Input method violation";
                        lbl_keyChar.Text = "not same keyboard language";
                        lbl_shiftUsed.Text = "Change your keyboard language";
                    }
                    else if(!txt_current_keyChar.Focused)
                    {
                        txt_current_keyChar.Focus();
                        lbl_keyCode.Text = $"enter last key you tried to";
                        lbl_keyChar.Text = "";
                        lbl_shiftUsed.Text = "";
                    }
                    else
                    {
                        txt_current_keyChar.Focus();
                        lbl_keyCode.Text = $"Select the form to capture keyboard";
                        lbl_keyChar.Text = "";
                        lbl_shiftUsed.Text = "";
                    }
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            eventProvider.KeyDown -= EventProvider_KeyDown;
            eventProvider.KeyUp -= EventProvider_KeyUp;
            lng.AddedChild -= Lng_AddedChild;
            lng.RemovedChild -= Lng_RemovedChild;
            lng.SortAll();
            _setting._hookManager.Suspended = false;
            base.OnHandleDestroyed(e);
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btn_RemoveSelectedKey_Click(object sender, EventArgs e)
        {
            foreach (var item in lst_keys.Controls)
            {
                if (item is SelectableLabel sl && sl.Selected && sl.Tag != null)
                {
                    lng.RemoveChild((INode)sl.Tag);
                }
            }
        }

        private void btn_advanced_Click(object sender, EventArgs e)
        {
            _setting.PanelStack.Push(this.GetType()); 
            _setting.PanelStack.Push(typeof(AdvancedLanguageSettingPanel));
            this.Dispose();
        }
    }
}
