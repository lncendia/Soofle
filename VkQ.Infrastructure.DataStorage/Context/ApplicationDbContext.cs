using Microsoft.EntityFrameworkCore;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Models.Reports.Base;
using VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;
using VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;
using VkQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace VkQ.Infrastructure.DataStorage.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<VkModel> Vks { get; set; } = null!;
    public DbSet<TransactionModel> Transactions { get; set; } = null!;
    public DbSet<ReportLogModel> ReportLogs { get; set; } = null!;
    public DbSet<ParticipantModel> Participants { get; set; } = null!;
    public DbSet<LinkModel> Links { get; set; } = null!;
    public DbSet<ProxyModel> Proxies { get; set; } = null!;

    public DbSet<PublicationModel> Publications { get; set; } = null!;

    public DbSet<LikeReportModel> LikeReports { get; set; } = null!;
    public DbSet<LikeReportElementModel> LikeReportElements { get; set; } = null!;

    public DbSet<ParticipantReportModel> ParticipantReports { get; set; } = null!;
    public DbSet<ParticipantReportElementModel> ParticipantReportElements { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().HasOne(t => t.Vk)
            .WithOne(t => t.User)
            .HasForeignKey<VkModel>(t => t.UserId);

        modelBuilder.Entity<VkModel>().HasOne(t => t.User)
            .WithOne(t => t.Vk)
            .HasForeignKey<UserModel>(t => t.VkId);

        modelBuilder.Entity<ReportLogModel>().HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<VkModel>().HasOne(x => x.ProxyModel).WithMany().OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<LinkModel>().HasOne(x => x.User1).WithMany().OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<LinkModel>().HasOne(x => x.User2).WithMany().OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ReportModel>().UseTpcMappingStrategy();
        modelBuilder.Entity<ReportElementModel>().UseTpcMappingStrategy();
        base.OnModelCreating(modelBuilder);
    }
}