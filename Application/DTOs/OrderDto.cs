namespace ECommerceApi.Application.DTOs
{
    public class OrderCreateDto
    {
        public Guid UserId { get; set; }
        // public List<OrderItemCreateDto> OrderItems { get; set; } = new();
    }

    public class OrderUpdateDto
    {
        public Guid UserId { get; set; }
        public decimal? TotalAmount { get; set; }
    }

    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserResponseDto? User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemResponseDto> OrderItems { get; set; } = new();
    }

    public class OrderListDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsCheckedOut { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int ItemCount { get; set; }
    }
}
