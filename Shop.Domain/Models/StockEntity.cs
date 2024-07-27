namespace Shop.Domain.Models
{
    public class StockEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; } = 0;
        public Guid ShopId { get; set; }
        public ShopEntity? Shop { get; set; }
        public Guid ProductId { get; set; }
        public ProductEntity? Product { get; set; }

    }
}
