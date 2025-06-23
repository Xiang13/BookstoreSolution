using BookstoreApi.Models.DTOs.HOME;
using BookstoreApi.Models.EFModels;
using BookstoreApi.Services.Interfaces;
using BookstoreApi.ViewModels.HomeVMs;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookstoreApi.Models.Infrastructures.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }

        public async Task<List<BookDTO>> GetBooksByCategoryAsync(string categoryName, int max = 5)
        {
            var query = _context.Books
            .Include(b => b.Category)
            .Where(b => b.Category.CategoryName == categoryName)
            .OrderByDescending(b => b.SoldQuantity)
            .Take(max);

            return await query.Select(b => new BookDTO
            {
                BookID = b.BookID,
                Title = b.Title,
                CoverImage = b.CoverImage,
                Price = b.Price,
                CategoryName = b.Category.CategoryName
            }).ToListAsync();
        }

        public async Task<List<BookDTO>> SearchBooksAsync(BookSearchVM search)
        {
            var query = _context.Books.Include(b => b.Category).AsQueryable();

            if (search.CategoryId.HasValue)
            {
                query = query.Where(b => b.Category.CategoryID == search.CategoryId);
            }

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(b => b.Title.Contains(search.Keyword));
            }

            return await query.Select(b => new BookDTO
            {
                BookID = b.BookID,
                Title = b.Title,
                CoverImage = b.CoverImage,
                Price = b.Price,
                CategoryName = b.Category.CategoryName
            }).ToListAsync();
        }
    }
}
