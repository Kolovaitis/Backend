using Backend.Database.DbContext;
using Backend.Database.DbRepositories;
using Backend.RepositoryAbstractions;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Database
{
    public class EntityFrameworkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbContextFactory<MongoDbContext>>().To<MongoDbContextFactory>();
            Bind<IUserRepository>().To<DbUserRepository>();
        }
    }
}
