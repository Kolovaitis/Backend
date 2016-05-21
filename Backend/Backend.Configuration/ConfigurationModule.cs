using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Configuration
{
    public class ConfigurationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguraiton>().To<Configuration>();
        }
    }
}
