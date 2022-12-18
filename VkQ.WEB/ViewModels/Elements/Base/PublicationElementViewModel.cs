namespace VkQ.WEB.ViewModels.Elements.Base;

public abstract class PublicationElementViewModel : ElementViewModel
{
    protected PublicationElementViewModel(string name, long vkId, string likeChatName, Guid participantId,
        bool isAccepted, bool vip) : base(name, vkId)
    {
        LikeChatName = likeChatName;
        ParticipantId = participantId;
        IsAccepted = isAccepted;
        Vip = vip;
    }

    public string LikeChatName { get; }
    public Guid ParticipantId { get; }
    public bool IsAccepted { get; }
    public bool Vip { get; }
}