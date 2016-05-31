using Backend.Configuration;
using Backend.Database;
using Backend.Service;
using Microsoft.Owin.Security.OAuth;
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
            Kernel.Load(new DatabaseModule());
            Kernel.Load(new ServiceModule());
            
            Bind<WebServer>().ToSelf();
        }
    }
}
