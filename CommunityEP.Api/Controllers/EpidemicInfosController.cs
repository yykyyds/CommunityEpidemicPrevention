using IdentityModel;
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
    public class EpidemicInfosController : ControllerBase//BaseApiController<EpidemicInfo>
    {
        private readonly IEpidemicInfoService epidemicInfoService;

        public EpidemicInfosController(IEpidemicInfoService epidemicInfoService)
        {
            this.epidemicInfoService = epidemicInfoService;
        }

        [HttpGet("{page?}/{limit?}")]
        [Authorize("Admin")]
        public async Task<ApiResponse<EpidemicInfo>> GetEpidemicInfos(int page, int limit)
        {
            var result = new ApiResponse<EpidemicInfo>();
            result.data = await epidemicInfoService.QueryAsync(page, limit, result.count);
            return result;
        }

        [HttpPost]
        [Authorize("Admin")]
        public async Task<bool> AddEpidemicInfo([FromBody] EpidemicInfo epidemicInfo)
        {
            return await epidemicInfoService.AddAsync(epidemicInfo);
        }

        [HttpPut]
        [Authorize("Admin")]
        public async Task<bool> UpdateEpidemicInfo([FromBody] EpidemicInfo epidemicInfo)
        {
            return await epidemicInfoService.UpdateAsync(epidemicInfo);
        }

        [HttpDelete]
        [Authorize("Admin")]
        public async Task<bool> DeleteEpidemicInfo([FromQuery] string Ids)
        {
            return await epidemicInfoService.DeleteAsync(Ids);
        }

    }
}
