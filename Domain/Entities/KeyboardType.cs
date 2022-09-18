﻿using System.Collections.Generic;

namespace eStore_Admin.Domain.Entities
{
    public class KeyboardType : Entity
    {
        public KeyboardType()
        {
            Keyboards = new List<Keyboard>();
        }

        public string Name { get; set; }

        public ICollection<Keyboard> Keyboards { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is KeyboardType other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * Name.GetHashCode() * IsDeleted.GetHashCode();
            }
        }
    }
}