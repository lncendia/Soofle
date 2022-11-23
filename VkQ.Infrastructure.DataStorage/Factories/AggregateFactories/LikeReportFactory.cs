using System.Reflection;
using VkQ.Domain.Reposts.BaseReport.Entities.Base;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.ValueObjects;
using VkQ.Infrastructure.DataStorage.Factories.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;
using VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

namespace VkQ.Infrastructure.DataStorage.Factories.AggregateFactories;

public class LikeReportFactory : IAggregateFactory<LikeReport, LikeReportModel>
{
    private static readonly Type PublicationReportType = typeof(PublicationReport);

    private static readonly FieldInfo LikeReportId =
        LikeReportType.GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    
    
    
    
    
    
    
    public Guid Id { get; }
    public Guid UserId { get; }
    public DateTimeOffset CreationDate { get; } = DateTimeOffset.Now;
    public DateTimeOffset? StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsSucceeded { get; private set; }
    public string? Message { get; private set; }

    protected readonly List<ReportElement> ReportElementsList = new();
    
    
    
    
    private readonly List<Guid> _linkedUsersList = new();
    public string Hashtag { get; }
    public DateTimeOffset? SearchStartDate { get; }

    protected List<Publication> PublicationsList = new();



    public LikeReport Create(LikeReportModel model)
    {
        var likeReport = new LikeReport(model.Name, model.Email);
        LikeReportId.SetValue(likeReport, model.Id);

        if (model.SubscriptionDate.HasValue)
            LikeReportSubscription.SetValue(likeReport, GetSubscription(model.SubscriptionDate.Value, model.ExpirationDate!.Value));

        if (model.Vk != null) LikeReportVk.SetValue(likeReport, GetVk(model.Vk));
        return likeReport;
    }


    private static readonly Type VkType = typeof(Vk);

    private static readonly FieldInfo VkId =
        VkType.GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo VkProxy =
        VkType.GetField("<ProxyId>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;


    private static Vk GetVk(VkModel model)
    {
        var vk = new Vk(model.LikeReportname, model.Password);
        VkId.SetValue(vk, model.Id);
        VkProxy.SetValue(vk, model.ProxyId);
        if (!string.IsNullOrEmpty(model.AccessToken)) vk.UpdateToken(model.AccessToken);
        return vk;
    }

    private static readonly FieldInfo SubscriptionEndDate =
        typeof(Subscription).GetField("<SubscriptionDate>k__BackingField",
            BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static Subscription GetSubscription(DateTimeOffset start, DateTimeOffset end)
    {
        var sub = new Subscription(end);
        SubscriptionEndDate.SetValue(sub, start);
        return sub;
    }
}