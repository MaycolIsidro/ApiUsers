using System.Text.Json.Serialization;

namespace API_Users.Models
{
    public class User
    {
        public Guid IdUser { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        [JsonIgnore]
        public string? TokenRecuperation { get; set; }
    }
}
