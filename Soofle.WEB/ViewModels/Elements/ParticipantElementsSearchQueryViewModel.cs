﻿using System.ComponentModel.DataAnnotations;
using Soofle.Domain.Participants.Enums;
using Soofle.Domain.Reposts.ParticipantReport.Enums;

namespace Soofle.WEB.ViewModels.Elements;

public class ParticipantElementsSearchQueryViewModel
{
    public int Page { get; set; } = 1;
    [Required] public Guid ReportId { get; set; }
    [StringLength(50)] public string? Name { get; set; }
    public ElementType? ElementType { get; set; }
    public bool? HasChildren { get; set; }
    public ParticipantType? ParticipantType { get; set; }
}