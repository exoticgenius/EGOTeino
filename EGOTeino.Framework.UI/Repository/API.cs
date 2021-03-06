using EGOTeino.Framework.Core;
using Fractal;
using EGO.SolidUI;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace EGOTeino.Framework.UI
{
    public static class API
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetKeyboardLanguage()
        {
            string currentLang = "";
            Thread thread = new Thread(() => { currentLang = InputLanguage.CurrentInputLanguage.Culture.Name; });
            thread.Start();
            thread.Join();
            return currentLang;
        }

        /// <summary>
        /// check if the pressed key in keyboard will write a character
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsWritable(this Keys key)
        {
            bool ok = false;
            int code = (int)key;
            // top row 0-9
            if (code > 47 && code < 58) ok = true;
            // A-Z
            else if (code > 64 && code < 91) ok = true;
            // numpad 0-9
            else if (code > 95 && code < 105) ok = true;
            // math
            else if (code > 105 && code < 112) ok = true;
            // oem
            else if (code > 185 && code < 193) ok = true;
            else if (code > 218 && code < 224) ok = true;
            else if (code == 226) ok = true;

            return ok;
        }
        /// <summary>
        /// reduce the width of control to prevent showing horizontal scroll
        /// </summary>
        /// <param name="sender"></param>
        public static void FixScroll(this Control sender)
        {
            if (sender is ScrollableControl sc)
                if (sc.VerticalScroll.Visible)
                    foreach (Control item in sc.Controls)
                    {

                        int m = (sc.Width - 18 - item.Width) / 2;
                        if (m != item.Margin.Left)
                        {
                            item.Margin = new Padding(0);
                            item.Margin = new Padding(m, 0, m, 8);
                        }
                    }
                else
                    foreach (Control item in sc.Controls)
                    {

                        int m = (sc.Width - item.Width) / 2;
                        if (m != item.Margin.Left)
                        {
                            item.Margin = new Padding(0);
                            item.Margin = new Padding(m, 0, m, 8);
                        }
                    }
        }
        /// <summary>
        /// generates an item control for language addtion panel by key item
        /// </summary>
        /// <param name="parameter1"></param>
        /// <returns></returns>
        public static SelectableLabel GenerateInterfaceForAdditionPanel(this KeyItem parameter1)
        {
            SelectableLabel lbl = new SelectableLabel(parameter1);
            lbl.Text = $"{parameter1.KeyChar}: {parameter1.KeyCode}";
            lbl.Tag = parameter1;
            lbl.AutoSize = true;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            return lbl;
        }
        /// <summary>
        /// generates an item control for language addtion panel by language
        /// </summary>
        /// <param name="parameter1"></param>
        /// <returns></returns>
        public static SelectableLabel GenerateInterfaceForLanguagePanel(this Language parameter1)
        {
            SelectableLabel lbl = new SelectableLabel(parameter1);
            lbl.Text = $"{parameter1.Name}{Environment.NewLine}keys:{parameter1.ChildrenCount}";
            lbl.Tag = parameter1;
            lbl.AutoSize = true;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            return lbl;
        }
        /// <summary>
        /// generate button for language to use in pop up
        /// </summary>
        /// <param name="parameter1"></param>
        /// <returns></returns>
        public static ThirdButton GenerateInterfaceForPopUp(this Language parameter1)
        {
            ThirdButton lbl = new ThirdButton();
            lbl.Text = $"{parameter1.Name}";
            lbl.Tag = parameter1;
            lbl.Size = new Size(120, 40);
            lbl.AutoSize = true;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.ReverseInvertAction = true;
            return lbl;
        }
        /// <summary>
        /// generate button for custom use like tray icon menu
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ThirdButton GenerateInterfaceForPopUp(string text)
        {
            ThirdButton lbl = new ThirdButton();
            lbl.Text = text;
            lbl.Size = new Size(120, 40);
            lbl.AutoSize = true;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.ReverseInvertAction = true;
            return lbl;
        }
    }
}
