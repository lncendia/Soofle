using Soofle.Infrastructure.DataStorage.Models.Reports.CommentReport;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting.Models;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.CommentReport.Ordering.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Sorting;

internal class CommentReportSortingVisitor : BaseSortingVisitor<CommentReportModel, ICommentReportSortingVisitor, CommentReport>, ICommentReportSortingVisitor
{
    protected override List<SortData<CommentReportModel>> ConvertOrderToList(IOrderBy<CommentReport, ICommentReportSortingVisitor> spec)
    {
        var visitor = new CommentReportSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}