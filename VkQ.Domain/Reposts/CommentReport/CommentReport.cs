using VkQ.Domain.Reposts.BaseReport.Entities;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.CommentReport;

public class CommentReport : PublicationReport
{
    public CommentReport(User user) : base(user)
    {
    }
}