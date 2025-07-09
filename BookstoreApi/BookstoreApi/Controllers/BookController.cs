using BookstoreApi.Models.Infrastructures.Extensions.Mapping.Books;
using BookstoreApi.Services.Books.Interfaces;
using BookstoreApi.ViewModels.BooksVMs;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        // 宣告一個欄位來保存注入進來的服務物件，型別為介面（抽象層）
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // 取得分類資料
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var data = await _bookService.GetAllCategoryListAsync();
            return Ok(data.ToCategoryVM());
        }

        // 取得輪播資料
        [HttpGet("carousel/all")]
        public async Task<IActionResult> GetCarousel()
        {
            var data = await _bookService.GetBooksForCarouselAsync();
            return Ok(data.ToCarouselSectionVMs());
        }

        // 依照分類或關鍵字篩選資料
        [HttpGet("books")]
        public async Task<IActionResult> GetBooks([FromQuery] BookQueryVM query)
        {
            var bookDtos = await _bookService.GetBooksAsync(query);
            var bookvms = bookDtos.Select(b => b.ToBookVM());
            return Ok(bookvms);
        }
    }
}
