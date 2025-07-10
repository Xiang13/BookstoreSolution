using BookstoreApi.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : Controller
    {
        private readonly MemberService _memberService;
        public MemberController(MemberService memberService)
        {
            _memberService = memberService;
        }

        // 取得使用者資料
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null) return Unauthorized("無效的使用者");
            int userId = int.Parse(userIdClaim.Value);

            var user = await _memberService.GetUserProfileAsync(userId);
            if (user == null) return NotFound("找不到使用者資料");

            return Ok(user);
        }
    }
}
