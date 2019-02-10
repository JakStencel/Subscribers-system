using SubscribersSystem.Business.Models;
using SubscribersSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Ninject;
using Ninject.Extensions.Factory;

namespace SubscribersSystem.Business.Services
{
    public interface IConnectionService
    {
        void AddConnection(ConnectionBl connectionBl, int pohneId);
    }

    internal class ConnectionService : IConnectionService
    {
        private IDataObjectMapper _dataObjectMapper;
        private Func<ISubscribersSystemDbContext> _dbContextFactory;

        public ConnectionService(Func<ISubscribersSystemDbContext> dbContextFactory, IDataObjectMapper dataObjectMapper)
        {
            _dbContextFactory = dbContextFactory;
            _dataObjectMapper = dataObjectMapper;
        }

        public void AddConnection(ConnectionBl connectionBl, int pohneId)
        {
            var connection = _dataObjectMapper.MapConnectionBlToConnection(connectionBl);

            using (var dbContext = _dbContextFactory())
            {
                var chosenPhone = dbContext.PhoneDbSet.SingleOrDefault(p => p.Id == pohneId);

                connection.Phone = chosenPhone;

                dbContext.ConnectionDbSet.Add(connection);
                dbContext.SaveChanges();
            }
        }
    }
}
