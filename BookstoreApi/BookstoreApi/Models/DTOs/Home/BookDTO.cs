namespace BookstoreApi.Models.DTOs.HOME
{
    public class BookDTO
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}
