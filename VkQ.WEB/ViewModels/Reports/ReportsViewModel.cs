namespace VkQ.WEB.ViewModels.Reports
{
    public class ReportsViewModel
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public User User { get; set; }
        public List<Report> Reports { get; set; }
        public int Count { get; set; }
    }
}