using BookstoreApi.Models.DTOs.Users;
using BookstoreApi.Models.EFModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Services.Users.Interfaces
{
    public interface IMemberRepository
    {
        public Task<bool> CheckPasswordAsync(User user, string password);
        public Task<IdentityResult> RegisterUserAsync(User user, string password);
        public Task<User> GetUserWithRolesAsync(string email);

        public Task<User> GetByAccountAsync(int userId);

        public Task<List<string>> GetRolesByUserIdAsync(int userId);
    }
}
