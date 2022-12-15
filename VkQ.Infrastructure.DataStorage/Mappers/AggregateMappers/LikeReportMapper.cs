using System.Reflection;
using System.Runtime.Serialization;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.LikeReport.ValueObjects;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class LikeReportMapper : IAggregateMapperUnit<LikeReport, LikeReportModel>
{
    private static readonly Type LikeReportType = typeof(LikeReport);
    private static readonly Type LikeReportElementType = typeof(LikeReportElement);

    private static readonly FieldInfo ElementLikes =
        LikeReportElementType.GetField("<Likes>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly ConstructorInfo ElementConstructor =
        LikeReportElementType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes,
            null)!;

    public LikeReport Map(LikeReportModel model)
    {
        var report = (LikeReport)FormatterServices.GetUninitializedObject(LikeReportType);
        var elements = model.ReportElementsList.Where(x => x.OwnerId == null).Cast<LikeReportElementModel>()
            .Select(x => GetLikeElement(x, model.ReportElementsList.Cast<LikeReportElementModel>()));
        ReportInitializer.InitPublicationReport(report, elements, model);
        return report;
    }

    private static LikeReportElement GetLikeElement(LikeReportElementModel model,
        IEnumerable<LikeReportElementModel>? allElements)
    {
        object?[] args =
        {
            model.Name, model.LikeChatName, model.VkId, model.ParticipantId,
            allElements?.Where(x => x.OwnerId == model.Id).Select(x => GetLikeElement(x, null)).ToList()
        };
        
        var element = ElementConstructor.Invoke(args);
        IdFields.EntityId.SetValue(element, model.Id);
        ElementLikes.SetValue(element, model.Likes.Select(GetLikeInfo).ToList());
        return (LikeReportElement)element;
    }

    private static LikeInfo GetLikeInfo(LikeModel model)
    {
        var like = new LikeInfo(model.PublicationId, model.IsLiked, model.IsLoaded);
        IdFields.EntityId.SetValue(like, model.Id);
        return like;
    }
}