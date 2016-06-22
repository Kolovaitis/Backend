using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loliboo.Models.ProjectModels
{
    public class RejectInvitationModel
    {
        public ObjectId ProjectId { get; set; }
        public string UserEmail { get; set; }
    }
}
