using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.ReportModels;
using SubscribersSystem.Data.Models;
using SubscribersSystem.Data.ReportModels;
using System.Collections.Generic;

namespace SubscribersSystem.Business
{
    public interface IDataObjectMapper
    {
        Connection MapConnectionBlToConnection(ConnectionBl connectionBl);
        List<Connection> MapConnectionsBlListToConnectionsList(List<ConnectionBl> connectionBlList);
        List<ConnectionBl> MapConnectionsListToConnectionsBlList(List<Connection> connectionsList);
        ConnectionBl MapConnectionToConnectionBl(Connection connection);
        Offer MapOfferBlToOffer(OfferBl offerBl);
        List<OfferBl> MapOfferListToOfferBlList(List<Offer> offers);
        OfferBl MapOfferToOfferBl(Offer offer);
        Phone MapPhoneBlToPhone(PhoneBl phoneBl);
        List<Phone> MapPhonesBlListToPhonesList(List<PhoneBl> phonesBl);
        List<PhoneBl> MapPhonesListToPhonesBlList(List<Phone> phones);
        PhoneBl MapPhoneToPhoneBl(Phone phone);
        List<Sms> MapSmsBlListToSmsList(List<SmsBl> smsBlList);
        Sms MapSmsBlToSms(SmsBl smsBl);
        List<SmsBl> MapSmsListToSmsBlList(List<Sms> smsList);
        SmsBl MapSmsToSmsBl(Sms sms);
        Subscriber MapSubscriberBlToSubscriber(SubscriberBl subscriberBl);
        List<SubscriberBl> MapSubscribersListToSubscribersBlList(List<Subscriber> subscribers);
        SubscriberBl MapSubscriberToSubscriberBl(Subscriber subscriber);
        PhoneReport MapPhoneReportBlToPhoneReport(PhoneReportBl phoneReportBl);
        PhoneReportBl MapPhoneReportToPhoneReportBl(PhoneReport phoneReport);
        Invoice MapInvoiceBlToInvoice(InvoiceBl invoiceBl);
        List<PhoneReport> MapPhoneReportBlListToPhoneReportList(List<PhoneReportBl> phoneReportsBl);
        InvoiceBl MapInvoiceToInvoiceBl(Invoice invoice);
        List<PhoneReportBl> MapPhoneReportListToPhoneReportBlList(List<PhoneReport> phoneReports);
        List<Invoice> MapInvoiceBlListToInvoiceList(List<InvoiceBl> invoicesBl);
        List<InvoiceBl> MapInvoiceListToInvoiceBlList(List<Invoice> invoices);
    }

    internal class DataObjectMapper : IDataObjectMapper
    {
        public Subscriber MapSubscriberBlToSubscriber(SubscriberBl subscriberBl)
        {
            var subscriber = new Subscriber
            {
                Id = subscriberBl.Id,
                Name = subscriberBl.Name,
                Surname = subscriberBl.Surname,
                DateOfBirth = subscriberBl.DateOfBirth,
                BillingCycle = subscriberBl.BillingCycle,
                Email = subscriberBl.Email,
                Phones = MapPhonesBlListToPhonesList(subscriberBl.Phones)
            };
            return subscriber;
        }

        public SubscriberBl MapSubscriberToSubscriberBl(Subscriber subscriber)
        {
            var subscriberBl = new SubscriberBl
            {
                Id = subscriber.Id,
                Name = subscriber.Name,
                Surname = subscriber.Surname,
                DateOfBirth = subscriber.DateOfBirth,
                BillingCycle = subscriber.BillingCycle,
                Email = subscriber.Email
            };

            if(subscriber.Phones != null)
            {
                subscriberBl.Phones = MapPhonesListToPhonesBlList(subscriber.Phones);
            }

            return subscriberBl;
        }

        public List<SubscriberBl> MapSubscribersListToSubscribersBlList(List<Subscriber> subscribers)
        {
            var subscribersBl = new List<SubscriberBl>();
            foreach(var subscriber in subscribers)
            {
                subscribersBl.Add(MapSubscriberToSubscriberBl(subscriber));
            }
            return subscribersBl;
        }

        public List<Phone> MapPhonesBlListToPhonesList(List<PhoneBl> phonesBl)
        {
            var phones = new List<Phone>();
            foreach(var phone in phonesBl)
            {
                phones.Add(MapPhoneBlToPhone(phone));
            }
            return phones;
        }

        public List<PhoneBl> MapPhonesListToPhonesBlList(List<Phone> phones)
        {
            var phonesBl = new List<PhoneBl>();
            foreach(var phone in phones)
            {
                phonesBl.Add(MapPhoneToPhoneBl(phone));
            }
            return phonesBl;
        }

