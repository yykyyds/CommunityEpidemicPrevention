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
    public class PurchaseOrderRepository : BaseRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        public async Task<List<PurchaseOrder>> GetPurchaseOrdersWithGoodsByUserIdAsync(string userId)
        {
            var purchaseOrders = await base.Context.Queryable<PurchaseOrder>()
                                    .Includes(p => p.goodsList).ToListAsync();
            return purchaseOrders;
        }

        public new async Task<bool> AddAsync(PurchaseOrder order)
        {
            var result = await base.Context.InsertNav(order)
                                            .Include(po => po.goodsList).ExecuteCommandAsync();
            return result;
        }

        public async Task<List<PurchaseDto>> GetPurchaseDtos(int page, int limit, RefAsync<int> total)
        {
            return await base.Context.Queryable<PurchaseOrder>()
                        .Includes<User>(po => po.user)
                        .ToPageListAsync(page, limit, total, po => new PurchaseDto()
                        {
                            Id = po.Id,
                            SubmitTime = po.SubmitTime,
                            DeliveryTime = po.DeliveryTime,
                            Address = po.Address,
                            Status = po.Status,
                            UserName = po.user.NickName
                        });
        }

        public async Task<bool> DeleteAsync(List<PurchaseOrder> entryRecords)
        {
            var result = true;
            foreach (var item in entryRecords)
            {
                result = result && await base.Context.DeleteNav<PurchaseOrder>(po => po.Id == item.Id)
                        .Include<Goods>(er => er.goodsList)
                        .ExecuteCommandAsync();
            }
            return result;
        }

        public async Task<int> GetPurchaseCountAsync()
        {
            return await base.Context.Queryable<PurchaseOrder>().CountAsync();
        }
    }
}
