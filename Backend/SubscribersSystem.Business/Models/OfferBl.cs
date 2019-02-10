namespace SubscribersSystem.Business.Models
{
    public class OfferBl
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BundleOfMinutes { get; set; }
        public int BundleOfTextMessages { get; set; }
        public decimal PricePerMinute { get; set; }
        public decimal PricePerTextMessage { get; set; }
        public decimal PriceOfTheOffer { get; set; }
    }
}
