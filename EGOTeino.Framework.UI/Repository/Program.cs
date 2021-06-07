using EGOTeino.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EGOTeino.Framework.UI
{
    static class Program
    {
        static Teino T;
        static int FailureLeft = 10;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (FailureLeft > 0)
            {
                bool thrown = false;
                try
                {
                    T = new Teino();

                    T.InitialDatabase();
                    T.InitialUI();
                    T.InitialEvents();
                    Application.Run(T.MainForm);
                }
                catch
                {
                    FailureLeft--;
                    thrown = true;
                }

                T.FinalEvents();
                T.FinalUI();
                T.FinalDatabase();

                T = null;
                if (!thrown) break;
            }
        }
    }
}
