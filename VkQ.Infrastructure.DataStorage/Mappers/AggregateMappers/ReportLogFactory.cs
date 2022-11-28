using System.Reflection;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ReportLogMapper : IAggregateMapper<ReportLog, ReportLogModel>
{
    private static readonly FieldInfo ReportLogId =
        typeof(ReportLog).GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public ReportLog Map(ReportLogModel model)
    {
        var reportLog = new ReportLog(model.UserId, model.ReportId, model.Type, model.CreatedAt, model.AdditionalInfo);
        ReportLogId.SetValue(reportLog, model.Id);
        return reportLog;
    }
}