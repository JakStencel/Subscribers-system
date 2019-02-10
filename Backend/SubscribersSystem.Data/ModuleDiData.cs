using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscribersSystem.Data
{
    public class ModuleDiData : NinjectModule
    {
        public override void Load()
        {
            Bind<ISubscribersSystemDbContext>().ToMethod(context => new SubscribersSystemDbContext());
        }
    }
}
