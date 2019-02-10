using SubscribersSystem.Data.ReportModels;
using System;
using System.Collections.Generic;

namespace SubscribersSystem.Data.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime GenerationDate { get; set; }
        public Subscriber Subscriber { get; set; }
        public decimal TotalCostOfConnections { get; set; }
        public decimal TotalCostOfTextMessages { get; set; }
        public decimal TotalOffersCost { get; set; }
        public decimal TotalCostToBePaid { get; set; }

        public List<PhoneReport> PhoneReports { get; set; }
    }
}
