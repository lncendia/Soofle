using VkQ.Application.Abstractions.ReportsQuery.DTOs.Base.ReportBaseDto;

namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.Base.PublicationReportBaseDto;

public abstract class PublicationReportBaseBuilder : ReportBaseBuilder
{
    public IEnumerable<Guid>? LinkedUsers;
    public string? Hashtag;
    public DateTimeOffset? SearchStartDate;
    public IEnumerable<PublicationDto>? Publications;

    public PublicationReportBaseBuilder WithLinkedUsers(IEnumerable<Guid> linkedUsers)
    {
        LinkedUsers = linkedUsers;
        return this;
    }

    public PublicationReportBaseBuilder WithHashtag(string hashtag)
    {
        Hashtag = hashtag;
        return this;
    }

    public PublicationReportBaseBuilder WithSearchStartDate(DateTimeOffset searchStartDate)
    {
        SearchStartDate = searchStartDate;
        return this;
    }

    public PublicationReportBaseBuilder WithPublications(IEnumerable<PublicationDto> publications)
    {
        Publications = publications;
        return this;
    }
}