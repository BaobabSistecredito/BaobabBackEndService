using System.Text.Json.Serialization;

namespace BaobabBackEndService.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public List<UserRole>? UserRole { get; set; }
    }
}