using System;

namespace SubscribersSystem.Utility.Helpers
{
    public interface IIoHelper
    {
        void ClearConsole();
        DateTime GetDateOfBirthFromUser(string message);
        decimal GetDecimalFromUser(string message);
        int GetIntFromUser();
        int GetIntFromUser(string message);
        string GetStringFromUser(string message);
        void PrintMessage(string message);
        void PrintMessageWithConsoleRead(string message);
        int ReturnInt();
    }

    public class IoHelper : IIoHelper
    {
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintMessageWithConsoleRead(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public DateTime GetDateOfBirthFromUser(string message)
        {
            Console.Write(message);
            DateTime dateOfBirth;

            while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy",
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out dateOfBirth))
            {
                Console.WriteLine("You have entered the wrong format of date, try again: ");
            }
            return dateOfBirth;
        }

        public string GetStringFromUser(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public int GetIntFromUser(string message)
        {
            Console.Write(message);
            return ReturnInt();
        }

        public int GetIntFromUser()
        {
            return ReturnInt();
        }

        public decimal GetDecimalFromUser(string message)
        {
            Console.Write(message);
            decimal number;
            while(!decimal.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Please, insert the correct format: ");
            }
            return number;
        }

        public int ReturnInt()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Please. insert a number: ");
            };
            return number;
        }
    }
}
