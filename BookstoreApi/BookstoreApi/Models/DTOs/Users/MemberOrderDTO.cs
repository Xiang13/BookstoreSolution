namespace BookstoreApi.Models.DTOs.Users
{
    public class MemberOrderDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public required string OrderStatus { get; set; }
        public required string PaymentStatus { get; set; }
        public required string PickupMethod { get; set; }       
        public List<MemberOrderDetailDTO> OrderDetails { get; set; } = new();
    }

    public class MemberOrderDetailDTO
    {
        public int OrderDetailId { get; set; }
        public int BookId { get; set; }
        public required string BookTitle { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public required string ProductImage { get; set; }
    }
}
