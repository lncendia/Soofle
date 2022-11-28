using System.ComponentModel.DataAnnotations;

namespace VkQ.Infrastructure.ApplicationDataStorage.Models;

public class Job
{
    [Key] public string JobId { get; set; } = null!;
    public Guid ReportId { get; set; }
}