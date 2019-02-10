using SubscribersSystem.Business.Models;
using SubscribersSystem.Data;
using SubscribersSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubscribersSystem.Business.Services
{
    public interface ISubscriberService
    {
        Task<int> AddSubscriberAsync(SubscriberBl subscriberBl);
        List<SubscriberBl> GetAll();
        Task<SubscriberBl> GetSubscriberAsync(int indexOfSubscriber);
        int ChooseLeastCountedBillingCycle();
        bool CheckIfSubscriberHasPhoneNumbers(int indexOfSubscriber);
        string CheckEmail(string emailFromUser);
        Task<string> GetSubscribersEmailAsync(int indexOfSubscriber);
    }

    internal class SubscriberService : ISubscriberService
    {
        private Func<ISubscribersSystemDbContext> _dbContextFactory;
        private IDataObjectMapper _dataObjectMapper;
        private const int _billingCycle1st = 1;
        private const int _billingCycle10th = 10;
        private const int _billingCycle20th = 20;

        public SubscriberService(Func<ISubscribersSystemDbContext> dbContextFactory, IDataObjectMapper dataObjectMapper)
        {
            _dbContextFactory = dbContextFactory;
            _dataObjectMapper = dataObjectMapper;
        }

        public async Task<int> AddSubscriberAsync(SubscriberBl subscriberBl)
        {
            var subscriber = _dataObjectMapper.MapSubscriberBlToSubscriber(subscriberBl);

            using (var dbContext = _dbContextFactory())
            {
                if (await dbContext.SubscriberDbSet.AnyAsync(s => s.Surname == subscriber.Surname && s.DateOfBirth == subscriber.DateOfBirth))
                {
                    throw new Exception("There is already a subscriber with given data");
                }

                await Task.Run(() =>
                                {
                                    dbContext.SubscriberDbSet.Add(subscriber);
                                    dbContext.SaveChanges();
                                });

                return subscriber.Id;
            }
        }

        public List<SubscriberBl> GetAll()
        {
            List<Subscriber> subscribers;
            using (var dbContext = _dbContextFactory())
            {
                subscribers = dbContext.SubscriberDbSet
                    .Include(s => s.Phones)
                    .Include(s => s.Phones.Select(p => p.Offer))
                    .Include(s => s.Phones.Select(p => p.Subscriber))
                    .Include(s => s.Phones.Select(p => p.Connections))
                    .Include(s => s.Phones.Select(p => p.ShortTextMessages))
                    .ToList();
            }

            return _dataObjectMapper.MapSubscribersListToSubscribersBlList(subscribers);
        }

        public async Task<SubscriberBl> GetSubscriberAsync(int indexOfSubscriber)
        {
            Subscriber subscriber;
            using (var dbContext = _dbContextFactory())
            {
                if (!(await dbContext.SubscriberDbSet.AnyAsync(s => s.Id == indexOfSubscriber)))
                {
                    throw new Exception("There is no subscriber with provided Id!");
                }
                subscriber = await dbContext.SubscriberDbSet
                    .Include(s => s.Phones)
                    .Include(s => s.Phones.Select(p => p.Offer))
                    .Include(s => s.Phones.Select(p => p.Connections))
                    .Include(s => s.Phones.Select(p => p.ShortTextMessages))
                    .SingleOrDefaultAsync(s => s.Id == indexOfSubscriber);
            }

            return _dataObjectMapper.MapSubscriberToSubscriberBl(subscriber);
        }

        public async Task<string> GetSubscribersEmailAsync(int indexOfSubscriber)
        {
            string subscribersEmail;
            using (var dbContext = _dbContextFactory())
            {
                subscribersEmail = await dbContext.SubscriberDbSet
                                                .Where(s => s.Id == indexOfSubscriber)
                                                .Select(s => s.Email).SingleOrDefaultAsync();
            }
            return subscribersEmail;
        }

        public bool CheckIfSubscriberHasPhoneNumbers(int indexOfSubscriber)
        {
            using (var dbContext = _dbContextFactory())
            {
                if (dbContext.SubscriberDbSet.Where(s => s.Id == indexOfSubscriber).SelectMany(s => s.Phones).Count() == 0)
                {
                    throw new Exception("Invoice generation impossible due to the lack of phone numbers of the selected subscriber");
                }
            }
            return true;
        }

        public string CheckEmail(string emailFromUser)
        {
            var regex = new Regex(@"\A(?<username>[a-zA-Z0-9][a-zA-Z0-9._-]*)@(?<domain>[a-zA-Z0-9_-]+){1}(?<region>\.[a-zA-Z0-9]{2,3})(?<optional>\.[a-zA-Z0-9]{2,3})?\Z");

            if (!regex.IsMatch(emailFromUser))
            {
                throw new ArgumentException("Provided mail is incorrect");
            }

            return emailFromUser;
        }

        public int ChooseLeastCountedBillingCycle()
        {
            int chosenBillingCycle;
            int NumberOfBillCycle1st;
            int NumberOfBillCycle10st;
            int NumberOfBillCycle20st;

            using (var dbContext = _dbContextFactory())
            {
                NumberOfBillCycle1st = dbContext.SubscriberDbSet.Where(s => s.BillingCycle == _billingCycle1st).Count();
                NumberOfBillCycle10st = dbContext.SubscriberDbSet.Where(s => s.BillingCycle == _billingCycle10th).Count();
                NumberOfBillCycle20st = dbContext.SubscriberDbSet.Where(s => s.BillingCycle == _billingCycle20th).Count();
            }

            chosenBillingCycle = NumberOfBillCycle1st <= NumberOfBillCycle10st && NumberOfBillCycle1st <= NumberOfBillCycle20st
                                 ? _billingCycle1st : NumberOfBillCycle10st < NumberOfBillCycle1st && NumberOfBillCycle10st <= NumberOfBillCycle20st
                                 ? _billingCycle10th : _billingCycle20th;

            return chosenBillingCycle;
        }
    }
}
