using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace IAmIt.DbEntity.DbEntity
{
    public class Card
    {
        public ObjectId Id { get; set; }
        public ObjectId ColumnId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
    }
}
