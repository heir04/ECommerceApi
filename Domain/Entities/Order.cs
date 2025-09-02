namespace ECommerceApi.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCheckedOut { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];     
    }
}