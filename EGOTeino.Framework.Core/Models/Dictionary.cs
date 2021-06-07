using Fractal;
using Fractal.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EGOTeino.Framework.Core
{
    public class Dictionary : Node
    {
        public static int FEATURES_LIMIT = 2;

        /// <summary>
        /// creates an empty dictionary
        /// </summary>
        public Dictionary() : base("Dictionary")
        {
            SetTypeName(this.GetType());
            for (int i = Features.Count; i < FEATURES_LIMIT; i++) Features.Add("");

            EmitFinish += Language_EmitFinish;
        }
        /// <summary>
        /// remove all non keyitem items to prevent casing errors
        /// </summary>
        /// <param name="sender"></param>
        private void Language_EmitFinish(INode sender)
        {
            RemoveChild(x => !(x is Language));
        }
        
        /// <summary>
        /// get all stored languages
        /// </summary>
        /// <returns></returns>
        public List<Language> Getlanguages()
        {
            return PullChildren<Language>();
        }

        /// <summary>
        /// get language by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Language Getlanguage(string name)
        {
            return PullChildren<Language>().Find(x=>x.Name==name);
        }

        /// <summary>
        /// add language to dictionary
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public bool AddLanguage(Language language)
        {
            var l = PullChildren().Find(x => x.Name == language.Name);
            if (l != null) return false;
            AddChild(language);
            return true;
        }

        /// <summary>
        /// first selected language for fast use
        /// can be null
        /// </summary>
        public Language FirstLanguage
        {
            get
            {
                return PullChildren<Language>().Find(x => x.Name == Features[0]);
            }
            set
            {
                this.SetFeature(0, value?.Name ?? "");
            }
        }
        /// <summary>
        /// second selected language for fast use
        /// can be null
        /// </summary>
        public Language SecondLanguage
        {
            get
            {
                return PullChildren<Language>().Find(x => x.Name == Features[1]);
            }
            set
            {
                this.SetFeature(1, value?.Name ?? "");
            }
        }
    }
}
