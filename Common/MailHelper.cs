using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using System.Net.Mail;

namespace Common
{
    public class MailHelper
    {
        public void SendMail(string toEmailAddress, string subject, string content)
        {
            try
            {
                MailAddress from = new MailAddress("diennguyenphuong9@gmail.com", "Nguyễn Phương Điền");
                MailAddress to = new MailAddress(toEmailAddress);
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);
                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("diennguyenphuong9@gmail.com", "vxmjrrggxdtcvehi");
                client.EnableSsl = true;
                client.Send(message);
                //Console.WriteLine("Gửi thành công");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
       
    }
}
