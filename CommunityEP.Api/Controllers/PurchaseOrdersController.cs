using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Models;
using Service;

namespace CommunityEP.Api.Controllers
{
    [Authorize("Ordinary")]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderService purchaseOrderService;

        public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService)
        {
            this.purchaseOrderService = purchaseOrderService;
        }

        [HttpPost]
        public async Task<bool> AddPurchaseOrder([FromBody]PurchaseOrder purchaseOrder)
        {
            return await purchaseOrderService.AddAsync(purchaseOrder);
        }

        [HttpGet("User/{userId?}")]
        public async Task<PurchaseByUserId> GetPurchaseOrdersByUserId(string userId)
        {
            return await purchaseOrderService.GetPurchaseOrdersByUserIdAsync(userId);
        }

        [Authorize("Admin")]
        [HttpGet("{page}/{limit}")]
        public async Task<ApiResponse<PurchaseDto>> GetPurchases(int page, int limit)
        {
            return await purchaseOrderService.GetPurchaseDtos(page,limit);
        }

        [Authorize("Admin")]
        [HttpPut]
        public async Task<bool> UpdatePurchase([FromBody] PurchaseDto purchaseDto)
        {
            return await purchaseOrderService.UpdateAsync(purchaseDto);
        }

        [Authorize("Admin")]
        [HttpDelete]
        public async Task<bool> DeletePurchase([FromQuery] string Ids)
        {
            return await purchaseOrderService.DeleteAsync(Ids);
        }
    }
}
