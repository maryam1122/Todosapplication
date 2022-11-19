using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Todolistapplication.Models;

namespace Todolistapplication.Interface
{
    public interface IUser
    {
        Task<User> GetByEmail(string email);
        Task<User> Create (User userInfo);


    }
}
