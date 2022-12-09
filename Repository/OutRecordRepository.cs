using IRepository;
using Models.Dtos;
using Models.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OutRecordRepository : BaseRepository<OutRecord>, IOutRecordRepository
    {
        public async Task<List<OutRecordDto>> QueryWithUserAsync(int page, int limit, RefAsync<int> total)
        {
            //Includes()后面禁使用Select，正确写法: ToList(it=>new <>f__AnonymousType0`5{....})
            var outRecord = await base.Context.Queryable<OutRecord>()
                                .Includes(e => e.user)
                                .ToPageListAsync(page, limit, total, e => new OutRecordDto
                                {
                                    Id = e.Id,
                                    Destination = e.Destination,
                                    OutTime = e.OutTime,
                                    Status = e.Status,
                                    UserName = e.user.NickName
                                });
            return outRecord;
        }

        public async Task<int> GetOutCountAsync()
        {
            return await base.Context.Queryable<OutRecord>().CountAsync();
        }
    }
}
