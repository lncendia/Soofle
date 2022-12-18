using VkQ.WEB.ViewModels.Elements.Base;

namespace VkQ.WEB.ViewModels.Elements;

public class LikeElementViewModel : PublicationElementViewModel
{
    public LikeElementViewModel(string name, long vkId, string likeChatName, Guid participantId, bool isAccepted,
        bool vip, IEnumerable<LikeElementViewModel> children, IEnumerable<LikeViewModel> likes) : base(name, vkId,
        likeChatName, participantId,
        isAccepted, vip)
    {
        Likes = likes.ToList();
        Children = children.ToList();
    }

    public List<LikeViewModel> Likes { get; }
    public List<LikeElementViewModel> Children { get; }
}

public class LikeViewModel
{
    public LikeViewModel(Guid publicationId, bool isLiked, bool isLoaded)
    {
        PublicationId = publicationId;
        IsLiked = isLiked;
        IsLoaded = isLoaded;
    }

    public Guid PublicationId { get; }
    public bool IsLiked { get; }
    public bool IsLoaded { get; }
}