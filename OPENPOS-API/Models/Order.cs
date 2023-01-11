namespace OpenPOS_API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BillId { get; set; }
        public bool Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public Order() { }
        public Order(int id, bool status, int userId, int billId, DateTime updatedAt, DateTime createdAt)
        {
            Id = id;
            Status = status;
            UserId = userId;
            BillId = billId;
            UpdatedAt = updatedAt;
            CreatedAt = createdAt;
        }
    }
}