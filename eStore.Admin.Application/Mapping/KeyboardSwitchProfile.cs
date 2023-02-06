using AutoMapper;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Mapping;

public class KeyboardSwitchProfile : Profile
{
    public KeyboardSwitchProfile()
    {
        CreateMap<KeyboardSwitch, KeyboardSwitchResponse>();
        CreateMap<KeyboardSwitchDto, KeyboardSwitch>();
    }
}