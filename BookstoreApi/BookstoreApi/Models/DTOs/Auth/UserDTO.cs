namespace BookstoreApi.Models.DTOs.Auth
{
    public class UserDTO
    {
        public int UserId {  get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
