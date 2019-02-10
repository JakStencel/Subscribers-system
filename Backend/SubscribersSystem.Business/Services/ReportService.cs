using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.ReportModels;
using System.Linq;

namespace SubscribersSystem.Business.Services
{
    public interface IReportService
    {
        PhoneReportBl GenerateReport(PhoneBl phone);
        decimal GetTotalCostOfConnections(SubscriberBl subscriber);
        decimal GetTotalCostOfTextMessages(SubscriberBl subscriber);
        decimal GetTotalCostBasedOnOffers(SubscriberBl subscriber);
        decimal GetTotalCostToBePaid(SubscriberBl subscriber);
    }

    public class ReportService : IReportService
    {
        public PhoneReportBl GenerateReport(PhoneBl phone)
        {
            var report = new PhoneReportBl
            {
                NameOfTheOffer = phone.Offer.Name,
                PriceOfTheOffer = phone.Offer.PriceOfTheOffer,
                TotlaCostOfConnections = phone.CostOfConnectionsOutsideBundle,
                TotlaCostOfTextMessages = phone.CostOfMessagesOutsideBundle,
                PhoneNumber = phone.PhoneNumber
            };

            return report;
        }

        public decimal GetTotalCostOfConnections(SubscriberBl subscriber)
        {
            return subscriber.Phones.Select(p => p.CostOfConnectionsOutsideBundle).Sum();
        }

        public decimal GetTotalCostOfTextMessages(SubscriberBl subscriber)
        {
            return subscriber.Phones.Select(p => p.CostOfMessagesOutsideBundle).Sum();
        }

        public decimal GetTotalCostBasedOnOffers(SubscriberBl subscriber)
        {
            return subscriber.Phones.Select(p => p.Offer).Select(o => o.PriceOfTheOffer).Sum();
        }

        public decimal GetTotalCostToBePaid(SubscriberBl subscriber)
        {
            return GetTotalCostOfConnections(subscriber)
                + GetTotalCostOfTextMessages(subscriber)
                + GetTotalCostBasedOnOffers(subscriber);
        }
    }
}
