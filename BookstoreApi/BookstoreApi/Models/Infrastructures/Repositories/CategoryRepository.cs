using BookstoreApi.Models.DTOs.HOME;
using BookstoreApi.Models.EFModels;
using BookstoreApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Models.Infrastructures.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var dbCategories = await _context.Categories.ToListAsync();

            // 人工分類補上（不在 DB）
            var extra = new List<CategoryDTO>
        {
            new CategoryDTO { CategoryId = -1, Name = "推薦作品" },
            new CategoryDTO { CategoryId = -2, Name = "暢銷作品" },
            new CategoryDTO { CategoryId = -3, Name = "所有作品" },
        };

            return dbCategories
                .Select(c => new CategoryDTO { CategoryId = c.CategoryId, Name = c.CategoryName })
                .Concat(extra)
                .ToList();
        }
    }
}
