using BookstoreApi.Models.DTOs.HOME;

namespace BookstoreApi.Services.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
    }
}
