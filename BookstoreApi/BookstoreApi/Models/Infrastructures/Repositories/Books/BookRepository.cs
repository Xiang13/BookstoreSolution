using BookstoreApi.Models.DTOs.Books;
using BookstoreApi.Models.EFModels;
using BookstoreApi.Models.Infrastructures.Extensions.Mapping.Books;
using BookstoreApi.Models.Infrastructures.Repositories.Base;
using BookstoreApi.Services.Books.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Models.Infrastructures.Repositories.Books
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }

        // 所有分類清單
        public async Task<IEnumerable<Category>> GetAllCategoryListAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        // 推薦作品：依評價排序
        public async Task<IEnumerable<Book>> GetTopRatedBooksAsync(double minRating, int count)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.BookReviews)
                .Where(b => b.BookReviews.Any())
                .Where(b => b.BookReviews.Average(r => r.Rating) >= minRating)
                .OrderByDescending(b => b.BookReviews.Average(r => r.Rating))
                .Take(count)
                .ToListAsync();
        }

        // 暢銷作品：依銷量排序
        public async Task<IEnumerable<Book>> GetBestSellingBooksAsync(int count)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.BookReviews)
                .OrderByDescending(b => b.SoldQuantity)
                .Take(count)
                .ToListAsync();
        }        

        // 指定分類下的書籍 (輪播用)
        public async Task<IEnumerable<Book>> GetBooksByCategoryNameAsync(string categoryName, int count)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.BookReviews)
                .Where(b => b.Category.CategoryName == categoryName)
                .OrderByDescending(b => b.SoldQuantity)
                .Take(count)
                .ToListAsync();
        }

        // 依照分類或關鍵字篩選資料
        public async Task<IEnumerable<Book>> GetBooksAsync(int? categoryId, string? keyword)
        {
            IQueryable<Book> query = _context.Books.Include(b => b.Author).Include(b => b.Category);

            // 推薦作品：平均評分 >= 4            
            if (categoryId == -1)
            {
                query = query
                    .Where(b => b.BookReviews.Any())
                    .Where(b => b.BookReviews.Average(r => r.Rating) >= 4)
                    .OrderByDescending(b => b.BookReviews.Average(r => r.Rating));
            }
            // 暢銷作品：依銷量排序
            else if (categoryId == -2)
            {
                query = query.OrderByDescending(b => b.SoldQuantity);
            }

            // 所有作品
            else if (categoryId == -3)
            {
                // 不做任何事
            }

            else if (categoryId.HasValue) // 一般分類
            {
                query = query.Where(b => b.Category.CategoryId == categoryId)
                             .OrderByDescending(b => b.SoldQuantity);
            }

            // 加入關鍵字篩選（無論哪一種分類）
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b =>
                    b.Title.Contains(keyword) ||
                    b.Author.AuthorName.Contains(keyword));
            }
            return await query.ToListAsync();

        }

    }
}
