using Ninject;
using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.ReportModels;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Business.Services.SupportServices;
using SubscribersSystem.Utility;
using SubscribersSystem.Utility.Display;
using SubscribersSystem.Utility.Helpers;
using SubscribersSystem.Utility.Menus;
using System;
using System.Collections.Generic;

namespace SubscribersSystem
{
    internal class Program
    {
        private readonly ISubscribersSystemMenu _menu;
        private readonly IIoHelper _ioHelper;
        private readonly ISystemDataCaptureService _systemDataCaptureService;
        private readonly IDetailsDisplay _detailsDisplay;
        private readonly ICombiningUserOfferService _combiningUserOfferService;
        private readonly IPhoneSimulatorService _phoneSimulatorService;
        private readonly IReportService _reportService;
        private readonly ISerializerProvider _serializerProvider;

        public Program(ISubscribersSystemMenu menu,
                       IIoHelper ioHelper,
                       ISystemDataCaptureService systemDataCaptureService,
                       IDetailsDisplay detailsDisplay,
                       ICombiningUserOfferService combiningUserOfferService,
                       IPhoneSimulatorService phoneSimulatorService,
                       IReportService reportService,
                       ISerializerProvider serializerProvider)
        {
            _menu = menu;
            _ioHelper = ioHelper;
            _systemDataCaptureService = systemDataCaptureService;
            _detailsDisplay = detailsDisplay;
            _combiningUserOfferService = combiningUserOfferService;
            _phoneSimulatorService = phoneSimulatorService;
            _reportService = reportService;
            _serializerProvider = serializerProvider;
        }

        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            CliDependenciesModule.Register(kernel);
            var program = kernel.Get<Program>();

            program.Run();
        }

        private void Run()
        {
            _menu.AddCommandToDictionaryMenu((int)SubscriberSystemCommand.AddSubscriber, AddSubscriber);
            _menu.AddCommandToDictionaryMenu((int)SubscriberSystemCommand.AddAnOffer, AddAnOffer);
            _menu.AddCommandToDictionaryMenu((int)SubscriberSystemCommand.AssignOfferToTheSubscriber, AssignOfferToTheSubscriber);
            _menu.AddCommandToDictionaryMenu((int)SubscriberSystemCommand.GenerateInvoice, GenerateInvoice);
            _menu.AddCommandToDictionaryMenu((int)SubscriberSystemCommand.ExportInvoice, ExportInvoice);
            _menu.AddCommandToDictionaryMenu((int)SubscriberSystemCommand.Exit, Exit);

            while (true)
            {
                _ioHelper.ClearConsole();
                _menu.PrintOutCommands();
                var command = _ioHelper.GetIntFromUser();

                try
                {
                    _menu.RunCommandFromDictionaryMenu(command);

                }
                catch (Exception e)
                {
                    _ioHelper.PrintMessageWithConsoleRead($"Run into an error {e.Message} {Environment.NewLine}To get back to menu press any key");
                    continue;
                }
            }
        }

        private void AddSubscriber()
        {
            SubscriberBl newSubscriber;
            try
            {
                newSubscriber = _systemDataCaptureService.CaptureSubscriber();
                _combiningUserOfferService.AddSubscriberAsync(newSubscriber).Wait();
            }
            catch(ArgumentException e)
            {
                _ioHelper.PrintMessageWithConsoleRead($"{e.Message}");
                return;
            }
            catch
            {
                _ioHelper.PrintMessageWithConsoleRead("Adding new subscribes failed... Try again!");
                return;
            }
            _ioHelper.PrintMessageWithConsoleRead("Adding new subscriber succeeded");
        }

        private void AddAnOffer()
        {
            var newOffer = _systemDataCaptureService.CaptureOffer();
            try
            {
                _combiningUserOfferService.AddAnOfferAsync(newOffer).Wait();
            }
            catch
            {
                _ioHelper.PrintMessageWithConsoleRead("Adding new offer failed... Try again!");
                return;
            }
            _ioHelper.PrintMessageWithConsoleRead("Adding new offer succeeded");
        }

