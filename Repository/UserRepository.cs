using IRepository;
using Models.Dtos;
using Models.Models;
using Org.BouncyCastle.Crypto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public new async Task<List<User>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<User>().Select(u => new User()
            {
                OpenId = u.OpenId,
                NickName = u.NickName,
                PasswordHash = u.PasswordHash,
                Gender = u.Gender,
                AvatarUrl = u.AvatarUrl,
                Address = u.Address,
                Email = u.Email,
                Phone = u.Phone,
                Role = u.Role,
                IsOut = u.IsOut
            }).ToPageListAsync(page,size,total);
        }

        public async Task<bool> DeleteUserByNamesAsync(string[] names)
        {
            var result = true;
            foreach (var name in names)
            {
                result = result && await base.Context.DeleteNav<User>(u => u.NickName == name)
                    .Include<EntryRecord>(u => u.entryRecords)
                    .Include<OutRecord>(u => u.outRecords)
                    .Include<EscalationInfo>(u => u.escalationInfos)
                    .Include<PurchaseOrder>(u => u.purchaseOrders).ThenInclude<Goods>(pu => pu.goodsList)
                    .ExecuteCommandAsync();
            }
            return result;
        }

        public async Task<bool> UpdateUserNoEntityAsync(Dictionary<string,object> dt)
        {
            var result = await base.Context.Updateable(dt).AS("User").WhereColumns("NickName").ExecuteCommandAsync();
            return result > 0;
        }

        public async Task<int> GetUserCountAsync()
        {
            return await base.Context.Queryable<User>().CountAsync();
        }
    }
}
