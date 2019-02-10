using SubscribersSystem.Business.Models;
using SubscribersSystem.Data;
using System;
using System.Linq;

namespace SubscribersSystem.Business.Services
{
    public interface ISmsService
    {
        void AddSms(SmsBl smsBl, int phoneId);
    }

    internal class SmsService : ISmsService
    {
        private IDataObjectMapper _dataObjectMapper;
        private Func<ISubscribersSystemDbContext> _dbContextFactory;

        public SmsService(Func<ISubscribersSystemDbContext> dbContextFactory, IDataObjectMapper dataObjectMapper)
        {
            _dbContextFactory = dbContextFactory;
            _dataObjectMapper = dataObjectMapper;
        }

        public void AddSms(SmsBl smsBl, int phoneId)
        {
            var sms = _dataObjectMapper.MapSmsBlToSms(smsBl);

            using (var dbContext = _dbContextFactory())
            {
                var chosenPhone = dbContext.PhoneDbSet.SingleOrDefault(p => p.Id == phoneId);

                sms.Phone = chosenPhone;

                dbContext.SmsDbSet.Add(sms);
                dbContext.SaveChanges();
            }
        }
    }
}
