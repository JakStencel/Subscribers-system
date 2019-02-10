using SubscribersSystem.Business.Models;
using SubscribersSystem.Data;
using SubscribersSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscribersSystem.Business.Services
{
    public interface IOfferService
    {
        Task<int> AddAnOfferAsync(OfferBl offerBl);
        List<OfferBl> GetAll();
        Task<OfferBl> GetOfferAsync(int indexOfTheOffer);
    }

    internal class OfferService : IOfferService
    {
        private Func<ISubscribersSystemDbContext> _dbContextFactory;
        private IDataObjectMapper _dataObjectMapper;

        public OfferService(Func<ISubscribersSystemDbContext> dbContextFactory, IDataObjectMapper dataObjectMapper)
        {
            _dbContextFactory = dbContextFactory;
            _dataObjectMapper = dataObjectMapper;
        }

        public async Task<int> AddAnOfferAsync(OfferBl offerBl)
        {
            var offer = _dataObjectMapper.MapOfferBlToOffer(offerBl);

            using (var dbContext = _dbContextFactory())
            {
                if (await dbContext.OfferDbSet.AnyAsync(o => o.Name == offer.Name))
                {
                    throw new Exception("There is already an offer with given name");
                }

                await Task.Run(() => 
                    {
                        dbContext.OfferDbSet.Add(offer);
                        dbContext.SaveChanges();
                    });
                return offer.Id;
            }
        }

        public List<OfferBl> GetAll()
        {
            List<Offer> offers;
            using (var dbContext = _dbContextFactory())
            {
                offers = dbContext.OfferDbSet.ToList();
            }

            return _dataObjectMapper.MapOfferListToOfferBlList(offers);
        }

        public async Task<OfferBl> GetOfferAsync(int indexOfTheOffer)
        {
            Offer offer;
            using (var dbContext = _dbContextFactory())
            {
                if(!dbContext.OfferDbSet.Any(o => o.Id == indexOfTheOffer))
                {
                    throw new Exception("There is no offer with provided index!");
                }
                offer = await Task.Run(() => dbContext.OfferDbSet.SingleOrDefault(o => o.Id == indexOfTheOffer));
            }
            return _dataObjectMapper.MapOfferToOfferBl(offer);
        }
    }
}
