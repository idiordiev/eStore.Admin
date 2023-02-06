using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Infrastructure.Tests.EqualityComparers;

public class OrderItemEqualityComparer : IEqualityComparer<OrderItem>
{
    public bool Equals(OrderItem x, OrderItem y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.Id == y.Id
               && x.IsDeleted == y.IsDeleted
               && x.Quantity == y.Quantity
               && x.UnitPrice == y.UnitPrice
               && x.OrderId == y.OrderId
               && x.GoodsId == y.GoodsId;
    }

    public int GetHashCode(OrderItem obj)
    {
        return HashCode.Combine(obj.Id, obj.IsDeleted, obj.Quantity, obj.UnitPrice, obj.OrderId, obj.GoodsId);
    }
}