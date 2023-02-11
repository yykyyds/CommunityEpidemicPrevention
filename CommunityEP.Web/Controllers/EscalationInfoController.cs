using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Service.UtilityService;

namespace CommunityEP.Web.Controllers
{
    [Authorize("Admin")]
    public class EscalationInfoController : Controller
    {
        private readonly VisitApiService visitApiService;

        public EscalationInfoController(VisitApiService visitApiService)
        {
            this.visitApiService = visitApiService;
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<ApiResponse<EscalationInfoDto>> GetEscalationInfos(int page, int limit)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/EscalationInfos/{page}/{limit}", "get", VisitApiService.Token ?? "");
            return visitApiService.DeSerialize<ApiResponse<EscalationInfoDto>>(result);
        }

        [HttpDelete]
        public async Task<bool> DeleteescalationInfo([FromBody] int[] Ids)
        {
            var ids = "";
            foreach (int id in Ids)
                ids += id.ToString() + ",";
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/EscalationInfos?Ids={ids}", "delete", VisitApiService.Token ?? "");
            return visitApiService.DeSerialize<bool>(result);
        }
    }
}
