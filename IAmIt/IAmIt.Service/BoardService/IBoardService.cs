using IAmIt.DbEntity.DbEntity;
using IAmIt.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Service.BoardService
{
    public interface IBoardService
    {
        Task<ICollection<BoardToSendLightModel>> GetMyBoardsAsync(ObjectId userId);
        Task<BoardToSendFullModel> GetBoardAsync(GetBoardModel model);
        Task<ObjectId> AddBoardAsync(AddBoardModel model);
        Task DeleteBoardAsync(DeleteBoardModel model);
        Task ChangeBoardAsync(ChangeBoardModel model);
        Task AddUserToBoardAsync(AddUserToBoardModel model);
        Task DeleteUserFromBoardAsync(DeleteUserFromBoardModel model);
        Task DeleteYourselfFromBoardAsync(DeleteYourselfFromBoardModel model);
        Task<ICollection<ObjectId>> GetUsersInBoardAsync(GetUsersInBoardModel model);
    }
}
