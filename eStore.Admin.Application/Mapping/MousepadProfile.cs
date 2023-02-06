using AutoMapper;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Mapping;

public class MousepadProfile : Profile
{
    public MousepadProfile()
    {
        CreateMap<Mousepad, MousepadResponse>();
        CreateMap<MousepadDto, Mousepad>();
    }
}