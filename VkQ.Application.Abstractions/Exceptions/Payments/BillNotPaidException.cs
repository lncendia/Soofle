namespace VkQ.Application.Abstractions.Exceptions.Payments;

public class BillNotPaidException : Exception
{
    public BillNotPaidException(string billId) : base($"Bill {billId} has not been paid.")
    {
    }
}