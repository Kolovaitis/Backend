using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.DbEntity.RepositoryModels
{
    public class GetAllTeamInvitationsRepositoryModel
    {
        public ObjectId TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
