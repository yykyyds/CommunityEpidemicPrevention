using IRepository;
using Models.Dtos;
using Models.Models;

namespace Repository
{
    public class EpidemicInfoRepository : BaseRepository<EpidemicInfo>, IEpidemicInfoRepository
    {
        public override async Task<List<EpidemicInfo>> QueryAsync()
        {
            return await base.QueryAsync(ep => ep.Time >= DateTime.Now.AddDays(-6));
        }

        public async Task<bool> DeleteAsync(int[] Ids)
        {
            return await base.Context.Deleteable<EpidemicInfo>(ep => Ids.Contains(ep.Id)).ExecuteCommandAsync() > 0;
        }
    }
}
