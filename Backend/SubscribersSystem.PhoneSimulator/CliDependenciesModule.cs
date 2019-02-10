using Ninject;
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

namespace SubscribersSystem.PhoneSimulator
{
    public class CliDependenciesModule
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<IIoHelper>().To<IoHelper>();
            kernel.Bind<ISimulatorDataCaptureService>().To<SimulatorDataCaptureService>();
            kernel.Bind<IPhoneSimulatorMenu>().To<PhoneSimulatorMenu>();
            kernel.Bind<IDetailsDisplay>().To<DetailsDisplay>();
            kernel.Bind<ISmsDataCapture>().To<SmsDataCapture>();
            kernel.Bind<IConnectionDataCapture>().To<ConnectionDataCapture>();
            kernel.Bind<IModelDisplay>().To<ModelDisplay>();

            BlDependenciesModule.Register(kernel);
        }
    }
}
