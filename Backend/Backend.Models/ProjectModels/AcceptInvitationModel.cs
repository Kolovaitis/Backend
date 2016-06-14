using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models.ProjectModels
{
    public class AcceptInvitationModel
    {
        public ObjectId ProjectId { get; set; }
        public string UserEmail { get; set; }
    }
}
