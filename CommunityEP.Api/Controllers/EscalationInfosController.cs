using AutoMapper;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Models;

namespace CommunityEP.Api.Controllers
{
    [Authorize("Ordinary")]
    [Route("api/[controller]")]
    [ApiController]
    public class EscalationInfosController : ControllerBase
    {
        private readonly IEscalationInfoService escalationInfoService;
        private readonly IMapper mapper;

        public EscalationInfosController(IEscalationInfoService escalationInfoService, [FromServices] IMapper mapper)
        {
            this.escalationInfoService = escalationInfoService;
            this.mapper = mapper;
        }

        [Authorize("Admin")]
        [HttpGet("{page}/{limit}")]
        public async Task<ApiResponse<EscalationInfoDto>> GetEscalationInfos(int page, int limit)
        {
            var result = new ApiResponse<EscalationInfoDto>();
            result.data = await escalationInfoService.QueryWithUserNameAsync(page, limit, result.count);
            return result;
        }

        [HttpPost]
        public async Task<bool> AddEscalationInfo([FromBody]EscalationInfo escalationInfo)
        {
            return await escalationInfoService.AddAsync(escalationInfo);
        }

        [Authorize("Admin")]
        [HttpDelete]
        public async Task<bool> DeleteEscalationInfo([FromQuery] string Ids)
        {
            return await escalationInfoService.DeleteAsync(Ids);
        }
    }
}
