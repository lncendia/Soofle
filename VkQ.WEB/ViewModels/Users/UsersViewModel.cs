using System.ComponentModel.DataAnnotations;
using VkQ.Domain.Users.Entities;

namespace VkQ.WEB.ViewModels.Users;

public class UsersViewModel
{
    [StringLength(50)] public string Username { get; }
    [StringLength(50)] public string Email { get; }
    public List<User> Users { get;  }
    public int Count { get; }
    public int Page { get; } = 1;
}