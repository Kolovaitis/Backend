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

        public async Task ChangeCredentials(UserEntity user)
        {
            var changedUser = await _context.users.Find(u => u.Email == user.Email).FirstOrDefaultAsync();

            user.PasswordHash = user.PasswordHash ?? changedUser.PasswordHash;
            user.PasswordSalt = user.PasswordSalt ?? changedUser.PasswordSalt;
            user.Email = user.Email ?? changedUser.Email;
            user.Name = user.Name ?? changedUser.Name;
            user._id = user._id ?? changedUser.Name;

            _context.users.DeleteOne(u => u.Email == user.Email);
            _context.users.InsertOne(user);
        }

        public async Task ChangeInfo(UserEntity user)
        {
            var changedUser = await _context.users.Find(u => u.Email == user.Email).FirstOrDefaultAsync();

            user.Name = user.Name ?? changedUser.Name;

            _context.users.DeleteOne(u => u.Email == user.Email);
            _context.users.InsertOne(user);
        }

        public UserEntity GetUserByEmail(string email)
        {
            return _context.users.Find(u => u.Email == email).FirstOrDefault();
        }



        public async Task Registration(UserEntity user)
        {
            var hashResult = _passwordHasher.GenerateHash(user.PasswordHash);
            user.PasswordHash = hashResult.Hash;
            user.PasswordSalt = hashResult.Salt;
            await _context.users.InsertOneAsync(user);
        }
    }
}
