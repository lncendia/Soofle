namespace VkQ.Application.Abstractions.Links.DTOs;

public class LinkDto
{
    public LinkDto(Guid user1Id, Guid user2Id, string user1Name, string user2Name, bool isConfirmed)
    {
        User1Id = user1Id;
        User2Id = user2Id;
        User1Name = user1Name;
        User2Name = user2Name;
        IsConfirmed = isConfirmed;
    }
    
    public Guid User1Id { get; }
    public Guid User2Id { get; }
    public string User1Name { get; }
    public string User2Name { get; }
    public bool IsConfirmed { get;}
}