namespace BookstoreApi.ViewModels.BooksVMs
{
    public class CarouselSectionVM
    {
        public required string CategoryName { get; set; }
        public required List<BookVM> Books { get; set; }
    }
}
