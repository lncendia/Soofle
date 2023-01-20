using System.ComponentModel.DataAnnotations.Schema;
using Soofle.Infrastructure.DataStorage.Models.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Models.Reports.Base;

public abstract class ReportElementModel : IEntityModel
{
    public int Id { get; set; }
    public int EntityId { get; set; }
    public string Name { get; set; } = null!;
    public long VkId { get; set; }
}