using BookstoreApi.Models.EFModels;
using BookstoreApi.Models.Infrastructures.Repositories.Base;
using BookstoreApi.Services.Users.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApi.Models.Infrastructures.Repositories.Users
{
    public class UserRepository : BaseRepository, IUserRepository       
    {
        public UserRepository(AppDbContext context) : base(context) { }

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