        public Phone MapPhoneBlToPhone(PhoneBl phoneBl)
        {
            var phone = new Phone
            {
                Id = phoneBl.Id,
                PhoneNumber = phoneBl.PhoneNumber,
                SecondsLeftInBundle = phoneBl.SecondsLeftInBundle,
                TextMessagesLeftInBundle = phoneBl.TextMessagesLeftInBundle,
                CostOfConnectionsOutsideBundle = phoneBl.CostOfConnectionsOutsideBundle,
                CostOfMessagesOutsideBundle = phoneBl.CostOfMessagesOutsideBundle,
                Offer = MapOfferBlToOffer(phoneBl.Offer),
                ShortTextMessages = MapSmsBlListToSmsList(phoneBl.ShortTextMessages),
                Connections = MapConnectionsBlListToConnectionsList(phoneBl.Connections)
            };
            return phone;
        }

        public PhoneBl MapPhoneToPhoneBl(Phone phone)
        {
            var phoneBl = new PhoneBl
            {
                Id = phone.Id,
                PhoneNumber = phone.PhoneNumber,
                SecondsLeftInBundle = phone.SecondsLeftInBundle,
                TextMessagesLeftInBundle = phone.TextMessagesLeftInBundle,
                CostOfConnectionsOutsideBundle = phone.CostOfConnectionsOutsideBundle,
                CostOfMessagesOutsideBundle = phone.CostOfMessagesOutsideBundle,
            };

            if (phone.ShortTextMessages != null)
            {
                phoneBl.ShortTextMessages = MapSmsListToSmsBlList(phone.ShortTextMessages);
            }
            if (phone.Connections != null)
            {
                phoneBl.Connections = MapConnectionsListToConnectionsBlList(phone.Connections);
            }
            if (phone.Offer != null)
            {
                phoneBl.Offer = MapOfferToOfferBl(phone.Offer);
            }

            return phoneBl;
        }

        public Offer MapOfferBlToOffer(OfferBl offerBl)
        {
            var offer = new Offer
            {
                Id = offerBl.Id,
                Name = offerBl.Name,
                BundleOfMinutes = offerBl.BundleOfMinutes,
                BundleOfTextMessages = offerBl.BundleOfTextMessages,
                PricePerMinute = offerBl.PricePerMinute,
                PricePerTextMessage = offerBl.PricePerTextMessage,
                PriceOfTheOffer = offerBl.PriceOfTheOffer
            };
            return offer;
        }

        public OfferBl MapOfferToOfferBl(Offer offer)
        {
            var offerBl = new OfferBl
            {
                Id = offer.Id,
                Name = offer.Name,
                BundleOfMinutes = offer.BundleOfMinutes,
                BundleOfTextMessages = offer.BundleOfTextMessages,
                PricePerMinute = offer.PricePerMinute,
                PricePerTextMessage = offer.PricePerTextMessage,
                PriceOfTheOffer = offer.PriceOfTheOffer
            };
            return offerBl;
        }

        public List<OfferBl> MapOfferListToOfferBlList(List<Offer> offers)
        {
            var offersBl = new List<OfferBl>();
            foreach (var offer in offers)
            {
                offersBl.Add(MapOfferToOfferBl(offer));
            }
            return offersBl;
        }

        public List<Sms> MapSmsBlListToSmsList(List<SmsBl> smsBlList)
        {
            var smsList = new List<Sms>();
            foreach(var smsBl in smsBlList)
            {
                smsList.Add(MapSmsBlToSms(smsBl));
            }
            return smsList;
        }

        public Sms MapSmsBlToSms(SmsBl smsBl)
        {
            var sms = new Sms
            {
                Id = smsBl.Id,
                MessageContent = smsBl.MessageContent
            };
            return sms;
        }

        public List<Connection> MapConnectionsBlListToConnectionsList(List<ConnectionBl> connectionBlList)
        {
            var connectionList = new List<Connection>();
            foreach (var connection in connectionBlList)
            {
                connectionList.Add(MapConnectionBlToConnection(connection));
            }
            return connectionList;
        }

        public Connection MapConnectionBlToConnection(ConnectionBl connectionBl)
        {
            var connection = new Connection
            {
                Id = connectionBl.Id,
                DateOfBeginning = connectionBl.DateOfBeginning,
                TimeOfConnectionInSeconds = connectionBl.TimeOfConnectionInSeconds
            };
            return connection;
        }

        public List<SmsBl> MapSmsListToSmsBlList(List<Sms> smsList)
        {
            var smsBlList = new List<SmsBl>();
            foreach (var sms in smsList)
            {
                smsBlList.Add(MapSmsToSmsBl(sms));
            }
            return smsBlList;
        }

        public SmsBl MapSmsToSmsBl(Sms sms)
        {
            var smsBl = new SmsBl
            {
                Id = sms.Id,
                MessageContent = sms.MessageContent,
            };
            return smsBl;
        }

