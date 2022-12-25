using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VkQ.Application.Abstractions.Users.Entities;
using VkQ.Infrastructure.ApplicationDataStorage.Models;

namespace VkQ.Infrastructure.ApplicationDataStorage;

public class ApplicationContext : IdentityDbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<UserData> ApplicationUsers { get; set; } = null!;
    public DbSet<RoleData> ApplicationRoles { get; set; } = null!;
    public DbSet<Job> Jobs { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Application");
        base.OnModelCreating(modelBuilder);
    }
}