using IAmIt.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Service.ColumnService
{
    public interface IColumnService
    {
        Task<ObjectId> AddColumnAsync(AddColumnModel model);
        Task DeleteColumnAsync(DeleteColumnModel model);
        Task ChangeColumnAsync(ChangeColumnModel model);
        Task MoveColumnAsync(MoveColumnModel model);
    }
}
