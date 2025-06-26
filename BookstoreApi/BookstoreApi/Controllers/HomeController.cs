using BookstoreApi.Services;
using BookstoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        // 宣告一個欄位來保存注入進來的服務物件，型別為介面（抽象層）
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        // 取得分類（含暢銷/推薦）
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var data = await _homeService.GetCategoriesAsync();
            return Ok(data);
        }

        // 輪播
        [HttpGet("carousel/all")]
        public async Task<IActionResult> GetBooksByCategory()
        {
            var data = await _homeService.GetBooksForCarouselAsync();
            return Ok(data);
        }
    }
}
