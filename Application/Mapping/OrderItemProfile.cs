using AutoMapper;
using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Mapping
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemResponse>();
            CreateMap<OrderItemRequest, OrderItem>();
        }
    }
}