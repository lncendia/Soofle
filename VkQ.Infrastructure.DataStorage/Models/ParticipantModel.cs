using VkQ.Domain.Participants.Enums;

namespace VkQ.Infrastructure.DataStorage.Models;

public class ParticipantModel : IModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ParticipantType Type { get; set; }
    public long VkId { get; set; }
    public Guid? ParentParticipantId { get; set; }
    public ParticipantModel? ParentParticipant { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = null!;
}