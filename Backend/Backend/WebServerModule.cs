using Backend.Configuration;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class WebServerModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(new ConfigurationModule());
            Bind<WebServer>().ToSelf();
        }
    }
}
