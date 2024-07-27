namespace Shop.Domain.Models
{
    public class OrderEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; } = 0;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public Guid ShopId { get; set; }
        public ShopEntity? Shop { get; set; }
        public Guid ProductId { get; set; }
        public ProductEntity? Product { get; set; }
        public Guid ClientID { get; set; }
        public ClientEntity? Client { get; set; }
    }
}
