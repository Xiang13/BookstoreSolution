using BookstoreApi.Models.DTOs.HOME;
using BookstoreApi.ViewModels.HomeVMs;

namespace BookstoreApi.Services.Interfaces
{
    public interface IBookRepository
    {
        Task<List<BookDTO>> GetBooksByCategoryAsync(string categoryName, int max = 20);
        Task<List<BookDTO>> SearchBooksAsync(BookSearchVM search);
    }
}
