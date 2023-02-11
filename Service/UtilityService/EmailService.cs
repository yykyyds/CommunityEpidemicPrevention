using IService;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Text;

namespace Service.UtilityService
{
    public class EmailService : IEmailService
    {
        //读取上面配置文件中的信息
        string? serverEmail = null;//邮件来自该邮件
        string? strPWD = null;//该邮件授权码
        string? strServer = null; //SMTP主机域
        int strPort = 0; //SMTP端口
        MailMessage? mailMsg;
        SmtpClient? smtpClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="EmailSubject">邮件主题</param>
        /// <param name="EmailTo">收件人</param>
        /// <param name="EmialContent">邮件内容</param>
        /// <param name="path">附件地址</param>
        public EmailService(IConfiguration configuration)
        {
            //配置邮件发送方信息
            serverEmail = configuration["EmailConfig:MailSender"];
            strPWD = configuration["EmailConfig:MailSenderPwd"];
            strServer = configuration["EmailConfig:MailSMTPServer"];
            strPort = Int32.Parse(configuration["EmailConfig:MailSMTPPort"]??"0");
        }

        public void SendConfig(string EmailSubject, string EmailTo, string EmialContent, string? path = null)
        {
            //邮件发送配置
            mailMsg = new MailMessage(serverEmail??"", EmailTo);//发送者加收件着
            mailMsg.Subject = EmailSubject;
            mailMsg.SubjectEncoding = Encoding.UTF8;
            mailMsg.Priority = MailPriority.Normal;
            mailMsg.IsBodyHtml = true;//如果你的邮件内容需要拼接HTML的话，改属性设置为true
            mailMsg.Body = EmialContent;
            mailMsg.BodyEncoding = Encoding.UTF8;
            if (path != null)
            {
                AddAttchment(path);
            }
        }

        /// <summary>
        /// 增加附件
        /// </summary>
        /// <param name="path">附件路径</param>
        public void AddAttchment(string path)
        {
            try
            {
                Attachment attachMent;
                attachMent = new Attachment(path);
                mailMsg?.Attachments.Add(attachMent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <returns>是否发送成功</returns>
        public bool SendEmail()
        {
            smtpClient = new SmtpClient(strServer, strPort);
            smtpClient.UseDefaultCredentials = false;//是否使用默认身份验证(如果你的SMTP不需要身份验证，可以设置此项为TRUE，否则就为false)，为false时，就需要身份验证，使用下面的Credentials
            smtpClient.EnableSsl = true;//是否加密链接，这里必须设置为true，否则无法发送邮件
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定发送邮件的方法，以网络的形式
            smtpClient.Timeout = 10000;
            //设置链接SMTP时的身份验证，也就是我要连接的QQ邮箱的服务器，我需要我邮箱的密码，才可以用它发送邮件，而且必须开通了QQ的SMTP服务，使用生成的凭证码
            smtpClient.Credentials = new System.Net.NetworkCredential(serverEmail, strPWD);
            try
            {
                smtpClient.Send(mailMsg??default!);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
