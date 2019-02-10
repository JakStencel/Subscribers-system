using System;
using System.Collections.Generic;

namespace SubscribersSystem.Data.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public int BillingCycle { get; set; }

        public List<Phone> Phones { get; set; }
    }
}
