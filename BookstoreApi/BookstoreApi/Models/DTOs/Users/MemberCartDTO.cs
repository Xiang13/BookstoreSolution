namespace BookstoreApi.Models.DTOs.Users
{
    public class MemberCartDTO
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public string? BookTitle { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public string? ProductImage { get; set; }
    }
}
