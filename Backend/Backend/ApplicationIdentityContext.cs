using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using Backend.Models;
using MongoDB.Driver;
using Backend.Models.UserModels;

namespace Backend
{
    public class ApplicationIdentityContext : IDisposable
    {
        public static ApplicationIdentityContext Create()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("loliboo");
            var users = database.GetCollection<ApplicationUser>("users");
            return new ApplicationIdentityContext(users);
        }

        private ApplicationIdentityContext(IMongoCollection<ApplicationUser> users)
        {
            Users = users;
        }

        public IMongoCollection<ApplicationUser> Users { get; set; }

        public void Dispose()
        {
        }
    }
}
