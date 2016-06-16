using Backend.RepositoryAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Backend.Models.ProjectModels;
using Backend.DbEntities;

namespace Backend.Service.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task AcceptInvitation(AcceptInvitationModel model)
        {
            await _projectRepository.AcceptInvitationToProjectAsync(model.ProjectId, model.UserEmail);
        }

        public async Task<ObjectId> AddProject(AddProjectModel model)
        {
            var id = ObjectId.GenerateNewId();
            if (await _projectRepository.GetProjectAsync(id) != null) //или нужно так: (_projectRepository.GetProjectAsync(id).IsCompleted)?
            {
                return await AddProject(model);
            }
            var project = new Project
            {
                Id = id,
                Name = model.Name,
            };
            await _projectRepository.AddProjectAsync(project);
            await _projectRepository.InviteUserToProjectAsync(id, model.UserEmail);
            await _projectRepository.AcceptInvitationToProjectAsync(id, model.UserEmail);
            return id;
        }

        public async Task ChangeProject(ChangeProjectModel model)
        {
            var project = (await _projectRepository.GetProjectAsync(model.ProjectId));
            project.Name = model.Name;
            await _projectRepository.ChangeProjectAsync(project);
        }

        public async Task DeleteProject(DeleteProjectModel model)
        {
            await _projectRepository.DeleteProjectAsync(model.ProjectId);
        }

        public async Task DeleteUserFromProject(DeleteUserFromProjectModel model)
        {
            await _projectRepository.DeleteUserFromProjectAsync(model.ProjectId, model.UserEmail);
        }

        public async Task<ICollection<InvitationModel>> GetAllInvitations(string Email)
        {
            return (await _projectRepository.GetAllInvitationsAsync(Email))
                .Select(g => new InvitationModel { ProjectId = g}).ToList();
        }

        public async Task<ICollection<ProjectToSendModel>> getMyProjects(string Email)
        {
            return (await _projectRepository.GetProjectsByUserAsync(Email))
                .Select(g => new ProjectToSendModel { ProjectId = g.Id, Name = g.Name }).ToList();
        }

        public async Task<ProjectToSendModel> GetProject(GetProjectModel model)
        {
            var project = (await _projectRepository.GetProjectAsync(model.ProjectId));
            return new ProjectToSendModel { ProjectId = project.Id, Name = project.Name };
        }

        public async Task InviteUserToProject(InviteUserToProjectModel model)
        {
            await _projectRepository.InviteUserToProjectAsync(model.ProjectId, model.EmailRecipient);
        }

        public async Task RejectInvitation(RejectInvitationModel model)
        {
            await _projectRepository.RejectInvitationToProjectAsync(model.ProjectId, model.UserEmail);
        }
    }
}
