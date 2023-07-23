using Assign.DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace Assign.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);

            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ahmed157728@gmail.com", "bpjcmuseqytbfprg");

            client.Send("ahmed157728@gmail.com", email.To, email.Title, email.Body);
        }
    }
}
