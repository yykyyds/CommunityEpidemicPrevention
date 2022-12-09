using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Models;

namespace CommunityEP.Api.Controllers
{
    //Authorize标签子类和父类用是一样的效果
    [Authorize("Ordinary")]
    [Route("api/[controller]")]
    [ApiController]
    public class EntryRecordsController : ControllerBase
    {
        private readonly IEntryRecordService entryRecordService;

        public EntryRecordsController(IEntryRecordService entryRecordService)
        {
            this.entryRecordService = entryRecordService;
        }

        [HttpPost]
        public async Task<bool> AddEntryRecord([FromBody]EntryRecord entryRecord)
        {
            return await entryRecordService.AddAsync(entryRecord);
        }

        [HttpGet("User/{userId?}")]
        public async Task<EntrysByUserId> GetEntryRecordsByUserId(string userId)
        {
            return await entryRecordService.GetEntrysByUserIdAsync(userId);
        }

        [Authorize("Admin")]
        [HttpGet("{page?}/{limit?}")]
        public async Task<ApiResponse<EntryRecordDto>> GetEntrys(int page, int limit)
        {
            var result = new ApiResponse<EntryRecordDto>();
            result.data = await entryRecordService.QueryWithUserAsync(page, limit, result.count);
            return result;
        }

        [Authorize("Admin")]
        [HttpPut]
        public async Task<bool> UpdateEntry([FromBody] EntryRecordDto entryRecordDto, [FromServices] IUserService userService)
        {
            return await entryRecordService.UpdateAsync(entryRecordDto, userService);
        }

        [Authorize("Admin")]
        [HttpDelete]
        public async Task<bool> DeleteEntry([FromQuery] string Ids)
        {
            return await entryRecordService.DeleteAsync(Ids);
        }

    }
}
