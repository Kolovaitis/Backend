using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Abstractions;
using Backend.Database.DbRepositories;
using Backend.DbEntities;
using MongoDB.Bson;

namespace Backend.Service
{
    public class Service : IService
    {

        private IUserRepository _userRepository;
        public Service(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserByEmail(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            return new User { Email = user.Email, Name = user.Name, PasswordHash = user.PasswordHash};
        }

        public UserEntity GetUserEntityByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public async Task Registration(User user)
        {
            await _userRepository.Registration(new UserEntity() { Email = user.Email, Name = user.Name, PasswordHash = user.PasswordHash, _id = new ObjectId() });
        }
    }
}
