using System.Collections.Generic;

namespace SubscribersSystem.Business.Models
{
    public class PhoneBl
    {
        public PhoneBl()
        {
            Connections = new List<ConnectionBl>();
            ShortTextMessages = new List<SmsBl>();
        }

        public int Id { get; set; }
        public int PhoneNumber { get; set; }
        public int SecondsLeftInBundle { get; set; }
        public int TextMessagesLeftInBundle { get; set; }
        public decimal CostOfConnectionsOutsideBundle { get; set; }
        public decimal CostOfMessagesOutsideBundle { get; set; }
        public OfferBl Offer { get; set; }

        public List<ConnectionBl> Connections { get; set; }
        public List<SmsBl> ShortTextMessages { get; set; }
    }
}
