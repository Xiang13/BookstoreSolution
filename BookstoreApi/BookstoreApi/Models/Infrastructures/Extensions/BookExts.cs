using BookstoreApi.Models.DTOs.HOME;
using BookstoreApi.Models.EFModels;

namespace BookstoreApi.Models.Infrastructures.Extensions
{
    public static class BookExts
    {
        public static IQueryable<BookDTO> ToBookDTO(this IQueryable<Book> query)
        {
            return query.Select(b => new BookDTO
            {
                BookId = b.BookId,
                Title = b.Title,
                CoverImage = b.CoverImage,
                Price = b.Price,
                CategoryName = b.Category.CategoryName,
                AverageRating = b.BookReviews.Any() ? b.BookReviews.Average(r => r.Rating) : 0
            });
        }
    }
}
