using BookstoreApi.Models.DTOs.Auth;
using BookstoreApi.Models.Infrastructures.Extensions.Mapping.Books;
using BookstoreApi.Services.Books.Interfaces;
using BookstoreApi.Services.Users.Interfaces;
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
        private readonly IUserRepository _userRepository;

        public BookController(IBookService bookService, IUserRepository userRepository)
        {
            _bookService = bookService;
            _userRepository = userRepository;
        }

        //// 登入
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        //{
        //    var token = await _userService.LoginAsync(dto.Email, dto.Password);
        //    if (token == null) return Unauthorized("帳號或密碼錯誤");

        //    return Ok(new { token });
        //}

        //// 註冊
        //[HttpPost("register")]
        //public Task<IActionResult> Register([FromBody] RegisterDTO dto)
        //{
        //    var result = await _userRepository.RegisterUserAsync(dto, dto.Password);
        //    if (!result.Succeeded) return BadRequest(result.Errors);
        //    return Ok();
        //}

        // 登出


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
