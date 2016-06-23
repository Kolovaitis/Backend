using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.Configuration;
using IAmIt.Database.EntityFramework.DbContext;
using IAmIt.DbEntity.DbEntity;
using MongoDB.Bson;
using MongoDB.Driver;
using Ninject;

namespace IAmIt.Database.EntityFramework.DbRepository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IConfiguraiton _configuration;
        private readonly IMongoCollection<Project> _projects;
        private readonly IMongoCollection<UserProjectMembership> _memberships;

        public ProjectRepository(IConfiguraiton configuration)
        {
            _configuration = configuration;
            var client = new MongoClient();
            var db = client.GetDatabase(_configuration.NameDatabase);
            _projects = db.GetCollection<Project>("projects");
            _memberships = db.GetCollection<UserProjectMembership>("userProjectMembership");
        }
        public async Task AcceptInvitationToProjectAsync(ObjectId projectId, string userEmail)
        {
            await _memberships.UpdateOneAsync(
                new BsonDocument {
                    {"ProjectId", projectId},
                    {"UserEmail", userEmail}},
                new BsonDocument("$set", new BsonDocument("IsVerified", true)));
        }

        public async Task AddProjectAsync(Project project)
        {
            await _projects.InsertOneAsync(project);
        }

        public async Task ChangeProjectAsync(Project project)
        {
            await _projects.UpdateOneAsync(
                new BsonDocument ("_id", project.Id),
                new BsonDocument("$set", new BsonDocument("Name", project.Name)));
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
