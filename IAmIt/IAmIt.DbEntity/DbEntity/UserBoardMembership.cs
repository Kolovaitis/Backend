using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.DbEntity.DbEntity
{
    public class UserBoardMembership
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId BoardId { get; set; }
    }
}
