using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace IAmIt.DbEntity.RepositoryModels
{
    public class GetAllInvitationsRepositoryModel
    {
        public ObjectId ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}
