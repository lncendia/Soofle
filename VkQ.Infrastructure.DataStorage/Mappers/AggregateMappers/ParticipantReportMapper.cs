using System.Reflection;
using System.Runtime.Serialization;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ParticipantReportMapper : IAggregateMapperUnit<ParticipantReport, ParticipantReportModel>
{
    private static readonly Type ParticipantReportType = typeof(ParticipantReportElement);
    private static readonly Type ParticipantReportElementType = typeof(ParticipantReportElement);

    private static readonly ConstructorInfo ElementConstructor = ParticipantReportElementType.GetConstructor(
        BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null)!;

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
        object?[] args =
        {
            model.Name, model.VkId, model.ParticipantId, model.ParticipantType,
            allElements?.Where(x => x.OwnerId == model.Id).Select(x => GetParticipantElement(x, null)).ToList()
        };
        var element = (ParticipantReportElement)ElementConstructor.Invoke(args);

        IdFields.EntityId.SetValue(element, model.Id);
        return element;
    }
}