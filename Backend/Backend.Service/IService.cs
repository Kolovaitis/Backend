using Backend.Abstractions;
using Backend.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface IService
    {
        User GetUserByEmail(string email);
        UserEntity GetUserEntityByEmail(string email);
        Task Registration(User user);
        Task ChangeInfo(User user);
        Task ChangeCredentials(User user);
    }
}
