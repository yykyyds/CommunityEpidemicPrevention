using IService;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Models;
using Service.UtilityService;

namespace CommunityEP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UsersController> logger;
        private readonly IEmailService emailService;

        public AccountController(IUserService userService, ILogger<UsersController> logger, IEmailService emailService)
        {
            this.userService = userService;
            this.logger = logger;
            this.emailService = emailService;
        }

        [HttpPost("PCLogin")]
        public async Task<GeneralApiMessage> Login([FromBody] User user)
        {
            //var user1 = HttpContext.User;
            var result = await userService.LoginAsync(user);
            if (result == "用户名或密码错误")
            {
                logger.LogError($"用户地址：{HttpContext.Connection.RemoteIpAddress}用户登录：{user.NickName} {result}");
                return GeneralApiResult.Success($"{user.NickName}登录失败", result);
            }
            else
            {
                logger.LogInformation($"用户登录成功：UserName：{user.NickName}");
                return GeneralApiResult.Success($"{user.NickName}登录成功", result);
            }
        }

        [HttpPost("PCRegister")]
        public async Task<GeneralApiMessage> Register([FromBody] User user)
        {
            var result = await userService.RegisterAsync(user);
            if (result == userRLStatus.注册发生异常)
            {
                logger.LogError($"用户注册：{user.NickName} {result}");
                return GeneralApiResult.Error($"{result}请检查用户字段是否违规！", null);
            }
            else
            {
                emailService.SendConfig("用户注册", user.Email??"", "你已经完成小区疫情防控系统的微信端注册。");
                emailService.SendEmail();
                logger.LogInformation($"用户注册：{user.NickName} {result}");
                return GeneralApiResult.Success($"{result}。", null);
            }
        }

        [HttpPost("MobileLR")]
        public async Task<GeneralApiMessage> MobileLR([FromBody] MobileLRModel mobileLRModel)
        {
            if (string.IsNullOrEmpty(mobileLRModel.Openid))
                return GeneralApiResult.Error("登录信息不能为空", mobileLRModel);
            var result = await userService.MobileLRAsync(mobileLRModel);
            if (result.code == 200)
            {
                logger.LogInformation($"用户登录成功：{mobileLRModel.Openid}");
                return result;
            }
            return result;
        }
    }
}
