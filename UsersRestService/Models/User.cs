namespace UsersRestService.Models
{
    /// <summary>
    /// This is the User data object
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
