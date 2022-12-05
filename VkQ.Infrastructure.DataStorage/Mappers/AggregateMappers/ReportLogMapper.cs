using System.Reflection;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ReportLogMapper : IAggregateMapper<ReportLog, ReportLogModel>
{
    public ReportLog Map(ReportLogModel model)
    {
        var reportLog = new ReportLog(model.UserId, model.ReportId, model.Type, model.CreatedAt, model.AdditionalInfo);
        IdFields.AggregateId.SetValue(reportLog, model.Id);
        return reportLog;
    }
}