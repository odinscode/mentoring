using StringConverter.Models;
using System;
using System.Text;

namespace StringConverter.Services
{
    public class SmartStringConverter
    {
        public const int FirstNumericValueInAscii = 48;

        public int ConvertStringToInteger(string input)
        {
            byte[] inputAsciiBytes = Encoding.ASCII.GetBytes(input);

            byte[] result = new byte[inputAsciiBytes.Length];

            for (int i = 0; i < inputAsciiBytes.Length; i++)
            {
                if (isNumber(inputAsciiBytes[i]))
                    result[i] = inputAsciiBytes[i];
                else
                {
                    throw new StringConverterException($"Input string contains at least one non-numeric symbol: {input[i]}");
                }
            }

            int output = BitConverter.ToInt32(result, 0);

            return output;
        }

        public bool isNumber(int symbolCode)
        {
            var diff = symbolCode - FirstNumericValueInAscii;
            return diff >= 0 && diff <= 9;
        }
    }
}
