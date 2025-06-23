using BookstoreApi.Models.DTOs.HOME;
using BookstoreApi.Services.Interfaces;
using BookstoreApi.ViewModels.HomeVMs;

namespace BookstoreApi.Services
{
    public class HomeService : IHomeService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IBookRepository _bookRepo;

        public HomeService(ICategoryRepository categoryRepo, IBookRepository bookRepo)
        {
            _categoryRepo = categoryRepo;
            _bookRepo = bookRepo;
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync()
        {
            return await _categoryRepo.GetAllCategoriesAsync();
        }

        public async Task<List<BookDTO>> GetBooksForCarouselAsync(string categoryName)
        {
            return await _bookRepo.GetBooksByCategoryAsync(categoryName, 10);
        }

        public async Task<List<BookDTO>> SearchBooksAsync(BookSearchVM search)
        {
            return await _bookRepo.SearchBooksAsync(search);
        }
    }

}
