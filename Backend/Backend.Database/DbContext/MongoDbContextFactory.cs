using Backend.Configuration;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Database.DbContext
{
    public class MongoDbContextFactory : IDbContextFactory<MongoDbContext>
    {
        public MongoDbContext Create()
        {
            var kernel = new StandardKernel();
            kernel.Load<ConfigurationModule>();

            return new MongoDbContext(kernel.Get<IConfiguraiton>().NameDatabase);
        }
    }
}
