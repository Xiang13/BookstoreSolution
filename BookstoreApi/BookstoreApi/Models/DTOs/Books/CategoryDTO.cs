namespace BookstoreApi.Models.DTOs.Books
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public required string Slug { get; set; }
    }
}
