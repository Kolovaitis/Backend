using Backend.DbEntities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.RepositoryAbstractions
{
    interface IProjectRepository
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
        List<ObjectId> GetAllInvitationsAsync(ObjectId userId);
    }
}
