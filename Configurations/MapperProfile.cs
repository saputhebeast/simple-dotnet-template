using AutoMapper;
using UrbanNest.Models;
using UrbanNest.Payloads;

namespace UrbanNest.Configurations;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserResponseDto>();
        CreateMap<UserRequestDto, User>();
    }
}