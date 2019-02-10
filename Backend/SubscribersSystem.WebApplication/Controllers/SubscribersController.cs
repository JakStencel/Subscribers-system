using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SubscribersSystem.WebApplication.Controllers
{
    [RoutePrefix("api/subscribers")]
    public class SubscribersController : ApiController
    {
        private readonly ISubscriberService _subscriberService;

        public SubscribersController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpGet, Route("getAll")]
        public async Task<HttpResponseMessage> GetAllSubscribers()
        {
            try
            {
                var subscribers = await Task.Run(() => _subscriberService.GetAll());
                return Request.CreateResponse(HttpStatusCode.OK, subscribers);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Error occured: {e.Message}");
            }
        }

        [HttpPost, Route("add")]
        public async Task<HttpResponseMessage> AddSubscriberAsync([FromBody]SubscriberBl subscriber)
        {
            try
            {
                subscriber.BillingCycle = await Task.Run(() => _subscriberService.ChooseLeastCountedBillingCycle());
                var subscriberId = await _subscriberService.AddSubscriberAsync(subscriber);
                return Request.CreateResponse(HttpStatusCode.OK, subscriberId);
            }
            catch(Exception e)
            {
                var message = $"failed adding new subscriber. {e.Message}";
                return Request.CreateResponse(HttpStatusCode.NotFound, message);
            }
        }
    }
}
