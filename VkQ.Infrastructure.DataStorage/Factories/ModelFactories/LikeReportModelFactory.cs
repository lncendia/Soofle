using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Factories.Abstractions;
using VkQ.Infrastructure.DataStorage.Factories.AggregateFactories.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

namespace VkQ.Infrastructure.DataStorage.Factories.ModelFactories;

internal class LikeReportModelFactory : IModelFactory<LikeReportModel, LikeReport>
{
    private readonly ApplicationDbContext _context;

    public LikeReportModelFactory(ApplicationDbContext context) => _context = context;

    public async Task<LikeReportModel> CreateAsync(LikeReport model)
    {
        var likeReport = await _context.LikeReports.Include(x => x.Publications).Include(x => x.LinkedUsers)
            .Include(x => x.ReportElementsList.Cast<LikeReportElementModel>()).ThenInclude(x => x.Likes)
            .FirstOrDefaultAsync(x => x.Id == model.Id) ?? new LikeReportModel();


        if (!likeReport.ReportElementsList.Any())
        {
            foreach (var reportElementModel in model.Elements)
            {
                var parent = Create(reportElementModel, null);
                likeReport.ReportElementsList.AddRange(reportElementModel.Children.Select(child => Create(child, parent)));
                likeReport.ReportElementsList.Add(parent);
            }
        }
        else
        {
            foreach (var reportElementModel in model.Elements)
            {
                Map(reportElementModel,
                    (LikeReportElementModel)likeReport.ReportElementsList.First(y => reportElementModel.Id == y.Id));
                foreach (var childModel in reportElementModel.Children)
                    Map(childModel,
                        (LikeReportElementModel)likeReport.ReportElementsList.First(y => childModel.Id == y.Id));
            }
        }

        if (!likeReport.LinkedUsers.Any())
        {
            var users = await _context.Users.Where(x => model.LinkedUsers.Any(y => y == x.Id)).ToListAsync();
            likeReport.LinkedUsers = users;
        }

        ReportModelInitializer.InitPublicationReportModel(likeReport, model);

        return likeReport;
    }


    private static LikeReportElementModel Create(LikeReportElement element, LikeReportElementModel? owner)
    {
        var model = new LikeReportElementModel
        {
            Id = element.Id, Name = element.Name, IsAccepted = element.IsAccepted,
            ParticipantId = element.ParticipantId, VkId = element.VkId, LikeChatName = element.LikeChatName,
            Owner = owner,
            Likes = element.Likes.Select(x => new LikeModel
                { PublicationId = x.PublicationId, IsLiked = x.IsLiked, IsLoaded = x.IsLoaded }).ToList()
        };

        return model;
    }

    private static void Map(LikeReportElement element, LikeReportElementModel model)
    {
        foreach (var like in element.Likes.Where(like => model.Likes.All(x => x.PublicationId != like.PublicationId)))
        {
            model.Likes.Add(new LikeModel
            {
                PublicationId = like.PublicationId, IsLiked = like.IsLiked, IsLoaded = like.IsLoaded,
                LikeReportElementId = model.Id
            });
        }
    }
}