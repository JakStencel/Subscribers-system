using System.Collections.Generic;

namespace SubscribersSystem.Data.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public int PhoneNumber { get; set; }
        public int SecondsLeftInBundle { get; set; }
        public int TextMessagesLeftInBundle { get; set; }
        public decimal CostOfConnectionsOutsideBundle { get; set; }
        public decimal CostOfMessagesOutsideBundle { get; set; }
        public Offer Offer { get; set; }

        public List<Connection> Connections { get; set; }
        public List<Sms> ShortTextMessages { get; set; }

        public Subscriber Subscriber { get; set; }
    }
}
