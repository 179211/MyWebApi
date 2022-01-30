using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsersAsync();
        bool IsUniqueUser(string username);
        User Register(string username, string password);
        Task<User> Authenticate(string username, string password);

    }
}
