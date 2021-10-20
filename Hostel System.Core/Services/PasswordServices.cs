using Hostel_System.Core.IServices;
using Hostel_System.Database.Entity;
using Microsoft.AspNetCore.Identity;

namespace Hostel_System.Core.Services
{
    public class PasswordServices : IPasswordServices
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordServices(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public string NewPassword()
        {
            var chars = "!.@ABCDEFGHIJKLMNOPQRSTUVWXYZ!.@abcdefghijklmnopqrstuvwxyz!.@0123456789!.@";
            var stringChars = new char[15];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new String(stringChars);
        }
        public string GetPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }
    }
}
