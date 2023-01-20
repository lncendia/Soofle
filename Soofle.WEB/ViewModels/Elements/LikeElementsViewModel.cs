using Soofle.WEB.ViewModels.Elements.Base;

namespace Soofle.WEB.ViewModels.Elements;

public class LikeElementsViewModel
{
    public LikeElementsViewModel(IEnumerable<LikeElementViewModel> elements, List<PublicationViewModel> publications)
    {
        Elements = elements.OrderByDescending(x => x.IsAccepted);
        Publications = publications;
    }

    public IEnumerable<LikeElementViewModel> Elements { get; }
    public List<PublicationViewModel> Publications { get; }
}

public class LikeElementViewModel : PublicationElementViewModel
{
    public LikeElementViewModel(string name, long vkId, string likeChatName, Guid participantId, bool isAccepted,
        bool vip, string? note, IEnumerable<LikeElementViewModel> children, IEnumerable<LikeViewModel> likes) : base(
        name, vkId,
        likeChatName, participantId,
        isAccepted, vip, note)
    {
        Likes = likes.OrderByDescending(x => x.IsConfirmed).ToList();
        Children = children.OrderByDescending(x => x.IsAccepted).ToList();
    }

    public List<LikeViewModel> Likes { get; }
    public List<LikeElementViewModel> Children { get; }
}

public class LikeViewModel
{
    public LikeViewModel(int publicationId, bool isConfirmed)
    {
        PublicationId = publicationId;
        IsConfirmed = isConfirmed;
    }

    public int PublicationId { get; }
    public bool IsConfirmed { get; }
}