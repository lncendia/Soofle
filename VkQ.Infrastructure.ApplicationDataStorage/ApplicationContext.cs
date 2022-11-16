using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VkQ.Application.Abstractions.Entities;

namespace VkQ.Infrastructure.ApplicationDataStorage;

public class ApplicationContext : IdentityDbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<UserData>? ApplicationUsers { get; set; } = null!;
    public DbSet<RoleData>? ApplicationRoles { get; set; } = null!;
}