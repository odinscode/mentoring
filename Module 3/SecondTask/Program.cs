using StringConverter.Services;
using StringConverter.Models;
using System;

namespace SecondTask
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter string: ");
                    string input = Console.ReadLine();
                    int result = SmartStringConverter.ConvertStringToInteger(input);
                    Console.WriteLine($"Input string was successfully converted to integer {result}");
                }
                catch(StringConverterException ex)
                {
                    Console.WriteLine($"Input string was NOT successfully converted to integer");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
