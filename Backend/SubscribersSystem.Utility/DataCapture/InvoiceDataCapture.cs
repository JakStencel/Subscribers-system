using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.ReportModels;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace SubscribersSystem.Utility.DataCapture
{
    public interface IInvoiceDataCapture
    {
        InvoiceBl Capture(SubscriberBl subscriber, List<PhoneReportBl> reportsBl);
        string GetFilePath(string serializerExtension, InvoiceBl invoice);
    }

    public class InvoiceDataCapture : IInvoiceDataCapture
    {
        private IInvoiceService _invoiceService;
        private IReportService _reportService;
        private IIoHelper _ioHelper;

        public InvoiceDataCapture(IInvoiceService invoiceService, IReportService reportService, IIoHelper ioHelper)
        {
            _invoiceService = invoiceService;
            _reportService = reportService;
            _ioHelper = ioHelper;
        }

        //Constructor for Web API, thanks to which we dont have to bind ioHelper in Web application
        public InvoiceDataCapture(IInvoiceService invoiceService, IReportService reportService)
        {
            _invoiceService = invoiceService;
            _reportService = reportService;
        }

        public InvoiceBl Capture(SubscriberBl subscriber, List<PhoneReportBl> reportsBl)
        {
            var newInvoice = new InvoiceBl
            {
                BeginningDate = _invoiceService.GetDateOfPreviousInvoice(subscriber),
                GenerationDate = DateTime.Now,
                Number = _invoiceService.GenerateInvoiceNumber(subscriber),
                Subscriber = subscriber,                                                       
                TotalCostOfConnections = _reportService.GetTotalCostOfConnections(subscriber),
                TotalCostOfTextMessages = _reportService.GetTotalCostOfTextMessages(subscriber),
                TotalOffersCost = _reportService.GetTotalCostBasedOnOffers(subscriber),
                TotalCostToBePaid = _reportService.GetTotalCostToBePaid(subscriber),
                PhoneReports = reportsBl
            };

            return newInvoice;
        }

        public string GetFilePath(string serializerExtension, InvoiceBl invoice)
        {
            var filePath = _ioHelper.GetStringFromUser("Enter the path of the file ( to save in working directory leave it empty ): ");

            while (!Directory.Exists(filePath))
            {
                if (String.IsNullOrEmpty(filePath))
                {
                    filePath = Directory.GetCurrentDirectory();
                    continue;
                }

                filePath = _ioHelper.GetStringFromUser($"Wrong path, insert the path again: {Environment.NewLine}");
            }

            return Path.Combine(filePath, $"{invoice.Number}.{serializerExtension.ToLowerInvariant()}");
        }
    }
}
