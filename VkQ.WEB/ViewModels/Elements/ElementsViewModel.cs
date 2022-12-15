using VkQ.Domain.Reposts.BaseReport.Entities.Base;
using VkQ.Domain.Users.Entities;

namespace VkQ.WEB.ViewModels.Reports;

public class ReportsViewModel
{
    public int Page { get; }
    public List<Report> Reports { get; }
    public int Count { get; }
}