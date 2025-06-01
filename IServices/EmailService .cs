using CommonAPIs.Services;
using System.Net.Mail;
using System.Net;

namespace CommonAPIs.ImpService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("jkomal1906@gmail.com", "rvcg eauo prcm xwew"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("jkomal1906@gmail.com"),
                Subject = subject,
                Body = body + "<br/><br/><i>Note: This is a dummy link for testing purposes only.</i>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }



    }
}
