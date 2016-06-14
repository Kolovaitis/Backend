using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DbEntities
{
    public class UserProjectMembership
    {
        public ObjectId UserId { get; set; }
        public ObjectId ProjectId { get; set; }
        public bool IsVerified { get; set; }
    }
}
