using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models.ProjectModels
{
    public class ChangeProjectModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
