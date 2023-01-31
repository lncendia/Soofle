using Soofle.Infrastructure.DataStorage.Models.Reports.Base;

namespace Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport;

public abstract class PublicationReportModel : ReportModel
{
    public List<UserModel> LinkedUsers { get; set; } = new();
    public string Hashtag { get; set; } = null!;
    public DateTimeOffset? SearchStartDate { get; set; }
    public List<PublicationModel> Publications { get; set; } = new();
    public int Process { get; set; }
    public bool AllParticipants { get; set; }
}