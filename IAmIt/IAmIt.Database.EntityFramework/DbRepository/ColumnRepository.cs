using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.DbEntity.DbEntity;
using MongoDB.Bson;
using IAmIt.Configuration;
using MongoDB.Driver;

namespace IAmIt.Database.EntityFramework.DbRepository
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Column> _columns;
        public ColumnRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient();
            var db = client.GetDatabase(_configuration.NameDatabase);
            _columns = db.GetCollection<Column>("columns");
        }
        public async Task AddColumnAsync(Column column)
        {
            await _columns.InsertOneAsync(column);
        }

        public async Task ChangeColumnAsync(Column column)
        {
            var update = Builders<Column>.Update
    .Set(c => c.Name, column.Name);
            await _columns.UpdateOneAsync(c => c.Id == column.Id, update);
        }

        public async Task DeleteColumnAsync(ObjectId id)
        {
            await _columns.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<List<Column>> GetAllColumnsInBoardAsync(ObjectId boardId)
        {
            return (await _columns.FindAsync(c => c.BoardId == boardId)).ToList();
        }

        public async Task MoveColumnAsync(ObjectId columnId, int newPosition)
        {
            var currentColumn = (await _columns.FindAsync(c => c.Id == columnId)).FirstOrDefault();
            var currentPosition = currentColumn.Position;
            var ids = new List<ObjectId>();
            if (currentPosition > newPosition)
            {
                ids = (await _columns.FindAsync(c => c.BoardId == currentColumn.BoardId && (c.Position <= newPosition && c.Position > currentColumn.Position))).ToList().Select(c => c.Id).ToList();
                var update = Builders<Column>.Update.Inc(c => c.Position, -1);
                await _columns.UpdateManyAsync(c => ids.Contains(c.Id), update);
            }
            else if (currentPosition < newPosition)
            {
                ids = (await _columns.FindAsync(c => c.BoardId == currentColumn.BoardId && (c.Position >= newPosition && c.Position < currentColumn.Position))).ToList().Select(c => c.Id).ToList();
                var update = Builders<Column>.Update.Inc(c => c.Position, 1);
                await _columns.UpdateManyAsync(c => ids.Contains(c.Id), update);
            }
            await _columns.UpdateOneAsync(c => c.Id == columnId, new BsonDocument("$set", new BsonDocument("Position", newPosition)));
        }

        public async Task<Column> GetColumnAsync(ObjectId columnId)
        {
            return (await _columns.FindAsync(c => c.Id == columnId)).FirstOrDefault();
        }
    }
}
