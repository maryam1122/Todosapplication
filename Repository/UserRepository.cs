using Microsoft.EntityFrameworkCore;
using Todolistapplication.Interface;
using Todolistapplication.Models;

namespace Todolistapplication.Repository
{
    public class UserRepository:IUser
    {
        readonly TodolistDbContext _dbContext;

        public UserRepository(TodolistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Create(User userInfo)
        {
          _dbContext.userInfos?.Add(userInfo);
          await  _dbContext.SaveChangesAsync();
          return userInfo;
        }
       public async Task<User> GetByEmail(string email)
        {
            return await (_dbContext.userInfos ?? throw new InvalidOperationException()).FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
