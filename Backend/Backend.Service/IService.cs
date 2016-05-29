using Backend.DbEntities;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface IService
    {
        UserToGetModel GetUserByEmail(UserOnlyEmailModel user);
        UserEntity GetUserEntityByEmail(string email);
        Task Registration(UserRegistrationModel user);
        Task ChangeInfo(UserChangeInfoModel user);
        Task ChangeCredentials(UserChangeCredentialsModel user);
    }
}
