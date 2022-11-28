﻿namespace VkQ.Application.Abstractions.DTO.Reports.Base.PublicationReportBaseDto;

public abstract class PublicationReportBaseDto : ReportBaseDto.ReportBaseDto
{
    protected PublicationReportBaseDto(PublicationReportBaseBuilder builder) : base(builder)
    {
        Hashtag = builder.Hashtag ?? throw new ArgumentNullException(nameof(builder.Hashtag));
        SearchStartDate = builder.SearchStartDate;
        if (builder.LinkedUsers != null) LinkedUsers.AddRange(builder.LinkedUsers);
        if (builder.Publications != null) Publications.AddRange(builder.Publications);
    }

    public List<Guid> LinkedUsers { get; } = new();
    public string Hashtag { get; }
    public DateTimeOffset? SearchStartDate { get; }
    public List<PublicationDto> Publications { get; } = new();
}