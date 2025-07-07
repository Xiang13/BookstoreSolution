using BookstoreApi.Models.EFModels;
using BookstoreApi.Services.Users.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApi.Models.Infrastructures.Repositories.Books
{
    public class UserRepository : IUserRepository
    {
        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
