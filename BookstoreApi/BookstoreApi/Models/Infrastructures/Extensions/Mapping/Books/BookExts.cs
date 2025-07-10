using BookstoreApi.Models.DTOs.Books;
using BookstoreApi.Models.EFModels;
using BookstoreApi.ViewModels.BooksVMs;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookstoreApi.Models.Infrastructures.Extensions.Mapping.Books
{
    public static class BookExts
    {
        // ToVM
        /// <summary>
        /// 將單一 CategoryDTO 轉換為 CategoryVM（給前端使用）
        /// </summary>
        /// <param name="dto">來源 CategoryDTO</param>
        /// <returns>轉換後的 CategoryVM</returns>
        public static List<CategoryVM> ToCategoryVM(this IEnumerable<CategoryDTO> dtos)
        {
            return dtos.Select(dto => new CategoryVM
            {
                CategoryId = dto.CategoryId,
                CategoryName = dto.CategoryName,
                Slug = dto.Slug,
            }).ToList();
        }

        /// <summary>
        /// 將單一 BookDTO 轉換為 BookVM（給前端使用）
        /// </summary>
        /// <param name="dto">來源 BookDTO</param>
        /// <returns>轉換後的 BookVM</returns>
        public static BookVM ToBookVM(this BookDTO dto)
        {
            return new BookVM
            {
                BookId = dto.BookId,
                Title = dto.Title,
                AuthorName = dto.AuthorName,
                CoverImage = dto.CoverImage,
                Price = dto.Price,
                CategoryName = dto.CategoryName,
                AverageRating = dto.AverageRating
            };
        }

        /// <summary>
        /// 將 CarouselSectionDTO 轉換為 CarouselSectionVM（含書籍清單）
        /// </summary>
        /// <param name="dto">來源區塊 DTO</param>
        /// <returns>轉換後的 ViewModel</returns>
        public static CarouselSectionVM ToCarouselSectionVM(this CarouselSectionDTO dto)
        {
            return new CarouselSectionVM
            {
                CategoryName = dto.CategoryName,
                Books = dto.Books.Select(b => b.ToBookVM()).ToList()
            };
        }

        /// <summary>
        /// 將 CarouselSectionDTO 的清單轉換為 CarouselSectionVM 清單，
        /// 適用於回傳多個書籍區塊給前端畫面使用。
        /// 此方法會逐筆呼叫 ToCarouselSectionVM 進行轉換。
        /// </summary>
        /// <param name="dtos">CarouselSectionDTO 清單</param>
        /// <returns>轉換後的 CarouselSectionVM 清單</returns>
        public static List<CarouselSectionVM> ToCarouselSectionVMs(this IEnumerable<CarouselSectionDTO> dtos)
        {
            return dtos.Select(d => d.ToCarouselSectionVM()).ToList();
        }
    
        // ToDTO
        public static List<BookDTO> ToBookDTO(this IEnumerable<Book> query)
        {
            return query.Select(b => new BookDTO
            {
                CategoryId = b.CategoryId,
                BookId = b.BookId,
                AuthorName = b.Author.AuthorName,
                Title = b.Title,
                CoverImage = b.CoverImage,
                Price = b.Price,
                CategoryName = b.Category.CategoryName,
                AverageRating = b.BookReviews.Any() ? b.BookReviews.Average(r => r.Rating) : 0
            }).ToList();
        }

        public static CategoryDTO ToCategoryDTO(this Category category)
        {
            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Slug = category.Slug,
            };
        }
    
        public static CarouselSectionDTO ToCarouselSectionDTO(this IEnumerable<Book> books, string categoryName)
        {
            return new CarouselSectionDTO
            {
                CategoryName = categoryName,
                Books = books.ToBookDTO()
            };
        }
    }
}
