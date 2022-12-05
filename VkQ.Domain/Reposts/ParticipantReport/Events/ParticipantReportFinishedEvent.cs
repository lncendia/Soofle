using VkQ.Domain.Abstractions;
using VkQ.Domain.Participants.Enums;
using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Domain.Reposts.ParticipantReport.Events;

public class ParticipantReportFinishedEvent : IDomainEvent
{
    public ParticipantReportFinishedEvent(Guid participantReportId, Guid userId,
        IEnumerable<ParticipantDto> participants)
    {
        ParticipantReportId = participantReportId;
        UserId = userId;
        Participants.AddRange(participants);
    }


    public Guid ParticipantReportId { get; }
    public Guid UserId { get; }
    public List<ParticipantDto> Participants { get; } = new();


    public class ParticipantDto
    {
        public ParticipantDto(Guid? id, string name, long vkId, ParticipantType type, ElementType elementType)
        {
            if (id == null && elementType != ElementType.New)
                throw new ArgumentException("Id must be set for existing participants");
            Id = id;
            Name = name;
            VkId = vkId;
            Type = type;
            ElementType = elementType;
        }

        public Guid? Id { get; }
        public string Name { get; }
        public long VkId { get; }
        public ParticipantType Type { get; }
        public ElementType ElementType { get; }
    }
}