using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Service.UtilityService;

namespace CommunityEP.Web.Controllers
{
    [Authorize("Admin")]
    public class OutController : Controller
    {
        private readonly VisitApiService visitApiService;

        public OutController(VisitApiService visitApiService)
        {
            this.visitApiService = visitApiService;
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<ApiResponse<OutRecordDto>> GetOuts(int page, int limit)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/OutRecords/{page}/{limit}", "get", VisitApiService.Token ?? "");
            return visitApiService.DeSerialize<ApiResponse<OutRecordDto>>(result);
        }

        [HttpPut]
        public async Task<bool> UpdateOut([FromBody] OutRecordDto entryRecordDto)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/OutRecords", "put", VisitApiService.Token ?? "", entryRecordDto);
            return visitApiService.DeSerialize<bool>(result);
        }

        [HttpDelete]
        public async Task<bool> DeleteOut([FromBody] int[] Ids)
        {
            var ids = "";
            foreach (int id in Ids)
                ids += id.ToString() + ",";
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/OutRecords?Ids={ids}"
                , "delete", VisitApiService.Token ?? "");
            return visitApiService.DeSerialize<bool>(result);
        }
    }
}
