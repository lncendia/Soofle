using Microsoft.EntityFrameworkCore;

namespace VkQ.Infrastructure.DataStorage.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoomBaseModel>().HasMany(x => x.Messages).WithOne(x => x.Room).HasForeignKey(x => x.RoomId);
    }
}