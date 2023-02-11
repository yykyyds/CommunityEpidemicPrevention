using CommunityEP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Dtos;
using Models.Models;
using Service.UtilityService;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Policy;

namespace CommunityEP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices] JwtService jwtService)
        {
            IEnumerable<Claim> claims = HttpContext.User.Claims;
            VisitApiService.Token = jwtService.GetJwtToken(claims.ToList());
            return View();
        }

        //页面缓存：https://www.cnblogs.com/chenxi001/p/13308860.html
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult IndexContent()
        {
            return View();
        }

        [Authorize]
        public async Task<List<EchartsModel>> GetAllApplications([FromServices]VisitApiService visitApiService)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/BaseApi/AllApplications", "get", VisitApiService.Token ?? "");
            return visitApiService.DeSerialize<List<EchartsModel>>(result);
        }

        [Authorize]
        public async Task<bool> NotifyClockIn([FromServices] VisitApiService visitApiService)
        {
            var result = await visitApiService.CallApiAsync(VisitApiService.Url + $"/BaseApi/NotifyClockIn", "post", VisitApiService.Token ?? "");
            return visitApiService.DeSerialize<bool>(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}