using IAmIt.DbEntity.DbEntity;
using MongoDB.Driver;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Database.EntityFramework.DbContext
{
    public class MongoDbContext : System.Data.Entity.DbContext
    {

        public IMongoCollection<Project> Projects { get; set; }

        public MongoDbContext(string connectionString, string nameDatabase) : base(connectionString)
        {

            var client = new MongoClient();

            var db = client.GetDatabase(nameDatabase);
            Projects = db.GetCollection<Project>("projects");
        }
        

    }
}
