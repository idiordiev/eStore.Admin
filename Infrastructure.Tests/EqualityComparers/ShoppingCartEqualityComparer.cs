using eStore_Admin.Domain.Entities;

namespace Infrastructure.Tests.EqualityComparers;

public class ShoppingCartEqualityComparer : IEqualityComparer<ShoppingCart>
{
    public bool Equals(ShoppingCart x, ShoppingCart y)
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
               && x.CustomerId == y.CustomerId;
    }

    public int GetHashCode(ShoppingCart obj)
    {
        return HashCode.Combine(obj.Id, obj.IsDeleted, obj.CustomerId);
    }
}