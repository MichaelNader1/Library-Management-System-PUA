namespace LibraryManagementSystem.Models.Identity
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int LibraryId { get; set; }
    }
}
