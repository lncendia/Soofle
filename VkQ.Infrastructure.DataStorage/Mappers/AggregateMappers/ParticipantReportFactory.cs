using System.Reflection;
using System.Runtime.Serialization;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ParticipantReportMapper : IAggregateMapper<ParticipantReport, ParticipantReportModel>
{
    private static readonly Type ParticipantReportType = typeof(ParticipantReportElement);

    private static readonly FieldInfo ElementId =
        typeof(ParticipantReportElement).GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
        !;


    public ParticipantReport Map(ParticipantReportModel model)
    {
        var report = (ParticipantReport)FormatterServices.GetUninitializedObject(ParticipantReportType);
        var elements = model.ReportElementsList.Where(x => x.OwnerId == null).Cast<ParticipantReportElementModel>()
            .Select(x => GetParticipantElement(x, model.ReportElementsList.Cast<ParticipantReportElementModel>()));
        ReportInitializer.InitReport(report, elements, model);
        return report;
    }

    private static ParticipantReportElement GetParticipantElement(ParticipantReportElementModel model,
        IEnumerable<ParticipantReportElementModel>? allElements)
    {
        var element = new ParticipantReportElement(model.Name, model.OldName, model.VkId, model.Type,
            allElements?.Where(x => x.OwnerId == model.Id).Select(x => GetParticipantElement(x, null)).ToList());
        ElementId.SetValue(element, model.Id);
        return element;
    }
}