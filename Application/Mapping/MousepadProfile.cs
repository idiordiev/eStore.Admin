using AutoMapper;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Mapping;

public class MousepadProfile : Profile
{
    public MousepadProfile()
    {
        CreateMap<Mousepad, MousepadResponse>();
        CreateMap<MousepadDto, Mousepad>();
    }
}