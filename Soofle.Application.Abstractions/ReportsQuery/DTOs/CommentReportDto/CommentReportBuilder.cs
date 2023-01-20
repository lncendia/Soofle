using Soofle.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;

namespace Soofle.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;

public class CommentReportBuilder : PublicationReportBuilder
{
    private CommentReportBuilder()
    {
    }

    public static CommentReportBuilder CommentReportDto() => new();

    public CommentReportDto Build() => new(this);
}