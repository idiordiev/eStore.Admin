using eStore_Admin.Domain.Entities;

namespace Infrastructure.Tests.EqualityComparers;

public class GamepadEqualityComparer : IEqualityComparer<Gamepad>
{
    public bool Equals(Gamepad x, Gamepad y)
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
               && x.Description == y.Description
               && x.Price == y.Price
               && x.ThumbnailImageUrl == y.ThumbnailImageUrl
               && x.BigImageUrl == y.BigImageUrl
               && x.Created.Equals(y.Created)
               && x.LastModified.Equals(y.LastModified)
               && x.Weight.Equals(y.Weight)
               && x.ConnectionType == y.ConnectionType
               && x.Feedback == y.Feedback
               && x.CompatibleDevices.Count == y.CompatibleDevices.Count
               && x.CompatibleDevices.Intersect(y.CompatibleDevices).Count() == x.CompatibleDevices.Count;
    }

    public int GetHashCode(Gamepad obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.Id);
        hashCode.Add(obj.IsDeleted);
        hashCode.Add(obj.Name);
        hashCode.Add(obj.Manufacturer);
        hashCode.Add(obj.Price);
        hashCode.Add(obj.ThumbnailImageUrl);
        hashCode.Add(obj.BigImageUrl);
        hashCode.Add(obj.Created);
        hashCode.Add(obj.LastModified);
        hashCode.Add(obj.Weight);
        hashCode.Add(obj.ConnectionType);
        hashCode.Add(obj.Feedback);
        hashCode.Add(obj.CompatibleDevices);

        return hashCode.ToHashCode();
    }
}