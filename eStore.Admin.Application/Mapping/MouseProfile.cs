using AutoMapper;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Mapping;

public class MouseProfile : Profile
{
    public MouseProfile()
    {
        CreateMap<Mouse, MouseResponse>();
        CreateMap<MouseDto, Mouse>();
    }
}