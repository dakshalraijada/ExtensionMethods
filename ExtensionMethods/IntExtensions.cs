using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionMethods
{
    public static class IntExtensions
    {
        /// <summary>
        /// Generates random number
        /// </summary>
        /// <param name="noOfDigits">Length of number</param>
        /// <returns></returns>
        public static int GenerateRandomNumber(int noOfDigits)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < noOfDigits; i++)
            {
                if (i == 0)
                {
                    sb.Append(new Random().Next(1, 9));
                }
                else
                {
                    sb.Append(new Random().Next(0, 9));
                }
            }

            return int.Parse(sb.ToString());
        }
    }
}
