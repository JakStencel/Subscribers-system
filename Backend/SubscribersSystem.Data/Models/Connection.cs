using System;

namespace SubscribersSystem.Data.Models
{
    public class Connection
    {
        public int Id { get; set; }
        public int TimeOfConnectionInSeconds { get; set; }
        public DateTime DateOfBeginning { get; set; }
        public Phone Phone { get; set; }
    }
}
