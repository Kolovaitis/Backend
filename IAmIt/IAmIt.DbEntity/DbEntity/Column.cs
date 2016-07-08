﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace IAmIt.DbEntity.DbEntity
{
    public class Column
    {
        public ObjectId Id { get; set; }
        public ObjectId BoardId { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
    }
}
