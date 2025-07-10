using BookstoreApi.Models.DTOs.Auth;
using BookstoreApi.Models.EFModels;
using BookstoreApi.Models.Infrastructures.Extensions.Mapping.Auth;
using BookstoreApi.Services.Auth;
using BookstoreApi.Services.Auth.Interfaces;
using BookstoreApi.ViewModels.AuthVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(AuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }
        // 登入
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM vm)
        {            
            var user = await _authService.ValidateUserAsync(vm.ToLoginDTO());            
            var token = _jwtService.GenerateToken(user);
            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                //Secure = true,  // 若在本機測試 http，可暫時設 false
                //SameSite = SameSiteMode.Strict,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1),
                Path = "/"
            });
            return Ok(new { message = "登入成功" });
        }

        // 登出
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            //Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("access_token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1), // 雖然 Delete 會自動處理，這只是保險
                Path = "/"
            });
            return Ok(new { message = "已登出" });
        }

        // 註冊
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            throw new NotImplementedException();
        }

        // 驗證是否登入 - 透過 JWT 判斷登入與回傳用戶資料
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            throw new NotImplementedException();
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RegisterDTO dto)
        {
            throw new NotImplementedException();
        }

        // 忘記密碼


        // 重設密碼
    }
}
