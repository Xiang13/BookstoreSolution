namespace BookstoreApi.Models.DTOs.HOME
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public double AverageRating { get; set; }
    }

    public class CarouselSectionDTO
    {
        public string CategoryName { get; set; }
        public bool IsSpecial { get; set; } = false;
        public List<BookDTO> Books { get; set; }
    }
}
