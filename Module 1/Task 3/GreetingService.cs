using System;

namespace GreetingsLibrary
{
    public static class GreetingService
    {
        private const string ANONYMOUS = "Anonymous";

        public static string GreetPerson(string personName)
        {
            var user = personName;
            if (IsUserInputInvalid(personName))
            {
                user = ANONYMOUS;
            }
            return $"{GetFormattedCurrentTime()} Hello, {user}!";
        }

        public static string GetFormattedCurrentTime()
        {
            return DateTime.Now.ToString("h:mm:ss tt");
        }

        private static bool IsUserInputInvalid(string userInput)
        {
            return string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput);
        }
    }
}
