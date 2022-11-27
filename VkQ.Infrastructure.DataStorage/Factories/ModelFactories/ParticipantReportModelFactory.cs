using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Factories.Abstractions;
using VkQ.Infrastructure.DataStorage.Factories.AggregateFactories.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

namespace VkQ.Infrastructure.DataStorage.Factories.ModelFactories;

internal class ParticipantReportModelFactory : IModelFactory<ParticipantReportModel, ParticipantReport>
{
    private readonly ApplicationDbContext _context;

    public ParticipantReportModelFactory(ApplicationDbContext context) => _context = context;

    public async Task<ParticipantReportModel> CreateAsync(ParticipantReport model)
    {
        var participantReport =
            await _context.ParticipantReports.Include(x => x.ReportElementsList)
                .FirstOrDefaultAsync(x => x.Id == model.Id) ?? new ParticipantReportModel();

        var elements = model.Participants
            .ExceptBy(participantReport.ReportElementsList.Select(x => x.Id), element => element.Id);

        foreach (var element in elements)
        {
            var parent = Create(element, null);
            participantReport.ReportElementsList.AddRange(element.Children.Select(x => Create(x, parent)));
            participantReport.ReportElementsList.Add(parent);
        }

        ReportModelInitializer.InitReportModel(participantReport, model);
        participantReport.VkId = model.VkId;
        return participantReport;
    }


    private static ParticipantReportElementModel Create(ParticipantReportElement element,
        ParticipantReportElementModel? owner)
    {
        var model = new ParticipantReportElementModel
        {
            Id = element.Id, Name = element.Name, Type = element.Type, OldName = element.OldName, VkId = element.VkId,
            Owner = owner
        };
        return model;
    }
}