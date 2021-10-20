using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Hostel_System.Core.IServices;
using Hostel_System.Database;
using Hostel_System.Database.Entity;

namespace Hostel_System.Core.Services
{
    public class MailServices : IMailServices
    {
        private readonly HostelSystemDbContext _hostelSystemDbContext;
        private readonly IPasswordServices _passwordServices;
        private readonly string ServiceMail = "pawel123@email.com";
        private readonly string ServiceMailPassword = "Pa$$w0rd1234.";
        private readonly string ServiceMailServer = "smtp.poczta.onet.pl";
        private readonly int ServiceMailServerPort = 465;
        public MailServices(HostelSystemDbContext hostelSystemDbContext,
            IPasswordServices passwordServices)
        {
            _hostelSystemDbContext = hostelSystemDbContext;
            _passwordServices = passwordServices;
        }
        public void ForgotPassword(string email, User user)
        {
            var smtpClient = new SmtpClient(ServiceMailServer, ServiceMailServerPort);
            smtpClient.Credentials = new NetworkCredential(ServiceMail, ServiceMailPassword);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            var password = _passwordServices.NewPassword();
            user.Password = _passwordServices.GetPassword(user, password);
            _hostelSystemDbContext.SaveChanges();
            var mail = CreateMail(email, "Forgot Password", $"Your new  Password is: {password}");
            Task.Run(() =>
            {
                smtpClient.Send(mail);
            });

        }

        public MailMessage CreateMail(string email, string subject, string body)
        {
            var mail = new MailMessage();
            mail.Subject = subject;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.From = new MailAddress(ServiceMail, "Forgot Password Hostel System");
            mail.To.Add(new MailAddress(ServiceMail));
            mail.CC.Add(email);
            return mail;
        }
    }
}
