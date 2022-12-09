using Models.Dtos;
using Models.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IOutRecordService : IBaseService<OutRecord>
    {
        Task<OutsByUserId> GetOutsByUserIdAsync(string userId);
        Task<List<OutRecordDto>> QueryWithUserAsync(int page, int limit, RefAsync<int> total);
        Task<bool> UpdateAsync(OutRecordDto entryRecordDto, IUserService userService);
        Task<int> GetOutCountAsync();
    }
}
