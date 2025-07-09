using BookstoreApi.Models.DTOs.Auth;
using BookstoreApi.Models.DTOs.Books;
using BookstoreApi.ViewModels.AuthVMs;
using BookstoreApi.ViewModels.BooksVMs;

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
    }
}
    
