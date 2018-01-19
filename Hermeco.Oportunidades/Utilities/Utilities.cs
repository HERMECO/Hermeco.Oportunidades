using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Hosting;
using System.Web.UI.WebControls;

namespace Hermeco.Oportunidades.Utilities
{
    public class Utilities
    {
        public static void SendEmail(string subject, string htmlPath, string body, string from, string to, List<string> pathAttachments, bool isBodyHtml, string CC, Dictionary<string, string> parameters = null)
        {
            MailMessage mail = new MailMessage(from, to);
            MailDefinition md = new MailDefinition();

            SmtpClient client = new SmtpClient() { Port = 25, DeliveryMethod = SmtpDeliveryMethod.Network, UseDefaultCredentials = false, Host = "correo.hermeco.com" };
            md.Subject = subject;
            md.From = from;
            md.IsBodyHtml = isBodyHtml;

            if (isBodyHtml)
            {
                using (StreamReader reader = File.OpenText(htmlPath))
                {
                    mail.Body = reader.ReadToEnd();

                    if (parameters != null)
                    {
                        mail = md.CreateMailMessage(to, parameters, mail.Body, new System.Web.UI.Control());

                    }
                }
            }
            else
            {
                mail.Body = body;
            }

            if (pathAttachments != null)
            {
                foreach (string file in pathAttachments)
                {
                    mail.Attachments.Add(new Attachment(file));
                }
            }
            

            if (!string.IsNullOrEmpty(CC))
            {
                var MailCC = new MailAddress(CC);
                mail.CC.Add(MailCC);
            }

            client.Send(mail);
        }


    }
}