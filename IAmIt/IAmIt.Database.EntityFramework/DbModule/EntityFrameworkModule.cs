using IAmIt.Database.EntityFramework.DbContext;
using IAmIt.Database.EntityFramework.DbRepository;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Database.EntityFramework.DbModule
{
    public class EntityFrameworkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbContextFactory<MongoDbContext>>().To<MongoDbContextFactory>();
            Bind<IProjectRepository>().To<ProjectRepository>();
        }
    }
}
