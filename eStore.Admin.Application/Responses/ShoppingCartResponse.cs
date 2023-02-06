using System.Collections.Generic;

namespace eStore.Admin.Application.Responses;

public class ShoppingCartResponse
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public int CustomerId { get; set; }
    public ICollection<int> GoodsIds { get; set; }
}