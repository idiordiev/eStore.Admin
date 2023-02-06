using AutoMapper;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Mapping;

public class GamepadProfile : Profile
{
    public GamepadProfile()
    {
        CreateMap<Gamepad, GamepadResponse>();
        CreateMap<GamepadDto, Gamepad>();
    }
}