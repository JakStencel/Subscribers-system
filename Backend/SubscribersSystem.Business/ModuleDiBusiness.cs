using Ninject.Modules;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Business.Services.SupportServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscribersSystem.Business
{
    public class ModuleDiBusiness : NinjectModule
    {
        public override void Load()
        {
            Bind<IPhoneSimulatorService>().To<PhoneSimulatorService>();
            Bind<ICombiningUserOfferService>().To<CombiningUserOfferService>();
            Bind<ISubscriberService>().To<SubscriberService>().InSingletonScope();
            Bind<IOfferService>().To<OfferService>().InSingletonScope();
            Bind<IInvoiceService>().To<InvoiceService>().InSingletonScope();
            Bind<IPhoneService>().To<PhoneService>().InSingletonScope();
            Bind<IReportService>().To<ReportService>().InSingletonScope();
            Bind<IDataObjectMapper>().To<DataObjectMapper>().InSingletonScope();
            Bind<IConnectionService>().To<ConnectionService>().InSingletonScope();
            Bind<ISmsService>().To<SmsService>().InSingletonScope();
        }
    }
}
