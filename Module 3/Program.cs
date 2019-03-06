using System;

namespace OutputFirstCharApp
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
                    string result = Console.ReadLine();
                    char firstChar = result[0];
                    Console.WriteLine($"First char is {firstChar}");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("You entered an empty string!");
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went definitely wrong!");
                }
            }
        }
    }
}
