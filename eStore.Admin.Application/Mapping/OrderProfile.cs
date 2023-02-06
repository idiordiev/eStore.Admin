using AutoMapper;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Responses;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponse>();
        CreateMap<OrderDto, Order>();
    }
}