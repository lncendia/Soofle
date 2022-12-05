using VkQ.Domain.Participants.Entities;

namespace VkQ.Domain.Reposts.BaseReport.DTOs;

public class ParticipantsDto
{
    public ParticipantsDto(IList<Participant> participants, string likeChatName)
    {
        Participants = participants;
        LikeChatName = likeChatName;
    }

    public string LikeChatName { get; }
    public IList<Participant> Participants { get; }
}