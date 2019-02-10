namespace SubscribersSystem.Data.Models
{
    public class Sms
    {
        public int Id { get; set; }
        public string MessageContent { get; set; }
        public Phone Phone { get; set; }
    }
}
