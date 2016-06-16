using Backend.Models.ProjectModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.ProjectService
{
    public interface IProjectService
    {
        Task<ObjectId> AddProjectAsync(AddProjectModel model);
        Task<ICollection<ProjectToSendModel>> getMyProjectsAsync(string Email);
        Task ChangeProjectAsync(ChangeProjectModel model);
        Task DeleteProjectAsync(DeleteProjectModel model);
        Task InviteUserToProjectAsync(InviteUserToProjectModel model);
        Task AcceptInvitationAsync(AcceptInvitationModel model);
        Task RejectInvitationAsync(RejectInvitationModel model);
        Task DeleteUserFromProjectAsync(DeleteUserFromProjectModel model);
        Task<ProjectToSendModel> GetProjectAsync(GetProjectModel model);
        Task<ICollection<InvitationModel>> GetAllInvitationsAsync(string Email);
    }
}
