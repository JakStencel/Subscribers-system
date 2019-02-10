using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Utility.Helpers;

namespace SubscribersSystem.Utility.DataCapture
{
    public interface IOfferDataCapture
    {
        OfferBl Capture();
    }

    public class OfferDataCapture : IOfferDataCapture
    {
        private IIoHelper _ioHelper;
        private IOfferService _offerService;

        public OfferDataCapture(IIoHelper ioHelper, IOfferService offerService)
        {
            _ioHelper = ioHelper;
            _offerService = offerService;
        }

        public OfferBl Capture()
        {
            var newOffer = new OfferBl
            {
                Name = _ioHelper.GetStringFromUser("Enter the name of the offer: "),
                BundleOfMinutes = _ioHelper.GetIntFromUser("Enter the bundle of minutes according to the offer: "),
                BundleOfTextMessages = _ioHelper.GetIntFromUser("Enter the bundle of short text messages according to the offer: "),
                PricePerMinute = _ioHelper.GetDecimalFromUser("Enter the price per minute outside of the bundle: "),
                PricePerTextMessage = _ioHelper.GetDecimalFromUser("Enter the price per text message outside of the bundle: "),
                PriceOfTheOffer = _ioHelper.GetDecimalFromUser("Enter the price of declared offer: ")
            };

            return newOffer;
        }
    }
}
