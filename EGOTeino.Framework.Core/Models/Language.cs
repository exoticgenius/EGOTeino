using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fractal;
using Fractal.Extensions;

namespace EGOTeino.Framework.Core
{
    /// <summary>
    /// represent a list of key items with two sorted list for binary search in codes and chars
    /// </summary>
    public class Language : Node
    {
        public static int FEATURES_LIMIT = 0;

        /// <summary>
        /// list for keyitems sorted by code
        /// </summary>
        public List<KeyItem> CodeSorted = new List<KeyItem>();

        /// <summary>
        /// list for keyitems sorted by characters
        /// </summary>
        public List<KeyItem> CharSorted = new List<KeyItem>();

        /// <summary>
        /// creates an empty language
        /// </summary>
        public Language() : base("default")
        {
            SetTypeName(this.GetType());

        }
        
        /// <summary>
        /// get all available keyitems
        /// </summary>
        /// <returns>all keyitems</returns>
        public List<KeyItem> GetKeys()
        {
            return PullChildren<KeyItem>();
        }

        /// <summary>
        /// get key item by character using char sorted list
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public KeyItem GetKey(string character)
        {
            lock (this)
            {
                int charPosition = Tools.BinarySearch(CharSorted, x => x.KeyChar, character);
                if (charPosition > 0)
                {
                    return CharSorted[charPosition];
                }
                else
                {
                    return (KeyItem)Children.Find(x => ((KeyItem)x).KeyChar == character); ;
                }
            }
        }

        /// <summary>
        /// get key item by code using code sorted list
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public KeyItem GetKey(int code)
        {
            lock (this)
            {
                int codePosition = Tools.BinarySearch(CodeSorted, x => x.KeyCode, code);
                if (codePosition > 0)
                {
                    return CodeSorted[codePosition];
                }
                else
                {
                    return (KeyItem)Children.Find(x => ((KeyItem)x).KeyCode == code);
                }
            }
        }

        /// <summary>
        /// create keyitem by character and code and add to primary list
        /// </summary>
        /// <param name="code"></param>
        /// <param name="character"></param>
        /// <returns>return false when code conflict exist</returns>
        public bool AddKey(int code, string character)
        {
            return AddKey(new KeyItem(code, character));
        }

        /// <summary>
        /// create and add key to primary list
        /// </summary>
        /// <param name="code"></param>
        /// <param name="character"></param>
        /// <returns>return false when code conflict exist</returns>
        public bool AddKey(KeyItem keyItem)
        {
            lock (this)
            {
                int codePosition = Tools.BinarySearch(CodeSorted, x => x.KeyCode, keyItem.KeyCode);
                if (codePosition < 0)
                {
                    AddChild(keyItem);
                    keyItem.Root.OnRootEmitFinish(keyItem);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// sort again code and char sorted lists
        /// </summary>
        public void SortAll()
        {
            CodeSorted.Sort(Tools.Sort<KeyItem>(SortMode.Ascending, x => x.KeyCode,true));
            CharSorted.Sort(Tools.Sort<KeyItem>(SortMode.Ascending, x => x.KeyChar));
        }
    }
}

