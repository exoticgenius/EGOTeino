using EGO.SolidUI;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EGOTeino.Framework.UI.Forms
{
    public partial class AssistantForm : IForm
    {
        #region fields used to defind sizeable feature for form
        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int THICKNESS = 5;
        #endregion fields
        #region props used to defind sizeable feature for form
        Rectangle R_Top { get => new Rectangle(0, 0, this.ClientSize.Width, THICKNESS); }
        Rectangle R_Left { get => new Rectangle(0, 0, THICKNESS, this.ClientSize.Height); }
        Rectangle R_Bottom { get => new Rectangle(0, this.ClientSize.Height - THICKNESS, this.ClientSize.Width, THICKNESS); }


        Rectangle R_Right { get => new Rectangle(this.ClientSize.Width - THICKNESS, 0, THICKNESS, this.ClientSize.Height); }

        Rectangle TopLeft { get => new Rectangle(0, 0, THICKNESS, THICKNESS); }
        Rectangle TopRight { get => new Rectangle(this.ClientSize.Width - THICKNESS, 0, THICKNESS, THICKNESS); }
        Rectangle BottomLeft { get => new Rectangle(0, this.ClientSize.Height - THICKNESS, THICKNESS, THICKNESS); }
        Rectangle BottomRight { get => new Rectangle(this.ClientSize.Width - THICKNESS, this.ClientSize.Height - THICKNESS, THICKNESS, THICKNESS); }
        #endregion props
        MainCore _mainCore;
        public AssistantForm(MainCore mainCore)
        {
            InitializeComponent();
            lbl_head.MouseDown += this.MouseKeyDown;
            this.Icon = Properties.Resources.MainIcon;
            Sync();
            this._mainCore = mainCore;
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (R_Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (R_Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (R_Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (R_Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
    }
}
