using AutoMapper;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.RequestModels;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerRequest, Customer>();
        }
    }
}