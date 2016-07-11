using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.DbEntity.DbEntity;
using MongoDB.Bson;
using MongoDB.Driver;
using IAmIt.Configuration;

namespace IAmIt.Database.EntityFramework.DbRepository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Board> _boards;
        private readonly IMongoCollection<UserBoardMembership> _toUserMemberships;
        public BoardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient();
            var db = client.GetDatabase(_configuration.NameDatabase);
            _boards = db.GetCollection<Board>("boards");
            _toUserMemberships = db.GetCollection<UserBoardMembership>("userBoardMembership");
        }
        public async Task AddBoardAsync(Board board)
        {
            await _boards.InsertOneAsync(board);
        }

        public async Task<ICollection<ObjectId>> GetUsersInBoardAsync(ObjectId boardId)
        {
            return (await _toUserMemberships.FindAsync(b => b.BoardId == boardId)).ToList().Select(u => u.UserId).ToList();
        }

        public async Task AddUserToBoardAsync(ObjectId userId, ObjectId boardId)
        {
            var membership = new UserBoardMembership
            {
                BoardId = boardId,
                UserId = userId
            };
            await _toUserMemberships.InsertOneAsync(membership);
        }

        public async Task ChangeBoardAsync(Board board)
        {
            await _boards.UpdateOneAsync(
                new BsonDocument("_id", board.Id),
                new BsonDocument("$set", new BsonDocument("Name", board.Name))
            );
        }

        public async Task DeleteBoardAsync(ObjectId id)
        {
            await _toUserMemberships.DeleteManyAsync(m => m.BoardId == id);
            await _boards.DeleteOneAsync(b => b.Id == id);
        }

        public async Task DeleteUserFromBoardAsync(ObjectId userId, ObjectId boardId)
        {
            await _toUserMemberships.DeleteOneAsync(m => m.BoardId == boardId && m.UserId == userId);
        }

        public async Task<Board> GetBoardAsync(ObjectId id)
        {
            return (await _boards.FindAsync(b => b.Id == id)).FirstOrDefault();
        }

        public async Task<List<Board>> GetBoardsByUserAsync(ObjectId userId)
        {
            var ids = (await _toUserMemberships.FindAsync(m => m.UserId == userId)).ToList().Select(m => m.BoardId);
            return (await _boards.FindAsync(b => ids.Contains(b.Id))).ToList();
        }

        public async Task<List<Board>> GetBoardsInProjectAsync(ObjectId projectId)
        {
            return (await _boards.FindAsync(b => b.ProjectId == projectId)).ToList();
        }
    }
}