        public List<ConnectionBl> MapConnectionsListToConnectionsBlList(List<Connection> connectionsList)
        {
            var connectionsBlList = new List<ConnectionBl>();
            foreach (var connection in connectionsList)
            {
                connectionsBlList.Add(MapConnectionToConnectionBl(connection));
            }
            return connectionsBlList;
        }

        public ConnectionBl MapConnectionToConnectionBl(Connection connection)
        {
            var connectionBl = new ConnectionBl
            {
                Id = connection.Id,
                DateOfBeginning = connection.DateOfBeginning,
                TimeOfConnectionInSeconds = connection.TimeOfConnectionInSeconds
            };
            return connectionBl;
        }

        public PhoneReport MapPhoneReportBlToPhoneReport(PhoneReportBl phoneReportBl)
        {
            var phoneReport = new PhoneReport
            {
                NameOfTheOffer = phoneReportBl.NameOfTheOffer,
                PriceOfTheOffer = phoneReportBl.PriceOfTheOffer,
                TotlaCostOfConnections = phoneReportBl.TotlaCostOfConnections,
                TotlaCostOfTextMessages = phoneReportBl.TotlaCostOfTextMessages,
                PhoneNumber = phoneReportBl.PhoneNumber
            };
            return phoneReport;
        }

        public PhoneReportBl MapPhoneReportToPhoneReportBl(PhoneReport phoneReport)
        {
            var phoneReportBl = new PhoneReportBl
            {
                NameOfTheOffer = phoneReport.NameOfTheOffer,
                PriceOfTheOffer = phoneReport.PriceOfTheOffer,
                TotlaCostOfConnections = phoneReport.TotlaCostOfConnections,
                TotlaCostOfTextMessages = phoneReport.TotlaCostOfTextMessages,
                PhoneNumber = phoneReport.PhoneNumber
            };
            return phoneReportBl;
        }

        public Invoice MapInvoiceBlToInvoice(InvoiceBl invoiceBl)
        {
            var invoice = new Invoice
            {
                BeginningDate = invoiceBl.BeginningDate,
                GenerationDate = invoiceBl.GenerationDate,
                Number = invoiceBl.Number,
                Subscriber = MapSubscriberBlToSubscriber(invoiceBl.Subscriber),
                TotalCostOfConnections = invoiceBl.TotalCostOfConnections,
                TotalCostOfTextMessages = invoiceBl.TotalCostOfTextMessages,
                TotalOffersCost = invoiceBl.TotalOffersCost,
                TotalCostToBePaid = invoiceBl.TotalCostToBePaid,
                PhoneReports = MapPhoneReportBlListToPhoneReportList(invoiceBl.PhoneReports)             
            };
            return invoice;
        }

        public List<PhoneReport> MapPhoneReportBlListToPhoneReportList(List<PhoneReportBl> phoneReportsBl)
        {
            List<PhoneReport> phoneReports = new List<PhoneReport>();
            phoneReportsBl.ForEach(p => phoneReports.Add(MapPhoneReportBlToPhoneReport(p)));
            return phoneReports;
        }

        public InvoiceBl MapInvoiceToInvoiceBl(Invoice invoice)
        {
            var invoiceBl = new InvoiceBl
            {
                BeginningDate = invoice.BeginningDate,
                GenerationDate = invoice.GenerationDate,
                Number = invoice.Number,
                TotalCostOfConnections = invoice.TotalCostOfConnections,
                TotalCostOfTextMessages = invoice.TotalCostOfTextMessages,
                TotalOffersCost = invoice.TotalOffersCost,
                TotalCostToBePaid = invoice.TotalCostToBePaid
            };

            if(invoice.PhoneReports != null)
            {
                invoiceBl.PhoneReports = MapPhoneReportListToPhoneReportBlList(invoice.PhoneReports);
            }
            if(invoice.Subscriber != null)
            {
                invoiceBl.Subscriber = MapSubscriberToSubscriberBl(invoice.Subscriber);
            }
            return invoiceBl;
        }

        public List<PhoneReportBl> MapPhoneReportListToPhoneReportBlList(List<PhoneReport> phoneReports)
        {
            List<PhoneReportBl> phoneReportsBl = new List<PhoneReportBl>();
            phoneReports.ForEach(p => phoneReportsBl.Add(MapPhoneReportToPhoneReportBl(p)));
            return phoneReportsBl;
        }

        public List<Invoice> MapInvoiceBlListToInvoiceList(List<InvoiceBl> invoicesBl)
        {
            List<Invoice> invoices = new List<Invoice>();
            invoicesBl.ForEach(i => invoices.Add(MapInvoiceBlToInvoice(i)));
            return invoices;
        }

        public List<InvoiceBl> MapInvoiceListToInvoiceBlList(List<Invoice> invoices)
        {
            List<InvoiceBl> invoicesBl = new List<InvoiceBl>();
            invoices.ForEach(i => invoicesBl.Add(MapInvoiceToInvoiceBl(i)));
            return invoicesBl;
        }
    }
}
