using Ninject.Modules;
using SubscribersSystem.Business;
using SubscribersSystem.Utility;
using SubscribersSystem.Utility.DataCapture;
using SubscribersSystem.Utility.Display;
using SubscribersSystem.Utility.Helpers;
using SubscribersSystem.Utility.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscribersSystem
{
    public class ModuleDI : NinjectModule
    {
        public override void Load()
        {
            Bind<ISubscribersSystemMenu>().To<SubscribersSystemMenu>();
            Bind<IIoHelper>().To<IoHelper>();
            Bind<ISystemDataCaptureService>().To<SystemDataCaptureService>();
            Bind<IOfferDataCapture>().To<OfferDataCapture>();
            Bind<ISubscriberDataCapture>().To<SubscriberDataCapture>();
            Bind<IPhoneDataCapture>().To<PhoneDataCapture>();
            Bind<IInvoiceDataCapture>().To<InvoiceDataCapture>();
            Bind<IDetailsDisplay>().To<DetailsDisplay>();
            Bind<IModelDisplay>().To<ModelDisplay>();
            Bind<ISerializerProvider>().To<SerializerProvider>();
           
        }
    }
}
