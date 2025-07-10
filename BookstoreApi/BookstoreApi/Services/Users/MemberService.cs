using BookstoreApi.Models.DTOs.Auth;
using BookstoreApi.Models.DTOs.Users;
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

        public async Task<MemberDTO> GetUserProfileAsync(int userId)
        {
            var user = await _userRepo.GetByAccountAsync(userId);
            var userRoles = await _userRepo.GetRolesByUserIdAsync(userId);

            if (user == null) return null;

            return user.ToMemberDTO(userRoles);
        }
    }
}
