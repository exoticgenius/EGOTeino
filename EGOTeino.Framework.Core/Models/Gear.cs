using Fractal;
using Fractal.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EGOTeino.Framework.Core
{
    /// <summary>
    /// represent single config item
    /// </summary>
    public class Gear:Node
    {
        public static int FEATURES_LIMIT = 1;
        /// <summary>
        /// creates an empty gear item
        /// </summary>
        public Gear()
        {
            SetTypeName(this.GetType());
            for (int i = Features.Count; i < FEATURES_LIMIT; i++) Features.Add("");
        }
        /// <summary>
        /// creates an gear item by getting it's name and value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Gear(string name , string value) : base(name,value)
        {
            SetTypeName(this.GetType());
            for (int i = Features.Count; i < FEATURES_LIMIT; i++) Features.Add("");
        }

        /// <summary>
        /// item value
        /// </summary>
        public string Value
        {
            get => Features[0];
            set => this.SetFeature(0, value);
        }
    }
}
