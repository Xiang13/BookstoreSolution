using BookstoreApi.Models.DTOs.HOME;
using BookstoreApi.ViewModels.HomeVMs;

namespace BookstoreApi.Services.Interfaces
{
    public interface IHomeService
    {
        Task<List<CategoryDTO>> GetCategoriesAsync();
        Task<List<CarouselSectionDTO>> GetBooksForCarouselAsync();
        Task<List<BookDTO>> SearchBooksAsync(BookSearchVM search);
    }
}
