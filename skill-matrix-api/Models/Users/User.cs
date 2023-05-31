namespace skill_matrix_api.Models.Users
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        //public string PasswordHash { get; set; }
        public string Role { get; set; } = string.Empty;
    }

}
