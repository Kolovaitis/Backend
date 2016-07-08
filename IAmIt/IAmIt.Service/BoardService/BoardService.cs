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
        private readonly IColumnRepository _columnRepository;
        private readonly ICardRepository _cardRepository;

        public BoardService(IBoardRepository boardRepository, IColumnRepository columnRepository, ICardRepository cardRepository)
        {
            _boardRepository = boardRepository;
            _columnRepository = columnRepository;
            _cardRepository = cardRepository;
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
                ProjectId =  new ObjectId(model.ProjectId)
            };
            await _boardRepository.AddBoardAsync(board);
            await _boardRepository.AddUserToBoardAsync(model.UserId, id);
            return id;
        }

        public async Task AddUserToBoardAsync(AddUserToBoardModel model)
        {
            if ((await _boardRepository.GetUsersInBoardAsync(new ObjectId(model.BoardId))).Contains(model.UserId))
            {
                throw new Exception("This user is already a member of the board");
            }
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

        public async Task DeleteYourselfFromBoardAsync(DeleteYourselfFromBoardModel model)
        {
            if ((await _boardRepository.GetUsersInBoardAsync(new ObjectId(model.BoardId))).AsQueryable().Count() == 1)
            {
                throw new Exception
                    ("You are the last member of the board. If you really want to delete yourself, delete the whole board");
            }
            await _boardRepository.DeleteUserFromBoardAsync(model.UserId,new ObjectId(model.BoardId));
        }

        public async Task<BoardToSendFullModel> GetBoardAsync(GetBoardModel model)
        {
            var id = new ObjectId(model.BoardId);
            var board = (await _boardRepository.GetBoardAsync(id));
            return new BoardToSendFullModel
            {
                BoardId = board.Id.ToString(),
                Name = board.Name,
                Columns = (await _columnRepository.GetAllColumnsInBoardAsync(board.Id))
                    .Select(c => new ColumnToSendModel
                    {
                        ColumnId = c.Id.ToString(),
                        Name = c.Name,
                        Position = c.Position,
                        //строчкой ниже - вселенское зло
                        Cards = _cardRepository.GetAllCardsByUserAsync(c.Id).Result
                    .Select(card => new CardToSendLightModel { Name = card.Name, Position = card.Position, ColumnId = card.ColumnId.ToString() }).ToList()
                    }).ToList()
            };
        }

        public async Task<ICollection<BoardToSendLightModel>> GetMyBoardsAsync(ObjectId userId)
        { 
            return (await _boardRepository.GetBoardsByUserAsync(userId))
                .Select(g => new BoardToSendLightModel { BoardId = g.Id.ToString(), Name = g.Name }).ToList();
        }

        public async Task<ICollection<ObjectId>> GetUsersInBoardAsync(GetUsersInBoardModel model)
        {
            return (await _boardRepository.GetUsersInBoardAsync(new ObjectId(model.BoardId)));
        }
    }
}
