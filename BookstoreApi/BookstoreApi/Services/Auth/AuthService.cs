using BookstoreApi.Models.Common;
using BookstoreApi.Models.DTOs.Auth;
using BookstoreApi.Models.Infrastructures.Extensions.Mapping.Auth;
using BookstoreApi.Services.Auth.Interfaces;
using BookstoreApi.Services.Users.Interfaces;

namespace BookstoreApi.Services.Auth
{
    public class AuthService
    {
        private readonly IMemberRepository _userRepo;

        public AuthService(IMemberRepository userRepo)
        {
            _userRepo = userRepo;
        }        

        public async Task<UserDTO?> ValidateUserAsync(LoginDTO dto)
        {
            var user = await _userRepo.GetUserWithRolesAsync(dto.Email);
            if (user == null) return null;
            // 這裡可以改成加密密碼比對
            //if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            //    return ApiResponse.FailResult("帳號或密碼錯誤");

            return user.ToUserDTO();            
        }
    }
}
