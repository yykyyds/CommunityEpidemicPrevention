using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Service;
using Service.UtilityService;

namespace CommunityEP.Api.Controllers
{
    //简单的类可以继承，业务逻辑复杂的继承了不好
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase//<Tentity> : ControllerBase where Tentity : class, new()
    {
        [Authorize]
        [HttpGet("AllApplications")]
        public async Task<List<EchartsModel>> GetAllApplications([FromServices] IEntryRecordService entryRecordService
                                                                    , [FromServices] IOutRecordService outRecordService
                                                                    , [FromServices] IPurchaseOrderService purchaseOrderService)
        {
            var list = new List<EchartsModel>();
            var list1 = await entryRecordService.QueryAsync(er => er.EntryTime > DateTime.Now.AddDays(-6) && er.Status == Models.Models.status.已完成);
            var list2 = await outRecordService.QueryAsync(or => or.OutTime > DateTime.Now.AddDays(-6) && or.Status == Models.Models.status.已完成);
            var list3 = await purchaseOrderService.QueryAsync(po => po.SubmitTime > DateTime.Now.AddDays(-6) && po.Status == Models.Models.status.已完成 );
            for(int i = 6; i >=0 ; i--)
            {
                EchartsModel echartsModel = new EchartsModel();
                echartsModel.Time = DateTime.Now.AddDays(-i).ToString("MM-dd");
                echartsModel.EntryCount = list1.Count(er => er.EntryTime.ToString("MM-dd") == echartsModel.Time);
                echartsModel.OutCount = list2.Count(or => or.OutTime.ToString("MM-dd") == echartsModel.Time);
                echartsModel.PurchaseCount = list3.Count(po => po.SubmitTime.ToString("MM-dd") == echartsModel.Time);
                list.Add(echartsModel);
            }
            return list;
        }

        [Authorize("Admin")]
        [HttpPost("NotifyClockIn")]
        public async Task<bool> NotifyClockIn([FromServices] IEscalationInfoService escalationInfoService
                                                ,[FromServices] IEmailService emailService
                                                , [FromServices]IUserService userService)
        {
            return await escalationInfoService.NotifyClockIn(emailService,userService);
        }
    }
}
