using Fractal;
using Fractal.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EGOTeino.Framework.Core
{
    /// <summary>
    /// represent a base Teino database
    /// </summary>
    public class DataSet : Node
    {
        public static int FEATURES_LIMIT = 0;

        /// <summary>
        /// creates an empty database
        /// </summary>
        public DataSet() : base("TEINO")
        {
            SetTypeName(this.GetType());
            for (int i = Features.Count; i < FEATURES_LIMIT; i++) Features.Add("");
            AddedChild += DataSet_AddedChild;
            RemovedChildStatic += Language_RemovedChildStatic;
            EmitFinish += DataSet_EmitFinish;
            EmitFinishStatic += DataSet_EmitFinishStatic;
        }

        /// <summary>
        /// this event raises when a key item added to a language and finished emitting data
        /// here keyitem will add to sorted lists in specific language
        /// </summary>
        /// <param name="sender">keyitem that finished emitting</param>
        private void DataSet_EmitFinishStatic(INode sender)
        {
            
            if (sender is KeyItem key)
                lock (sender)
                {
                    Language lang = (Language)key.Parent;
                    int codePosition = Tools.BinarySearch(lang.CodeSorted, x => x.KeyCode, ((KeyItem)key).KeyCode);
                    if (codePosition < 0)
                    {
                        codePosition = ~codePosition;
                    }
                    lang.CodeSorted.Insert(codePosition, (KeyItem)key);

                    int charPosition = Tools.BinarySearch(lang.CharSorted, x => x.KeyChar, ((KeyItem)key).KeyChar);
                    if (charPosition < 0)
                    {
                        charPosition = ~charPosition;
                    }
                    lang.CharSorted.Insert(charPosition, (KeyItem)key);
                }
        }

        /// <summary>
        /// raises when laguage deleted a keyitem from itself
        /// here keyitem will remove from sorted lists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="parameter1"></param>
        private void Language_RemovedChildStatic(INode sender, INode parameter1)
        {
            if (sender is Language lang)
                lock (sender)
                {
                    lang.CodeSorted.Remove((KeyItem)parameter1);
                    lang.CharSorted.Remove((KeyItem)parameter1);
                }
        }

        /// <summary>
        /// check all added children to the database and remove add items not matching condition
        /// </summary>
        /// <param name="sender"></param>
        private void DataSet_EmitFinish(INode sender)
        {
            sender.RemoveChild(x => !(x is GearBox) && !(x is Dictionary));
        }

        /// <summary>
        /// remove all duplicated children in database level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="parameter1"></param>
        private void DataSet_AddedChild(INode sender, INode parameter1)
        {
            if (parameter1 is GearBox)
            {
                if (PullChildren().FindAll(x => x is GearBox).Count > 1) sender.RemoveChild(parameter1);
            }
            else if (parameter1 is Dictionary)
            {
                if (PullChildren().FindAll(x => x is Dictionary).Count > 1) sender.RemoveChild(parameter1);
                
            }
            else
            {
                sender.RemoveChild(parameter1);
            }
        }

        /// <summary>
        /// provide the dictionary in database level
        /// </summary>
        public Dictionary Dictionary
        {
            get => (Dictionary)PullChildren().Find(x => x is Dictionary);
        }
        /// <summary>
        /// provide the gearbox in database level
        /// </summary>
        public GearBox GearBox
        {
            get => (GearBox)PullChildren().Find(x => x is GearBox);
        }
    }
}
