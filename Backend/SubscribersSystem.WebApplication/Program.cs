using System;
using Topshelf;

namespace SubscribersSystem.WebApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(serviceConfig =>
                        {
                            serviceConfig.Service<WebAppService>(serviceInstance =>
                            {
                                serviceInstance.ConstructUsing(() => new WebAppService());
                                serviceInstance.WhenStarted(execute => execute.Start());
                                serviceInstance.WhenStopped(execute => execute.Stop());
                            });
                            serviceConfig.RunAsLocalSystem();
                            serviceConfig.SetServiceName("SubscribersSystemAPI");
                            serviceConfig.SetDisplayName("Subscribers System API");
                            serviceConfig.SetDescription("API for subscribersSystem manager");
                            serviceConfig.StartAutomatically();
                        });
            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
