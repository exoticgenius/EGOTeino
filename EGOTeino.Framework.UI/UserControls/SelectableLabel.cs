using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fractal;
using EGO.SolidUI;

namespace EGOTeino.Framework.UI
{
    public class SelectableLabel : FirstLabel
    {
        private bool _Selected = false;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                SyncLogic(value);

                _Selected = value;
            }
        }

        private void SyncLogic(bool value)
        {
            if (value && Parent != null)
            {
                foreach (var item in Parent.Controls)
                {
                    if (item is SelectableLabel sl && sl.Selected)
                    {
                        sl.Selected = false;
                    }
                }
            }

            ReverseInvertAction = value;
        }

        public SelectableLabel(INode target):this()
        {
            Tag = target;
        }
        public SelectableLabel()
        {
            Click += SelectableLabel_Click;
        }

        private void SelectableLabel_Click(object sender, EventArgs e)
        {
            Selected = !Selected;
            if (Parent is IThemeReference tr) tr.PerformStyle();
        }
    }
}
