namespace BookstoreApi.Models.DTOs.Books
{
    public class CarouselSectionDTO
    {
        public required string CategoryName { get; set; }
        public required List<BookDTO> Books { get; set; }
    }
}
