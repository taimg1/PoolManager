using AutoMapper;
using PoolMS.Service.DTO;

namespace PoolMS.UI.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PoolUpdateDto, PoolCreateDto>().ReverseMap();
            CreateMap<VisitUpdateDto, VisitCreateDto>().ReverseMap();
            CreateMap<SubscriptionUpdateDto, SubscriptionCreateDto>().ReverseMap();
            CreateMap<SubTypeUpdateDto, SubTypeCreateDto>().ReverseMap();
            CreateMap<PoolSizeUpdateDto, PoolSizeCreateDto>().ReverseMap();
            CreateMap<ReservationUpdateDto, ReservationCreateDto>().ReverseMap();
            CreateMap<RoleUpdateDto, RoleCreateDto>().ReverseMap();
            CreateMap<UserUpdateDto, UserRegDto>().ReverseMap();

            CreateMap<PoolDto, PoolUpdateDto>().ReverseMap();
            CreateMap<VisitDto, VisitUpdateDto>().ReverseMap();
            CreateMap<SubscriptionDto, SubscriptionUpdateDto>().ReverseMap();
            CreateMap<SubTypeDto, SubTypeUpdateDto>().ReverseMap();
            CreateMap<PoolSizeDto, PoolSizeUpdateDto>().ReverseMap();
            CreateMap<ReservationDto, ReservationUpdateDto>().ReverseMap();
            CreateMap<RoleDto, RoleUpdateDto>().ReverseMap();
            CreateMap<UserDto, UserUpdateDto>().ReverseMap();


        }
    }
}
