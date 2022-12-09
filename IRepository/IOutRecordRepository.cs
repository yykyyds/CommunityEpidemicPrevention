using Models.Dtos;
using Models.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IOutRecordRepository : IBaseRepository<OutRecord>
    {
        Task<List<OutRecordDto>> QueryWithUserAsync(int page, int limit, RefAsync<int> total);
        Task<bool> DeleteAsync(List<OutRecord> outRecordDtos);
        Task<int> GetOutCountAsync();
    }
}
