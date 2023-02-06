using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Infrastructure.Tests.EqualityComparers;

public class KeyboardSwitchEqualityComparer : IEqualityComparer<KeyboardSwitch>
{
    public bool Equals(KeyboardSwitch x, KeyboardSwitch y)
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
               && x.Name == y.Name
               && x.Manufacturer == y.Manufacturer
               && x.IsTactile == y.IsTactile
               && x.IsClicking == y.IsClicking;
    }

    public int GetHashCode(KeyboardSwitch obj)
    {
        return HashCode.Combine(obj.Id, obj.IsDeleted, obj.Name, obj.Manufacturer, obj.IsTactile, obj.IsClicking);
    }
}