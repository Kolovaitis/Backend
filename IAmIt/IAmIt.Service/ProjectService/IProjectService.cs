using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.Models;
using MongoDB.Bson;

namespace IAmIt.Service.ProjectService
{
    public interface IProjectService
    {
        Task<ObjectId> AddProjectAsync(AddProjectModel model);
        Task<ICollection<ProjectToSendLightModel>> getMyProjectsAsync(ObjectId userId);
        Task ChangeProjectAsync(ChangeProjectModel model);
        Task DeleteProjectAsync(DeleteProjectModel model);
        Task InviteUserToProjectAsync(InviteUserToProjectModel model);
        Task AcceptInvitationAsync(AcceptInvitationModel model);
        Task RejectInvitationAsync(RejectInvitationModel model);
        Task DeleteUserFromProjectAsync(DeleteUserFromProjectModel model);
        Task DeleteYourselfAsync(DeleteUserFromProjectModel model);
        Task<ProjectToSendFullModel> GetProjectAsync(GetProjectModel model);
        Task<ICollection<InvitationModel>> GetAllInvitationsAsync(ObjectId userId);
        Task<ICollection<ObjectId>> GetAllUsersInProjectAsync(GetProjectModel model);
    }
}
