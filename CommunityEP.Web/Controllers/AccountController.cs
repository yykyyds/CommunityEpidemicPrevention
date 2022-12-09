using IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Service.UtilityService;
using System.Security.Claims;

namespace CommunityEP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly HashService hashService;

        public AccountController(IUserService userService, HashService hashService)
        {
            this.userService = userService;
            this.hashService = hashService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl, string? message)
        {
            var dt = ModelState;
            //TempData["returnUrl"] = returnUrl ?? "";
            ViewData["message"] = message ?? "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User userL)
        {
            var dt = ModelState;
            var user = (await userService.QueryAsync(u => u.NickName == userL.NickName)).FirstOrDefault();
            string passwordHash = hashService.ComputeMD5Hash(userL.PasswordHash);
            if (user == null || user.PasswordHash != passwordHash)
                return RedirectToAction(nameof(Login), new { message = "用户名或密码错误！" });
            else if (user.Role != role.管理员)
                return RedirectToAction(nameof(Login), new { message = "角色是管理员才能登录后台系统！" });
            var result = await userService.LoginAsync(userL);
            VisitApiService.Token = result;
            List<Claim> claims = new List<Claim>()
            {
                new Claim("avatarUrl",user.AvatarUrl),
                new Claim("name",user.NickName),
                new Claim("role",user.Role.ToString()),
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            TempData.TryGetValue("returnUrl", out object returnUrl);
            await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties()
            {
                IsPersistent = userL.IsPersistent,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                AllowRefresh = true,
                RedirectUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}"
                //RedirectUri = ((returnUrl??"").ToString() == "" ? $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" : "").ToString()
            });
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Register(string? message)
        {
            ViewData["message"] = message ?? "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User userR)
        {
            var user = userService.QueryAsync(u => u.NickName == userR.NickName).Result.FirstOrDefault();
            if (user != null)
                return RedirectToAction(nameof(Register), new { message = "用户名已存在" });
            userR.OpenId = Guid.NewGuid().ToString("N");//Guid.ToString()  https://blog.csdn.net/chinaherolts2008/article/details/115167887
            userR.Role = role.管理员;
            userR.AvatarUrl = "https://thirdwx.qlogo.cn/mmopen/vi_32/9ELDAVVRIkwYfmCVpHEUE1wzW6dico7YLWq1dicExLw17N5JZPFjwA5oO2b94aZjBDOgkZBevSYia3WUQqQPuRLkg/132";
            userR.PasswordHash = hashService.ComputeMD5Hash(userR.PasswordHash);
            await userService.AddAsync(userR);
            return RedirectToAction(nameof(Login), new { message = "注册成功，可以登陆了" });
        }

        public async Task<IActionResult> Denied()
        {
            return View();
        }

        [Authorize]
        public void Logout()
        {
            Task.Run(async () =>
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);//参数可加可不加
                VisitApiService.Token = null;
            }).Wait();
        }
    }
}
