using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Models
{
    public class UserEntity
    {
        [Key]
        public Guid Id { get; set; }
        public byte[]? Image { get; set; } = [];
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
