using AutoMapper;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Mapping;

public class KeyboardProfile : Profile
{
    public KeyboardProfile()
    {
        CreateMap<Keyboard, KeyboardResponse>();
        CreateMap<KeyboardDto, Keyboard>();
    }
}