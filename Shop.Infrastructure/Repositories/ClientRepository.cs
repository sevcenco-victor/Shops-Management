using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories
{
    public class ClientRepository
    {
        private readonly NexusDbContext _context;
        private readonly Random random = new Random();
        public ClientRepository(NexusDbContext context)
        {
            _context = context;
        }

        public void AddClient(ClientEntity client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public IEnumerable<ClientEntity> GetAllClients()
        {
            return _context.Clients.ToList();
        }

        public ClientEntity? GetClientById(Guid id)
        {
            return _context.Clients.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateClient(Guid clientId, byte[] image, string name,
            string email, string gender, string address, string city, string country)
        {
            _context.Clients
              .Where(c => c.Id == clientId)
              .ExecuteUpdate(c => c
                  .SetProperty(c => c.Name, name)
                  .SetProperty(c => c.Image, image)
                  .SetProperty(c => c.Email, email)
                  .SetProperty(c => c.Gender, gender)
                  .SetProperty(c => c.Address, address)
                  .SetProperty(c => c.City, city)
                  .SetProperty(c => c.Country, country)
                  );
        }
        public void UpdateClientCountry(Guid clientId, string country)
        {
            _context.Clients
              .Where(c => c.Id == clientId)
              .ExecuteUpdate(c => c
                  .SetProperty(c => c.Country, country)
                  );
        }
        public ClientEntity? GetRandomClient()
        {
            int index = random.Next(0, _context.Clients.Count());
            return _context.Clients.Skip(index).FirstOrDefault();
        }
        public void DeleteClient(ClientEntity client)
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }

        public void DeleteClientById(Guid clientId)
        {
            var clientToDelete = _context.Clients.FirstOrDefault(c => c.Id == clientId);
            if (clientToDelete != null)
            {
                _context.Clients.Remove(clientToDelete);
                _context.SaveChanges();
            }
        }
    }
}
