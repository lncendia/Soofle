using System.Reflection;
using System.Runtime.Serialization;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Mappers.StaticMethods;
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
        var elementsGrouping = model.ReportElementsList.Cast<ParticipantReportElementModel>().GroupBy(x => x.Owner);
        var elements = new List<ParticipantReportElement>();
        foreach (var group in elementsGrouping)
        {
            ParticipantReportElement? parent = null;
            if (group.Key != null)
            {
                parent = GetParticipantElement(group.Key, null);
                elements.Add(parent);
            }

            elements.AddRange(group.Select(x => GetParticipantElement(x, parent)));
        }

        ReportInitializer.InitReport(report, elements, model);
        return report;
    }

    private static ParticipantReportElement GetParticipantElement(ParticipantReportElementModel model,
        ParticipantReportElement? owner)
    {
        object?[] args =
        {
            model.Name, model.VkId, model.ParticipantId, model.ParticipantType, owner
        };
        var element = (ParticipantReportElement)ElementConstructor.Invoke(args);

        IdFields.EntityId.SetValue(element, model.Id);
        return element;
    }
}