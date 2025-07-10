namespace BookstoreApi.ViewModels.BooksVMs
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public required string Slug { get; set; }
    }
}
