using IAmIt.DbEntity.DbEntity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Database.EntityFramework.DbRepository
{
    public interface IColumnRepository
    {
        Task AddColumnAsync(Column column);
        Task ChangeColumnAsync(Column column);
        Task DeleteColumnAsync(ObjectId id);
        Task<List<Column>> GetAllColumnsInBoardAsync(ObjectId boardId);
        Task MoveColumnAsync(ObjectId columnId, int newPosition);
    }
}
