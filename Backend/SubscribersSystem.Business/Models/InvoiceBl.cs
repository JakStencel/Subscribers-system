using SubscribersSystem.Business.ReportModels;
using System;
using System.Collections.Generic;

namespace SubscribersSystem.Business.Models
{
    public class InvoiceBl
    {
        public InvoiceBl()
        {
            PhoneReports = new List<PhoneReportBl>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime GenerationDate { get; set; }
        public SubscriberBl Subscriber { get; set; }
        public decimal TotalCostOfConnections { get; set; }
        public decimal TotalCostOfTextMessages { get; set; }
        public decimal TotalOffersCost { get; set; }
        public decimal TotalCostToBePaid { get; set; }

        public List<PhoneReportBl> PhoneReports { get; set; }
    }
}
