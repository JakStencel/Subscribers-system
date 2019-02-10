using SubscribersSystem.Business.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SubscribersSystem.Utility.MessageSender
{
    public interface IMessageSender
    {
        Task SendAsync(string email, string filePath, InvoiceBl invoice);
    }

    public class Emailer : IMessageSender
    {
        public async Task SendAsync(string email, string filePath, InvoiceBl invoice)
        {
            await Task.Run(() =>
                   {
                       MailMessage mail = new MailMessage();
                       SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                       mail.From = new MailAddress("jacobjoshua.telecom@gmail.com");
                       mail.To.Add(email);
                       mail.Subject = $"Telecom system Invoice for the period: {invoice.BeginningDate.ToShortDateString()} " +
                                      $"to: {invoice.GenerationDate.ToShortDateString()}";
                       mail.Body = $"Dear {invoice.Subscriber.Name}{Environment.NewLine}" +
                                   $"As an attachment we are sending an invoice for telecom services provided to you by our company. {Environment.NewLine}" +
                                   $"An invoice covers period mentioned in the subject.{Environment.NewLine}" +
                                   $"Whole informations about payment are included in the attached invoice.{Environment.NewLine}" +
                                   $"In addition, we encourage to settle of receivables quickly. {Environment.NewLine}" +
                                   $"Best regards,{Environment.NewLine}{Environment.NewLine}" +
                                   $"Telecom company business manager{Environment.NewLine}" +
                                   $"Jacob Joshua";

                       Attachment attachment;
                       attachment = new Attachment(filePath);
                       mail.Attachments.Add(attachment);

                       SmtpServer.Port = 587;
                       SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"],  
                                                                      ConfigurationManager.AppSettings["Password"]);
                       SmtpServer.EnableSsl = true;

                       SmtpServer.Send(mail);
                       attachment.Dispose(); //required to delete created file after sending an email
                    });
        }
    }
}
