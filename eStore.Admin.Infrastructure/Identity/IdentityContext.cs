using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eStore.Admin.Infrastructure.Identity;

public class IdentityContext : IdentityDbContext
{
    public IdentityContext()
    {
    }

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new RoleConfiguration());

        base.OnModelCreating(builder);
    }
}