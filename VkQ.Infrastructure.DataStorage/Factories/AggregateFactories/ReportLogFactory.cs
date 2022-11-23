using System.Reflection;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Infrastructure.DataStorage.Factories.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Factories.AggregateFactories;

public class ReportLogFactory : IAggregateFactory<ReportLog, ReportLogModel>
{
    private static readonly FieldInfo ReportLogId =
        typeof(ReportLog).GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public ReportLog Create(ReportLogModel model)
    {
        var reportLog = new ReportLog(model.UserId, model.ReportId, model.Type, model.CreatedAt, model.AdditionalInfo);
        ReportLogId.SetValue(reportLog, model.Id);
        return reportLog;
    }
}