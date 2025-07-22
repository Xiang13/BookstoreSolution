using BookstoreApi.Models.DTOs.Users;
using BookstoreApi.Models.EFModels;
using BookstoreApi.Models.Infrastructures.Repositories.Base;
using BookstoreApi.Services.Users.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Models.Infrastructures.Repositories.Books
{
    public class MemberRepository : BaseRepository, IMemberRepository
    {
        public MemberRepository(AppDbContext context) : base(context) { }
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


        public async Task<User?> GetByAccountAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<List<string>> GetRolesByUserIdAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.RoleName)
                .ToListAsync();
        }
        public async Task<IEnumerable<Order?>> GetOrderAsync(int userId)
        {
            IQueryable<Order> query = _context.Orders.Include(o => o.OrderDetails).Where(o => o.UserId == userId);
            return await query.ToListAsync();
        }

        public async Task<Order?> GetOrderDetailAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<IEnumerable<CartItem?>> GetUserCaryItemAsync(int userId)
        {
            IQueryable<CartItem> query = _context.CartItems.Include(c => c.Book);
            query = query.Where(c => c.Cart.UserId == userId);
            return await query.ToListAsync();
        }
    }
}
