using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.DbEntity.DbEntity;
using IAmIt.DbEntity.RepositoryModels;
using MongoDB.Bson;

namespace IAmIt.Database.EntityFramework.DbRepository
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjectsByUserAsync(ObjectId userId);
        Task<List<ObjectId>> GetUsersInProjectAsync(ObjectId projectId);

        Task AddProjectAsync(Project project);
        Task<Project> GetProjectAsync(ObjectId projectId);
        Task ChangeProjectAsync(Project project);
        Task DeleteProjectAsync(ObjectId projectId);

        Task InviteUserToProjectAsync(ObjectId projectId, ObjectId userId);
        Task DeleteUserFromProjectAsync(ObjectId projectId, ObjectId userId);

        Task AcceptInvitationToProjectAsync(ObjectId projectId, ObjectId userId);
        Task RejectInvitationToProjectAsync(ObjectId projectId, ObjectId userId);
        Task<List<GetAllInvitationsRepositoryModel>> GetAllInvitationsAsync(ObjectId userId);
    }
}
