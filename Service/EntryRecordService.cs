using AutoMapper;
using IdentityModel;
using IRepository;
using IService;
using Models.Dtos;
using Models.Models;
using SqlSugar;

namespace Service
{
    public class EntryRecordService : BaseService<EntryRecord>, IEntryRecordService
    {
        private readonly IEntryRecordRepository entryRecordRepository;
        private readonly IMapper mapper;

        public EntryRecordService(IEntryRecordRepository entryRecordRepository,IMapper mapper)
        {
            base.repository = entryRecordRepository;
            this.entryRecordRepository = entryRecordRepository;
            this.mapper = mapper;
        }

        public new async Task<bool> AddAsync(EntryRecord entity)
        {
            var entry = await base.QueryAsync(e => e.Status == status.审核中);
            if (entry.Count > 0) return false;
            entity.EntryTime = TimeZoneInfo.ConvertTime(entity.EntryTime, TimeZoneInfo.Local);//将时间转化为中国时间
            return await base.repository.AddAsync(entity);
        }

        public async Task<EntrysByUserId> GetEntrysByUserIdAsync(string userId)
        {
            EntrysByUserId entrysByUserId = new EntrysByUserId();
            List<EntryRecord> entryRecords = await base.QueryAsync(e => e.UserId == userId);
            List<EntryRecordDto> records = new List<EntryRecordDto>();
            foreach (var item in entryRecords)
            {
                records.Add(mapper.Map<EntryRecordDto>(item));
            }
            entrysByUserId.Pending = records.Where(e => e.Status == status.审核中).OrderByDescending(e => e.EntryTime).ToList();
            entrysByUserId.NoComplete = records.Where(e => e.Status == status.未通过).OrderByDescending(e => e.EntryTime).ToList();
            entrysByUserId.Completed = records.Where(e => e.Status == status.已完成).OrderByDescending(e => e.EntryTime).ToList();
            return entrysByUserId;
        }

        public async Task<List<EntryRecordDto>> QueryWithUserAsync(int page, int limit, RefAsync<int> total)
        {
            return await entryRecordRepository.QueryWithUserAsync(page,limit,total);
        }

        public async Task<bool> UpdateAsync(EntryRecordDto entryRecordDto, IUserService userService)
        {
            var user = (await userService.QueryAsync(u => u.NickName == entryRecordDto.UserName)).FirstOrDefault();
            EntryRecord entryRecord = (await base.QueryAsync(er => er.Id == entryRecordDto.Id)).FirstOrDefault();
            if (user.IsOut == true && entryRecordDto.Status == status.已完成)
            {
                user.IsOut = false;
            }
            entryRecord.Status = entryRecordDto.Status;
            entryRecord.Destination = entryRecordDto.Destination;
            entryRecord.EntryTime = entryRecordDto.EntryTime;
            return await base.UpdateAsync(entryRecord) && await userService.UpdateAsync(user);
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
            List<EntryRecord> entryRecords = await QueryAsync(er => Ids1.Contains(er.Id.ToString()));
            return await entryRecordRepository.DeleteAsync(entryRecords);
        }

        public async Task<int> GetEntryCountAsync()
        {
            return await entryRecordRepository.GetEntryCountAsync();
        }
    }
}
