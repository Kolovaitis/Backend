﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace IAmIt.DbEntity.DbEntity
{
    public class Project
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
