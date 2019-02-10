using SubscribersSystem.Business.Models;
using SubscribersSystem.Data;
using SubscribersSystem.Data.Models;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribersSystem.Business.Services
{
    public interface IPhoneService
    {
        Task<int> AddPhoneAsync(int indexOfSubscriber, int indexOfOffer, PhoneBl phoneBl);
        bool CheckIfBundleOfMinutesExceeded(int secondsLeftInBundle);
        bool CheckIfBundleOfSMSExceeded(PhoneBl phone);
        bool CheckIfNumberExist(int phoneNumber);
        int DecreaseBundleOfMinutes(PhoneBl phone, ConnectionBl connection);
        int DecreaseNumberOfSmsInBundle(PhoneBl phone);
        int GeneratePhoneNumber();
        PhoneBl GetPhoneById(int phoneIndex);
        PhoneBl GetPhoneByNumber(int phoneNumber);
        decimal GetPriceOfTheConnection(PhoneBl phone, int secondsOfConnection);
        void AddCostOfConnectionToTotalCost(PhoneBl phone, int secondsLeftInBundle);
        void AddCostOfTextMessageToTotalCost(PhoneBl phone);
        void UpdatePhone(PhoneBl phoneBl);
        int GetPhoneNumberFromAppConfigFile();
        Task UpdateBundlesAsync(int indexOfSubscriber);
    }

    public class PhoneService : IPhoneService
    {
        private IDataObjectMapper _dataObjectMapper;
        private Func<ISubscribersSystemDbContext> _dbContextFactory;
        private const int _numberOfSecInMinute = 60;
        private const decimal resetValue = 0;
        private const int bottomPhoneNumberRange = 750000000;
        private const int upperPhoneNumberRange = 800000000;

        public PhoneService(Func<ISubscribersSystemDbContext> dbContextFactory, IDataObjectMapper dataObjectMapper)
        {
            _dbContextFactory = dbContextFactory;
            _dataObjectMapper = dataObjectMapper;
        }

        public async Task<int> AddPhoneAsync(int indexOfSubscriber, int indexOfOffer, PhoneBl phoneBl)
        {
            var phone = _dataObjectMapper.MapPhoneBlToPhone(phoneBl);

            using (var dbContext = _dbContextFactory())
            {
                var chosenSubscriber = await dbContext.SubscriberDbSet
                    .Include(s => s.Phones)
                    .SingleOrDefaultAsync(s => s.Id == indexOfSubscriber);

                var chosenOffer = await dbContext.OfferDbSet.
                    SingleOrDefaultAsync(o => o.Id == indexOfOffer);

                phone.Subscriber = chosenSubscriber;
                phone.Offer = chosenOffer;

                await Task.Run(() =>
                        {
                            dbContext.PhoneDbSet.Add(phone);
                            dbContext.SaveChanges();
                        });

                return phone.Id;
            }
        }

        public void UpdatePhone(PhoneBl phoneBl)
        {
            var phone = _dataObjectMapper.MapPhoneBlToPhone(phoneBl);

            using (var dbContext = _dbContextFactory())
            {
                dbContext.PhoneDbSet.Attach(phone);
                dbContext.Entry(phone).State = EntityState.Modified;

                dbContext.SaveChanges();
            }
        }

        public PhoneBl GetPhoneByNumber(int phoneNumber)
        {
            Phone phone;
            using (var dbContext = _dbContextFactory())
            {
                if (!dbContext.PhoneDbSet.Any(p => p.PhoneNumber == phoneNumber))
                {
                    return null;
                }
                phone = dbContext.PhoneDbSet
                    .Include(p => p.Connections)
                    .Include(p => p.Offer)
                    .Include(p => p.ShortTextMessages)
                    .Include(p => p.Subscriber)
                    .SingleOrDefault(p => p.PhoneNumber == phoneNumber);
            }
            return _dataObjectMapper.MapPhoneToPhoneBl(phone);
        }

        public PhoneBl GetPhoneById(int phoneIndex)
        {
            Phone phone;
            using (var dbContext = _dbContextFactory())
            {
                if (!dbContext.PhoneDbSet.Any(p => p.Id == phoneIndex))
                {
                    return null;
                }
                phone = dbContext.PhoneDbSet
                    .Include(p => p.Connections)
                    .Include(p => p.Offer)
                    .Include(p => p.ShortTextMessages)
                    .Include(p => p.Subscriber)
                    .SingleOrDefault(p => p.Id == phoneIndex);
            }
            return _dataObjectMapper.MapPhoneToPhoneBl(phone);
        }

        public async Task UpdateBundlesAsync(int indexOfSubscriber)
        {
            using (var dbContext = _dbContextFactory())
            {
                await Task.Run(() =>
                        {
                            dbContext.SubscriberDbSet
                              .Include(s => s.Phones)
                              .Include(s => s.Phones.Select(p => p.Offer))
                              .SingleOrDefault(s => s.Id == indexOfSubscriber)
                              .Phones.ForEach(p =>
                                  {
                                      p.SecondsLeftInBundle = p.Offer.BundleOfMinutes * _numberOfSecInMinute;
                                      p.TextMessagesLeftInBundle = p.Offer.BundleOfTextMessages;
                                      p.CostOfConnectionsOutsideBundle = resetValue;
                                      p.CostOfMessagesOutsideBundle = resetValue;
                                  });

                            dbContext.SaveChanges();
                        });
            }
        }

        public bool CheckIfNumberExist(int phoneNumber)
        {
            using (var dbContext = _dbContextFactory())
            {
                if (dbContext.PhoneDbSet.Any(p => p.PhoneNumber == phoneNumber))
                {
                    return true;
                }
            }
            return false;
        }

        public int GeneratePhoneNumber()
        {
            int phoneNumber = new Random().Next(bottomPhoneNumberRange, upperPhoneNumberRange);

            using (var dbContext = _dbContextFactory())
            {
                while (dbContext.PhoneDbSet.Any(p => p.PhoneNumber == phoneNumber))
                {
                    phoneNumber = new Random().Next(bottomPhoneNumberRange, upperPhoneNumberRange);
                };
            }
            return phoneNumber;
        }

        public int GetPhoneNumberFromAppConfigFile()
        {
            return Int32.Parse(ConfigurationManager.AppSettings["PhoneNumber"]);
        }

        public decimal GetPriceOfTheConnection(PhoneBl phone, int secondsOfConnection)
        {
            return Math.Ceiling((phone.Offer.PricePerMinute / _numberOfSecInMinute) * Math.Abs(secondsOfConnection) * 100) / 100;
        }

        public void AddCostOfConnectionToTotalCost(PhoneBl phone, int secondsLeftInBundle)
        {
            if (CheckIfBundleOfMinutesExceeded(secondsLeftInBundle))
            {
                phone.CostOfConnectionsOutsideBundle += GetPriceOfTheConnection(phone, secondsLeftInBundle);
                phone.SecondsLeftInBundle = (int)resetValue;
            }
        }

        public void AddCostOfTextMessageToTotalCost(PhoneBl phone)
        {
            if (CheckIfBundleOfSMSExceeded(phone))
            {
                phone.CostOfMessagesOutsideBundle += phone.Offer.PricePerTextMessage;
                phone.TextMessagesLeftInBundle = (int)resetValue;
            }
        }

        public bool CheckIfBundleOfMinutesExceeded(int secondsLeftInBundle)
        {
            if (secondsLeftInBundle <= 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfBundleOfSMSExceeded(PhoneBl phone)
        {
            if (phone.TextMessagesLeftInBundle < 0)
            {
                return true;
            }
            return false;
        }

        public int DecreaseBundleOfMinutes(PhoneBl phone, ConnectionBl connection)
        {
            phone.SecondsLeftInBundle -= connection.TimeOfConnectionInSeconds;
            return phone.SecondsLeftInBundle;
        }

        public int DecreaseNumberOfSmsInBundle(PhoneBl phone)
        {
            return phone.TextMessagesLeftInBundle--;
        }
    }
}
