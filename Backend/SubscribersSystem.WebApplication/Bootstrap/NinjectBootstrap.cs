using Ninject;
using SubscribersSystem.Business;
using SubscribersSystem.Utility;
using SubscribersSystem.Utility.DataCapture;
using SubscribersSystem.Utility.Helpers;
using SubscribersSystem.Utility.MessageSender;
using SubscribersSystem.WebApplication.Controllers;

namespace SubscribersSystem.WebApplication.Bootstrap
{
    internal class NinjectBootstrap
    {
        public IKernel GetKernel()
        {
            var kernel = new StandardKernel();

            RegisterUtilities(kernel);

            BlDependenciesModule.Register(kernel);

            return kernel;
        }

        private void RegisterUtilities(IKernel kernel)
        {
            kernel.Bind<IPhoneDataCapture>().To<PhoneDataCapture>();
            kernel.Bind<IInvoiceDataCapture>().To<InvoiceDataCapture>();
            kernel.Bind<ISerializerProvider>().To<SerializerProvider>();
            kernel.Bind<IMessageSender>().To<Emailer>();
        }
    }
}
