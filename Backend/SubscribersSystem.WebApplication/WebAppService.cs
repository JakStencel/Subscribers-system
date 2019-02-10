using Microsoft.Owin.Hosting;
using System;
using System.Configuration;

namespace SubscribersSystem.WebApplication
{
    internal class WebAppService
    {
        private IDisposable _api;

        public void Start()
        {
            _api = WebApp.Start<OwinBootstrap>(ConfigurationManager.AppSettings["uri"]);
        }

        public void Stop()
        {
            _api.Dispose();
        }
    }
}
