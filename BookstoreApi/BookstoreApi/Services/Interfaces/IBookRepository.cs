using BookstoreApi.Models.DTOs.HOME;
using BookstoreApi.ViewModels.HomeVMs;

namespace BookstoreApi.Services.Interfaces
{
    public interface IBookRepository
    {
        Task<List<CarouselSectionDTO>> GetBooksByCategoryAsync(int carouselCount);
        Task<List<BookDTO>> SearchBooksAsync(BookSearchVM search);
    }
}
