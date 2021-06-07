using EGOTeino.Framework.Core;
using Fractal;
using Hook;
using EGO.SolidUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace EGOTeino.Framework.UI
{
    public partial class SelectorPopup : IForm
    {
        HookManager GlobalEventProvider;
        public SelectorPopup()
        {
            InitializeComponent();
            GlobalEventProvider = new HookManager();
            GlobalEventProvider.MouseDown += GlobalEventProvider_MouseDown;
            HandleDestroyed += SelectorPopup_HandleDestroyed;
            StartPosition = FormStartPosition.Manual;
            this.Icon = Properties.Resources.MainIcon;
            TopMost = true;
            ReverseInvertAction = true;
            Sync();
        }

        private void SelectorPopup_HandleDestroyed(object sender, EventArgs e)
        {
            GlobalEventProvider.MouseDown -= GlobalEventProvider_MouseDown;
            GlobalEventProvider.TryUnsubscribeFromGlobalMouseEvents();
        }

        private void GlobalEventProvider_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Bounds.Contains(Cursor.Position))
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        public DialogResult ShowDialog(params Language[] languages)
        {
            foreach (var item in languages)
            {
                AddItem(API.GenerateInterfaceForPopUp(item));
            }
            return ShowDialog();
        }
        public DialogResult ShowDialog(params Control[] Controls)
        {
            foreach (var item in Controls)
            {
                AddItem(item);
            }
            return ShowDialog();
        }

        public void AddItem(Control item)
        {
            item.Click += Button_Click;
            lst_langs.Controls.Add(item);
        }
        public new DialogResult ShowDialog()
        {
            API.FixScroll(lst_langs);

            this.Location = GetPosition(Cursor.Position);

            Sync();
            return base.ShowDialog();
        }

        private Point GetPosition(Point p)
        {
            var screen = Screen.GetBounds(p);
            Point widthOverFlow =new Point(p.X+Width,p.Y);
            Point HeightOverFlow = new Point(p.X, p.Y + Height);

            if (!screen.Contains(widthOverFlow)) p.X = screen.Width - Width + screen.X;
            if (!screen.Contains(HeightOverFlow)) p.Y = screen.Height - Height + screen.Y;

            return p;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Tag = ((ThirdButton)sender).Tag;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
