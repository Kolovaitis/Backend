using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace IAmIt.DbEntity.DbEntity
{
    public class UserCardMembership
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId CardId { get; set; }
    }
}
