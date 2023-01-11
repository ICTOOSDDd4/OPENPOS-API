namespace OpenPOS_API.Models;

public class Tikkie
{
    public string SubscriptionId { get; set; }
    public string NotificationType { get; set; }
    public string PaymentRequestToken { get; set; }
    public string PaymentToken { get; set; }
    
    // No constructor needed, only used to pass on objects
}