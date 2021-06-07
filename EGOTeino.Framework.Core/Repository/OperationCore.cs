using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EGOTeino.Framework.Core
{
    public class OperationCore
    {
        /// <summary>
        /// dependency dictionary
        /// </summary>
        public Dictionary Dictionary { get; set; }

        /// <summary>
        /// creates a operation core with provided dictionary
        /// </summary>
        /// <param name="dictionary">this arguement can not be null</param>
        public OperationCore(Dictionary dictionary)
        {
            Dictionary = dictionary;
        }

        /// <summary>
        /// primary operation for processing stirngs
        /// </summary>
        /// <param name="input">the that needs to be processed</param>
        /// <param name="higherPriority">first language that method will use to extract parts</param>
        /// <param name="lowerPriority">second language that method will use to extract parts.<br/> this one will use if first language do not contain the source character</param>
        /// <returns>processed string</returns>
        public string InsideOut(string input, Language higherPriority, Language lowerPriority)
        {
            StringBuilder built = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                built.Append(GetComplementChar(input.Substring(i, 1), higherPriority, lowerPriority));
            }

            return built.ToString();
        }
        /// <summary>
        /// find complement character or string for provided character or string
        /// </summary>
        /// <param name="input">the that needs to be processed</param>
        /// <param name="higherPriority">first language that method will use to extract part(s)</param>
        /// <param name="lowerPriority">second language that method will use to extract part(s).<br/> this one will use if first language do not contain the source character</param>
        /// <returns>processed character or string</returns>
        public string GetComplementChar(string character, Language higherPriority, Language lowerPriority)
        {
            var high = higherPriority.GetKey(character);

            if (high == null)
            {
                var low = lowerPriority.GetKey(character);

                if (low == null) return character;

                return pullComplement(higherPriority, low);
            }
            else
            {
                return pullComplement(lowerPriority, high);
            }

            string pullComplement(Language l,KeyItem k)
            {
                var complement = l.GetKey(k.KeyCode);

                if (complement == null) return character;

                return complement.KeyChar;
            }
        }
    }
}
