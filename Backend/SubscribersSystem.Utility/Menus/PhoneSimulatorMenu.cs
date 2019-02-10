using System;
using System.Collections.Generic;

namespace SubscribersSystem.Utility.Menus
{
    public enum PhoneSimulatorCommand
    {
        BeginConnection = 1,
        SendTextMessage,
        ShowAccountBalance,
        Exit
    }

    public interface IPhoneSimulatorMenu
    {
        void AddCommandToDictionaryMenu(int nameOfCommand, Func<int, bool> command);
        void PrintOutCommands();
        bool RunCommandFromDictionaryMenu(int indexOfCommand, int phoneIndex);
    }

    public class PhoneSimulatorMenu : IPhoneSimulatorMenu
    {
        private Dictionary<int, Func<int, bool>> _menuFuncDictionary = new Dictionary<int, Func<int, bool>>();

        public void AddCommandToDictionaryMenu(int nameOfCommand, Func<int, bool> command)
        {
            _menuFuncDictionary.Add(nameOfCommand, command);
        }

        public bool RunCommandFromDictionaryMenu(int indexOfCommand, int phoneIndex)
        {
            return _menuFuncDictionary[indexOfCommand](phoneIndex);
        }

        public void PrintOutCommands()
        {
            Console.WriteLine("Choose an option from below:");
            foreach (var command in _menuFuncDictionary.Keys)
            {
                PhoneSimulatorCommand order = (PhoneSimulatorCommand)Enum.ToObject(typeof(PhoneSimulatorCommand), command);
                Console.WriteLine($"{command}) To {order}, please key in '{command}'");
            }
        }
    }
}
