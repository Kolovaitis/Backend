using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmIt.Models
{
    public class AddCardModel
    {
        public string Name { get; set; }
        public string ColumnId { get; set; }
    }

    public class DeleteCardModel
    {
        public string CardId { get; set; }
    }

    public class ChangeCardModel
    {
        public string CardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class MoveCardInOtherColumnModel
    {
        public string CardId { get; set; }
        public string NewColumnId { get; set; }
        public int NewPosition { get; set; }
    }

    public class MoveCardModel
    {
        public string CardId { get; set; }
        public int NewPosition { get; set; }
    }

    public class AddUserToCardModel
    {
        public string CardId { get; set; }
        public string UserEmail { get; set; }
    }

    public class DeleteUserFromCardModel
    {
        public string CardId { get; set; }
        public string UserEmail { get; set; }
    }

    public class DeleteYourselfFromCardModel
    {
        public string CardId { get; set; }
        public string UserEmail { get; set; }
    }

    public class GetCardModel
    {
        public string CardId { get; set; }
    }

    public class GetUsersInCardModel
    {
        public string CardId { get; set; }
    }

    public class CardToSendLightModel
    {
        public string ColumnId { get; set; }
        public string Name { get; set; }
    }

    public class CardToFullLightModel
    {
        public string ColumnId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
