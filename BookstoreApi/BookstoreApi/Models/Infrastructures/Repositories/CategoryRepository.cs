using BookstoreApi.Models.DTOs.HOME;
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
            new CategoryDTO { CategoryID = -1, Name = "暢銷作品" },
            new CategoryDTO { CategoryID = -2, Name = "推薦作品" }
        };

            return dbCategories
                .Select(c => new CategoryDTO { CategoryID = c.CategoryID, Name = c.CategoryName })
                .Concat(extra)
                .ToList();
        }
    }
}
