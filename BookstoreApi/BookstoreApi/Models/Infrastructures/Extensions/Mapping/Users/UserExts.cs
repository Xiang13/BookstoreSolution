using BookstoreApi.Models.DTOs.Auth;
using BookstoreApi.Models.EFModels;
using BookstoreApi.ViewModels.AuthVMs;
using BookstoreApi.ViewModels.UserVMs;

namespace BookstoreApi.Models.Infrastructures.Extensions.Mapping.Users
{
    public static class UserExts
    {
        public static LoginDTO ToLoginDTO(this UserVM vm)
        {
            return new LoginDTO
            {
                Email = vm.Email,
                Password = vm.Password
            };
        }

        public static LoginVM ToLoginVM(this string token)
        {
            return new LoginVM
            {
                Token = token
            };
        }
    }
}
