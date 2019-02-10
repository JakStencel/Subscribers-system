using SubscribersSystem.Business.Models;
using SubscribersSystem.Utility.Helpers;
using System;

namespace SubscribersSystem.Utility.Display
{
    public interface IModelDisplay
    {
        void DisplayOffer(OfferBl offer);
        void DisplaySubscriber(SubscriberBl subscriber);
        void DisplayInvoice(InvoiceBl invoice);
    }

    public class ModelDisplay : IModelDisplay
    {
        private IIoHelper _ioHelper;

        public ModelDisplay(IIoHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        public void DisplaySubscriber(SubscriberBl subscriber)
        {
            _ioHelper.PrintMessage($"Name of the subscriber: {subscriber.Name} {Environment.NewLine}" +
                                   $"Surname of the subscriber: {subscriber.Surname} {Environment.NewLine}" +
                                   $"Date of birth of the subscriber: {subscriber.DateOfBirth.ToShortDateString()} {Environment.NewLine}");
        }

        public void DisplayOffer(OfferBl offer)
        {
            _ioHelper.PrintMessage($"Name of the offer: {offer.Name} {Environment.NewLine}" +
                                   $"Price of the offer: {offer.PriceOfTheOffer} {Environment.NewLine}");
        }

        public void DisplayInvoice(InvoiceBl invoice)
        {
            _ioHelper.PrintMessage($"Invoice number: {invoice.Number} {Environment.NewLine}" +
                                   $"Invoice beginning date: {invoice.BeginningDate} {Environment.NewLine}" +
                                   $"Invoice generation date: {invoice.GenerationDate} {Environment.NewLine}" +
                                   $"Total cost of Invoice to be paid: {invoice.TotalCostToBePaid} {Environment.NewLine}");
        }
    }
}
