using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Models
{
    public class ClientEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public byte[] Image { get; set; } = [];
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
