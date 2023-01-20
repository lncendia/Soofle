using Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Participants.Specification;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Services.ReportsProcessors.Initializers;

public class ParticipantReportInitializer : IReportInitializerUnit<ParticipantReport>
{
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantReportInitializer(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task InitializeReportAsync(ParticipantReport report, CancellationToken token)
    {
        var participants =
            await _unitOfWork.ParticipantRepository.Value.FindAsync(
                new ParticipantsByUserIdSpecification(report.UserId));
        report.Start(participants);
    }
}