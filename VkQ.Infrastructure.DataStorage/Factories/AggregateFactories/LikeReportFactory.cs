using System.Reflection;
using System.Runtime.Serialization;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.LikeReport.ValueObjects;
using VkQ.Infrastructure.DataStorage.Factories.Abstractions;
using VkQ.Infrastructure.DataStorage.Factories.AggregateFactories.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

namespace VkQ.Infrastructure.DataStorage.Factories.AggregateFactories;

internal class LikeReportFactory : IAggregateFactory<LikeReport, LikeReportModel>
{
    private static readonly Type LikeReportType = typeof(LikeReport);
    private static readonly Type LikeReportElementType = typeof(LikeReportElement);

    private static readonly FieldInfo ElementId =
        LikeReportElementType.GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ElementLikes =
        LikeReportElementType.GetField("<Likes>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo LikeInfoId =
        typeof(LikeInfo).GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;


    public LikeReport Create(LikeReportModel model)
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
        var element = new LikeReportElement(model.Name, model.LikeChatName, model.VkId, model.ParticipantId,
            allElements?.Where(x => x.OwnerId == model.Id).Select(x => GetLikeElement(x, null)).ToList());
        ElementId.SetValue(element, model.Id);
        ElementLikes.SetValue(element, model.Likes.Select(GetLikeInfo).ToList());
        return element;
    }

    private static LikeInfo GetLikeInfo(LikeModel model)
    {
        var like = new LikeInfo(model.PublicationId, model.IsLiked, model.IsLoaded);
        LikeInfoId.SetValue(like, model.Id);
        return like;
    }
}