using Ninject;
using SubscribersSystem.Business;
using SubscribersSystem.Utility;
using SubscribersSystem.Utility.DataCapture;
using SubscribersSystem.Utility.Display;
using SubscribersSystem.Utility.Helpers;
using SubscribersSystem.Utility.Menus;
using SubscribersSystem.Utility.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscribersSystem
{
    public class CliDependenciesModule
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<ISubscribersSystemMenu>().To<SubscribersSystemMenu>();
            kernel.Bind<IIoHelper>().To<IoHelper>();
            kernel.Bind<ISystemDataCaptureService>().To<SystemDataCaptureService>();
            kernel.Bind<IOfferDataCapture>().To<OfferDataCapture>();
            kernel.Bind<ISubscriberDataCapture>().To<SubscriberDataCapture>();
            kernel.Bind<IPhoneDataCapture>().To<PhoneDataCapture>();
            kernel.Bind<IInvoiceDataCapture>().To<InvoiceDataCapture>();
            kernel.Bind<IDetailsDisplay>().To<DetailsDisplay>();
            kernel.Bind<IModelDisplay>().To<ModelDisplay>();
            kernel.Bind<ISerializerProvider>().To<SerializerProvider>();

            BlDependenciesModule.Register(kernel);
        }
    }
}
