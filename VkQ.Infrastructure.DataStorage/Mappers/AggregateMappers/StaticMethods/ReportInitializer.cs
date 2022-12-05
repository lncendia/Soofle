using System.Reflection;
using VkQ.Domain.Reposts.BaseReport.Entities.Base;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Infrastructure.DataStorage.Models.Reports.Base;
using VkQ.Infrastructure.DataStorage.Models.Reports.Base.PublicationReport;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers.StaticMethods;

internal static class ReportInitializer
{
    private static readonly Type PublicationReportType = typeof(PublicationReport);
    private static readonly Type ReportType = PublicationReportType.BaseType!;

    private static readonly FieldInfo ReportUserId =
        ReportType.GetField("<UserId>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportCreationDate =
        ReportType.GetField("<CreationDate>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportStartDate =
        ReportType.GetField("<StartDate>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportEndDate =
        ReportType.GetField("<EndDate>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportCompleted =
        ReportType.GetField("<IsCompleted>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportSucceeded =
        ReportType.GetField("<IsSucceeded>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportMessage =
        ReportType.GetField("<Message>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportElementsList =
        ReportType.GetField("ReportElementsList", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportLinkedUsersList =
        PublicationReportType.GetField("_linkedUsersList", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportHashtag =
        PublicationReportType.GetField("<Hashtag>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportSearchStartDate =
        PublicationReportType.GetField("<UserId>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo ReportPublicationsList =
        PublicationReportType.GetField("PublicationsList", BindingFlags.Instance | BindingFlags.NonPublic)!;


    internal static void InitPublicationReport(PublicationReport report, IEnumerable<PublicationReportElement> elements,
        PublicationReportModel model)
    {
        ReportLinkedUsersList.SetValue(report, model.LinkedUsers.Select(x => x.Id).ToList());
        ReportHashtag.SetValue(report, model.Hashtag);
        ReportSearchStartDate.SetValue(report, model.SearchStartDate);
        ReportPublicationsList.SetValue(report, model.Publications.Select(GetPublication).ToList());
        InitReport(report, elements, model);
    }

    private static Publication GetPublication(PublicationModel model)
    {
        var pub = new Publication(model.ItemId, model.OwnerId);
        IdFields.EntityId.SetValue(pub, model.Id);
        return pub;
    }

    internal static void InitReport(Report report, IEnumerable<ReportElement> elements, ReportModel reportModel)
    {
        IdFields.AggregateId.SetValue(report, reportModel.Id);
        ReportUserId.SetValue(report, reportModel.UserId);
        ReportCreationDate.SetValue(report, reportModel.CreationDate);
        ReportStartDate.SetValue(report, reportModel.StartDate);
        ReportEndDate.SetValue(report, reportModel.EndDate);
        ReportCompleted.SetValue(report, reportModel.IsCompleted);
        ReportSucceeded.SetValue(report, reportModel.IsSucceeded);
        ReportMessage.SetValue(report, reportModel.Message);
        ReportElementsList.SetValue(report, elements.ToList());
    }
}