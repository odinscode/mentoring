using GreetingsLibrary;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var defaultUser = string.Empty;
            if (args.Length != 0)
            {
                defaultUser = args[0];
            }
            System.Console.WriteLine(GreetingService.GreetPerson(defaultUser));
        }
    }
}
