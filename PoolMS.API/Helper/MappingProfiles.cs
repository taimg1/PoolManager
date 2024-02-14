using AutoMapper;
using PoolMS.Core.Entities;
using PoolMS.Repository.DTO;
using PoolMS.Service.DTO;

namespace PoolMS.Service.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<SubType, SubTypeDto>().ReverseMap();
        CreateMap<Pool, PoolDto>().ReverseMap();
        CreateMap<Subscription, SubscriptionDto>().ReverseMap();
        CreateMap<Reservation, ReservationDto>().ReverseMap();
        CreateMap<Visit, VisitDto>().ReverseMap();
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<PoolSize, PoolSizeDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();

        CreateMap<User,UserRegDto>().ReverseMap();
        CreateMap<User, UserLoginDto>().ReverseMap();

        CreateMap<Payment,PaymentCreateDto>().ReverseMap();
        CreateMap<Subscription, SubscriptionCreateDto>().ReverseMap();
        CreateMap<Pool, PoolCreateDto>().ReverseMap();
        CreateMap<PoolSize, PoolSizeCreateDto>().ReverseMap();
        CreateMap<SubType, SubTypeCreateDto>().ReverseMap();
        CreateMap<Reservation, ReservationCreateDto>().ReverseMap();
        CreateMap<Visit, VisitCreateDto>().ReverseMap();
        CreateMap<Role,RoleCreateDto>().ReverseMap();

        CreateMap<Pool, PoolUpdateDto>().ReverseMap();  
        CreateMap<PoolSize, PoolSizeUpdateDto>().ReverseMap();
        CreateMap<SubType, SubTypeUpdateDto>().ReverseMap();
        CreateMap<Subscription, SubscriptionUpdateDto>().ReverseMap();
        CreateMap<Reservation, ReservationUpdateDto>().ReverseMap();
        CreateMap<Visit, VisitUpdateDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<Role, RoleUpdateDto>().ReverseMap();

        CreateMap<User, LowUserDto>().ReverseMap();
        CreateMap<Visit, LowVisitDto>().ReverseMap();
        CreateMap<Subscription, LowSubDto>().ReverseMap();

        CreateMap<Subscription,SubscriptionCreateUserDto>().ReverseMap();

       


    }
}