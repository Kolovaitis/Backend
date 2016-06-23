using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.Service.ProjectService;

namespace IAmIt.Service
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectService>().To<ProjectService.ProjectService>();
        }
    }
}
