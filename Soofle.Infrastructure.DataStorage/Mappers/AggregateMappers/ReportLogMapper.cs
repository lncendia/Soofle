using Soofle.Infrastructure.DataStorage.Mappers.Abstractions;
using Soofle.Infrastructure.DataStorage.Mappers.StaticMethods;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.ReportLogs.Entities;

namespace Soofle.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ReportLogMapper : IAggregateMapperUnit<ReportLog, ReportLogModel>
{
    public ReportLog Map(ReportLogModel model)
    {
        var reportLog = new ReportLog(model.UserId, model.ReportId ?? Guid.Empty, model.Type, model.CreatedAt,
            model.AdditionalInfo);
        IdFields.AggregateId.SetValue(reportLog, model.Id);
        if (model.FinishedAt.HasValue)
        {
            reportLog.Finish(model.Success!.Value, model.FinishedAt.Value);
        }

        if (!model.ReportId.HasValue) reportLog.ReportDeleted();

        return reportLog;
    }
}