using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Users
{
    public class UsersViewModel
    {
        [StringLength(50)] public string Username { get; set; }
        [StringLength(50)] public string Email { get; set; }
        public List<User> Users { get; set; }
        public int Count { get; set; }
        public int Page { get; set; } = 1;
        public string Message { get; set; }
    }
}