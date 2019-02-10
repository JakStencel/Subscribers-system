﻿namespace SubscribersSystem.Business.ReportModels
{
    public class PhoneReportBl
    {
        public int Id { get; set; }
        public int PhoneNumber { get; set; }
        public string NameOfTheOffer { get; set; }
        public decimal PriceOfTheOffer { get; set; }
        public decimal TotlaCostOfConnections { get; set; }
        public decimal TotlaCostOfTextMessages { get; set; }
    }
}
