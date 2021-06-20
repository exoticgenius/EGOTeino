using EGOTeino.Framework.Core;
using Fractal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EGOTeino.Framework.UI
{
    /// <summary>
    /// setting store
    /// </summary>
    public class SettingProvider
    {
        public const string TopMost_AccessName = "TopMost";
        public const string Capture_AccessName = "Capture";
        public const string ExitToTray_AccessName = "ExitToTray";
        public const string ConfirmOperation_AccessName = "ConfirmOperation";
        public const string DarkTheme_AccessName = "DarkTheme";
        public const string InvertedTheme_AccessName = "InvertedTheme_AccessName";
        public const string ThemeColor_AccessName = "ThemeColor";
        public const string ReverseDarkColor_AccessName = "ReverseDarkColor";
        public const string ReverseLightColor_AccessName = "ReverseLightColor";
        public GearBox GearBox;
        public SettingProvider(GearBox gearBox)
        {
            GearBox = gearBox;
        }
        public bool TopMost
        {
            get
            {
                bool x = false;
                Gear g = GearBox.GetGear(TopMost_AccessName);
                if (g != null)
                {
                    if (!bool.TryParse(g.Value, out x))
                    {
                        g.Value = bool.FalseString;
                    }
                }
                else
                {
                    GearBox.AddGear(TopMost_AccessName, bool.FalseString);
                }
                return x;
            }
            set
            {
                Gear g = GearBox.GetGear(TopMost_AccessName);
                if (g != null)
                {
                    g.Value = value.ToString();
                }
                else
                {
                    GearBox.AddGear(TopMost_AccessName, value.ToString());
                }
            }
        }
        public bool Capture
        {
            get
            {
                bool x = true;
                Gear g = GearBox.GetGear(Capture_AccessName);
                if (g != null)
                {
                    if (!bool.TryParse(g.Value, out x))
                    {
                        g.Value = bool.TrueString;
                    }
                }
                else
                {
                    GearBox.AddGear(Capture_AccessName, bool.TrueString);
                }
                return x;
            }
            set
            {
                Gear g = GearBox.GetGear(Capture_AccessName);
                if (g != null)
                {
                    g.Value = value.ToString();
                }
                else
                {
                    GearBox.AddGear(Capture_AccessName, value.ToString());
                }
            }
        }
        public bool ExitToTray
        {
            get
            {
                bool x = true;
                Gear g = GearBox.GetGear(ExitToTray_AccessName);
                if (g != null)
                {
                    if (!bool.TryParse(g.Value, out x))
                    {
                        g.Value = bool.TrueString;
                    }
                }
                else
                {
                    GearBox.AddGear(ExitToTray_AccessName, bool.TrueString);
                }
                return x;
            }
            set
            {
                Gear g = GearBox.GetGear(ExitToTray_AccessName);
                if (g != null)
                {
                    g.Value = value.ToString();
                }
                else
                {
                    GearBox.AddGear(ExitToTray_AccessName, value.ToString());
                }
            }
        }
        public bool ConfirmOperation
        {
            get
            {
                bool x = true;
                Gear g = GearBox.GetGear(ConfirmOperation_AccessName);
                if (g != null)
                {
                    if (!bool.TryParse(g.Value, out x))
                    {
                        g.Value = bool.FalseString;
                    }
                }
                else
                {
                    GearBox.AddGear(ConfirmOperation_AccessName, bool.FalseString);
                }
                return x;
            }
            set
            {
                Gear g = GearBox.GetGear(ConfirmOperation_AccessName);
                if (g != null)
                {
                    g.Value = value.ToString();
                }
                else
                {
                    GearBox.AddGear(ConfirmOperation_AccessName, value.ToString());
                }
            }
        }
        public bool DarkTheme
        {
            get
            {
                bool x = false;
                Gear g = GearBox.GetGear(DarkTheme_AccessName);
                if (g != null)
                {
                    if (!bool.TryParse(g.Value, out x))
                    {
                        g.Value = bool.FalseString;
                    }
                }
                else
                {
                    GearBox.AddGear(DarkTheme_AccessName, bool.FalseString);
                }
                return x;
            }
            set
            {
                Gear g = GearBox.GetGear(DarkTheme_AccessName);
                if (g != null)
                {
                    g.Value = value.ToString();
                }
                else
                {
                    GearBox.AddGear(DarkTheme_AccessName, value.ToString());
                }
            }
        }
        public bool InvertedTheme
        {
            get
            {
                bool x = false;
                Gear g = GearBox.GetGear(InvertedTheme_AccessName);
                if (g != null)
                {
                    if (!bool.TryParse(g.Value, out x))
                    {
                        g.Value = bool.FalseString;
                    }
                }
                else
                {
                    GearBox.AddGear(InvertedTheme_AccessName, bool.FalseString);
                }
                return x;
            }
            set
            {
                Gear g = GearBox.GetGear(InvertedTheme_AccessName);
                if (g != null)
                {
                    g.Value = value.ToString();
                }
                else
                {
                    GearBox.AddGear(InvertedTheme_AccessName, value.ToString());
                }
            }
        }
        public Color ThemeColor
        {
            get
            {
                Gear g = GearBox.GetGear(ThemeColor_AccessName);
                Color y = Color.Purple;
                if (g != null)
                {
                    if (Tools.IsNum(g.Value))
                    {
                        y = Color.FromArgb(int.Parse(g.Value));
                    }
                }
                else
                {
                    GearBox.AddGear(ThemeColor_AccessName, y.ToArgb().ToString());
                }
                return Color.FromArgb(y.R, y.G, y.B);
            }
            set
            {
                Gear g = GearBox.GetGear(ThemeColor_AccessName);
                if (g != null)
                {
                    g.Value = value.ToArgb().ToString();
                }
                else
                {
                    GearBox.AddGear(ThemeColor_AccessName, value.ToArgb().ToString());
                }
            }
        }
        public Color ReverseDarkColor
        {
            get
            {
                Gear g = GearBox.GetGear(ReverseDarkColor_AccessName);
                Color y = Color.FromArgb(10, 10, 10);
                if (g != null)
                {
                    if (Tools.IsNum(g.Value))
                    {
                        y = Color.FromArgb(int.Parse(g.Value));
                    }
                }
                else
                {
                    GearBox.AddGear(ReverseDarkColor_AccessName, y.ToArgb().ToString());
                }
                return Color.FromArgb(y.R, y.G, y.B);
            }
            set
            {
                Gear g = GearBox.GetGear(ReverseDarkColor_AccessName);
                if (g != null)
                {
                    g.Value = value.ToArgb().ToString();
                }
                else
                {
                    GearBox.AddGear(ReverseDarkColor_AccessName, value.ToArgb().ToString());
                }
            }
        }
        public Color ReverseLightColor
        {
            get
            {
                Gear g = GearBox.GetGear(ReverseLightColor_AccessName);
                Color y = Color.FromArgb(245, 245, 245);
                if (g != null)
                {
                    if (Tools.IsNum(g.Value))
                    {
                        y = Color.FromArgb(int.Parse(g.Value));
                    }
                }
                else
                {
                    GearBox.AddGear(ReverseLightColor_AccessName, y.ToArgb().ToString());
                }
                return Color.FromArgb(y.R, y.G, y.B);
            }
            set
            {
                Gear g = GearBox.GetGear(ReverseLightColor_AccessName);
                if (g != null)
                {
                    g.Value = value.ToArgb().ToString();
                }
                else
                {
                    GearBox.AddGear(ReverseLightColor_AccessName, value.ToArgb().ToString());
                }
            }
        }
    }
}
