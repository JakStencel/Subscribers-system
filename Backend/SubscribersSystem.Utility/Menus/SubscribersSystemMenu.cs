using System;
using System.Collections.Generic;

namespace SubscribersSystem.Utility.Menus
{
    public enum SubscriberSystemCommand
    {
        AddSubscriber = 1,
        AddAnOffer,
        AssignOfferToTheSubscriber,
        GenerateInvoice,
        ExportInvoice,
        Exit
    }

    public interface ISubscribersSystemMenu
    {
        void AddCommandToDictionaryMenu(int nameOfCommand, Action command);
        void PrintOutCommands();
        void RunCommandFromDictionaryMenu(int indexOfCommand);
    }

    public class SubscribersSystemMenu : ISubscribersSystemMenu
    {
        private Dictionary<int, Action> _menuActionDictionary = new Dictionary<int, Action>();

        public void AddCommandToDictionaryMenu(int nameOfCommand, Action command)
        {
            _menuActionDictionary.Add(nameOfCommand, command);
        }

        public void RunCommandFromDictionaryMenu(int indexOfCommand)
        {
            _menuActionDictionary[indexOfCommand]();
        }

        public void PrintOutCommands()
        {
            Console.WriteLine("Choose an option from below:");
            foreach (var command in _menuActionDictionary.Keys)
            {
                SubscriberSystemCommand order = (SubscriberSystemCommand)Enum.ToObject(typeof(SubscriberSystemCommand), command);
                Console.WriteLine($"{command}) To {order}, please key in '{command}'");
            }
        }
    }
}
