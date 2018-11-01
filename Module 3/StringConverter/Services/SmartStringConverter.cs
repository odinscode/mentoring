using StringConverter.Models;
using System.Text;

namespace StringConverter.Services
{
    public static class SmartStringConverter
    {
        public const int FirstNumericValueInAscii = 48;

        public static int ConvertStringToInteger(string input)
        {
            byte[] inputAsciiBytes = Encoding.ASCII.GetBytes(input);

            int[] result = new int[inputAsciiBytes.Length];

            for (int i = 0; i < inputAsciiBytes.Length; i++)
            {
                int currentNumberCode = GetNumberCode(inputAsciiBytes[i]);

                if (IsNumber(currentNumberCode))
                    result[i] = currentNumberCode;
                else
                {
                    throw new StringConverterException($"Input string contains at least one non-numeric symbol: {input[i]} at position {i}");
                }
            }

            int output = 0;

            foreach (var number in result)
            {
                if (number != 0)
                {
                    if (output == 0)
                        output = number;
                    else
                        output = 10 * output + number;
                }
            }

            return output;
        }

        public static int GetNumberCode(int symbolCode)
        {
            return symbolCode - FirstNumericValueInAscii;
        }

        public static bool IsNumber(int symbolCode)
        {
            return symbolCode >= 0 && symbolCode <= 9;
        }
    }
}
