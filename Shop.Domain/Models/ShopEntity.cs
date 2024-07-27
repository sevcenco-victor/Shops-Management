namespace Shop.Domain.Models
{
    public class ShopEntity
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; } = [];
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
