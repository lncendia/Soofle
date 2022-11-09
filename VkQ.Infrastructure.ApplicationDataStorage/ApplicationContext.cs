using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VkQ.Infrastructure.ApplicationDataStorage;

public class ApplicationContext<TU, TR> : IdentityDbContext<TU, TR, string>
    where TU : IdentityUser where TR : IdentityRole
{
    public ApplicationContext(DbContextOptions<ApplicationContext<TU, TR>> options)
        : base(options)
    {
    }

    public DbSet<TU>? ApplicationUsers { get; set; } = null!;
    public DbSet<TR>? ApplicationRoles { get; set; } = null!;
}