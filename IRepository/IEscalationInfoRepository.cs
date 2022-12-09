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
    public interface IEscalationInfoRepository : IBaseRepository<EscalationInfo>
    {
        Task<bool> DeleteAsync(List<EscalationInfo> escalationInfos);
        Task<List<EscalationInfoDto>> QueryWithUserNameAsync(int page, int limit, RefAsync<int> total);
    }
}
