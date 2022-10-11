using AutoMapper;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Mapping
{
    public class KeyboardProfile : Profile
    {
        public KeyboardProfile()
        {
            CreateMap<Keyboard, KeyboardResponse>();
            CreateMap<KeyboardDto, Keyboard>();
        }
    }
}