namespace Domain
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Permissions { get; set; }
        public List<string> Roles { get; set; }
    }
}
