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
    public interface IEntryRecordService : IBaseService<EntryRecord>
    {
        Task<EntrysByUserId> GetEntrysByUserIdAsync(string userId);
        Task<List<EntryRecordDto>> QueryWithUserAsync(int page, int limit, RefAsync<int> total);
        Task<bool> UpdateAsync(EntryRecordDto entryRecordDto, IUserService userService);
        Task<int> GetEntryCountAsync();
    }
}
