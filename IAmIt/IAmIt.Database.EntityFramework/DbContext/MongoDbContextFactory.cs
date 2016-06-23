using IAmIt.Configuration;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Database.EntityFramework.DbContext
{
    public class MongoDbContextFactory : IDbContextFactory<MongoDbContext>
    {
        public MongoDbContext Create()
        {
            var kernel = new StandardKernel();
            kernel.Load<ConfigurationModule>();

            return new MongoDbContext(kernel.Get<IConfiguraiton>().DbConnectionString, kernel.Get<IConfiguraiton>().NameDatabase);
        }
    }
}
