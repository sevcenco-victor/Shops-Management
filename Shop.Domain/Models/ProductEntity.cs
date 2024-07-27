namespace Shop.Domain.Models
{
    public class ProductEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; } = 0.0;
        public short AgeRestrict { get; set; } = 0;
        public string Country { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }
    }
}
