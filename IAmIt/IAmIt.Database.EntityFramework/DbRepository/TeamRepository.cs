using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.DbEntity.DbEntity;
using MongoDB.Bson;
using IAmIt.Configuration;
using MongoDB.Driver;
using IAmIt.DbEntity.RepositoryModels;

namespace IAmIt.Database.EntityFramework.DbRepository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Team> _teams;
        private readonly IMongoCollection<UserTeamMembership> _userTeamMemberships;
        private readonly IMongoCollection<UserProjectMembership> _userProjectMemberships;
        private readonly IMongoCollection<UserBoardMembership> _userBoardMemberships;
        public TeamRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient();
            var db = client.GetDatabase(_configuration.NameDatabase);
            _teams = db.GetCollection<Team>("teams");
            _userTeamMemberships = db.GetCollection<UserTeamMembership>("userTeamMembership");
            _userProjectMemberships = db.GetCollection<UserProjectMembership>("userProjectMembership");
            _userBoardMemberships = db.GetCollection<UserBoardMembership>("userBoardMembership");
        }
        public async Task AcceptInvitationToTeamAsync(ObjectId teamId, ObjectId userId)
        {
            var update = Builders<UserTeamMembership>.Update
                .Set(m => m.IsVerified, true);
            await _userTeamMemberships.UpdateOneAsync(m => m.TeamId == teamId && m.UserId == userId && !m.IsVerified, update);
        }

        public async Task AddTeamAsync(Team team)
        {
            await _teams.InsertOneAsync(team);
        }

        public async Task AddTeamToBoardAsync(ObjectId teamId, ObjectId boardId)
        {
            var memberships = (await _userTeamMemberships.FindAsync(m => m.TeamId == teamId && m.IsVerified)).ToList().Select(m => new UserBoardMembership { Id = ObjectId.GenerateNewId(),UserId = m.UserId, BoardId = boardId}).ToList();
            await _userBoardMemberships.InsertManyAsync(memberships);
        }

        public async Task AddTeamToProjectAsync(ObjectId teamId, ObjectId projectId)
        {
            var memberships = (await _userTeamMemberships.FindAsync(m => m.TeamId == teamId && m.IsVerified)).ToList().Select(m => new UserProjectMembership { Id = ObjectId.GenerateNewId(), UserId = m.UserId, ProjectId = projectId, IsVerified = true }).ToList();
            await _userProjectMemberships.InsertManyAsync(memberships);
        }

        public async Task ChangeTeamAsync(Team team)
        {
            var update = Builders<Team>.Update
                .Set(t => t.Name, team.Name);
            await _teams.UpdateOneAsync(t => t.Id == team.Id, update);
        }

        public async Task DeleteTeamAsync(ObjectId teamId)
        {
            await _teams.DeleteOneAsync(t => t.Id == teamId);
            await _userTeamMemberships.DeleteManyAsync(m => m.TeamId == teamId);
        }

        public async Task DeleteTeamFromBoardAsync(ObjectId teamId, ObjectId boardId)
        {
            var ids = (await _userTeamMemberships.FindAsync(m => m.TeamId == teamId && m.IsVerified)).ToList().Select(m => m.UserId).ToList();
            await _userBoardMemberships.DeleteManyAsync(m => ids.Contains(m.UserId) && m.BoardId == boardId);
        }

        public async Task DeleteTeamFromProjectAsync(ObjectId teamId, ObjectId projectId)
        {
            var ids = (await _userTeamMemberships.FindAsync(m => m.TeamId == teamId && m.IsVerified)).ToList().Select(m => m.UserId).ToList();
            await _userProjectMemberships.DeleteManyAsync(m => ids.Contains(m.UserId) && m.ProjectId == projectId);
        }

        public async Task DeleteUserFromTeamAsync(ObjectId teamId, ObjectId userId)
        {
            await _userTeamMemberships.DeleteOneAsync(m => m.TeamId == teamId && m.UserId == userId);
        }

        public async Task<Team> GetTeamAsync(ObjectId teamId)
        {
            return (await _teams.FindAsync(t => t.Id == teamId)).FirstOrDefault();
        }

        public async Task<List<Team>> GetTeamsByUserAsync(ObjectId userId)
        {
            var ids = (await _userTeamMemberships.FindAsync(m => m.UserId == userId && m.IsVerified)).ToList().Select(m => m.TeamId).ToList();
            return (await _teams.FindAsync(t => ids.Contains(t.Id))).ToList();
        }

        public async Task<List<ObjectId>> GetUsersInTeamAsync(ObjectId teamId)
        {
            return (await _userTeamMemberships.FindAsync(m => m.TeamId == teamId && m.IsVerified)).ToList().Select(m => m.UserId).ToList();
        }

        public async Task InviteUserToTeamAsync(ObjectId teamId, ObjectId userId)
        {
            await _userTeamMemberships.InsertOneAsync(
                new UserTeamMembership
                {
                    Id = ObjectId.GenerateNewId(),
                    UserId = userId,
                    TeamId = teamId,
                    IsVerified = false
                });
        }

        public async Task RejectInvitationToTeamAsync(ObjectId teamId, ObjectId userId)
        {
            await _userTeamMemberships.DeleteOneAsync(m => m.TeamId == teamId && m.UserId == userId && !m.IsVerified);
        }

        public async Task<List<GetAllTeamInvitationsRepositoryModel>> GetAllTeamInvitationsAsync(ObjectId userId)
        {
            var ids = (await _userTeamMemberships.FindAsync(m => m.UserId == userId && !m.IsVerified)).ToList().Select(m => m.TeamId).ToList();
            return (await _teams.FindAsync(t => ids.Contains(t.Id))).ToList().Select(t => new GetAllTeamInvitationsRepositoryModel {
                TeamId = t.Id,
                TeamName = t.Name
            }).ToList();
        }
    }
}
