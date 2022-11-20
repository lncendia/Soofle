using System.Diagnostics.CodeAnalysis;
using VkQ.Domain.Reposts.BaseReport.DTOs;
using VkQ.Domain.Reposts.BaseReport.Entities.Base;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.BaseReport.Entities.Publication;

public abstract class PublicationReport : Report
{
    protected PublicationReport(User user, string hashtag, DateTimeOffset? searchStartDate = null,
        List<Guid>? coAuthors = null) : base(user)
    {
        Hashtag = hashtag;
        SearchStartDate = searchStartDate;
        if (coAuthors is { Count: > 3 }) throw new TooManyLinksException();
        LinkedUsers = coAuthors;
    }

    public List<Guid>? LinkedUsers { get; }
    public string Hashtag { get; }
    public DateTimeOffset? SearchStartDate { get; }

    protected List<Publication>? PublicationsList;
    public List<Publication>? Publications => PublicationsList?.ToList();

    protected List<PublicationReportElement>? ReportElementsList;

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public void LoadPublications(IEnumerable<PublicationDto> dtos)
    {
        if (!IsCompleted) throw new ReportAlreadyCompletedException();
        if (!dtos.Any()) throw new PublicationsListEmptyException();
        var id = 0;
        PublicationsList = dtos.Select(dto => new Publication(id++, dto.ItemId, dto.OwnerId)).ToList();
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ElementsListEmptyException">elements collection is empty</exception>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    protected void LoadElements(IEnumerable<PublicationReportElement> elements)
    {
        if (!IsCompleted) throw new ReportAlreadyCompletedException();
        if (!elements.Any()) throw new ElementsListEmptyException();
        ReportElementsList = elements.ToList();
    }

    public bool IsInitialized => PublicationsList != null && ReportElementsList != null;
}