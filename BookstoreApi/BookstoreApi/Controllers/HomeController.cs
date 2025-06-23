using BookstoreApi.Services;
using BookstoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
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
        [HttpGet("carousel/{categoryName}")]
        public async Task<IActionResult> GetBooksByCategory(string categoryName)
        {
            var data = await _homeService.GetBooksForCarouselAsync(categoryName);
            return Ok(data);
        }
    }
}
