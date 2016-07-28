using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.Models;
using MongoDB.Bson;
using IAmIt.Database.EntityFramework.DbRepository;

namespace IAmIt.Service.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public Task AcceptTeamInvitationAsync(AcceptTeamInvitationModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ObjectId> AddTeamAsync(AddTeamModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddTeamToBoardAsync(AddTeamToBoardModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddTeamToProjectAsync(AddTeamToProjectModel model)
        {
            throw new NotImplementedException();
        }

        public Task ChangeProjectAsync(ChangeTeamModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProjectAsync(DeleteTeamModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeamFromBoardAsync(DeleteTeamFromBoardModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeamFromProjectAsync(DeleteTeamFromProjectModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserFromTeamAsync(DeleteUserFromTeamModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteYourselfAsync(DeleteYourselfFromTeamModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TeamInvitationModel>> GetAllTeamInvitationsAsync(ObjectId userId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ObjectId>> GetAllUsersInTeamAsync(GetTeamModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TeamToSendLightModel>> getMyTeamsAsync(ObjectId userId)
        {
            throw new NotImplementedException();
        }

        public Task<TeamToSendFullModel> GetTeamAsync(GetTeamModel model)
        {
            throw new NotImplementedException();
        }

        public Task InviteUserToTeamAsync(InviteUserToTeamModel model)
        {
            throw new NotImplementedException();
        }

        public Task RejectTeamInvitationAsync(RejectTeamInvitationModel model)
        {
            throw new NotImplementedException();
        }
    }
}
