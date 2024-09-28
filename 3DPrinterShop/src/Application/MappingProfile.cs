using AutoMapper;
using PrinterShop.Core.Domain.Entities;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.Core.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Printer, PrinterDto>().ReverseMap();
        
        CreateMap<Component, ComponentDto>().ReverseMap();
        
        CreateMap<Order, OrderDto>().ReverseMap();

        CreateMap<UserDto, User>();
        
        CreateMap<User, UserDto>().ForMember(x => x.Password, opt => opt.Ignore());
    }
}