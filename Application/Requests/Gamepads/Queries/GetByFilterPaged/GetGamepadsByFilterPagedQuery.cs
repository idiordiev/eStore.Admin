using System;
using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Gamepads.Queries.GetByFilterPaged
{
    public class GetGamepadsByFilterPagedQuery : IRequest<IEnumerable<GamepadResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ICollection<bool?> IsDeletedValues { get; set; }
        public string Name { get; set; }
        public ICollection<int> ManufacturerIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime CreatedStartDate { get; set; }
        public DateTime CreatedEndDate { get; set; }
        public ICollection<int> ConnectionTypeIds { get; set; }
        public ICollection<int> FeedbackIds { get; set; }
        public float? MinWeight { get; set; }
        public float? MaxWeight { get; set; }
    }
}