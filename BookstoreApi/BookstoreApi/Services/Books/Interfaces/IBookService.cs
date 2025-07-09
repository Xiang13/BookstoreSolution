using BookstoreApi.Models.DTOs.Books;
using BookstoreApi.ViewModels.BooksVMs;

namespace BookstoreApi.Services.Books.Interfaces
{
    public interface IBookService
    {
        // 所有分類清單
        Task<IEnumerable<CategoryDTO>> GetAllCategoryListAsync();

        // 根據分類取得書籍輪播
        Task<IEnumerable<CarouselSectionDTO>> GetBooksForCarouselAsync();

        // 依照分類或關鍵字篩選資料
        Task<IEnumerable<BookDTO>> GetBooksAsync(BookQueryVM query);
    }
}
