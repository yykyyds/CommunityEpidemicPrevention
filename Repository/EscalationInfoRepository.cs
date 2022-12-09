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
    public class EscalationInfoRepository : BaseRepository<EscalationInfo>, IEscalationInfoRepository
    {
        public Task<List<EscalationInfoDto>> QueryWithUserNameAsync(int page, int limit, RefAsync<int> total)
        {
            var escalationInfos = base.Context.Queryable<EscalationInfo>()
                                        .Includes<User>(ei => ei.user)
                                        .ToPageListAsync(page, limit, total, ei => new EscalationInfoDto()
                                        {
                                            Id = ei.Id,
                                            CurrentLocation = ei.CurrentLocation,
                                            Time = ei.Time,
                                            HealthCodeColor = ei.HealthCodeColor,
                                            HealthStasus = ei.HealthStasus,
                                            TravelCardStatus = ei.TravelCardStatus,
                                            UserName = ei.user.NickName
                                        });
            return escalationInfos;
        }
    }
}
