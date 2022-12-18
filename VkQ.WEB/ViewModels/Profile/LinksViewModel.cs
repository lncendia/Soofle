namespace VkQ.WEB.ViewModels.Profile;

public class LinksViewModel
{
    public LinksViewModel(Guid currentUserId, string scheme, IEnumerable<LinkViewModel> links)
    {
        CurrentUserId = currentUserId;
        Scheme = scheme;
        Links = links.ToList();
    }

    public List<LinkViewModel> Links { get; }
    public Guid CurrentUserId { get; }
    public string Scheme { get; }
}

public class LinkViewModel
{
    public LinkViewModel(Guid id, string user1, string user2, bool isConfirmed)
    {
        IsConfirmed = isConfirmed;
        Id = id;
        User1 = user1;
        User2 = user2;
    }

    public Guid Id { get; }
    public string User1 { get; }
    public string User2 { get; }
    public bool IsConfirmed { get; }
}