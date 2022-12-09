using IRepository;
using Models.Dtos;
using Models.Models;
using SqlSugar;

namespace Repository
{
    public class EntryRecordRepository : BaseRepository<EntryRecord>, IEntryRecordRepository
    {
        public async Task<List<EntryRecordDto>> QueryWithUserAsync(int page, int limit, RefAsync<int> total)
        {
            //Includes()后面禁使用Select，正确写法: ToList(it=>new <>f__AnonymousType0`5{....})
            var entryRecord = await base.Context.Queryable<EntryRecord>()
                                .Includes(e => e.user)
                                .ToPageListAsync(page,limit,total, e => new EntryRecordDto
                                {
                                    Id = e.Id,
                                    Destination = e.Destination,
                                    EntryTime = e.EntryTime,
                                    Status = e.Status,
                                    UserName = e.user.NickName
                                });
            return entryRecord;
        }

        public async Task<int> GetEntryCountAsync()
        {
            return await base.Context.Queryable<EntryRecord>().CountAsync();
        }
    }
}
