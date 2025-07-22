namespace BookstoreApi.Models.DTOs.Users
{
    public class MemberOrderListDTO
    {
        public int OrderId { get; set; }
        //public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public required string OrderStatus { get; set; }
        public required string PaymentStatus { get; set; }
        public required string PickupMethod { get; set; }
        public List<string> BookImages { get; set; } = new();
    }
}
