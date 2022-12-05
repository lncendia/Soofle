using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.ReportBaseDto;

namespace VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.PublicationReportBaseDto;

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

    protected PublicationReportBaseBuilder WithReportElements(IEnumerable<PublicationReportElementBaseDto> elements) =>
        (PublicationReportBaseBuilder)base.WithReportElements(elements);
}