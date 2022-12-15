using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

namespace VkQ.Infrastructure.DataStorage.Mappers.ModelMappers;

internal class ParticipantReportModelMapper : IModelMapperUnit<ParticipantReportModel, ParticipantReport>
{
    private readonly ApplicationDbContext _context;

    public ParticipantReportModelMapper(ApplicationDbContext context) => _context = context;

    public async Task<ParticipantReportModel> MapAsync(ParticipantReport model)
    {
        var participantReport =
            await _context.ParticipantReports.Include(x => x.ReportElementsList)
                .FirstOrDefaultAsync(x => x.Id == model.Id) ?? new ParticipantReportModel();

        if (!participantReport.ReportElementsList.Any())
        {
            participantReport.ReportElementsList.AddRange(model.Participants.Select(Create));
        }
        else
        {
            var allElements = model.Participants;
            var deleteElements = participantReport.ReportElementsList
                .ExceptBy(allElements.Select(x => x.Id), element => element.Id).Cast<ParticipantReportElementModel>()
                .ToList();
            participantReport.ReportElementsList.RemoveAll(deleteElements.Contains);

            model.Participants.ForEach(x =>
                Map(x, (ParticipantReportElementModel)participantReport.ReportElementsList.First(y => x.Id == y.Id)));
        }

        ReportModelInitializer.InitReportModel(participantReport, model);
        participantReport.VkId = model.VkId;
        return participantReport;
    }


    private static ParticipantReportElementModel Create(ParticipantReportElement element)
    {
        var model = new ParticipantReportElementModel
        {
            Id = element.Id, Name = element.Name, Type = element.Type, NewName = element.NewName, VkId = element.VkId,
            OwnerId = element.Parent?.Id
        };
        return model;
    }

    private static void Map(ParticipantReportElement element, ParticipantReportElementModel model)
    {
        model.Type = element.Type;
        model.NewName = element.NewName;
    }
}