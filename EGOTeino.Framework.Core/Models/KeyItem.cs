using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Fractal;
using Fractal.Extensions;

namespace EGOTeino.Framework.Core
{
    /// <summary>
    /// Represent item to store keyboard keys
    /// </summary>
    public class KeyItem : Node
    {
        public static int FEATURES_LIMIT = 2;
        /// <summary>
        /// temp store keycode to increase speed
        /// </summary>
        private int? _KeyCode = null;

        /// <summary>
        /// create key by getting code and char
        /// </summary>
        /// <param name="Code">keyboard bounded code</param>
        /// <param name="character">keyboard bounded char</param>
        public KeyItem(int Code, string character) : this()
        {
            KeyCode = Code;
            KeyChar = character;
        }

        /// <summary>
        /// creates an empty keyitem
        /// </summary>
        public KeyItem():base()
        {
            SetTypeName(this.GetType());
            for (int i = Features.Count; i < FEATURES_LIMIT; i++) Features.Add("");
            this.EmitFinish += KeyItem_EmitFinish;
        }

        /// <summary>
        /// uses when program launched and retrieved data from file to store temp data
        /// </summary>
        /// <param name="sender"></param>
        private void KeyItem_EmitFinish(INode sender)
        {
            _ = KeyCode;
        }

        /// <summary>
        /// item code
        /// </summary>
        public int KeyCode
        {
            get
            {
                if (_KeyCode.HasValue) return _KeyCode.Value;

                int x = 0;
                if (int.TryParse(Features[0], out x))
                {
                    _KeyCode = x;
                    return x;
                }
                return -1;
            }
            set
            {
                this.SetFeature(0, value.ToString());
                _KeyCode = value;
            }
        }

        /// <summary>
        /// item character
        /// </summary>
        public string KeyChar
        {
            get
            {
                return Features[1];
            }
            set
            {
                this.SetFeature(1, value);
            }
        }
    }
}
