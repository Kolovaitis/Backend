using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loliboo.Models.ProjectModels
{
    public class InviteUserToProjectModel
    {
        //public string EmailSender { get; set; }
        public string EmailRecipient { get; set; }
        public ObjectId ProjectId { get; set; }
    }
}
