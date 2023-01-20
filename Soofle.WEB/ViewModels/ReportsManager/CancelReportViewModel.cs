using System.ComponentModel.DataAnnotations;
using Soofle.Domain.ReportLogs.Enums;

namespace Soofle.WEB.ViewModels.ReportsManager;

public class CancelReportViewModel
{
    [Required] public Guid Id { get; set; }
    [Required] public ReportType ReportType { get; set; }
}