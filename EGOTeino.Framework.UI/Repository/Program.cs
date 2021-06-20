using EGOTeino.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Windows.Forms;

namespace EGOTeino.Framework.UI
{
    static class Program
    {
        /// <summary>
        /// the current running instance of app
        /// </summary>
        static Teino T;
        /// <summary>
        /// fault tolerance counter
        /// </summary>
        static int FailureLeft = 10;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            GC.Collect();

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

                // if code gets here and thrown stayed false means user decided to close app
                if (!thrown) break;
            }
        }
    }
}
