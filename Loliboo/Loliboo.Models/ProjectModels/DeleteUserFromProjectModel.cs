using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loliboo.Models.ProjectModels
{
    public class DeleteUserFromProjectModel
    {
        public string UserEmail { get; set; }
        public ObjectId ProjectId { get; set; }
    }
}
