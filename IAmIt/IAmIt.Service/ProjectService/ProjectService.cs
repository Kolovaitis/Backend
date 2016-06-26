using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.Models;
using MongoDB.Bson;
using IAmIt.Database.EntityFramework.DbRepository;
using IAmIt.DbEntity.DbEntity;

namespace IAmIt.Service.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task AcceptInvitationAsync(AcceptInvitationModel model)
        {
            var users = await _projectRepository.GetUsersInProjectAsync(new ObjectId(model.ProjectId));
            if(users.Contains(model.UserId))
            {
                throw new Exception("You are already in the project");
            }
            var id = new ObjectId(model.ProjectId);
            await _projectRepository.AcceptInvitationToProjectAsync(id, model.UserId);
        }

        public async Task<ObjectId> AddProjectAsync(AddProjectModel model)
        {
            var id = ObjectId.GenerateNewId();
            if (await _projectRepository.GetProjectAsync(id) != null) //или нужно так: (_projectRepository.GetProjectAsync(id).IsCompleted)?
            {
                return await AddProjectAsync(model);
            }
            var project = new Project
            {
                Id = id,
                Name = model.Name,
            };
            await _projectRepository.AddProjectAsync(project);
            await _projectRepository.InviteUserToProjectAsync(id, model.UserId);
            await _projectRepository.AcceptInvitationToProjectAsync(id, model.UserId);
            return id;
        }

        public async Task ChangeProjectAsync(ChangeProjectModel model)
        {
            var id = new ObjectId(model.ProjectId);
            var project = (await _projectRepository.GetProjectAsync(id));
            project.Name = model.Name;
            await _projectRepository.ChangeProjectAsync(project);
        }

        public async Task DeleteProjectAsync(DeleteProjectModel model)
        {
            var id = new ObjectId(model.ProjectId);
            await _projectRepository.DeleteProjectAsync(id);
        }

        public async Task DeleteUserFromProjectAsync(DeleteUserFromProjectModel model)
        {
            var id = new ObjectId(model.ProjectId);
            await _projectRepository.DeleteUserFromProjectAsync(id, model.UserId);
        }

        public async Task DeleteYourselfAsync(DeleteUserFromProjectModel model)
        {
            await _projectRepository.DeleteUserFromProjectAsync(new ObjectId(model.ProjectId) ,model.UserId);
        }

        public async Task<ICollection<InvitationModel>> GetAllInvitationsAsync(ObjectId userId)
        {
            return (await _projectRepository.GetAllInvitationsAsync(userId))
                .Select(g => new InvitationModel { ProjectId = g.ProjectId.ToString(), ProjectName = g.ProjectName}).ToList();
        }

        public async Task<ICollection<ObjectId>> GetAllUsersInProjectAsync(GetProjectModel model)
        {
            return (await _projectRepository.GetUsersInProjectAsync(new ObjectId(model.ProjectId))).ToList();
        }

        public async Task<ICollection<ProjectToSendModel>> getMyProjectsAsync(ObjectId userId)
        {
            return (await _projectRepository.GetProjectsByUserAsync(userId))
                .Select(g => new ProjectToSendModel { ProjectId = g.Id.ToString(), Name = g.Name }).ToList();
        }

        public async Task<ProjectToSendModel> GetProjectAsync(GetProjectModel model)
        {
            var id = new ObjectId(model.ProjectId);
            var project = (await _projectRepository.GetProjectAsync(id));
            return new ProjectToSendModel { ProjectId = project.Id.ToString(), Name = project.Name };
        }

        public async Task InviteUserToProjectAsync(InviteUserToProjectModel model)
        {
            var users = await _projectRepository.GetUsersInProjectAsync(new ObjectId(model.ProjectId));
            if (users.Contains(model.RecipientId))
            {
                throw new Exception("User is already in the project");
            }
            var invitations = await _projectRepository.GetAllInvitationsAsync(model.RecipientId);
            if (invitations.Select(i => i.ProjectId).Contains(new ObjectId(model.ProjectId)))
            {
                throw new Exception("User has already been invited");
            }
            var id = new ObjectId(model.ProjectId);
            await _projectRepository.InviteUserToProjectAsync(id, model.RecipientId);
        }

        public async Task RejectInvitationAsync(RejectInvitationModel model)
        {
            var users = await _projectRepository.GetUsersInProjectAsync(new ObjectId(model.ProjectId));
            if (users.Contains(model.UserId))
            {
                throw new Exception("You are already in the project");
            }
            var id = new ObjectId(model.ProjectId);
            await _projectRepository.RejectInvitationToProjectAsync(id, model.UserId);
        }
    }
}
