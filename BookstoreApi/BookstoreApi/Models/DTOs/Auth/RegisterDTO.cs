namespace BookstoreApi.Models.DTOs.Auth
{
    public class RegisterDTO
    {
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
