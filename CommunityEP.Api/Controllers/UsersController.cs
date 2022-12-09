using AutoMapper;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Service.UtilityService;

namespace CommunityEP.Api.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]//在帮助界面不显示，但是还是可以调用
    //[Area("MobileTerminal")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Ordinary")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService,[FromServices] IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet("{userId}/AllApply")]
        public async Task<GetAllApplyByUserId> AllApplyByUserId(string userId, [FromServices]IOutRecordService outRecordService
                            , [FromServices] IEntryRecordService entryService
                            , [FromServices] IPurchaseOrderService purchaseService)
        {
            return await userService.GetAllApplyByUserIdAsync(userId,outRecordService,entryService,purchaseService);
        }

        [HttpPut("UserUpload")]
        public async Task<bool> UserUpload([FromBody] UserUpload userUpload)
        {
            return await userService.UserUploadAsync(userUpload);
        }

        [HttpGet("UserStatus/{userId}")]
        public async Task<UserStatus> UserStatus(string userId)
        {
            return await userService.UserStatusAsync(userId);
        }

        [Authorize("Admin")]
        [HttpGet("{page}/{limit}")]
        public async Task<ApiResponse<UsersDto>> GetUsers(int page, int limit)
        {
            var result = new ApiResponse<UsersDto>();
            var users = await userService.QueryAsync(page, limit, result.count);
            List<UsersDto> usersDto = new List<UsersDto>();
            foreach (var user in users)
                usersDto.Add(mapper.Map<UsersDto>(user));
            result.data = usersDto;
            return result;
        }

        [HttpPut("NoEntity")]
        public async Task<bool> UpdateUser([FromBody] UsersDto usersDto)
        {
            return await userService.UpdateUserNoEntityAsync(usersDto);
        }

        [HttpDelete]
        public async Task<bool> DeleteUserByNames([FromQuery]string names)
        {
            return await userService.DeleteUserByNames(names);
        }

        //[HttpDelete("{id?}")]
        //public async Task<bool> DeleteUser([FromQuery] string id)
        //{
        //    return await userService.DeleteAsync(id);
        //}
    }
}
