using SubscribersSystem.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscribersSystem.Business.Services.SupportServices
{
    public interface ICombiningUserOfferService
    {
        Task AddAnOfferAsync(OfferBl offerBl);
        Task AddInvoiceAsync(InvoiceBl invoiceBl, int indexOfSubscriber);
        Task AddSubscriberAsync(SubscriberBl subscriberBl);
        bool CheckIfSubscriberHasPhoneNumbers(int indexOfSubscriber);
        InvoiceBl GetInvoice(string numberOfInvoice);
        Task<OfferBl> GetOfferAsync(int indexOfTheOffer);
        Task<SubscriberBl> GetSubscriberAsync(int indexOfSubscriber);
    }

    public class CombiningUserOfferService : ICombiningUserOfferService
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IOfferService _offerService;
        private readonly IInvoiceService _invoiceService;

        public CombiningUserOfferService(ISubscriberService subscriberService,
                                         IOfferService offerService,
                                         IInvoiceService invoiceService)
        {
            _subscriberService = subscriberService;
            _offerService = offerService;
            _invoiceService = invoiceService;
        }

        public async Task AddSubscriberAsync(SubscriberBl subscriberBl)
        {
            await _subscriberService.AddSubscriberAsync(subscriberBl);
        }

        public async Task<SubscriberBl> GetSubscriberAsync(int indexOfSubscriber)
        {
            return await _subscriberService.GetSubscriberAsync(indexOfSubscriber);
        }

        public bool CheckIfSubscriberHasPhoneNumbers(int indexOfSubscriber)
        {
            return _subscriberService.CheckIfSubscriberHasPhoneNumbers(indexOfSubscriber);
        }

        public async Task AddAnOfferAsync(OfferBl offerBl)
        {
            await _offerService.AddAnOfferAsync(offerBl);
        }

        public Task<OfferBl> GetOfferAsync(int indexOfTheOffer)
        {
            return _offerService.GetOfferAsync(indexOfTheOffer);
        }

        public async Task AddInvoiceAsync(InvoiceBl invoiceBl, int indexOfSubscriber)
        {
            await _invoiceService.AddAsync(invoiceBl, indexOfSubscriber);
        }

        public InvoiceBl GetInvoice(string numberOfInvoice)
        {
            return _invoiceService.GetInvoiceByNumber(numberOfInvoice);
        }
    }
}
