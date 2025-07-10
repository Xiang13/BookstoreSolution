using BookstoreApi.Models.DTOs.Users;
using BookstoreApi.Models.EFModels;

namespace BookstoreApi.Models.Infrastructures.Extensions.Mapping.Users
{
    public static class MemberExts
    {
        // ToDTO
        public static MemberDTO ToMemberDTO(this User user, List<string> roles)
        {
            return new MemberDTO
            {
                UserId = user.UserId,
                DisplayName = user.DisplayName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Roles = roles,
            };
        }
    }
}
