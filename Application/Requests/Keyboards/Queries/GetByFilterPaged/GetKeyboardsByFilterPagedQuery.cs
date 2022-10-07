using System;
using System.Collections.Generic;
using eStore_Admin.Application.Responses;
using MediatR;

namespace eStore_Admin.Application.Requests.Keyboards.Queries.GetByFilterPaged
{
    public class GetKeyboardsByFilterPagedQuery : IRequest<IEnumerable<KeyboardResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
        public ICollection<bool> IsDeletedValues { get; set; }
        public string Name { get; set; }
        public ICollection<string> Manufacturers { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? CreatedStartDate { get; set; }
        public DateTime? CreatedEndDate { get; set; }
        public ICollection<string> Types { get; set; }
        public ICollection<string> Sizes { get; set; }
        public ICollection<string> ConnectionTypes { get; set; }
        public ICollection<int?> SwitchIds { get; set; }
        public ICollection<string> KeyRollovers { get; set; }
        public ICollection<string> Backlights { get; set; }
    }
}