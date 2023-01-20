using System.ComponentModel.DataAnnotations;

namespace Soofle.WEB.ViewModels.ReportsManager;

public class ParticipantReportCreateViewModel
{
    [Display(Name = "Укадите, через сколько начать")]
    [DataType(DataType.Time)]
    public TimeSpan? Timer { get; set; }
}