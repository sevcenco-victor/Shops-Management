using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;

namespace Shop.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private readonly NexusDbContext _context;

        public OrderRepository(NexusDbContext context)
        {
            _context = context;
        }
        public void AddOrder(OrderEntity order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public OrderEntity? GetOrderById(Guid id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<OrderEntity> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public void UpdateOrder(OrderEntity order)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteOrder(Guid id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
