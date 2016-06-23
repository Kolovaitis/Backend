using IAmIt.Configuration;
using IAmIt.Database.EntityFramework.DbModule;
using IAmIt.Service;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Modules;

namespace IAmIt
{
    public class WebServerModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(new ConfigurationModule());
            Kernel.Load(new EntityFrameworkModule());
            Kernel.Load(new ServiceModule());
            
            Bind<WebServer>().ToSelf();
        }
    }
}
