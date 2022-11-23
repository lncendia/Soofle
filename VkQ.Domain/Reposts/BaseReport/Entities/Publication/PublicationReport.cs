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
        IReadOnlyCollection<Guid>? coAuthors = null) : base(user)
    {
        Hashtag = hashtag;
        SearchStartDate = searchStartDate;
        if (coAuthors == null) return;
        if (coAuthors.Count > 3) throw new TooManyLinksException();
        _linkedUsersList.AddRange(coAuthors);
    }

    private readonly List<Guid> _linkedUsersList = new();
    public List<Guid> LinkedUsers => _linkedUsersList.ToList();
    public string Hashtag { get; }
    public DateTimeOffset? SearchStartDate { get; }

    protected List<Publication> PublicationsList = new();
    public List<Publication> Publications => PublicationsList.ToList();

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    private void LoadPublications(IEnumerable<PublicationDto> dtos)
    {
        if (!dtos.Any()) throw new PublicationsListEmptyException();
        PublicationsList = dtos.Select(dto => new Publication(dto.ItemId, dto.OwnerId)).ToList();
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ElementsListEmptyException">elements collection is empty</exception>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    private void LoadElements(IEnumerable<PublicationReportElement> elements)
    {
        if (!elements.Any()) throw new ElementsListEmptyException();
        ReportElementsList.AddRange(elements);
    }

    protected void Start(IEnumerable<PublicationDto> dtos, IEnumerable<PublicationReportElement> elements)
    {
        LoadPublications(dtos);
        LoadElements(elements);
        base.Start();
    }
}