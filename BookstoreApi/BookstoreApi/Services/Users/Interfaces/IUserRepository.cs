using BookstoreApi.Models.EFModels;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApi.Services.Users.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> CheckPasswordAsync(User user, string password);
        public Task<IdentityResult> RegisterUserAsync(User user, string password);
        public Task<User> GetByEmailAsync(string email);

    }
}
