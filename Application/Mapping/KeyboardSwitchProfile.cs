using AutoMapper;
using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Mapping
{
    public class KeyboardSwitchProfile : Profile
    {
        public KeyboardSwitchProfile()
        {
            CreateMap<KeyboardSwitch, KeyboardSwitchResponse>();
            CreateMap<KeyboardSwitchRequest, KeyboardSwitch>();
        }
    }
}