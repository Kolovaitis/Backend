using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IService>().To<Service>();
        }
    }
}

