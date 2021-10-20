using System.Net.Mail;
using Hostel_System.Database.Entity;

namespace Hostel_System.Core.IServices;

public interface IMailServices
{
    void ForgotPassword(string email, User user);
    MailMessage CreateMail(string email, string subject, string body);
}