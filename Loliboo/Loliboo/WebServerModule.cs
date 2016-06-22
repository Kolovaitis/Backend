using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loliboo.Configuration;
using Loliboo.Database;
using Loliboo.Service;
using Ninject;
using Ninject.Modules;

namespace Loliboo
{
    public class WebServerModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(new ConfigurationModule());
            Kernel.Load(new DatabaseModule());
            Kernel.Load(new ServiceModule());

            Bind<WebServer>().ToSelf();
        }
    }
}
