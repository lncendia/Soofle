using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Domain.Services.StaticMethods;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.Specification;
using VkQ.Domain.Users.Specification.Visitor;

namespace VkQ.Domain.Services.Services.Processors;

public class ParticipantReportProcessorService : IReportProcessorService<ParticipantReport>
{
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantReportProcessorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ProcessReportAsync(ParticipantReport report, CancellationToken token)
    {
        if (!report.IsStarted) throw new ReportNotStartedException();
        if (report.IsCompleted) throw new ReportAlreadyCompletedException();
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);
        
        report.Finish();
        await _unitOfWork.LikeReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }
    
    private async Task LoadParticipantsAsync(ParticipantReport report)
    {
        ISpecification<Participant, IParticipantSpecificationVisitor> participantSpec =
            new ParticipantsByUserIdSpecification(report.UserId);
        var participants = await _unitOfWork.ParticipantRepository.Value.FindAsync(participantSpec);

        try
        {
            var participantsList = participants.Result.GroupBy(x => x.UserId)
                .Select(x => (x.AsEnumerable(), chats.Result.First(y => y.Id == x.Key).Name)).ToList();
            report.LoadElements(participantsList);
        }
        catch (InvalidOperationException)
        {
            throw new LinkedUserNotFoundException();
        }
    }
    
}