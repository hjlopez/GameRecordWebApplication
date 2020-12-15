using System.Net.Mail;

namespace API.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmailAsync(string destinationEmail, string gamerTag)
        {
            var mail = new MailMessage();
            var smtpServer = new SmtpClient("smtp.gmail.com");

            //mail.From = new MailAddress("gamerecordsmoderator@gmail.com");
            mail.From = new MailAddress("gamerecordsmoderator@gmail.com");
            mail.To.Add(destinationEmail);
            mail.Subject = "Temporary Password - RESET PASSWORD ON LOGIN!";
            mail.Body = "Hi " + gamerTag + ", /n/n/tHere is your temporary password: P@ssw0rd /n/tChange" + 
                    "your password immediately after loggin in.";

            smtpServer.Port = 587;
            smtpServer.UseDefaultCredentials = true;
            smtpServer.Credentials = new System.Net.NetworkCredential("gamerecordsmoderator@gmail.com", "GravityFalls2012");
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
        }
    }
}