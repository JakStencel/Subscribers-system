using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.ReportModels;
using SubscribersSystem.Utility.DataCapture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubscribersSystem.Utility
{
    public interface ISystemDataCaptureService
    {
        InvoiceBl CaptureInvoice(SubscriberBl subscriber, List<PhoneReportBl> reportsBl);
        OfferBl CaptureOffer();
        PhoneBl CapturePhone(OfferBl chosenOffer, int generatedNumber);
        SubscriberBl CaptureSubscriber();
        string GetFilePath(string serializerExtension, InvoiceBl invoice);
    }

    public class SystemDataCaptureService : ISystemDataCaptureService
    {
        private readonly IInvoiceDataCapture _invoiceDataCapture;
        private readonly IOfferDataCapture _offerDataCapture;
        private readonly IPhoneDataCapture _phoneDataCapture;
        private readonly ISubscriberDataCapture _subscriberDataCapture;

        public SystemDataCaptureService(IInvoiceDataCapture invoiceDataCapture,
                                        IOfferDataCapture offerDataCapture,
                                        IPhoneDataCapture phoneDataCapture,
                                        ISubscriberDataCapture subscriberDataCapture)
        {

            _invoiceDataCapture = invoiceDataCapture;
            _offerDataCapture = offerDataCapture;
            _phoneDataCapture = phoneDataCapture;
            _subscriberDataCapture = subscriberDataCapture;
        }

        public SubscriberBl CaptureSubscriber()
        {
            return _subscriberDataCapture.Capture();
        }

        public OfferBl CaptureOffer()
        {
            return _offerDataCapture.Capture();
        }

        public PhoneBl CapturePhone(OfferBl chosenOffer, int generatedNumber)
        {
            return _phoneDataCapture.Capture(chosenOffer, generatedNumber);
        }

        public InvoiceBl CaptureInvoice(SubscriberBl subscriber, List<PhoneReportBl> reportsBl)
        {
            return _invoiceDataCapture.Capture(subscriber, reportsBl);
        }

        public string GetFilePath(string serializerExtension, InvoiceBl invoice)
        {
            return _invoiceDataCapture.GetFilePath(serializerExtension, invoice);
        }
    }
}
