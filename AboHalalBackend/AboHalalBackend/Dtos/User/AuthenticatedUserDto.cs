namespace AboHalalBackend.Dtos.User
{
    public class AuthenticatedUserDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
