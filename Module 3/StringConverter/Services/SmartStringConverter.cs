using StringConverter.Models;
using System.Text;

namespace StringConverter.Services
{
    public static class SmartStringConverter
    {
        public const int FirstNumericValueInAscii = 48;

        public const int MinusSignAsciiCode = 45;

        public static int ConvertStringToInteger(string input)
        {
            byte[] inputAsciiBytes = Encoding.ASCII.GetBytes(input);

            int[] result = new int[inputAsciiBytes.Length];

            bool isNegative = false;

            for (int i = 0; i < inputAsciiBytes.Length; i++)
            {
                int currentNumberCode = GetNumberCode(inputAsciiBytes[i]);

                if (IsNumber(currentNumberCode))
                    result[i] = currentNumberCode;
                else
                {
                    if (IsMinusSign(inputAsciiBytes[i]) && i == 0)
                        isNegative = true;
                    else
                        throw new StringConverterException(input[i], i);
                }
            }

            int output = 0;

            foreach (int number in result)
            {
                if (number != 0)
                {
                    if (output == 0)
                        output = number;
                    else
                        output = 10 * output + number;
                }
                else if (output != 0)
                {
                    output = 10 * output + number;
                }
            }

            return isNegative
                ? -1 * output
                : output;
        }

        private static bool IsMinusSign(int symbolCode)
        {
            return symbolCode == MinusSignAsciiCode;
        }

        private static int GetNumberCode(int symbolCode)
        {
            return symbolCode - FirstNumericValueInAscii;
        }

        private static bool IsNumber(int symbolCode)
        {
            return symbolCode >= 0 && symbolCode <= 9;
        }
    }
}
