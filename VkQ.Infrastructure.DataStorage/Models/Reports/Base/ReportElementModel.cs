namespace VkQ.Infrastructure.DataStorage.Models.Reports.Base;

public class ReportElementModel : IModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public long VkId { get; set; }
    public List<ReportElementModel> Children { get; set; } = new();
}