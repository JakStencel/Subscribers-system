using SubscribersSystem.Business.Models;

namespace SubscribersSystem.Utility.DataCapture
{
    public interface IPhoneDataCapture
    {
        PhoneBl Capture(OfferBl chosenOffer, int generatedNumber);
    }

    public class PhoneDataCapture : IPhoneDataCapture
    {
        private const int _secondsInMinute = 60;

        public PhoneBl Capture(OfferBl chosenOffer, int generatedNumber)
        {
            var newPhone = new PhoneBl
            {
                Offer = chosenOffer,
                PhoneNumber = generatedNumber,
                SecondsLeftInBundle = chosenOffer.BundleOfMinutes * _secondsInMinute,
                TextMessagesLeftInBundle = chosenOffer.BundleOfTextMessages
            };
            return newPhone;
        }
    }
}
