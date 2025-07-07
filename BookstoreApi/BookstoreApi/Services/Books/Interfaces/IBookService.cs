using BookstoreApi.Models.DTOs.Books;
using BookstoreApi.ViewModels.BooksVMs;

namespace BookstoreApi.Services.Books.Interfaces
{
    public interface IBookService
    {
        // 分類資料
        Task<IEnumerable<CategoryDTO>> GetAllCategoryListAsync();

        // 輪播資料
        Task<IEnumerable<CarouselSectionDTO>> GetBooksForCarouselAsync();

        // 依照分類或關鍵字篩選資料
        Task<IEnumerable<BookDTO>> GetBooksAsync(BookQueryVM query);
    }
}
