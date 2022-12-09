using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Dtos;
using Service;
using Service.UtilityService;
using System.Collections.Generic;

namespace CommunityEP.Web.Controllers
{
    [Authorize("Admin")]
    public class PurchaseController : Controller
    {
        private readonly VisitApiService visitApiService;

        public PurchaseController(VisitApiService visitApiService)
        {
            this.visitApiService = visitApiService;
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<ApiResponse<PurchaseDto>> GetPurchases(int page,int limit)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/PurchaseOrders/{page}/{limit}", "get", VisitApiService.Token);
            return visitApiService.DeSerialize<ApiResponse<PurchaseDto>>(result);
        }

        [HttpPut]
        public async Task<bool> UpdatePurchase([FromBody]PurchaseDto purchaseDto)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/PurchaseOrders", "put", VisitApiService.Token, purchaseDto);
            return visitApiService.DeSerialize<bool>(result);
        }

        [HttpDelete]
        public async Task<bool> DeletePurchase([FromBody] int[] Ids)
        {
            var ids = "";
            foreach (int id in Ids)
                ids += id.ToString() + ",";
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/PurchaseOrders?Ids={ids}", "delete", VisitApiService.Token);
            return visitApiService.DeSerialize<bool>(result);
        }
    }
}
