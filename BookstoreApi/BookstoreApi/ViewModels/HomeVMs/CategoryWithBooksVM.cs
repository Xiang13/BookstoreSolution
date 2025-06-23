namespace BookstoreApi.ViewModels.HomeVMs
{
    public class BookVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public decimal Price { get; set; }
    }

    public class CategoryWithBooksVM
    {
        public string CategoryName { get; set; }
        public List<BookVM> Books { get; set; }
    }
}
