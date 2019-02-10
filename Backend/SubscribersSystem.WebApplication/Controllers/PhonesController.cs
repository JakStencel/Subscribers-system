using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Business.Services.SupportServices;
using SubscribersSystem.Utility.DataCapture;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SubscribersSystem.WebApplication.Controllers
{
    [RoutePrefix("api/phones")]
    public class PhonesController : ApiController
    {
        private readonly IPhoneService _phoneService;
        private readonly ICombiningUserOfferService _combiningUserOfferService;
        private readonly IPhoneDataCapture _phoneDataCapture;

        public PhonesController(IPhoneService phoneService, 
                                ICombiningUserOfferService combiningUserOfferService,
                                IPhoneDataCapture phoneDataCapture)
        {
            _phoneService = phoneService;
            _combiningUserOfferService = combiningUserOfferService;
            _phoneDataCapture = phoneDataCapture;
        }

        [HttpGet, Route("assignOfferToSubscriber/{subscriberId}/{offerId}")]
        public async Task<HttpResponseMessage> AssignOfferToTheSubscriber([FromUri]int subscriberId, [FromUri]int offerId)
        {
            try
            {
                Task<SubscriberBl> chosenSubscriber = _combiningUserOfferService.GetSubscriberAsync(subscriberId);
                Task<OfferBl> chosenOffer = _combiningUserOfferService.GetOfferAsync(offerId);

                await Task.WhenAll(chosenSubscriber, chosenOffer);

                var generatedNumber = _phoneService.GeneratePhoneNumber();

                var newPhone = _phoneDataCapture.Capture(chosenOffer.Result, generatedNumber);

                var phoneId =  await _phoneService.AddPhoneAsync(subscriberId, offerId, newPhone);
                return Request.CreateResponse(HttpStatusCode.OK, phoneId);
            }
            catch(Exception e)
            {
                var message = $"Assigning offer to the subscriber failed. {e.Message}";
                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }

        }
    }
}
