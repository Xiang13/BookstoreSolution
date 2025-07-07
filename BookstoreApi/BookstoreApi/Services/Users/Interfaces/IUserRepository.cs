using BookstoreApi.Models.EFModels;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApi.Services.Users.Interfaces
{
    public interface IUserRepository
    {
        // 取得 Email
        Task<User> GetByEmailAsync(string email);
        // 確認密碼
        Task<bool> CheckPasswordAsync(User user, string password);
        // 註冊
        Task<IdentityResult> RegisterUserAsync(User user, string password);
    }
}
