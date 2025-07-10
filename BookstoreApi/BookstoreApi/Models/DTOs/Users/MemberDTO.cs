namespace BookstoreApi.Models.DTOs.Users
{
    public class MemberDTO
    {
        public  int UserId { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public List<string> Roles { get; set; }
    }
}
