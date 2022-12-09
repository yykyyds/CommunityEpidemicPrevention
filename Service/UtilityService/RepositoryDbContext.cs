using Models.Models;
using Repository;
using SqlSugar;

namespace Service.UtilityService
{
    public class RepositoryDbContext : SugarUnitOfWork
    {
        public BaseRepository<User> userRepository { get; set; }
        public BaseRepository<EntryRecord> entryRepository { get; set; }
        public BaseRepository<PurchaseOrder> purchaseOrderRepository { get; set; }
        public BaseRepository<Goods> goodsRepository { get; set; }
        public BaseRepository<EscalationInfo> escalationInfoRepository { get; set; }
        public BaseRepository<EpidemicInfo> epidemicInfoRepository { get; set; }
    }
}
