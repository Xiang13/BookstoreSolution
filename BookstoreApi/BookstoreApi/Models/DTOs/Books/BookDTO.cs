namespace BookstoreApi.Models.DTOs.Books
{
    public class BookDTO
    {
        public int BookId { get; set; }        
        public int CategoryId { get; set; }
        public required string AuthorName { get; set; }
        public required string Title { get; set; }
        public required string CoverImage { get; set; }
        public required string CategoryName { get; set; }
        public decimal Price { get; set; }
        public double AverageRating { get; set; }
    }    
}
