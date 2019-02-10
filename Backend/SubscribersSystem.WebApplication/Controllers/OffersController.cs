using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SubscribersSystem.WebApplication.Controllers
{
    [RoutePrefix("api/offers")]
    public class OffersController : ApiController
    {
        private readonly IOfferService _offerService;

        public OffersController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet,Route("getAll")]
        public async Task<HttpResponseMessage> GetAllOffers()
        {
            try
            {
                var offers = await Task.Run(() => _offerService.GetAll());
                return Request.CreateResponse(HttpStatusCode.OK, offers);
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Error occured: {e.Message}");
            }
        }


        [HttpPost, Route("add")]
        public async Task<HttpResponseMessage> AddAnOfferAsync([FromBody]OfferBl offer)
        {
            try
            {
                var offerId = await _offerService.AddAnOfferAsync(offer);
                return Request.CreateResponse(HttpStatusCode.OK, offerId);
            }
            catch(Exception e)
            {
                var message = $"Adding new offer failde. {e.Message}";
                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }
        }
    }
}
