using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Models
{
    public class AcceptTeamInvitationModel
    {
        public string TeamId { get; set; }
        public ObjectId UserId { get; set; }
    }

    public class AddTeamModel
    {
        public string Name { get; set; }
        public ObjectId UserId { get; set; }
    }

    public class ChangeTeamModel
    {
        public string TeamId { get; set; }
        public string Name { get; set; }
    }

    public class DeleteTeamModel
    {
        public string TeamId { get; set; }
    }

    public class DeleteUserFromTeamModel
    {
        public ObjectId UserId { get; set; }
        public string UserEmail { get; set; }
        public string TeamId { get; set; }
    }

    public class DeleteYourselfFromTeamModel
    {
        public ObjectId UserId { get; set; }
        public string TeamId { get; set; }
    }

    public class GetTeamModel
    {
        public string TeamId { get; set; }
    }

    public class TeamInvitationModel
    {
        public string TeamId { get; set; }
        public string TeamName { get; set; }
    }

    public class InviteUserToTeamModel
    {
        //public string EmailSender { get; set; }
        public string EmailRecipient { get; set; }
        public ObjectId RecipientId { get; set; }
        public string TeamId { get; set; }
    }

    public class TeamToSendLightModel
    {
        public string TeamId { get; set; }
        public string Name { get; set; }
    }

    public class TeamToSendFullModel
    {
        public string TeamId { get; set; }
        public string Name { get; set; }
        public ICollection<UserToSendModel> Users { get; set; }
    }

    public class RejectTeamInvitationModel
    {
        public string TeamId { get; set; }
        public ObjectId UserId { get; set; }
    }
    public class DeleteTeamFromBoardModel
    {
        public string TeamId { get; set; }
        public string BoardId { get; set; }
    }

    public class AddTeamToBoardModel
    {
        public string TeamId { get; set; }
        public string BoardId { get; set; }
    }
    public class DeleteTeamFromProjectModel
    {
        public string TeamId { get; set; }
        public string ProjectId { get; set; }
    }

    public class AddTeamToProjectModel
    {
        public string TeamId { get; set; }
        public string ProjectId { get; set; }
    }
}
