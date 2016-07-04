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
        private readonly IBoardRepository _boardRepository;

        public ProjectService(IProjectRepository projectRepository, IBoardRepository boardRepository)
        {
            _projectRepository = projectRepository;
            _boardRepository = boardRepository;
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
            if (await _projectRepository.GetProjectAsync(id) != null)
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

        public async Task<ICollection<ProjectToSendLightModel>> getMyProjectsAsync(ObjectId userId)
        {
            return (await _projectRepository.GetProjectsByUserAsync(userId))
                .Select(g => new ProjectToSendLightModel { ProjectId = g.Id.ToString(), Name = g.Name }).ToList();
        }

        public async Task<ProjectToSendFullModel> GetProjectAsync(GetProjectModel model)
        {
            var id = new ObjectId(model.ProjectId);
            var project = (await _projectRepository.GetProjectAsync(id));
            return new ProjectToSendFullModel {
                ProjectId = project.Id.ToString(),
                Name = project.Name,
                Boards = (await _boardRepository.GetBoardsInProjectAsync(id)).Select(b => new BoardToSendLightModel { BoardId = b.Id.ToString(), Name = b.Name}).ToList() };
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
