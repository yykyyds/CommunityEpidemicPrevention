using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Models.Dtos;
using Models.Models;
using Service.UtilityService;

namespace CommunityEP.Web.Controllers
{
    [Authorize("Admin")]
    public class EntryController : Controller
    {
        private readonly VisitApiService visitApiService;

        public EntryController(VisitApiService visitApiService)
        {
            this.visitApiService = visitApiService;
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<ApiResponse<EntryRecordDto>> GetEntrys(int page,int limit)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/EntryRecords/{page}/{limit}", "get", VisitApiService.Token ?? "");
            return visitApiService.DeSerialize<ApiResponse<EntryRecordDto>>(result);
        }

        [HttpPut]
        public async Task<bool> UpdateEntry([FromBody]EntryRecordDto entryRecordDto)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/EntryRecords", "put", VisitApiService.Token ?? "", entryRecordDto);
            return visitApiService.DeSerialize<bool>(result);
        }

        [HttpDelete]
        public async Task<bool> DeleteEntry([FromBody] int[] Ids)
        {
            var ids = "";
            foreach (int id in Ids)
                ids += id.ToString() + ",";
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/EntryRecords?Ids={ids}"
                , "delete", VisitApiService.Token ?? "");
            return visitApiService.DeSerialize<bool>(result);
        }
    }
}
