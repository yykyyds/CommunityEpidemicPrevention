using IRepository;
using IService;
using Models.Dtos;
using Models.Models;
using SqlSugar;

namespace Service
{
    public class EscalationInfoService : BaseService<EscalationInfo>, IEscalationInfoService
    {
        private readonly IEscalationInfoRepository escalationInfoRepository;

        public EscalationInfoService(IEscalationInfoRepository escalationInfoRepository)
        {
            base.repository = escalationInfoRepository;
            this.escalationInfoRepository = escalationInfoRepository;
        }

        public new async Task<bool> DeleteAsync(string ids)
        {
            string[] Ids = new string[ids.Length];
            string[] Ids1 = new string[ids.Length];
            Ids = ids.Split(',');
            int j = 0;
            for (int i = 0; i < Ids.Length; i++)
                if (Ids[i] != "")
                    Ids1[j++] = Ids[i];
            List<EscalationInfo> escalationInfos = await QueryAsync(er => Ids1.Contains(er.Id.ToString()));
            return await escalationInfoRepository.DeleteAsync(escalationInfos);
        }

        public async Task<bool> NotifyClockIn(IEmailService emailService, IUserService userService)
        {
            var t = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            var userIds = this.escalationInfoRepository.QueryAsync(ec => ec.Time > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).Result.Select(ec => ec.UserId).Distinct();
            var emails = userService.QueryAsync(u => !userIds.Contains(u.OpenId) && u.Role != role.管理员).Result.Select(u => u.Email).Distinct();
            bool res = true;
            foreach (var email in emails)
            {
                emailService.SendConfig("通知每日打卡", email, "疫情防控期间请每日完成打卡，你还没有打卡哟。");
                res = res && emailService.SendEmail();
            }
            return res;
        }

        public async Task<List<EscalationInfoDto>> QueryWithUserNameAsync(int page, int limit, RefAsync<int> total)
        {
            return await escalationInfoRepository.QueryWithUserNameAsync(page, limit, total);
        }
    }
}
