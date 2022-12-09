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
    public class OutRecordsController : ControllerBase
    {
        private readonly IOutRecordService outRecordService;

        public OutRecordsController(IOutRecordService outRecordService)
        {
            this.outRecordService = outRecordService;
        }

        [HttpPost]
        public async Task<bool> AddEntryRecord([FromBody] OutRecord outRecord)
        {
            return await outRecordService.AddAsync(outRecord);
        }

        [HttpGet("User/{userId?}")]
        public async Task<OutsByUserId> GetOutRecordsByUserId(string userId)
        {
            return await outRecordService.GetOutsByUserIdAsync(userId);
        }

        [Authorize("Admin")]
        [HttpGet("{page?}/{limit?}")]
        public async Task<ApiResponse<OutRecordDto>> GetEntrys(int page, int limit)
        {
            var result = new ApiResponse<OutRecordDto>();
            result.data = await outRecordService.QueryWithUserAsync(page, limit, result.count);
            return result;
        }

        [Authorize("Admin")]
        [HttpPut]
        public async Task<bool> UpdateOut([FromBody] OutRecordDto outRecordDto, [FromServices] IUserService userService)
        {
            return await outRecordService.UpdateAsync(outRecordDto, userService);
        }

        [Authorize("Admin")]
        [HttpDelete]
        public async Task<bool> DeleteOut([FromQuery] string Ids)
        {
            return await outRecordService.DeleteAsync(Ids);
        }
    }
}
