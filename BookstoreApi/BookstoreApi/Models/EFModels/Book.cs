namespace BookstoreApi.Models.EFModels
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public string AuthorName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public int SoldQuantity { get; set; }
        public string CoverImage { get; set; }
    }
}




