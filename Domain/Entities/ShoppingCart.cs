﻿using System.Collections.Generic;

namespace eStore_Admin.Domain.Entities
{
    public class ShoppingCart : Entity
    {
        public ShoppingCart()
        {
            Goods = new List<GoodsInCart>();
        }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<GoodsInCart> Goods { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ShoppingCart other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && CustomerId == other.CustomerId;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * CustomerId.GetHashCode() * IsDeleted.GetHashCode();
            }
        }
    }
}