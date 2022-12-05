using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Proxy
{
    public class AddProxyViewModel
    {
        public List<ProxyViewModel> Proxies { get; set; } = null!;

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Список прокси")]
        [StringLength(5000, ErrorMessage = "Не более 5000 символов")]
        public string ProxyList { get; set; } = null!;
    }
}