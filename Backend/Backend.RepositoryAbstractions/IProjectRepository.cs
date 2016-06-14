using Backend.DbEntities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.RepositoryAbstractions
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjectsByUserAsync(string userEmail);
        Task<List<string>> GetUsersInProjectAsync(ObjectId projectId);

        Task AddProjectAsync(Project project);
        Task<Project> GetProjectAsync(ObjectId projectId);
        Task ChangeProjectAsync(Project project);
        Task DeleteProjectAsync(ObjectId projectId);

        Task InviteUserToProjectAsync(ObjectId projectId, string userEmail);
        Task DeleteUserFromProjectAsync(ObjectId projectId, string userEmail);

        Task AcceptInvitationToProjectAsync(ObjectId projectId, string userEmail);
        Task RejectInvitationToProjectAsync(ObjectId projectId, string userEmail);
        List<ObjectId> GetAllInvitationsAsync(string userEmail);
    }
}
