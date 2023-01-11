namespace OpenPOS_API.Models;

public class Listener
{
    public string PaymentRequestToken { get; set; }
    public string  ConnectionId { get; set; }
    
    // No constructor needed, only used to pass on objects
}