using Models.Dtos;
using Models.Models;

namespace IService
{
    public interface IUserService : IBaseService<User>
    {
        Task<string> LoginAsync(User user);
        Task<userRLStatus> RegisterAsync(User user);
        Task<GeneralApiMessage> MobileLRAsync(MobileLRModel mobileLRModel);
        Task<GetAllApplyByUserId> GetAllApplyByUserIdAsync(string userId, IOutRecordService outRecordService
                            , IEntryRecordService entryService
                            , IPurchaseOrderService purchaseService);
        Task<bool> UserUploadAsync(UserUpload userUpload);
        Task<UserStatus> UserStatusAsync(string userId);
        Task<bool> DeleteUserByNames(string names);
        Task<bool> UpdateUserNoEntityAsync(UsersDto usersDto);
        Task<int> GetUserCountAsync();
    }
}
