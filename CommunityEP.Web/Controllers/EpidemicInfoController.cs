using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Dtos;
using Models.Models;
using Service.UtilityService;
using System.Collections.Generic;

namespace CommunityEP.Web.Controllers
{
    [Authorize("Admin")]
    public class EpidemicInfoController : Controller
    {
        private readonly VisitApiService visitApiService;

        public EpidemicInfoController(VisitApiService visitApiService)
        {
            this.visitApiService = visitApiService;
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<ApiResponse<EpidemicInfo>> GetEpidemicInfos(int page, int limit)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/EpidemicInfos/{page}/{limit}", "get", VisitApiService.Token ?? "");
            return visitApiService.DeSerialize<ApiResponse<EpidemicInfo>>(result);
        }

        [HttpPost]
        public async Task<bool> AddEpidemicInfo([FromBody]EpidemicInfo epidemicInfo)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/EpidemicInfos", "post", VisitApiService.Token ?? "", epidemicInfo);
            return visitApiService.DeSerialize<bool>(result);
        }

        [HttpPut]
        public async Task<bool> UpdateEpidemicInfo([FromBody]EpidemicInfo epidemicInfo)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/EpidemicInfos", "put", VisitApiService.Token ?? "", epidemicInfo);
            return visitApiService.DeSerialize<bool>(result);
        }

        [HttpDelete]
        public async Task<bool> DeleteEpidemicInfo([FromBody] int[] Ids)
        {
            var ids = "";
            foreach (int id in Ids)
                ids += id.ToString() + ",";
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/EpidemicInfos?Ids={ids}"
                , "delete", VisitApiService.Token ?? "");
            return visitApiService.DeSerialize<bool>(result);
        }
    }
}
