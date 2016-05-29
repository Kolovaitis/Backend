using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Database.DbRepositories;
using Backend.DbEntities;
using MongoDB.Bson;
using Backend.RepositoryAbstractions;
using Backend.Models;

namespace Backend.Service
{
    public class Service : IService
    {

        private IUserRepository _userRepository;
        public Service(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ChangeCredentials(UserChangeCredentialsModel user)
        {
            if (_userRepository.GetUserByEmail(user.Email) != null)
                throw new Exception("user with this email already exist");
            var entity = new UserEntity
            {
                Email = user.Email,
            };
            var newPassword = user.PasswordHash;
            try {
                await _userRepository.ChangeCredentials(user.OldPassword, newPassword, entity);
            } catch (Exception e)
            {
                throw new Exception (e.Message);
            }
        }

        public async Task ChangeInfo(UserChangeInfoModel user)
        {
            var entity = new UserEntity
            {
                Email = user.Email,
                Name = user.Name
                
            };
            await _userRepository.ChangeInfo(entity);
        }

        public UserToGetModel GetUserByEmail(UserOnlyEmailModel user)
        {
            var _user = _userRepository.GetUserByEmail(user.Email);
            if (_user == null)
                throw new Exception ("invalid email");
            return new UserToGetModel { Email = _user.Email, Name = _user.Name};
        }

        public UserEntity GetUserEntityByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public async Task Registration(UserRegistrationModel user)
        {
            if (_userRepository.GetUserByEmail(user.Email) != null)
                throw new Exception("user with this email already exist");
            await _userRepository.Registration(new UserEntity() { Email = user.Email, Name = user.Name, PasswordHash = user.PasswordHash, _id = new ObjectId() });
        }
    }
}
