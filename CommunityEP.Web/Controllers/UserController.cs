using AutoMapper;
using IService;
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
    public class UserController : Controller
    {
        private readonly VisitApiService visitApiService;

        public UserController(VisitApiService visitApiService)
        {
            this.visitApiService = visitApiService;
        }

        public IActionResult List()
        {
            return View();
        }

        public async Task<ApiResponse<UsersDto>> GetUsers(int page,int limit)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/Users/{page}/{limit}", "get", VisitApiService.Token);
            return visitApiService.DeSerialize<ApiResponse<UsersDto>>(result);
        }

        [HttpPut]
        public async Task<bool> UpdateUser([FromBody]UsersDto usersDto)
        {
            usersDto.AvatarUrl = usersDto.AvatarUrl ?? "";
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/Users/NoEntity", "put", VisitApiService.Token,usersDto);
            return visitApiService.DeSerialize<bool>(result);
        }

        [HttpDelete]
        public async Task<bool> DeleteUserByNames([FromBody] string[] names)
        {
            var nas = "";
            foreach (string name in names)
                nas += name+",";
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/Users?names={nas}", "delete", VisitApiService.Token);
            return visitApiService.DeSerialize<bool>(result);
        }
    }
}
