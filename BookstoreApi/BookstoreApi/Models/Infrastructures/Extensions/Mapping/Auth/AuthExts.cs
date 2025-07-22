using BookstoreApi.Models.DTOs.Auth;
using BookstoreApi.Models.DTOs.Books;
using BookstoreApi.Models.EFModels;
using BookstoreApi.ViewModels.AuthVMs;
using BookstoreApi.ViewModels.BooksVMs;
using System.Runtime.CompilerServices;

namespace BookstoreApi.Models.Infrastructures.Extensions.Mapping.Auth
{
    public static class AuthExts
    {
        public static LoginDTO ToLoginDTO(this LoginVM vm)
        {
            return new LoginDTO
            {
                Email = vm.Email,
                Password = vm.Password,
            };
        }

        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                UserId = user.UserId,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList()
            };
        }
    }
}
    
