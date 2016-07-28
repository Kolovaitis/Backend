using IAmIt.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Service.TeamService
{
    public interface ITeamService
    {
        Task<ObjectId> AddTeamAsync(AddTeamModel model);
        Task<ICollection<TeamToSendLightModel>> getMyTeamsAsync(ObjectId userId);
        Task ChangeProjectAsync(ChangeTeamModel model);
        Task DeleteProjectAsync(DeleteTeamModel model);
        Task InviteUserToTeamAsync(InviteUserToTeamModel model);
        Task AcceptTeamInvitationAsync(AcceptTeamInvitationModel model);
        Task RejectTeamInvitationAsync(RejectTeamInvitationModel model);
        Task DeleteUserFromTeamAsync(DeleteUserFromTeamModel model);
        Task DeleteYourselfAsync(DeleteYourselfFromTeamModel model);
        Task<TeamToSendFullModel> GetTeamAsync(GetTeamModel model);
        Task<ICollection<TeamInvitationModel>> GetAllTeamInvitationsAsync(ObjectId userId);
        Task<ICollection<ObjectId>> GetAllUsersInTeamAsync(GetTeamModel model);
        Task AddTeamToProjectAsync(AddTeamToProjectModel model);
        Task DeleteTeamFromProjectAsync(DeleteTeamFromProjectModel model);
        Task AddTeamToBoardAsync(AddTeamToBoardModel model);
        Task DeleteTeamFromBoardAsync(DeleteTeamFromBoardModel model);
    }
}
