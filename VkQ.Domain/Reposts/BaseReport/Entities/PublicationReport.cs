using VkQ.Domain.Reposts.BaseReport.DTOs;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.BaseReport.Entities;

public abstract class PublicationReport : Report
{
    protected PublicationReport(User user) : base(user)
    {
    }

    protected List<Publication>? PublicationsList;
    public List<Publication> Publications => PublicationsList?.ToList() ?? new List<Publication>();

    public void LoadPublications(IEnumerable<PublicationDto> dtos)
    {
        var id = 0;
        PublicationsList = dtos.Select(dto => new Publication(id++)).ToList();
    }
}