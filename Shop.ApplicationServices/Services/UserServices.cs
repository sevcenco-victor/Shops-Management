using BCrypt.Net;
using Shop.Domain.Models;
using Shop.Infrastructure.Repositories;
namespace Shop.ApplicationServices.Services
{
    public class UserServices
    {
        private readonly UserRepository _userRepository = new UserRepository(new Infrastructure.NexusDbContext());
        
        public static bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigits = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            int criteriaMet = 0;
            if (hasUpperCase) criteriaMet++;
            if (hasLowerCase) criteriaMet++;
            if (hasDigits) criteriaMet++;
            if (hasSpecialChar) criteriaMet++;

            return criteriaMet >= 3;
        }
        public static string GetHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public UserEntity? AuthenticateUser(string email, string password)
        {
            UserEntity? user = _userRepository.GetUserByEmail(email);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }

            return null;
        }
    }
}
