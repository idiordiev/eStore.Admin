using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore_Admin.Infrastructure.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole()
                {
                    Name = "Sales Manager",
                    NormalizedName = "SALES MANAGER"
                },
                new IdentityRole()
                {
                    Name = "Storage Manager",
                    NormalizedName = "STORAGE MANAGER"
                },
                new IdentityRole()
                {
                    Name = "CEO",
                    NormalizedName = "CEO"
                }
            });
        }
    }
}