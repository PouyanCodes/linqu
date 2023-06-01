
using System.Net;
using System.Net.Mail;


namespace linqu.Core.Senders
{
    public static class EmailSender
    {
        public static bool Send(string to , string subject , string body , bool isBodyHtml = false)
        {
            try
            {
                var myMail = "ICutty.Web@gmail.com";
                var password = "kxkl vhxn dfvi fhgw";

                // ----- Change Later X ------------

                var mail = new MailMessage();
                var smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(myMail);
                mail.To.Add(to);
                mail.IsBodyHtml = isBodyHtml;
                mail.Body = body;

                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(myMail, password);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                return true;
            }

            catch
            {
                return false;
            }
        }
    }
}
