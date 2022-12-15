using VkQ.Domain.ReportLogs.Enums;

namespace VkQ.Application.Abstractions.ReportsQuery.DTOs;

public class SearchQuery
{
    public SearchQuery(int page, ReportType? reportType, string? hashtag, DateTimeOffset? from, DateTimeOffset? to)
    {
        Page = page;
        ReportType = reportType;
        Hashtag = hashtag;
        From = from;
        To = to;
    }

    public int Page { get; }
    public ReportType? ReportType { get; }
    public string? Hashtag { get; }
    public DateTimeOffset? From { get; }
    public DateTimeOffset? To { get; }
}