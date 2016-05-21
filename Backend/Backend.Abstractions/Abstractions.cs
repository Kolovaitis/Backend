using Backend.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Abstractions
{
    public abstract class Entity
    {
        public string Name { get; set; }
    }

    public class User : Entity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

    }

    public interface IUserRepository
    {
        UserEntity GetUserByEmail(string email);
        Task Registration(UserEntity user); 
    }
}
