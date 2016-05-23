using Backend.Abstractions;
using Backend.Database.DbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.DbEntities;
using MongoDB.Driver;
using Backend.PasswordHasher;

namespace Backend.Database.DbRepositories
{
    public class DbUserRepository : IUserRepository
    {
        private readonly MongoDbContext _context;
        private IPasswordHasher _passwordHasher;
        public DbUserRepository(IDbContextFactory<MongoDbContext> contextFactory, IPasswordHasher passwordHasher)
        {
            _context = contextFactory.Create();
            _passwordHasher = passwordHasher;
        }

        public async Task ChangeCredentials(string oldPassword, string newPassword, UserEntity user)
        {
            var changedUser = _context.users.AsQueryable().ToList().FirstOrDefault(u => _passwordHasher.VerifyHash(oldPassword, u.PasswordHash, u.PasswordSalt));
            if (changedUser != null)
            {
                if (newPassword != null)
                {
                    var newHash = _passwordHasher.GenerateHash(newPassword);
                    user.PasswordHash = newHash.Hash;
                    user.PasswordSalt = newHash.Salt;
                } else
                {
                    user.PasswordHash = changedUser.PasswordHash;
                    user.PasswordSalt = changedUser.PasswordSalt;
                }
                if (_context.users.Find(u => u.Email == user.Email).FirstOrDefault() != null)
                    user.Email = changedUser.Email;
                else
                    user.Email = user.Email ?? changedUser.Email;
                user.Name = changedUser.Name;
                user._id = changedUser._id;

                _context.users.DeleteOne(u => u._id == changedUser._id);
                _context.users.InsertOne(user);
            } else
            {
                throw new Exception("specific wildfowl: incorrect password");
            }
        }

        public async Task ChangeInfo(UserEntity user)
        {
            var changedUser = await _context.users.Find(u => u.Email == user.Email).FirstOrDefaultAsync();

            user.PasswordHash = changedUser.PasswordHash;
            user.PasswordSalt = changedUser.PasswordSalt;
            user.Email = changedUser.Email;
            user.Name = user.Name ?? changedUser.Name;
            user._id = changedUser._id;

            _context.users.DeleteOne(u => u.Email == user.Email);
            _context.users.InsertOne(user);
        }

        public UserEntity GetUserByEmail(string email)
        {
            return _context.users.Find(u => u.Email == email).FirstOrDefault();
        }

        public async Task Registration(UserEntity user)
        {
            if (_context.users.Find(u => u.Email == user.Email).FirstOrDefault() != null)
                throw new Exception("specific wildfowl: this user already exists");
            var hashResult = _passwordHasher.GenerateHash(user.PasswordHash);
            user.PasswordHash = hashResult.Hash;
            user.PasswordSalt = hashResult.Salt;
            await _context.users.InsertOneAsync(user);
        }
    }
}
