using AutoMapper;
using IRepository;
using IService;
using Models.Dtos;
using Models.Models;
using Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OutRecordService : BaseService<OutRecord>, IOutRecordService
    {
        private readonly IOutRecordRepository outRecordRepository;
        private readonly IMapper mapper;

        public OutRecordService(IOutRecordRepository outRecordRepository,IMapper mapper)
        {
            base.repository = outRecordRepository;
            this.outRecordRepository = outRecordRepository;
            this.mapper = mapper;
        }

        public new async Task<bool> AddAsync(OutRecord entity)
        {
            var entry = await base.QueryAsync(e => e.Status == status.审核中);
            if (entry.Count > 0) return false;
            entity.OutTime = TimeZoneInfo.ConvertTime(entity.OutTime, TimeZoneInfo.Local);//将时间转化为中国时间
            return await base.repository.AddAsync(entity);
        }

        public async Task<OutsByUserId> GetOutsByUserIdAsync(string userId)
        {
            OutsByUserId outsByUserId = new OutsByUserId();
            var outRecords = await base.QueryAsync(o => o.UserId == userId);
            List<OutRecordDto> records = new List<OutRecordDto>();
            foreach (var item in outRecords)
            {
                records.Add(mapper.Map<OutRecordDto>(item));
            }
            outsByUserId.Pending = records.Where(o => o.Status == status.审核中).OrderByDescending(o => o.OutTime).ToList();
            outsByUserId.NoComplete = records.Where(o => o.Status == status.未通过).OrderByDescending(o => o.OutTime).ToList();
            outsByUserId.Completed = records.Where(o => o.Status == status.已完成).OrderByDescending(o => o.OutTime).ToList();
            return outsByUserId;
        }

        public async Task<List<OutRecordDto>> QueryWithUserAsync(int page, int limit, RefAsync<int> total)
        {
            return await outRecordRepository.QueryWithUserAsync(page, limit, total);
        }

        public async Task<bool> UpdateAsync(OutRecordDto outRecordDto, IUserService userService)
        {
            var user = (await userService.QueryAsync(u => u.NickName == outRecordDto.UserName)).FirstOrDefault();
            OutRecord outRecord = (await base.QueryAsync(or => or.Id == outRecordDto.Id)).FirstOrDefault();
            if (user.IsOut == false && outRecordDto.Status == status.已完成)
            {
                user.IsOut = true;
            }
            outRecord.Status = outRecordDto.Status;
            outRecord.Destination = outRecordDto.Destination;
            outRecord.OutTime = outRecordDto.OutTime;
            return await base.UpdateAsync(outRecord) && await userService.UpdateAsync(user);
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
            List<OutRecord> outRecords = await QueryAsync(er => Ids1.Contains(er.Id.ToString()));
            return await outRecordRepository.DeleteAsync(outRecords);
        }

        public async Task<int> GetOutCountAsync()
        {
            return await outRecordRepository.GetOutCountAsync();
        }
    }
}
