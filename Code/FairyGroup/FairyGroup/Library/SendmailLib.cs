using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;

namespace FairyGroup.Library
{
    public class SendmailLib
    {
        public static void  Sendmail(string emailTo, string subject, string body)
        {
           
            try
            {
                string smtpAddress = "smtp.fairygroup.co.th";
                int portNumber = 25;
                bool enableSSL = true;

                string emailFrom = "administrator@fairygroup.co.th";
                string password = "F@1ryGr0up";
                //string emailTo = conf.ConfigValue;

                MailMessage Message = new MailMessage();
                Message.To = password;
                Message.From = emailFrom;
                Message.Subject = subject;
                Message.Body = body;


                SmtpMail.SmtpServer = "smtp.fairygroup.co.th";
                SmtpMail.Send(Message);
            }
            catch (System.Web.HttpException ehttp)
            {
                throw ehttp;
                //Console.WriteLine("{0}", ehttp.Message);
                //Console.WriteLine("Here is the full error message output");
                //Console.Write("{0}", ehttp.ToString());
            }
        }
    }
}