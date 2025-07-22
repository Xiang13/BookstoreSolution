using BookstoreApi.Models.DTOs.Auth;
using BookstoreApi.Models.DTOs.Users;
using BookstoreApi.Models.EFModels;
using BookstoreApi.Models.Infrastructures.Extensions.Mapping.Users;
using BookstoreApi.Services.Users.Interfaces;

namespace BookstoreApi.Services.Users
{
    public class MemberService
    {
        private readonly IMemberRepository _userRepo;
        public MemberService(IMemberRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // 取得使用者資料
        public async Task<MemberDTO> GetUserProfileAsync(int userId)
        {
            var user = await _userRepo.GetByAccountAsync(userId);
            var userRoles = await _userRepo.GetRolesByUserIdAsync(userId);

            if (user == null) return null;

            return user.ToMemberDTO(userRoles);
        }

        // 取得使用者訂單資料
        public async Task<IEnumerable<MemberOrderListDTO>> GetUserOrderAsync(int userId)
        {
            var order = await _userRepo.GetOrderAsync(userId);
            if (order == null) return null;


            return order.Select(o => o.ToMemberOrderDTO()).ToList();
        }

        // 取得使用者訂單詳細資料
        public async Task<MemberOrderDTO> GetOrderDetailAsync(int orderId)
        {
            var orderDetail = await _userRepo.GetOrderDetailAsync(orderId);
            if (orderDetail == null) return null;


            return orderDetail.ToMemberOrderDetailDTO();
        }

        // 取得使用者購物車資料
        public async Task<IEnumerable<MemberCartDTO>> GetUserCaryAsync(int userId)
        {
            var cartItem = await _userRepo.GetUserCaryItemAsync(userId);
            if (cartItem == null) return null;

            return cartItem.ToMemberCartItemlDTO();
        }
    }
}
