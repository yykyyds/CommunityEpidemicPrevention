using Models.Dtos;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> DeleteUserByNamesAsync(string[] names);
        Task<bool> UpdateUserNoEntityAsync(Dictionary<string, object> dt);
        Task<int> GetUserCountAsync();
    }
}
