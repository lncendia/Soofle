using MediatR;
using Microsoft.EntityFrameworkCore;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Infrastructure.DataStorage.Models.Reports.Base;
using Soofle.Infrastructure.DataStorage.Models.Reports.CommentReport;
using Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport;
using Soofle.Infrastructure.DataStorage.Models.Reports.ParticipantReport;
using Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace Soofle.Infrastructure.DataStorage.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    internal List<INotification> Notifications { get; } = new();

    internal DbSet<UserModel> Users { get; set; } = null!;
    internal DbSet<VkModel> Vks { get; set; } = null!;
    internal DbSet<TransactionModel> Transactions { get; set; } = null!;
    internal DbSet<ReportLogModel> ReportLogs { get; set; } = null!;
    internal DbSet<ParticipantModel> Participants { get; set; } = null!;
    internal DbSet<LinkModel> Links { get; set; } = null!;
    internal DbSet<ProxyModel> Proxies { get; set; } = null!;

    internal DbSet<PublicationModel> Publications { get; set; } = null!;

    internal DbSet<LikeReportModel> LikeReports { get; set; } = null!;
    internal DbSet<LikeReportElementModel> LikeReportElements { get; set; } = null!;
    
    internal DbSet<CommentReportModel> CommentReports { get; set; } = null!;
    internal DbSet<CommentReportElementModel> CommentReportElements { get; set; } = null!;

    internal DbSet<ParticipantReportModel> ParticipantReports { get; set; } = null!;
    internal DbSet<ParticipantReportElementModel> ParticipantReportElements { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().HasOne(t => t.Vk)
            .WithOne(t => t.User)
            .HasForeignKey<UserModel>(t => t.VkId);

        modelBuilder.Entity<ReportLogModel>().HasOne(x => x.User).WithMany();
        modelBuilder.Entity<VkModel>().HasOne(x => x.Proxy).WithMany().OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<LinkModel>().HasOne(x => x.User1).WithMany();
        modelBuilder.Entity<LinkModel>().HasOne(x => x.User2).WithMany().OnDelete(DeleteBehavior.ClientCascade);
        modelBuilder.Entity<PublicationReportModel>().HasMany(x => x.LinkedUsers).WithMany();

        modelBuilder.Entity<ReportModel>().UseTpcMappingStrategy();
        modelBuilder.Entity<ReportElementModel>().UseTpcMappingStrategy();
        base.OnModelCreating(modelBuilder);
    }
}