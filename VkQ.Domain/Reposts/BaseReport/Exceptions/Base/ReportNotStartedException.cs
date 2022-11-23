﻿namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

public class ReportNotStartedException : Exception
{
    public ReportNotStartedException(Guid reportId) : base("Report not started")
    {
        ReportId = reportId;
    }
    public Guid ReportId { get; }
}