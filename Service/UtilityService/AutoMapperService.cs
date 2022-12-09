using AutoMapper;
using Models.Dtos;
using Models.Models;

namespace Service.UtilityService
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService()
        {
            base.CreateMap<User, MobileLRModel>();
            base.CreateMap<MobileLRModel, User>();
            base.CreateMap<EntryRecord, EntryRecordDto>();
            base.CreateMap<OutRecord, OutRecordDto>();
            base.CreateMap<PurchaseOrder, PurchaseDto>();
            base.CreateMap<User, UsersDto>();
            base.CreateMap<EscalationInfo, EscalationInfoDto>();
        }
    }
}
