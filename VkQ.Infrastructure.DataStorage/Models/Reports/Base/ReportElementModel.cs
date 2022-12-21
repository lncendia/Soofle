namespace VkQ.Infrastructure.DataStorage.Models.Reports.Base;

public abstract class ReportElementModel : IModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public long VkId { get; set; }

    public Guid ReportId { get; set; }
    public ReportModel Report { get; set; } = null!;
}