using Backend.RepositoryAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Backend.Models.ProjectModels;

namespace Backend.Service.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Task AcceptInvitation(AcceptInvitationModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ObjectId> AddProject(AddProjectModel model)
        {
            throw new NotImplementedException();
        }

        public Task ChangeProject(ChangeProjectModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProject(DeleteProjectModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserFromProject(DeleteUserFromProjectModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<InvitationModel>> GetAllInvitations(string Email)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProjectToSendModel>> getMyProjects(string Email)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectToSendModel> GetProject(GetProjectModel model)
        {
            throw new NotImplementedException();
        }

        public Task InviteUserToProject(InviteUserToProjectModel model)
        {
            throw new NotImplementedException();
        }

        public Task RejectInvitation(RejectInvitationModel model)
        {
            throw new NotImplementedException();
        }
    }
}
