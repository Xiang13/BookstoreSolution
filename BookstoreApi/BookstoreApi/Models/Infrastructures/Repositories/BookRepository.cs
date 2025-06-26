using BookstoreApi.Models.DTOs.HOME;
using BookstoreApi.Models.EFModels;
using BookstoreApi.Models.Infrastructures.Extensions;
using BookstoreApi.Services.Interfaces;
using BookstoreApi.ViewModels.HomeVMs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookstoreApi.Models.Infrastructures.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }

        public async Task<List<CarouselSectionDTO>> GetBooksByCategoryAsync(int carouselCount)
        {

            var result = new List<CarouselSectionDTO>();

            // 虛擬分類：推薦作品（假設根據 Rating 排序）
            var recommendedBooks = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.BookReviews)
                .Where(b => b.BookReviews.Any())
                .Where(b => b.BookReviews.Average(r => r.Rating) >= 4)
                .Take(carouselCount)
                .ToBookDTO()
                .ToListAsync();
            result.Add(new CarouselSectionDTO
            {
                CategoryName = "推薦作品",
                Books = recommendedBooks
            });

            // 虛擬分類：暢銷作品（依銷量排序）
            var popularBooks = await _context.Books
                .OrderByDescending(b => b.SoldQuantity)
                .Take(carouselCount)
                .ToBookDTO()
                .ToListAsync();
            result.Add(new CarouselSectionDTO
            {
                CategoryName = "暢銷作品",
                Books = popularBooks
            });

            // 一般分類（從資料庫讀出）
            var categoryNames = await _context.Categories
                .Select(c => c.CategoryName)
                .ToListAsync(); ;

            foreach (var name in categoryNames)
            {
                var books = await _context.Books
                    .Where(b => b.Category.CategoryName == name)
                    .OrderByDescending(b => b.SoldQuantity)
                    .Take(carouselCount)
                    .ToBookDTO()
                    .ToListAsync();

                result.Add(new CarouselSectionDTO
                {
                    CategoryName = name,
                    Books = books
                });
            }

            return result;



































            //一般分類           
            //var query = await _context.Books
            //.Include(b => b.Category)
            //.Where(b => b.Category.CategoryName == categoryName)
            //.OrderByDescending(b => b.SoldQuantity)
            //.Take(carouselCount)
            //.ToBookDTO()
            //.ToListAsync();

            //return query;
        }

        public async Task<List<BookDTO>> SearchBooksAsync(BookSearchVM search)
        {
            var query = _context.Books.Include(b => b.Category).AsQueryable();

            if (search.CategoryId.HasValue)
            {
                query = query.Where(b => b.Category.CategoryId == search.CategoryId);
            }

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(b => b.Title.Contains(search.Keyword));
            }

            return await query.Select(b => new BookDTO
            {
                BookId = b.BookId,
                Title = b.Title,
                CoverImage = b.CoverImage,
                Price = b.Price,
                CategoryName = b.Category.CategoryName,
            }).ToListAsync();
        }
    }
}
