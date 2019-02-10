using SubscribersSystem.Business.Models;
using System.Threading.Tasks;

namespace SubscribersSystem.Business.Services.SupportServices
{
    public interface IPhoneSimulatorService
    {
        Task AddGeneratedPhoneAsync(int indexOfSubscriber, int indexOfOffer, PhoneBl phoneBl);
        void AddConnection(ConnectionBl connectionBl, int phoneId);
        void AddCostOfConnectionToTotalCost(PhoneBl phone, int secondsLeftInBundle);
        void AddCostOfTextMessageToTotalCost(PhoneBl phone);
        void AddSms(SmsBl smsBl, int phoneId);
        bool CheckIfBundleOfMinutesExceeded(int secondsLeftInBundle);
        bool CheckIfBundleOfSMSExceeded(PhoneBl phone);
        int DecreaseBundleOfMinutes(PhoneBl phone, ConnectionBl connection);
        int DecreaseNumberOfSmsInBundle(PhoneBl phone);
        int GeneratePhoneNumber();
        PhoneBl GetPhoneById(int phoneIndex);
        PhoneBl GetPhoneByNumber(int phoneNumber);
        int GetPhoneNumberFromAppConfigFile();
        decimal GetPriceOfTheConnection(PhoneBl phone, int secondsOfConnection);
        Task UpdateBundlesAsync(int indexOfSubscriber);
        void UpdatePhone(PhoneBl phoneBl);
    }

    public class PhoneSimulatorService : IPhoneSimulatorService
    {
        private readonly IConnectionService _connectionService;
        private readonly ISmsService _smsService;
        private readonly IPhoneService _phoneService;

        public PhoneSimulatorService(IConnectionService connectionService, ISmsService smsService, IPhoneService phoneService)
        {
            _connectionService = connectionService;
            _smsService = smsService;
            _phoneService = phoneService;
        }

        public async Task AddGeneratedPhoneAsync(int indexOfSubscriber, int indexOfOffer, PhoneBl phoneBl)
        {
            await _phoneService.AddPhoneAsync(indexOfSubscriber, indexOfOffer, phoneBl);
        }

        public void AddConnection(ConnectionBl connectionBl, int phoneId)
        {
            _connectionService.AddConnection(connectionBl, phoneId);
        }

        public void AddSms(SmsBl smsBl, int phoneId)
        {
            _smsService.AddSms(smsBl, phoneId);
        }

        public int GeneratePhoneNumber()
        {
            return _phoneService.GeneratePhoneNumber();
        }

        public async Task UpdateBundlesAsync(int indexOfSubscriber)
        {
            await _phoneService.UpdateBundlesAsync(indexOfSubscriber);
        }

        public int GetPhoneNumberFromAppConfigFile()
        {
            return _phoneService.GetPhoneNumberFromAppConfigFile();
        }

        public PhoneBl GetPhoneByNumber(int phoneNumber)
        {
            return _phoneService.GetPhoneByNumber(phoneNumber);
        }

        public PhoneBl GetPhoneById(int phoneIndex)
        {
            return _phoneService.GetPhoneById(phoneIndex);
        }

        public int DecreaseBundleOfMinutes(PhoneBl phone, ConnectionBl connection)
        {
            return _phoneService.DecreaseBundleOfMinutes(phone, connection);
        }

        public bool CheckIfBundleOfMinutesExceeded(int secondsLeftInBundle)
        {
            return _phoneService.CheckIfBundleOfMinutesExceeded(secondsLeftInBundle);
        }

        public decimal GetPriceOfTheConnection(PhoneBl phone, int secondsOfConnection)
        {
            return _phoneService.GetPriceOfTheConnection(phone, secondsOfConnection);
        }

        public void AddCostOfConnectionToTotalCost(PhoneBl phone, int secondsLeftInBundle)
        {
            _phoneService.AddCostOfConnectionToTotalCost(phone, secondsLeftInBundle);
        }

        public void UpdatePhone(PhoneBl phoneBl)
        {
            _phoneService.UpdatePhone(phoneBl);
        }

        public int DecreaseNumberOfSmsInBundle(PhoneBl phone)
        {
            return _phoneService.DecreaseNumberOfSmsInBundle(phone);
        }

        public bool CheckIfBundleOfSMSExceeded(PhoneBl phone)
        {
            return _phoneService.CheckIfBundleOfSMSExceeded(phone);
        }

        public void AddCostOfTextMessageToTotalCost(PhoneBl phone)
        {
            _phoneService.AddCostOfTextMessageToTotalCost(phone);
        }

    }
}
