namespace VkQ.Application.Abstractions.DTO.Payments;

public class PaymentData
{
    public PaymentData(string id, string payUrl, decimal cost)
    {
        BillId = id;
        PayUrl = payUrl;
        Cost = cost;
    }

    public string BillId { get; }
    public string PayUrl { get; }
    public decimal Cost { get; }
}