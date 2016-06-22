using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loliboo.Service.ProjectService;
using Ninject.Modules;

namespace Loliboo.Service
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectService>().To<ProjectService.ProjectService>();
        }
    }
}
