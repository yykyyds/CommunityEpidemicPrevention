using Models.Dtos;
using Models.Models;
using SqlSugar;

namespace IService
{
    public interface IEscalationInfoService : IBaseService<EscalationInfo>
    {
        Task<List<EscalationInfoDto>> QueryWithUserNameAsync(int page, int limit, RefAsync<int> total);
        Task<bool> NotifyClockIn(IEmailService emailService, IUserService userService);
    }
}
