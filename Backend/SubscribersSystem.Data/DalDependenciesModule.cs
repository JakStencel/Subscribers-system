using Ninject;
using Ninject.Extensions.Factory;
using System;

namespace SubscribersSystem.Data
{
    public class DalDependenciesModule
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<ISubscribersSystemDbContext>().ToMethod(context => new SubscribersSystemDbContext());
        }
    }
}
