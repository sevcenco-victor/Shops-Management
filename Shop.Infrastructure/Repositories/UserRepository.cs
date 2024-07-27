using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Shop.Domain.Models;

namespace Shop.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly NexusDbContext _context;

        public UserRepository(NexusDbContext context)
        {
            _context = context;
        }
        public void AddUser(UserEntity user)
        {
            if (GetUserByEmail(user.Email) != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public IEnumerable<UserEntity> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public UserEntity? GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public void UpdateUser(Guid userId, byte[] image, string name, string email, string role, string password)
        {
            _context.Users
              .Where(s => s.Id == userId)
              .ExecuteUpdate(s => s
                  .SetProperty(c => c.Name, name)
                  .SetProperty(c => c.Email, email)
                  .SetProperty(c => c.Role, role)
                  .SetProperty(c => c.Password, password));
        }
        public void UpdateUserPassword(UserEntity user, string newPassword)
        {
            _context.Users.Where(u => u.Id == user.Id)
                  .ExecuteUpdate(s => s
                  .SetProperty(c => c.Password, newPassword));
        }

        public UserEntity? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
        public void DeleteUser(UserEntity user)
        {
            _context.Users.Remove(user);
        }
        public void DeleteUserById(Guid userId)
        {
            var userToDelete = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }
    }
}