        private void AssignOfferToTheSubscriber()
        {
            SubscriberBl chosenSubscriber;
            OfferBl chosenOffer;

            _ioHelper.ClearConsole();
            _ioHelper.PrintMessage("Choose subscriber, to which you want to assign the offer:");
            _detailsDisplay.ShowAllSubscribers();
            var indexOfSubscriberFromUser = _ioHelper.GetIntFromUser();

            _ioHelper.PrintMessage("Choose the offer you want to assign to the chosen above subscriber:");
            _detailsDisplay.ShowAllOffers();
            var indexOfTheOfferFromUser = _ioHelper.GetIntFromUser();

            try
            {
                chosenSubscriber = _combiningUserOfferService.GetSubscriberAsync(indexOfSubscriberFromUser).Result;
                chosenOffer = _combiningUserOfferService.GetOfferAsync(indexOfTheOfferFromUser).Result;
            }
            catch (Exception e)
            {
                _ioHelper.PrintMessageWithConsoleRead($"{e.Message} {Environment.NewLine}press any key to go back to main menu");
                return;
            }
            _ioHelper.ClearConsole();

            var generatedNumber = _phoneSimulatorService.GeneratePhoneNumber();

            var newPhone = _systemDataCaptureService.CapturePhone(chosenOffer, generatedNumber);

            _phoneSimulatorService.AddGeneratedPhoneAsync(indexOfSubscriberFromUser, indexOfTheOfferFromUser, newPhone).Wait();
        }

        private void GenerateInvoice()
        {
            SubscriberBl chosenSubscriber;
            _ioHelper.ClearConsole();
            _ioHelper.PrintMessage("Choose subscriber, to which you want generate invoice");
            _detailsDisplay.ShowAllSubscribers();
            var indexOfSubscriberFromUser = _ioHelper.GetIntFromUser();

            try
            {
                chosenSubscriber = _combiningUserOfferService.GetSubscriberAsync(indexOfSubscriberFromUser).Result;
                _combiningUserOfferService.CheckIfSubscriberHasPhoneNumbers(indexOfSubscriberFromUser);
            }
            catch (Exception e)
            {
                _ioHelper.PrintMessageWithConsoleRead($"{e.Message} {Environment.NewLine}press any key to go back to main menu");
                return;
            }
            _ioHelper.ClearConsole();

            var reportsBl = new List<PhoneReportBl>();
            chosenSubscriber.Phones.ForEach(p => reportsBl.Add(_reportService.GenerateReport(p)));

            var newInvoice = _systemDataCaptureService.CaptureInvoice(chosenSubscriber, reportsBl);

            _combiningUserOfferService.AddInvoiceAsync(newInvoice, indexOfSubscriberFromUser).Wait();
            _phoneSimulatorService.UpdateBundlesAsync(indexOfSubscriberFromUser).Wait();

            _ioHelper.PrintMessageWithConsoleRead("Generating new invoice succeeded");
        }

        private void ExportInvoice()
        {
            string numberOFInvoiceFromUser;
            InvoiceBl chosenInvoice;

            _ioHelper.ClearConsole();

            _ioHelper.PrintMessage("Choose subscriber whose invoice you want to export: ");
            _detailsDisplay.ShowAllSubscribers();
            var indexOfSubscriberFromUser = _ioHelper.GetIntFromUser();

            _ioHelper.ClearConsole();
            _ioHelper.PrintMessage("Choose invoice form given below: ");

            try
            {
                _detailsDisplay.ShowAllInvoices(indexOfSubscriberFromUser);
                numberOFInvoiceFromUser = _ioHelper.GetStringFromUser("");
                chosenInvoice = _combiningUserOfferService.GetInvoice(numberOFInvoiceFromUser);
            }
            catch (Exception e)
            {
                _ioHelper.PrintMessageWithConsoleRead($"{e.Message} {Environment.NewLine}press any key to go back to main menu");
                return;
            }

            _ioHelper.ClearConsole();
            var serializer = _ioHelper.GetStringFromUser("Choose export format (xml/json): ");
            var _fileSerializer = _serializerProvider.SerializerChanger(serializer);

            if (_fileSerializer == null)
            {
                _ioHelper.PrintMessageWithConsoleRead("To get back to menu press any key");
                return;
            }

            _fileSerializer.SaveToFileAsync(_systemDataCaptureService.GetFilePath(serializer, chosenInvoice), chosenInvoice).Wait();
            _ioHelper.PrintMessageWithConsoleRead("Exporting invoice succeeded");
        }

        private void Exit()
        {
            Environment.Exit(0);
        }
    }
}
