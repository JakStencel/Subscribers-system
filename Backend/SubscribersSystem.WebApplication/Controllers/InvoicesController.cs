using SubscribersSystem.Business.ReportModels;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Utility;
using SubscribersSystem.Utility.DataCapture;
using SubscribersSystem.Utility.MessageSender;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SubscribersSystem.WebApplication.Controllers
{
    [RoutePrefix("api/invoices")]
    public class InvoicesController : ApiController
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ISubscriberService _subscriberService;
        private readonly IReportService _reportService;
        private readonly IInvoiceDataCapture _invoiceDataCapture;
        private readonly IPhoneService _phoneService;
        private readonly ISerializerProvider _serializerProvider;
        private readonly IMessageSender _messageSender;

        public InvoicesController(IInvoiceService invoiceService, 
                                  ISubscriberService subscriberService,     
                                  IReportService reportService, 
                                  IInvoiceDataCapture invoiceDataCapture,
                                  IPhoneService phoneService,
                                  ISerializerProvider serializerProvider,
                                  IMessageSender messageSender)
        {
            _invoiceService = invoiceService;
            _subscriberService = subscriberService;
            _reportService = reportService;
            _invoiceDataCapture = invoiceDataCapture;
            _phoneService = phoneService;
            _serializerProvider = serializerProvider;
            _messageSender = messageSender;
        }

        [HttpGet, Route("generate/{suscriberId}")]
        public async Task<HttpResponseMessage> GenerateInvoiceAsync(int suscriberId)
        {
            try
            {
                var chosenSubscriber = await _subscriberService.GetSubscriberAsync(suscriberId);
                var reportsBl = new List<PhoneReportBl>();
                chosenSubscriber.Phones.ForEach(p => reportsBl.Add(_reportService.GenerateReport(p)));

                var newInvoice = _invoiceDataCapture.Capture(chosenSubscriber, reportsBl);

                var invoiceToReturn = await _invoiceService.AddAsync(newInvoice, suscriberId);

                await _phoneService.UpdateBundlesAsync(suscriberId);

                var serializerFormat = "json";
                var chosenSubscriberEmail = await _subscriberService.GetSubscribersEmailAsync(suscriberId);

                var _fileSerializer = _serializerProvider.SerializerChanger(serializerFormat);
                var path = _serializerProvider.GetCurrentDirectory(serializerFormat, invoiceToReturn);

                await _fileSerializer.SaveToFileAsync(path, invoiceToReturn);
                await _messageSender.SendAsync(chosenSubscriberEmail, path, invoiceToReturn);

                _invoiceService.RemoveInvoiceFile(path);

                return Request.CreateResponse(HttpStatusCode.OK, invoiceToReturn);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Generating new invoice failed. {e.Message}");
            }

        }

        [HttpGet, Route("export/{invoiceId}/{serializerFormat}")]
        public async Task<HttpResponseMessage> ExportInvoiceAsync([FromUri]int invoiceId, [FromUri]string serializerFormat)
        {
            try
            {
                var chosenInvoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);

                var _fileSerializer = _serializerProvider.SerializerChanger(serializerFormat);
                var path = _serializerProvider.GetCurrentDirectory(serializerFormat, chosenInvoice);

                var invoiceAsString = await _fileSerializer.SaveToFileAsync(path, chosenInvoice);
                return Request.CreateResponse(HttpStatusCode.OK, invoiceAsString);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Exporting invoice failed. {e.Message}");
            }
        }
    }
}
