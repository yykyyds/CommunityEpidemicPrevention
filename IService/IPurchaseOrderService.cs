using Models.Dtos;
using Models.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IPurchaseOrderService : IBaseService<PurchaseOrder>
    {
        Task<PurchaseByUserId> GetPurchaseOrdersByUserIdAsync(string userId);
        Task<List<PurchaseOrder>> GetPurchaseOrdersWithGoodsByUserIdAsync(string userId);
        Task<ApiResponse<PurchaseDto>> GetPurchaseDtos(int page, int limit);
        Task<bool> DeleteAsync(string ids);
        Task<bool> UpdateAsync(PurchaseDto purchaseDto);
        Task<int> GetPurchaseCountAsync();
    }
}
