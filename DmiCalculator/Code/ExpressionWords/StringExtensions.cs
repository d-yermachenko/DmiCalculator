using System;
using System.Collections;

namespace DmiCalc
{
    public static class StringExtensions {
        public static bool StartsWith(this String st, IEnumerable possibleBegins) {
            bool result = false;
            foreach (object possibleBegin in possibleBegins)
                if (possibleBegin != null)
                    result |= st?.StartsWith(possibleBegin?.ToString()) ?? false;
            return result;
        }

        public static string GetLongestPossibleBeginning(this String st, IEnumerable possibleBegins) {
            string possibleBeginning = null;
            int longestBeginning = -1;
            foreach (object possibleBegin in possibleBegins) {
                string potentialPossibleBegin = possibleBegin?.ToString();
                if (!String.IsNullOrEmpty(potentialPossibleBegin) && st.StartsWith(potentialPossibleBegin)) {
                    if (potentialPossibleBegin.Length > longestBeginning) {
                        possibleBeginning = potentialPossibleBegin;
                        longestBeginning = potentialPossibleBegin.Length;
                    }
                }
            }
            return possibleBeginning;
        }

    }
}