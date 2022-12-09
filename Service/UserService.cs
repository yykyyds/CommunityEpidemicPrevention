using AutoMapper;
using IdentityModel;
using IRepository;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Models;
using Service.UtilityService;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly HashService hashService;
        private readonly JwtService jwtService;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository
                            , HashService hashService
                            , JwtService jwtService
                            , IMapper mapper)
        {
            base.repository = userRepository;
            this.userRepository = userRepository;
            this.hashService = hashService;
            this.jwtService = jwtService;
            this.mapper = mapper;
        }

        public async Task<GeneralApiMessage> MobileLRAsync(MobileLRModel mobileLRModel)
        {
            User user = await base.FindAsync(mobileLRModel.Openid);
            try
            {
                List<Claim> claims = new List<Claim>();
                if (user == null)
                {
                    await base.AddAsync(mapper.Map<User>(mobileLRModel));
                    claims = new List<Claim>()
                    {
                        new Claim("name",mobileLRModel.NickName),
                        new Claim("role",mobileLRModel.Role.ToString()),
                        new Claim("email",mobileLRModel.Email??""),
                        new Claim("isOut","false")
                    };
                }
                else
                {
                    claims = new List<Claim>()
                    {
                        new Claim("name",user.NickName),
                        new Claim("role",$"{user.Role}"),
                        new Claim("email",user.Email??""),
                        new Claim("isOut",user.IsOut.ToString())
                    };
                }
                return GeneralApiResult.Success("登录/注册成功", jwtService.GetJwtToken(claims));
            }
            catch
            {
                return GeneralApiResult.Error("登录/注册发生异常", null);
            }
        }

        public async Task<string> LoginAsync(User user)
        {
            User? user1 = (await QueryAsync(u => u.NickName == user.NickName)).FirstOrDefault();
            if (user1 == null || hashService.ComputeMD5Hash(user.PasswordHash) != user1.PasswordHash)
                return userRLStatus.用户名或密码错误.ToString();
            else
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim("name",user1.NickName),
                    new Claim("role",user1.Role.ToString()),
                };
                return jwtService.GetJwtToken(claims);
            }
        }

        public async Task<userRLStatus> RegisterAsync(User user)
        {
            User? user1 = (await QueryAsync(u => u.NickName == user.NickName)).FirstOrDefault();
            if (user1 != null)
                return userRLStatus.用户名已存在;
            try
            {
                //对用户密码进行加密然后存如数据库
                user.PasswordHash = hashService.ComputeMD5Hash(user.PasswordHash);
                await AddAsync(user);
                return userRLStatus.注册成功;
            }
            catch
            {
                return userRLStatus.注册发生异常;
            }
        }

        public async Task<GetAllApplyByUserId> GetAllApplyByUserIdAsync(string userId, IOutRecordService outRecordService
                            ,  IEntryRecordService entryService
                            ,  IPurchaseOrderService purchaseService)
        {
            var data = new GetAllApplyByUserId();
            var outRecords = await outRecordService.GetOutsByUserIdAsync(userId);
            data.OutData = outRecords;
            var entryRecords = await entryService.GetEntrysByUserIdAsync(userId);
            data.EntryData = entryRecords;
            var purchaseOrders = await purchaseService.GetPurchaseOrdersByUserIdAsync(userId);
            data.PurchaseData = purchaseOrders;
            return data;
        }

        public async Task<bool> UserUploadAsync(UserUpload userUpload)
        {
            var user = await base.FindAsync(userUpload.OpenId);
            user.HealthCode = userUpload.HealthCode;
            user.TripCode = userUpload.TripCode;
            user.NucleicAcidReport = userUpload.NucleicAcidReport;
            return await UpdateAsync(user);
        }

        public async Task<UserStatus> UserStatusAsync(string userId)
        {
            UserStatus userStatus = new UserStatus();
            User user = await base.FindAsync(userId);
            userStatus.IsOut = user.IsOut;
            return userStatus;
        }

        public async Task<bool> DeleteUserByNames(string names)
        {
            string[] nas = new string[names.Length];
            string[] nas1 = new string[names.Length];
            nas = names.Split(',');
            int j = 0;
            for (int i = 0; i < nas.Length; i++)
                if(nas[i] != "")
                    nas1[j++] = nas[i];
            return await userRepository.DeleteUserByNamesAsync(nas1);
        }

        public async Task<bool> UpdateUserNoEntityAsync(UsersDto usersDto)
        {
            var dt = new Dictionary<string, object>
            {
                {"NickName",usersDto.NickName??" "},
                {"Email",usersDto.Email??" "},
                {"Phone",usersDto.Phone??" "},
                {"Address",usersDto.Address??" "},
                {"Gender",usersDto.Gender},
                {"Role",usersDto.Role},
            };
            return await userRepository.UpdateUserNoEntityAsync(dt);
        }

        public async Task<int> GetUserCountAsync()
        {
            return await userRepository.GetUserCountAsync();
        }
    }
}
