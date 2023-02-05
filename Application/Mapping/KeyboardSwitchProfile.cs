using AutoMapper;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Mapping;

public class KeyboardSwitchProfile : Profile
{
    public KeyboardSwitchProfile()
    {
        CreateMap<KeyboardSwitch, KeyboardSwitchResponse>();
        CreateMap<KeyboardSwitchDto, KeyboardSwitch>();
    }
}