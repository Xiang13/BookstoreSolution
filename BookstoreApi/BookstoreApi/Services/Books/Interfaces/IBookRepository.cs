using BookstoreApi.Models.DTOs.Books;
using BookstoreApi.Models.EFModels;

namespace BookstoreApi.Services.Books.Interfaces
{
    public interface IBookRepository
    {
        // 所有分類清單
        Task<IEnumerable<Category>> GetAllCategoryListAsync();
        // 推薦作品：依評價排序
        Task<IEnumerable<Book>> GetTopRatedBooksAsync(double minRating, int count);
        // 暢銷作品：依銷量排序
        Task<IEnumerable<Book>> GetBestSellingBooksAsync(int count);        
        // 根據分類取得書籍輪播
        Task<IEnumerable<Book>> GetBooksByCategoryNameAsync(string categoryName, int count);
        // 依照分類或關鍵字篩選資料
        Task<IEnumerable<Book>> GetBooksAsync(int? categoryId, string? keyword);
    }
}
