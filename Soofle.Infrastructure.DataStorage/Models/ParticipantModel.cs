using Soofle.Infrastructure.DataStorage.Models.Abstractions;
using Soofle.Domain.Participants.Enums;

namespace Soofle.Infrastructure.DataStorage.Models;

public class ParticipantModel : IAggregateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Notes { get; set; }
    public bool Vip { get; set; }
    public ParticipantType Type { get; set; }
    public long VkId { get; set; }
    public Guid? ParentParticipantId { get; set; }
    public ParticipantModel? ParentParticipant { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = null!;
}