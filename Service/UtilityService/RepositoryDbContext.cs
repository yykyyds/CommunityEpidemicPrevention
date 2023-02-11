using Models.Models;
using Repository;
using SqlSugar;

namespace Service.UtilityService
{
    public class RepositoryDbContext : SugarUnitOfWork
    {
        public BaseRepository<User> userRepository { get; set; } = default!;
        public BaseRepository<EntryRecord> entryRepository { get; set; } = default!;
        public BaseRepository<PurchaseOrder> purchaseOrderRepository { get; set; } = default!;
        public BaseRepository<Goods> goodsRepository { get; set; } = default!;
        public BaseRepository<EscalationInfo> escalationInfoRepository { get; set; } = default!;
        public BaseRepository<EpidemicInfo> epidemicInfoRepository { get; set; } = default!;
    }
}
