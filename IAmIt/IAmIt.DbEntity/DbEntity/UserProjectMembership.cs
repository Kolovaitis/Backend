using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace IAmIt.DbEntity.DbEntity
{
    public class UserProjectMembership
    {
        public ObjectId Id { get; set; }
        public string UserEmail { get; set; }
        public ObjectId ProjectId { get; set; }
        public bool IsVerified { get; set; }
    }
}
