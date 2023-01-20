﻿using Soofle.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;
using Soofle.Domain.Reposts.PublicationReport.Entities;

namespace Soofle.Application.Services.ReportsQuery.Mappers.StaticMethods;

internal static class ReportMapper
{
    public static void InitReportBuilder(PublicationReportBuilder builder, PublicationReport report)
    {
        builder
            .WithHashtag(report.Hashtag)
            .WithId(report.Id)
            .WithCreationDate(report.CreationDate);
        if (report.SearchStartDate != null) builder.WithSearchStartDate(report.SearchStartDate.Value);

        if (!report.IsStarted) return;
        builder
            .WithProcess(report.Process)
            .WithPublicationsCount(report.Publications.Count)
            .WithDates(report.StartDate!.Value, report.EndDate)
            .WithStatus(report.IsCompleted, report.IsSucceeded);

        if (!string.IsNullOrEmpty(report.Message)) builder.WithMessage(report.Message);
    }
}