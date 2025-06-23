using BookstoreApi.Models.DTOs.HOME;
using BookstoreApi.ViewModels.HomeVMs;

namespace BookstoreApi.Services.Interfaces
{
    public interface IHomeService
    {
        Task<List<CategoryDTO>> GetCategoriesAsync();
        Task<List<BookDTO>> GetBooksForCarouselAsync(string categoryName);
        Task<List<BookDTO>> SearchBooksAsync(BookSearchVM search);
    }
}
