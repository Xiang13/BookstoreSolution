namespace BookstoreApi.ViewModels.BooksVMs
{
    public class BookVM
    {
        public int BookId { get; set; }
        public required string Title { get; set; }
        public required string AuthorName { get; set; }
        public required string CoverImage { get; set; }
        public decimal Price { get; set; }
        public required string CategoryName { get; set; }
        public double AverageRating { get; set; }
    }
}
