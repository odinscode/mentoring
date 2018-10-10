using GreetingsLibrary;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(GreetingService.GreetPerson(args[0]));
        }
    }
}
