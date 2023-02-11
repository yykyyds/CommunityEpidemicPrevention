using AutoMapper;
using IRepository;
using IService;
using Models.Dtos;
using Models.Models;

namespace Service
{
    public class PurchaseOrderService : BaseService<PurchaseOrder>, IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository purchaseOrderRepository;
        private readonly IMapper mapper;

        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository, IMapper mapper)
        {
            base.repository = purchaseOrderRepository;
            this.purchaseOrderRepository = purchaseOrderRepository;
            this.mapper = mapper;
        }

        public async Task<PurchaseByUserId> GetPurchaseOrdersByUserIdAsync(string userId)
        {
            PurchaseByUserId purchaseByUser = new PurchaseByUserId();
            var purchaseOrders = await GetPurchaseOrdersWithGoodsByUserIdAsync(userId);
            List<PurchaseDto> records = new List<PurchaseDto>();
            foreach (var item in purchaseOrders)
            {
                records.Add(mapper.Map<PurchaseDto>(item));
            }
            purchaseByUser.Pending = records.Where(p => p.Status == status.审核中).OrderByDescending(p => p.SubmitTime).ToList();
            purchaseByUser.NoComplete = records.Where(p => p.Status == status.未通过).OrderByDescending(p => p.SubmitTime).ToList();
            purchaseByUser.Completed = records.Where(p => p.Status == status.已完成).OrderByDescending(p => p.SubmitTime).ToList();
            return purchaseByUser;
        }

        public async Task<List<PurchaseOrder>> GetPurchaseOrdersWithGoodsByUserIdAsync(string userId)
        {
            return await purchaseOrderRepository.GetPurchaseOrdersWithGoodsByUserIdAsync(userId);
        }

        public async Task<ApiResponse<PurchaseDto>> GetPurchaseDtos(int page, int limit)
        {
            var result = new ApiResponse<PurchaseDto>();
            result.data = await purchaseOrderRepository.GetPurchaseDtos(page, limit, result.count);
            return result;
        }

        public async Task<bool> UpdateAsync(PurchaseDto purchaseDto)
        {
            PurchaseOrder po = (await purchaseOrderRepository.QueryAsync(po => po.Id == purchaseDto.Id)).FirstOrDefault() ?? default!;
            po.DeliveryTime = purchaseDto.DeliveryTime;
            po.Status = purchaseDto.Status;
            return await purchaseOrderRepository.UpdateAsync(po);
        }

        public async new Task<bool> DeleteAsync(string ids)
        {
            string[] Ids = new string[ids.Length];
            string[] Ids1 = new string[ids.Length];
            Ids = ids.Split(',');
            int j = 0;
            for (int i = 0; i < Ids.Length; i++)
                if (Ids[i] != "")
                    Ids1[j++] = Ids[i];
            List<PurchaseOrder> purchaseOrders = await purchaseOrderRepository.QueryAsync(po => Ids1.Contains(po.Id.ToString()));
            return await purchaseOrderRepository.DeleteAsync(purchaseOrders);
        }

        public async Task<int> GetPurchaseCountAsync()
        {
            return await purchaseOrderRepository.GetPurchaseCountAsync();
        }
    }
}
