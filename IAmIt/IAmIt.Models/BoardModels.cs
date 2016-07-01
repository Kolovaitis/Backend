using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace IAmIt.Models
{

    public class AddBoardModel
    {
        public string Name { get; set; }
        public ObjectId UserId { get; set; }
    }

    public class ChangeBoardModel
    {
        public string BoardId { get; set; }
        public string Name { get; set; }
    }

    public class DeleteBoardModel
    {
        public string BoardId { get; set; }
    }

    public class DeleteUserFromBoardModel
    {
        public ObjectId UserId { get; set; }
        public string UserEmail { get; set; }
        public string BoardId { get; set; }
    }

    public class AddUserToBoardModel
    {
        public ObjectId UserId { get; set; }
        public string UserEmail { get; set; }
        public string BoardId { get; set; }
    }

    public class GetBoardModel
    {
        public string BoardId { get; set; }
    }

    public class BoardToSendLightModel
    {
        public string BoardId { get; set; }
        public string Name { get; set; }
    }

    public class BoardToSendFullModel
    {
        public string BoardId { get; set; }
        public string Name { get; set; }
        //List<List>
    }
}
