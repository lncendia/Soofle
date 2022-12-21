using VkQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;

public abstract class PublicationReportBuilder : ReportBuilder
{
    public IEnumerable<string>? LinkedUsers;
    public string? Hashtag;
    public DateTimeOffset? SearchStartDate;
    public IEnumerable<PublicationDto>? Publications;

    public PublicationReportBuilder WithLinkedUsers(IEnumerable<string> linkedUsers)
    {
        LinkedUsers = linkedUsers;
        return this;
    }

    public PublicationReportBuilder WithHashtag(string hashtag)
    {
        Hashtag = hashtag;
        return this;
    }

    public PublicationReportBuilder WithSearchStartDate(DateTimeOffset searchStartDate)
    {
        SearchStartDate = searchStartDate;
        return this;
    }

    public PublicationReportBuilder WithPublications(IEnumerable<PublicationDto> publications)
    {
        Publications = publications;
        return this;
    }
}