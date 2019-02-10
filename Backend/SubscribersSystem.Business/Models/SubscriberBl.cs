using System;
using System.Collections.Generic;

namespace SubscribersSystem.Business.Models
{
    public class SubscriberBl
    {
        public SubscriberBl()
        {
            Phones = new List<PhoneBl>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public int BillingCycle { get; set; }

        public List<PhoneBl> Phones { get; set; }
    }
}
