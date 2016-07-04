using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.DbEntity.DbEntity;
using IAmIt.Models;
using MongoDB.Bson;
using IAmIt.Database.EntityFramework.DbRepository;

namespace IAmIt.Service.BoardService
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }
        public async Task<ObjectId> AddBoardAsync(AddBoardModel model)
        {
            var id = ObjectId.GenerateNewId();
            if (await _boardRepository.GetBoardAsync(id) != null)
            {
                return await AddBoardAsync(model);
            }
            var board = new Board
            {
                Id = id,
                Name = model.Name,
                ProjectId = 
            };
            await _boardRepository.AddBoardAsync(board);
            return id;
        }

        public async Task AddUserToBoardAsync(AddUserToBoardModel model)
        {
            var id = new ObjectId(model.BoardId);
            await _boardRepository.AddUserToBoardAsync(model.UserId, id);
        }

        public async Task ChangeBoardAsync(ChangeBoardModel model)
        {
            var id = new ObjectId(model.BoardId);
            var board = (await _boardRepository.GetBoardAsync(id));
            board.Name = model.Name;
            await _boardRepository.ChangeBoardAsync(board);
        }

        public async Task DeleteBoardAsync(DeleteBoardModel model)
        {
            var id = new ObjectId(model.BoardId);
            await _boardRepository.DeleteBoardAsync(id);
        }

        public async Task DeleteUserFromBoardAsync(DeleteUserFromBoardModel model)
        {
            var id = new ObjectId(model.BoardId);
            await _boardRepository.DeleteUserFromBoardAsync(model.UserId, id);
        }

        public async Task DeleteYourselfFromBoardAsync(DeleteUserFromBoardModel model)
        {
            await _boardRepository.DeleteUserFromBoardAsync(model.UserId,new ObjectId(model.BoardId));
        }

        public async Task<BoardToSendFullModel> GetBoardAsync(GetBoardModel model)
        {
            var id = new ObjectId(model.BoardId);
            var board = (await _boardRepository.GetBoardAsync(id));
            return new BoardToSendFullModel
            {
                BoardId = board.Id.ToString(),
                Name = board.Name
            };
        }

        public async Task<ICollection<BoardToSendLightModel>> getMyBoardsAsync(GetMyBoardsModel model)
        { 
            return (await _boardRepository.GetBoardsByUserAsync(new ObjectId(model.UserId)))
                .Select(g => new BoardToSendLightModel { BoardId = g.Id.ToString(), Name = g.Name }).ToList();
        }
    }
}
