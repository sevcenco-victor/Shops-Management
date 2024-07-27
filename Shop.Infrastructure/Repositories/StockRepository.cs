using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;

namespace Shop.Infrastructure.Repositories
{
    public class StockRepository
    {
        private readonly NexusDbContext _context;

        public StockRepository(NexusDbContext context)
        {
            _context = context;
        }

        public void AddStock(StockEntity stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();
        }

        public IEnumerable<StockEntity> GetAllStocks()
        {
            return _context.Stocks.ToList();
        }

        public StockEntity? GetStockById(Guid id)
        {
            return _context.Stocks.FirstOrDefault(s => s.Id == id);
        }

        public void UpdateStock(Guid stockId, int quantity)
        {
            var stockToUpdate = _context.Stocks.FirstOrDefault(s => s.Id == stockId);
            if (stockToUpdate != null)
            {
                stockToUpdate.Quantity = quantity;
                _context.SaveChanges();
            }
        }
        public IEnumerable<(ProductEntity? Product, StockEntity Stock)> GetProductsAndStocksByShopId(Guid shopId)
        {
            var productsAndStocks = _context.Stocks
                .Where(stock => stock.ShopId == shopId)
                .Include(stock => stock.Product)
                .ToList();

            return productsAndStocks.Select(stock => (stock.Product, stock));
        }
        public IEnumerable<ProductEntity?> GetProductsByShopId(Guid shopId)
        {
            return _context.Stocks
                .Where(stock => stock.ShopId == shopId)
                .Select(stock => stock.Product)
                .ToList();
        }
        public void DeleteStock(StockEntity stock)
        {
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
        }

        public void DeleteStockById(Guid stockId)
        {
            var stockToDelete = _context.Stocks.FirstOrDefault(s => s.Id == stockId);
            if (stockToDelete != null)
            {
                _context.Stocks.Remove(stockToDelete);
                _context.SaveChanges();
            }
        }
        public void DeleteStocksWithZeroQuantity()
        {
            var stocksToDelte = _context.Stocks.Where(s => s.Quantity == 0);
            _context.RemoveRange(stocksToDelte);
            _context.SaveChanges();
        }
    }
}