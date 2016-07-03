using IAmIt.DbEntity.DbEntity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Database.EntityFramework.DbRepository
{
    public interface IBoardRepository
    {
        Task<Board> GetBoardAsync(ObjectId id);
        Task AddBoardAsync(Board board);
        Task DeleteBoardAsync(ObjectId id);
        Task ChangeBoardAsync(Board board);
        Task<List<Board>> GetBoardsByUserAsync(ObjectId userId);
        Task<List<Board>> GetBoardsInProjectAsync(ObjectId projectId);
        Task AddUserToBoardAsync(ObjectId userId, ObjectId boardId);
        Task DeleteUserFromBoardAsync(ObjectId userId, ObjectId boardId);

    }
}
