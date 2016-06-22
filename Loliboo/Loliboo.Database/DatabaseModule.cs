using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loliboo.Database.DbContext;
using Loliboo.Database.DbRepositories;
using Loliboo.RepositoryAbstractions;
using Ninject.Modules;

namespace Loliboo.Database
{
    public class DatabaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbContextFactory<MongoDbContext>>().To<MongoDbContextFactory>();
            Bind<IProjectRepository>().To<ProjectRepository>();
        }
    }
}
