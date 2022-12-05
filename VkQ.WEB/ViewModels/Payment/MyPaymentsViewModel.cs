namespace VkQ.WEB.ViewModels.Payment
{
    public class MyPaymentsViewModel
    {
        public MyPaymentsViewModel(List<PaymentViewModel> payments, int count, int page = 1)
        {
            Page = page;
            Payments = payments;
            Count = count;
        }

        public int Page { get; }
        public List<PaymentViewModel> Payments { get; set; }
        public int Count { get; }
    }
}