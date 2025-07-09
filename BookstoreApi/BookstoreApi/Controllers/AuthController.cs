using BookstoreApi.Models.DTOs.Auth;
using BookstoreApi.Models.Infrastructures.Extensions.Mapping.Auth;
using BookstoreApi.Services.Auth;
using BookstoreApi.Services.Auth.Interfaces;
using BookstoreApi.ViewModels.AuthVMs;
using Microsoft.AspNetCore.Mvc;


namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        // 登入
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM vm)
        {            
            var result = await _authService.LoginAsync(vm.ToLoginDTO());

            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(result);
        }

        // 登出
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            throw new NotImplementedException();
        }

        // 註冊
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
