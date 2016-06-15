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
        Task<ObjectId> AddProject(AddProjectModel model);
        Task<ICollection<ProjectToSendModel>> getMyProjects(string Email);
        Task ChangeProject(ChangeProjectModel model);
        Task DeleteProject(DeleteProjectModel model);
        Task InviteUserToProject(InviteUserToProjectModel model);
        Task AcceptInvitation(AcceptInvitationModel model);
        Task RejectInvitation(RejectInvitationModel model);
        Task DeleteUserFromProject(DeleteUserFromProjectModel model);
        Task<ProjectToSendModel> GetProject(GetProjectModel model);
        Task<ICollection<InvitationModel>> GetAllInvitations(string Email);
    }
}
