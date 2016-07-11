using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.Models;
using MongoDB.Bson;
using IAmIt.Database.EntityFramework.DbRepository;
using IAmIt.DbEntity.DbEntity;

namespace IAmIt.Service.ColumnService
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRepository _columnRepository;

        public ColumnService(IColumnRepository columnRepository)
        {
            _columnRepository = columnRepository;
        }
        public async Task<ObjectId> AddColumnAsync(AddColumnModel model)
        {
            var id = ObjectId.GenerateNewId();
            if (await _columnRepository.GetColumnAsync(id) != null)
            {
                return await AddColumnAsync(model);
            }
            var column = new Column
            {
                Id = id,
                Name = model.Name,
                BoardId = new ObjectId(model.BoardId),
                Position = -1
            };
            await _columnRepository.AddColumnAsync(column);
            return id;
        }

        public async Task ChangeColumnAsync(ChangeColumnModel model)
        {
            var id = new ObjectId(model.ColumnId);
            var column = (await _columnRepository.GetColumnAsync(new ObjectId(model.ColumnId)));
            column.Name = model.Name;
            await _columnRepository.ChangeColumnAsync(column);
        }

        public async Task DeleteColumnAsync(DeleteColumnModel model)
        {
            await _columnRepository.DeleteColumnAsync(new ObjectId(model.ColumnId));
        }

        public async Task MoveColumnAsync(MoveColumnModel model)
        {
            await _columnRepository.MoveColumnAsync(new ObjectId(model.ColumnId), model.NewPosition);
        }
    }
}
