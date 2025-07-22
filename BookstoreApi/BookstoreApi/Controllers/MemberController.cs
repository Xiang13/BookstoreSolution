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

        // 取得使用者訂單資料
        [Authorize]
        [HttpGet("orders")]
        public async Task<IActionResult> GetUserOrders()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null) return Unauthorized("無效的使用者");
            int userId = int.Parse(userIdClaim.Value);

            var userOrders = await _memberService.GetUserOrderAsync(userId);
            if (userOrders == null) return NotFound("找不到訂單資料");

            return Ok(userOrders);
        }

        // 取得使用者訂單詳細資料
        [Authorize]
        [HttpGet("orderDetail")]
        public async Task<IActionResult> GetUserOrderDetail(int orderId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null) return Unauthorized("無效的使用者");
            int userId = int.Parse(userIdClaim.Value);


            var userOrderDetail = await _memberService.GetOrderDetailAsync(orderId);
            if (userOrderDetail == null) return NotFound("找不到訂單資料");

            return Ok(userOrderDetail);
        }

        [Authorize]
        [HttpGet("cartItems")]
        public async Task<IActionResult> GetUserCart()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null) return Unauthorized("無效的使用者");
            int userId = int.Parse(userIdClaim.Value);

            var userCart = await _memberService.GetUserCaryAsync(userId);

            if (userCart == null) return NotFound("找不到購物車資料");

            return Ok(userCart);
        }
    }
}
