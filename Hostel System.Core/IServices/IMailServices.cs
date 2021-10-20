using Hostel_System.Database.Entity;
using System.Net.Mail;

namespace Hostel_System.Core.IServices;

public interface IMailServices
{
    void ForgotPassword(string email, User user);
    MailMessage CreateMail(string email, string subject, string body);
}