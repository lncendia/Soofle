using Microsoft.EntityFrameworkCore;
using VkQ.Infrastructure.DataStorage.Models;
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
    public DbSet<ReportLogModel> ReportLogs { get; set; } = null!;
    public DbSet<ParticipantModel> Participants { get; set; } = null!;
    public DbSet<LinkModel> Links { get; set; } = null!;
    public DbSet<ProxyModel> Proxies { get; set; } = null!;
    
    public DbSet<PublicationModel> Publications { get; set; } = null!;

    public DbSet<LikeReportModel> LikeReports { get; set; } = null!;
    public DbSet<LikeReportElementModel> LikeReportElements { get; set; } = null!;

    public DbSet<ParticipantReportModel> ParticipantReports { get; set; } = null!;
    public DbSet<ParticipantReportElementModel> ParticipantReportElements { get; set; } = null!;


    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     
    // }
}