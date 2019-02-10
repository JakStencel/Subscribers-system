using Ninject;
using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Business.Services.SupportServices;
using SubscribersSystem.Utility;
using SubscribersSystem.Utility.Display;
using SubscribersSystem.Utility.Helpers;
using SubscribersSystem.Utility.Menus;
using System;
using System.Diagnostics;

namespace SubscribersSystem.PhoneSimulator
{
    internal class Program
    {
        private IPhoneSimulatorMenu _menu;
        private IIoHelper _ioHelper;
        private IDetailsDisplay _detailsDisplay;
        private ISimulatorDataCaptureService _simulatorDataCaptureService;
        private IPhoneSimulatorService _phoneSimulatorService;
        private IReportService _reportService;

        public Program(IPhoneSimulatorMenu menu,
                       IIoHelper ioHelper,
                       IDetailsDisplay detailsDisplay,
                       ISimulatorDataCaptureService simulatorDataCaptureService,
                       IPhoneSimulatorService phoneSimulatorService,
                       IReportService reportService)
        {
            _menu = menu;
            _ioHelper = ioHelper;
            _detailsDisplay = detailsDisplay;
            _simulatorDataCaptureService = simulatorDataCaptureService;
            _phoneSimulatorService = phoneSimulatorService;
            _reportService = reportService;
        }

        private const int _numberOfMilisecInSecond = 1000;
        private bool shouldExit = false;

        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            CliDependenciesModule.Register(kernel);
            var program = kernel.Get<Program>();

            program.Run();
        }

        public void Run()
        {
            _menu.AddCommandToDictionaryMenu((int)PhoneSimulatorCommand.BeginConnection, BeginConnection);
            _menu.AddCommandToDictionaryMenu((int)PhoneSimulatorCommand.SendTextMessage, SendTextMessage);
            _menu.AddCommandToDictionaryMenu((int)PhoneSimulatorCommand.ShowAccountBalance, ShowAccountBalance);
            _menu.AddCommandToDictionaryMenu((int)PhoneSimulatorCommand.Exit, Exit);

            var phoneBl = ChoosePhoneToSimulate();

            while (!shouldExit)
            {
                _ioHelper.ClearConsole();
                _menu.PrintOutCommands();
                var command = _ioHelper.GetIntFromUser();

                try
                {
                    shouldExit = _menu.RunCommandFromDictionaryMenu(command, phoneBl.Id);
                }
                catch (Exception e)
                {
                    _ioHelper.PrintMessageWithConsoleRead($"Run into an error {e.Message} {Environment.NewLine}To get back to menu press any key...");
                    continue;
                }
            }
        }

        public PhoneBl ChoosePhoneToSimulate()
        {
            var phoneNumber = _phoneSimulatorService.GetPhoneNumberFromAppConfigFile();  
            if (_phoneSimulatorService.GetPhoneByNumber(phoneNumber) == null)
            {
                _ioHelper.PrintMessageWithConsoleRead("There is no number provided! Press any key to exit the application");
                Environment.Exit(0);
            }
            return _phoneSimulatorService.GetPhoneByNumber(phoneNumber);
        }

        public bool BeginConnection(int indexOfPhone)
        {
            Stopwatch stopwatch = new Stopwatch();
            _ioHelper.PrintMessageWithConsoleRead("To begin Connection, press any key");
            _ioHelper.ClearConsole();

            stopwatch.Start();
            _ioHelper.PrintMessageWithConsoleRead($" Connection is started...{Environment.NewLine}To stop connection, press any key");
            _ioHelper.ClearConsole();

            stopwatch.Stop();
            var timeElapsed = stopwatch.Elapsed.TotalMilliseconds / _numberOfMilisecInSecond;
            _ioHelper.PrintMessage($"Connection is ended{Environment.NewLine}Time elapsed: {timeElapsed} second(s) ");

            var newConnection = _simulatorDataCaptureService.CaptureConnection(timeElapsed);

            var phoneBl = _phoneSimulatorService.GetPhoneById(indexOfPhone);                
            var decreasedBundleOfSeconds = _phoneSimulatorService.DecreaseBundleOfMinutes(phoneBl, newConnection);

            var message = _phoneSimulatorService.CheckIfBundleOfMinutesExceeded(decreasedBundleOfSeconds)
                ? $"The price of the connection: " +
                $"{_phoneSimulatorService.GetPriceOfTheConnection(phoneBl, decreasedBundleOfSeconds)}" 
                : "Connection made as a part of bundle";

            _ioHelper.PrintMessageWithConsoleRead(message);

            _phoneSimulatorService.AddCostOfConnectionToTotalCost(phoneBl, decreasedBundleOfSeconds); 

            phoneBl.Connections.Add(newConnection);

            _phoneSimulatorService.AddConnection(newConnection, indexOfPhone);
            _phoneSimulatorService.UpdatePhone(phoneBl);

            return false;
        }

        public bool SendTextMessage(int indexOfPhone)
        {
            var newSms = _simulatorDataCaptureService.CaptureSms();

            var phoneBl = _phoneSimulatorService.GetPhoneById(indexOfPhone);

            _phoneSimulatorService.DecreaseNumberOfSmsInBundle(phoneBl);

            var message = _phoneSimulatorService.CheckIfBundleOfSMSExceeded(phoneBl) 
                ? $"The price of the message: {phoneBl.Offer.PricePerTextMessage}" 
                : "Message send as a part of bundle";

            _ioHelper.PrintMessageWithConsoleRead(message);

            _phoneSimulatorService.AddCostOfTextMessageToTotalCost(phoneBl);

            phoneBl.ShortTextMessages.Add(newSms);

            _phoneSimulatorService.AddSms(newSms, indexOfPhone);
            _phoneSimulatorService.UpdatePhone(phoneBl);

            return false;
        }

        public bool ShowAccountBalance(int indexOfPhone)
        {
            var phoneBl = _phoneSimulatorService.GetPhoneById(indexOfPhone);
            var accontBalanceReport = _reportService.GenerateReport(phoneBl);

            _detailsDisplay.ShowAccountBalance(accontBalanceReport);

            return false;
        }

        public bool Exit(int indexOfPhone)
        {
            return true;
        }
    }
}
