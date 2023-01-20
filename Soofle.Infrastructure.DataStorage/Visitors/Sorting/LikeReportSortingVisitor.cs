using Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport;
using Soofle.Infrastructure.DataStorage.Visitors.Sorting.Models;
using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Ordering.Visitor;

namespace Soofle.Infrastructure.DataStorage.Visitors.Sorting;

internal class LikeReportSortingVisitor : BaseSortingVisitor<LikeReportModel, ILikeReportSortingVisitor, LikeReport>, ILikeReportSortingVisitor
{
    protected override List<SortData<LikeReportModel>> ConvertOrderToList(IOrderBy<LikeReport, ILikeReportSortingVisitor> spec)
    {
        var visitor = new LikeReportSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}