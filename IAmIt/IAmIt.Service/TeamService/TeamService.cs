using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.Models;
using MongoDB.Bson;
using IAmIt.Database.EntityFramework.DbRepository;
using IAmIt.DbEntity.DbEntity;

namespace IAmIt.Service.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public async Task AcceptTeamInvitationAsync(AcceptTeamInvitationModel model)
        {
            var users = await _teamRepository.GetUsersInTeamAsync(new ObjectId(model.TeamId));
            if (users.Contains(model.UserId))
            {
                throw new Exception("You are already a member of that project");
            }
            var id = new ObjectId(model.TeamId);
            await _teamRepository.AcceptInvitationToTeamAsync(id, model.UserId);
        }

        public async Task<ObjectId> AddTeamAsync(AddTeamModel model)
        {
            var id = ObjectId.GenerateNewId();
            if (await _teamRepository.GetTeamAsync(id) != null)
            {
                return await AddTeamAsync(model);
            }
            var team = new Team
            {
                Id = id,
                Name = model.Name,
                
            };
            await _teamRepository.AddTeamAsync(team);
            await _teamRepository.InviteUserToTeamAsync(id, model.UserId);
            await _teamRepository.AcceptInvitationToTeamAsync(id, model.UserId);
            return id;
        }

        public async Task AddTeamToBoardAsync(AddTeamToBoardModel model)
        {
            await _teamRepository.AddTeamToBoardAsync(new ObjectId(model.TeamId), new ObjectId(model.BoardId));
        }

        public async Task AddTeamToProjectAsync(AddTeamToProjectModel model)
        {
            await _teamRepository.AddTeamToProjectAsync(new ObjectId(model.TeamId), new ObjectId(model.ProjectId));
        }

        public async Task ChangeTeamAsync(ChangeTeamModel model)
        {
            var team = (await _teamRepository.GetTeamAsync(new ObjectId(model.TeamId)));
            team.Name = model.Name;
            await _teamRepository.ChangeTeamAsync(team);
        }

        public async Task DeleteTeamAsync(DeleteTeamModel model)
        {
            await _teamRepository.DeleteTeamAsync(new ObjectId(model.TeamId));
        }

        public async Task DeleteTeamFromBoardAsync(DeleteTeamFromBoardModel model)
        {
            await _teamRepository.DeleteTeamFromBoardAsync(new ObjectId(model.TeamId), new ObjectId(model.BoardId));
        }

        public async Task DeleteTeamFromProjectAsync(DeleteTeamFromProjectModel model)
        {
            await _teamRepository.DeleteTeamFromProjectAsync(new ObjectId(model.TeamId), new ObjectId(model.ProjectId));
        }

        public async Task DeleteUserFromTeamAsync(DeleteUserFromTeamModel model)
        {
            await _teamRepository.DeleteUserFromTeamAsync(new ObjectId(model.TeamId), model.UserId);
        }

        public async Task DeleteYourselfAsync(DeleteYourselfFromTeamModel model)
        {
            if ((await _teamRepository.GetUsersInTeamAsync(new ObjectId(model.TeamId))).AsQueryable().Count() == 1)
            {
                throw new Exception
                    ("You are the last member of the team. If you really want to delete yourself, delete the team");
            }
                await _teamRepository.DeleteUserFromTeamAsync(new ObjectId(model.TeamId), model.UserId);
        }

        public async Task<ICollection<TeamInvitationModel>> GetAllTeamInvitationsAsync(ObjectId userId)
        {
            return (await _teamRepository.GetAllTeamInvitationsAsync(userId))
                .Select(g => new TeamInvitationModel { TeamId = g.TeamId.ToString(), TeamName = g.TeamName }).ToList();
        }

        public async Task<ICollection<ObjectId>> GetAllUsersInTeamAsync(GetTeamModel model)
        {
            return await _teamRepository.GetUsersInTeamAsync(new ObjectId(model.TeamId));
        }

        public async Task<ICollection<TeamToSendLightModel>> getMyTeamsAsync(ObjectId userId)
        {
            return (await _teamRepository.GetTeamsByUserAsync(userId))
                .Select(g => new TeamToSendLightModel { TeamId = g.Id.ToString(), Name = g.Name }).ToList();
        }

        public async Task<TeamToSendFullModel> GetTeamAsync(GetTeamModel model)
        {
            var team = (await _teamRepository.GetTeamAsync(new ObjectId(model.TeamId)));
            return new TeamToSendFullModel { Name = team.Name, TeamId = model.TeamId};
        }

        public async Task InviteUserToTeamAsync(InviteUserToTeamModel model)
        {
            var users = await _teamRepository.GetUsersInTeamAsync(new ObjectId(model.TeamId));
            if (users.Contains(model.RecipientId))
            {
                throw new Exception("User is already a member of the team");
            }
            var invitations = await _teamRepository.GetAllTeamInvitationsAsync(model.RecipientId);
            if (invitations.Select(i => i.TeamId).Contains(new ObjectId(model.TeamId)))
            {
                throw new Exception("User has already been invited");
            }
            var id = new ObjectId(model.TeamId);
            await _teamRepository.InviteUserToTeamAsync(id, model.RecipientId);
        }

        public async Task RejectTeamInvitationAsync(RejectTeamInvitationModel model)
        {
            var users = await _teamRepository.GetUsersInTeamAsync(new ObjectId(model.TeamId));
            if (users.Contains(model.UserId))
            {
                throw new Exception("You are a member of that project");
            }
            var id = new ObjectId(model.TeamId);
            await _teamRepository.RejectInvitationToTeamAsync(id, model.UserId);
        }
    }
    }
