using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Utility.Helpers;
using System.Threading.Tasks;

namespace SubscribersSystem.Utility.DataCapture
{
    public interface ISubscriberDataCapture
    {
        SubscriberBl Capture();
    }

    public class SubscriberDataCapture : ISubscriberDataCapture
    {
        private IIoHelper _ioHelper;
        private ISubscriberService _subscriberService;

        public SubscriberDataCapture(ISubscriberService subscriberService, IIoHelper ioHelper)
        {
            _subscriberService = subscriberService;
            _ioHelper = ioHelper;
        }

        public SubscriberBl Capture()
        {
            var newSubscriber = new SubscriberBl
            {
                Name = _ioHelper.GetStringFromUser("Enter the name of the subscriber: "),
                Surname = _ioHelper.GetStringFromUser("Enter the surname of the subscriber: "),
                DateOfBirth = _ioHelper.GetDateOfBirthFromUser("Enter the birth date of the subscriber in the following format: 'dd-mm-yyyy': "),
                Email = _subscriberService.CheckEmail(_ioHelper.GetStringFromUser("Enter the email (e.g. xxx@yyy.zzz): ")),
                BillingCycle = _subscriberService.ChooseLeastCountedBillingCycle()
            };

            return newSubscriber;
        }
    }
}
