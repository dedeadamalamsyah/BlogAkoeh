namespace BlogAkoeh.Models.ViewModels
{
    public class UserForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int Role { get; set; }
        public string? Photo { get; set; }
    }
}