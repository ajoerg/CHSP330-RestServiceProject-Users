using System.Text.Json.Serialization;

namespace UsersRestService.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("dateAdded")]
        public DateTime DateAdded { get; set; }
    }
}
