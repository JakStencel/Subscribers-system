using SubscribersSystem.Business.Models;
using SubscribersSystem.Utility.Helpers;

namespace SubscribersSystem.Utility.DataCapture
{
    public interface ISmsDataCapture
    {
        SmsBl Capture();
    }

    public class SmsDataCapture : ISmsDataCapture
    {
        private IIoHelper _ioHelper;

        public SmsDataCapture(IIoHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        public SmsBl Capture()
        {
            var newSMS = new SmsBl
            {
                MessageContent = _ioHelper.GetStringFromUser("Enter the message: ")
            };
            return newSMS;
        }
    }
}
