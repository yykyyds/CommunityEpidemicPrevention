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
    public interface IPurchaseOrderRepository : IBaseRepository<PurchaseOrder>
    {
        Task<List<PurchaseOrder>> GetPurchaseOrdersWithGoodsByUserIdAsync(string userId);
        Task<List<PurchaseDto>> GetPurchaseDtos(int page, int limit, RefAsync<int> total);
        Task<bool> DeleteAsync(List<PurchaseOrder> entryRecords);
        Task<int> GetPurchaseCountAsync();
    }
}
