using BookstoreApi.Models.Common;
using BookstoreApi.Models.DTOs.Auth;
using BookstoreApi.Services.Auth.Interfaces;
using BookstoreApi.Services.Users.Interfaces;

namespace BookstoreApi.Services.Auth
{
    public class AuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<ApiResponse> LoginAsync(LoginDTO dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null)
                return ApiResponse.FailResult("帳號或密碼錯誤 帳號不存在");

            if(user.PasswordHash != dto.Password)
            {
                return ApiResponse.FailResult("帳號或密碼錯誤");
            }
            //if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            //    return ApiResponse.FailResult("帳號或密碼錯誤");

            return ApiResponse.SuccessResult(new
            {
                user.UserId,
                user.DisplayName,
                user.Email
            });
        }
    }
}
