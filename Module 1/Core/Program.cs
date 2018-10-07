using System;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            SayGrettings(args[0]);
        }

        static void SayGrettings(string personName)
        {
            System.Console.WriteLine($"Hello, {personName}!");
        }
    }
}
