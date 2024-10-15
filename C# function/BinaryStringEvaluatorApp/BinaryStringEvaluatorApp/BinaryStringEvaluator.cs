using System;

namespace BinaryStringEvaluatorApp
{
    public class BinaryStringEvaluator
    {
        public static bool IsGoodBinaryString(string s)
        {
            // Check if the string contains consecutive '1's
            return !s.Contains("11");
        }
    }
}
