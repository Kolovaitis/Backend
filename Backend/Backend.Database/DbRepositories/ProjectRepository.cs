using Backend.RepositoryAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.DbEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using Backend.Database.DbContext;
using Ninject;
using System.Data.Entity.Infrastructure;

namespace Backend.Database.DbRepositories
{
    public class ProjectRepository : IProjectRepository
    {
        private IMongoCollection<Project> _projects;
        private IMongoCollection<UserProjectMembership> _memberships;
        private readonly MongoDbContext _context;

        public ProjectRepository(IDbContextFactory<MongoDbContext> contextFactory)
        {
            _context = contextFactory.Create();
            _projects = _context.Projects;
            _memberships = _context.UserProjectMembership;
        }
        public async Task AcceptInvitationToProjectAsync(ObjectId projectId, string userEmail)
        {
            var obj = new object();
            var updateDefinition = new ObjectUpdateDefinition<UserProjectMembership>(obj);
            updateDefinition.Set(m => m.IsVerified, true);
            await _memberships.UpdateOneAsync(m => m.ProjectId == projectId && m.UserEmail == userEmail && !m.IsVerified, updateDefinition);
        }

        public async Task AddProjectAsync(Project project)
        {
            await _projects.InsertOneAsync(project);
        }

        public async Task ChangeProjectAsync(Project project)
        {
            var obj = new object();
            var updateDefinition = new ObjectUpdateDefinition<Project>(obj);
            updateDefinition.Set(p => p.Name, project.Name);
            await _projects.UpdateOneAsync(p => p.Id == project.Id, updateDefinition);
        }

        public async Task DeleteProjectAsync(ObjectId projectId)
        {
            await _projects.DeleteOneAsync(p => p.Id == projectId);
        }

        public async Task DeleteUserFromProjectAsync(ObjectId projectId, string userEmail)
        {
            await _memberships.DeleteOneAsync(m => m.ProjectId == projectId && m.UserEmail == userEmail);
        }

        public async Task<List<ObjectId>> GetAllInvitationsAsync(string userEmail)
        {
            return (await _memberships.FindAsync(m => m.UserEmail == userEmail && !m.IsVerified)).ToList().Select(m => m.ProjectId).ToList();
        }

        public async Task<Project> GetProjectAsync(ObjectId projectId)
        {
            return (await _projects.FindAsync(p => p.Id == projectId)).FirstOrDefault();
        }

        public async Task<List<Project>> GetProjectsByUserAsync(string userEmail)
        {
            var ids = (await _memberships.FindAsync(m => m.UserEmail == userEmail && m.IsVerified)).ToList().Select(m => m.ProjectId).ToList();
            return (await _projects.FindAsync(p => ids.Contains(p.Id))).ToList();
        }

        public async Task<List<string>> GetUsersInProjectAsync(ObjectId projectId)
        {
            return (await _memberships.FindAsync(m => m.ProjectId == projectId && m.IsVerified)).ToList().Select(m => m.UserEmail).ToList();
        }

        public async Task InviteUserToProjectAsync(ObjectId projectId, string userEmail)
        {
            var membership = new UserProjectMembership
            {
                IsVerified = false,
                ProjectId = projectId,
                UserEmail = userEmail
            };
            await _memberships.InsertOneAsync(membership);
        }

        public async Task RejectInvitationToProjectAsync(ObjectId projectId, string userEmail)
        {
            await _memberships.DeleteOneAsync(m => m.ProjectId == projectId && m.UserEmail == userEmail && !m.IsVerified);
        }
    }
}
