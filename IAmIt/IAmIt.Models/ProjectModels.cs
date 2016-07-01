using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace IAmIt.Models
{
    public class AcceptInvitationModel
    {
        public string ProjectId { get; set; }
        public ObjectId UserId { get; set; }
    }

    public class AddProjectModel
    {
        public string Name { get; set; }
        public ObjectId UserId { get; set; }
    }

    public class ChangeProjectModel
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
    }

    public class DeleteProjectModel
    {
        public string ProjectId { get; set; }
    }

    public class DeleteUserFromProjectModel
    {
        public ObjectId UserId { get; set; }
        public string UserEmail { get; set; }
        public string ProjectId { get; set; }
    }

    public class GetProjectModel
    {
        public string ProjectId { get; set; }
    }

    public class InvitationModel
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
    }

    public class InviteUserToProjectModel
    {
        //public string EmailSender { get; set; }
        public string EmailRecipient { get; set; }
        public ObjectId RecipientId { get; set; }
        public string ProjectId { get; set; }
    }

    public class ProjectToSendLightModel
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
    }

    public class ProjectToSendFullModel
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
        ICollection<BoardToSendLightModel> Boards { get; set; }
    }

    public class RejectInvitationModel
    {
        public string ProjectId { get; set; }
        public ObjectId UserId { get; set; }
    }
}
