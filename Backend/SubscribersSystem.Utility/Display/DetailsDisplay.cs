using SubscribersSystem.Business.ReportModels;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Utility.Helpers;
using System;

namespace SubscribersSystem.Utility.Display
{
    public interface IDetailsDisplay
    {
        void ShowAccountBalance(PhoneReportBl report);
        void ShowAllOffers();
        void ShowAllSubscribers();
        void ShowAllInvoices(int indexOfSubscriber);
    }

    public class DetailsDisplay : IDetailsDisplay
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IOfferService _offerService;
        private readonly IInvoiceService _invoiceService;
        private readonly IModelDisplay _modelDisplay;
        private readonly IIoHelper _ioHelper;

        public DetailsDisplay(ISubscriberService subscriberService,
                                IOfferService offerService,
                                IInvoiceService invoiceService,
                                IModelDisplay modelDisplay,
                                IIoHelper ioHelper)
        {
            _subscriberService = subscriberService;
            _offerService = offerService;
            _invoiceService = invoiceService;
            _modelDisplay = modelDisplay;
            _ioHelper = ioHelper;
        }

        public void ShowAllSubscribers()
        {
            var subscribers = _subscriberService.GetAll();

            foreach (var subscriber in subscribers)
            {
                _ioHelper.PrintMessage($"{Environment.NewLine} To choose this Subscriber kye in '{subscriber.Id}': {Environment.NewLine}");
                _modelDisplay.DisplaySubscriber(subscriber);
            }
        }

        public void ShowAllOffers()
        {
            var offers = _offerService.GetAll();

            foreach (var offer in offers)
            {
                _ioHelper.PrintMessage($"{Environment.NewLine} To choose this Offer key in '{offer.Id}': {Environment.NewLine}");
                _modelDisplay.DisplayOffer(offer);
            }
        }

        public void ShowAllInvoices(int indexOfSubscriber)
        {
            var invoices = _invoiceService.GetAllInvoicesForSubscriber(indexOfSubscriber);

            foreach(var invoice in invoices)
            {
                _ioHelper.PrintMessage($"{Environment.NewLine} To choose this invoice key in '{invoice.Number}': {Environment.NewLine}");
                _modelDisplay.DisplayInvoice(invoice);
            }
        }

        public void ShowAccountBalance(PhoneReportBl report)
        {
            _ioHelper.PrintMessageWithConsoleRead($"The account balans: {Environment.NewLine}" +
                                   $"The name of the offer: {report.NameOfTheOffer}{Environment.NewLine}" +
                                   $"The price of the offer: {report.PriceOfTheOffer}{Environment.NewLine}" +
                                   $"The total cost of minutes outside of the bundle: {report.TotlaCostOfConnections}{Environment.NewLine}" +
                                   $"The total cost of text messages outside of the bundle: {report.TotlaCostOfTextMessages}");
        }
    }
}
