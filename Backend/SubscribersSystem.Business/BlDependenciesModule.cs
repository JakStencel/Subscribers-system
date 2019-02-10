using Ninject;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Business.Services.SupportServices;
using SubscribersSystem.Data;

namespace SubscribersSystem.Business
{
    public class BlDependenciesModule
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<IPhoneSimulatorService>().To<PhoneSimulatorService>();
            kernel.Bind<ICombiningUserOfferService>().To<CombiningUserOfferService>();
            kernel.Bind<ISubscriberService>().To<SubscriberService>().InSingletonScope();
            kernel.Bind<IOfferService>().To<OfferService>().InSingletonScope();
            kernel.Bind<IInvoiceService>().To<InvoiceService>().InSingletonScope();
            kernel.Bind<IPhoneService>().To<PhoneService>().InSingletonScope();
            kernel.Bind<IReportService>().To<ReportService>().InSingletonScope();
            kernel.Bind<IDataObjectMapper>().To<DataObjectMapper>().InSingletonScope();
            kernel.Bind<IConnectionService>().To<ConnectionService>().InSingletonScope();
            kernel.Bind<ISmsService>().To<SmsService>().InSingletonScope();

            DalDependenciesModule.Register(kernel);
        }
    }
}
