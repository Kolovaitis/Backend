using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace IAmIt.Models
{
    public class AddColumnModel
    {
        public string Name { get; set; }
        public string BoardId { get; set; }
    }

    public class DeleteColumnModel
    {
        public string ColumnId { get; set; }
    }

    public class ChangeColumnModel
    {
        public string ColumnId { get; set; }
        public string Name { get; set; }
    }

    public class MoveColumnModel
    {
        public string ColumnId { get; set; }
        public int NewPosition { get; set; }
    }

    public class ColumnToSendModel
    {
        public string ColumnId { get; set; }
        public string Name { get; set; }
        public ICollection<CardToSendLightModel> Cards { get; set; }
    }
}
