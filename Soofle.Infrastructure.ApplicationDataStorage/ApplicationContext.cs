using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Soofle.Application.Abstractions.Users.Entities;
using Soofle.Infrastructure.ApplicationDataStorage.Models;

namespace Soofle.Infrastructure.ApplicationDataStorage;

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