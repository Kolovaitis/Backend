using IAmIt.DbEntity.DbEntity;
using IAmIt.DbEntity.RepositoryModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Database.EntityFramework.DbRepository
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeamsByUserAsync(ObjectId userId);
        Task<List<ObjectId>> GetUsersInTeamAsync(ObjectId teamId);

        Task AddTeamAsync(Team team);
        Task<Team> GetTeamAsync(ObjectId teamId);
        Task ChangeTeamAsync(Team team);
        Task DeleteTeamAsync(ObjectId teamId);

        Task InviteUserToTeamAsync(ObjectId teamId, ObjectId userId);
        Task DeleteUserFromTeamAsync(ObjectId teamId, ObjectId userId);

        Task AcceptInvitationToTeamAsync(ObjectId teamId, ObjectId userId);
        Task RejectInvitationToTeamAsync(ObjectId teamId, ObjectId userId);
        Task<List<GetAllTeamInvitationsRepositoryModel>> GetAllTeamInvitationsAsync(ObjectId userId);

        Task AddTeamToProjectAsync(ObjectId teamId, ObjectId projectId);
        Task DeleteTeamFromProjectAsync(ObjectId teamId, ObjectId projectId);
        Task AddTeamToBoardAsync(ObjectId teamId, ObjectId boardId);
        Task DeleteTeamFromBoardAsync(ObjectId teamId, ObjectId boardId);
    }
}
