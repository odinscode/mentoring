using System;

namespace GreetingsLibrary
{
    public static class GreetingService
    {
        public static string GreetPerson(string personName)
        {
            if (String.IsNullOrEmpty(personName))
            {
                return $"{GetFormattedCurrentTime()} Hello, anonymous!";
            }
            return $"{GetFormattedCurrentTime()} Hello, {personName}!";
        }

        public static string GetFormattedCurrentTime()
        {
            return DateTime.Now.ToString("h:mm:ss tt");
        }
    }
}
