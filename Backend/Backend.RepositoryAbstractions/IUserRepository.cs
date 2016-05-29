using Backend.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.RepositoryAbstractions
{
    public interface IUserRepository
    {
        UserEntity GetUserByEmail(string email);
        Task Registration(UserEntity user);
        Task ChangeInfo(UserEntity user);
        Task ChangeCredentials(string oldPassword, string newPassword, UserEntity user);
    }
}
