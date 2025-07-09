using BookstoreApi.Models.DTOs.Books;
using BookstoreApi.Models.Infrastructures.Extensions.Mapping.Books;
using BookstoreApi.Services.Books.Interfaces;
using BookstoreApi.ViewModels.BooksVMs;

namespace BookstoreApi.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;

        public BookService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        // 所有分類清單
        public async Task<IEnumerable<CategoryDTO>> GetAllCategoryListAsync()
        {
            var categories = await _bookRepo.GetAllCategoryListAsync();
            var dtoList = categories.Select(c => c.ToCategoryDTO()).ToList();
            dtoList.InsertRange(0, new List<CategoryDTO>
            {
                new CategoryDTO { CategoryId = -1, CategoryName = "推薦作品" },
                new CategoryDTO { CategoryId = -2, CategoryName = "暢銷作品" },
                new CategoryDTO { CategoryId = -3, CategoryName = "所有作品" },
            });

            return dtoList;    
        }

        // 根據分類取得書籍輪播
        public async Task<IEnumerable<CarouselSectionDTO>> GetBooksForCarouselAsync()
        {
            int carouselCount = 10;
            var result = new List<CarouselSectionDTO>();

            // 1️. 推薦作品（高評分）
            var recommended = await _bookRepo.GetTopRatedBooksAsync(4.0, carouselCount);
            result.Add(recommended.ToCarouselSectionDTO("推薦作品"));

            // 2️. 暢銷作品（銷售量）
            var bestSellers = await _bookRepo.GetBestSellingBooksAsync(carouselCount);
            result.Add(bestSellers.ToCarouselSectionDTO("暢銷作品"));

            // 3️. 一般分類
            var categories = await _bookRepo.GetAllCategoryListAsync();

            foreach (var category in categories)
            {
                var books = await _bookRepo.GetBooksByCategoryNameAsync(category.CategoryName, carouselCount);

                if (books.Any())
                {
                    result.Add(books.ToCarouselSectionDTO(category.CategoryName));
                }
            }

            return result;
        }

        // 依照分類或關鍵字篩選資料
        public async Task<IEnumerable<BookDTO>> GetBooksAsync(BookQueryVM query)
        {
            var books = await _bookRepo.GetBooksAsync(query.CategoryId, query.Keyword);
            return books.ToBookDTO().ToList();
        }
    }

}
