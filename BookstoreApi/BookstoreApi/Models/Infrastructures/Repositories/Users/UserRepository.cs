using BookstoreApi.Models.EFModels;
using BookstoreApi.Models.Infrastructures.Repositories.Base;
using BookstoreApi.Services.Users.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Models.Infrastructures.Repositories.Books
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
    

        public async Task<User?> GetUserWithRolesAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
