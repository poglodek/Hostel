using Hostel_System.Database.Entity;

namespace Hostel_System.Core.IServices;

public interface IPasswordServices
{
    string NewPassword();
    string GetPassword(User user, string password);
}