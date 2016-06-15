
using Backend.DbEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Database.DbContext
{
    public class MongoDbContext : System.Data.Entity.DbContext
    {
        public IMongoCollection<Project> Projects { get; set; }
        public IMongoCollection<UserProjectMembership> UserProjectMembership { get; set; }

        public MongoDbContext(string nameDatabase)
        {

            var client = new MongoClient();

            var db = client.GetDatabase(nameDatabase);
            Projects = db.GetCollection<Project>(nameof(Project));
            UserProjectMembership = db.GetCollection<UserProjectMembership>(nameof(UserProjectMembership));
        }
    }
}
