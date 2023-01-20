using System.Diagnostics.CodeAnalysis;
using Soofle.Domain.Links.Entities;
using Soofle.Domain.Reposts.BaseReport.Entities;
using Soofle.Domain.Reposts.BaseReport.Exceptions;
using Soofle.Domain.Reposts.PublicationReport.DTOs;
using Soofle.Domain.Reposts.PublicationReport.Exceptions;
using Soofle.Domain.Users.Entities;

namespace Soofle.Domain.Reposts.PublicationReport.Entities;

public abstract class PublicationReport : Report
{
    protected internal PublicationReport(User user, string hashtag, DateTimeOffset? searchStartDate = null,
        IReadOnlyCollection<Link>? coAuthors = null) : base(user)
    {
        if (!hashtag.StartsWith('#')) hashtag = '#' + hashtag;
        Hashtag = hashtag;
        SearchStartDate = searchStartDate;
        if (coAuthors == null) return;
        if (coAuthors.Count > 3) throw new TooManyLinksException();
        foreach (var l in coAuthors)
        {
            if (!l.IsConfirmed) throw new ArgumentException(null, nameof(coAuthors));
            if (l.User1Id == user.Id) _linkedUsersList.Add(l.User2Id);
            else if (l.User2Id == user.Id) _linkedUsersList.Add(l.User1Id);
            else throw new ArgumentException(null, nameof(coAuthors));
        }
    }

    private readonly List<Guid> _linkedUsersList = new();
    public IReadOnlyCollection<Guid> LinkedUsers => _linkedUsersList.AsReadOnly();
    public string Hashtag { get; }
    public DateTimeOffset? SearchStartDate { get; }
    public int Process { get; protected set; }

    protected List<Publication> PublicationsList = new();
    public IReadOnlyCollection<Publication> Publications => PublicationsList.AsReadOnly();

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    private void LoadPublications(IEnumerable<PublicationDto> publications)
    {
        if (!publications.Any()) throw new PublicationsListEmptyException();
        var id = 1;
        PublicationsList = publications.Select(dto => new Publication(dto.ItemId, dto.OwnerId, id++)).ToList();
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ElementsListEmptyException">elements collection is empty</exception>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    private void LoadElements(IEnumerable<PublicationReportElement> elements)
    {
        if (!elements.Any()) throw new ElementsListEmptyException();
        ReportElementsList.AddRange(elements);
    }

    protected void Start(IEnumerable<PublicationDto> publications, IEnumerable<PublicationReportElement> elements)
    {
        LoadPublications(publications);
        LoadElements(elements);
        base.Start();
    }
}