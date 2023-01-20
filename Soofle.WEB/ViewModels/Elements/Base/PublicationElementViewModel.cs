﻿namespace Soofle.WEB.ViewModels.Elements.Base;

public abstract class PublicationElementViewModel : ElementViewModel
{
    protected PublicationElementViewModel(string name, long vkId, string likeChatName, Guid participantId,
        bool isAccepted, bool vip, string? note) : base(name, vkId)
    {
        LikeChatName = likeChatName;
        ParticipantId = participantId;
        IsAccepted = isAccepted;
        Vip = vip;
        Note = note;
    }

    public string LikeChatName { get; }
    public Guid ParticipantId { get; }
    public bool IsAccepted { get; }
    public bool Vip { get; }
    public string? Note { get; }
}