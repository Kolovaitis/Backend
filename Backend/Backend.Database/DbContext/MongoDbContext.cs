﻿
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Database.DbContext
{
    public class MongoDbContext : System.Data.Entity.DbContext
    {

        public MongoDbContext(string nameDatabase)
        {

            var client = new MongoClient();

            var db = client.GetDatabase(nameDatabase);
        }
    }
}
