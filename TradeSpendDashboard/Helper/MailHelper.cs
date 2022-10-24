using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;

namespace TradeSpendDashboard
{
    public class MailHelper
    {

        private readonly ILogger<MailHelper> _logger;
        private readonly AppHelper _appHelper;
        //private readonly IFlowProcessStatusEmailAssignService _flowMail;

        private readonly string mailFrom;
        private readonly string mailSmtp;
        private readonly string mailSmtpPort;
        private readonly string mailSmtpSsl;
        private readonly string mailUsername;
        private readonly string mailPassword;
        private readonly string receiver;
        private readonly string ccEmail;


        public MailHelper(
              ILogger<MailHelper> logger,
              AppHelper app
              //IFlowProcessStatusEmailAssignService flowMail
        )
        {
            _logger = logger;
            _appHelper = app;
            //_flowMail = flowMail;
            mailFrom = app.Application.MailFrom;
            mailSmtp = app.Application.MailSMTP;
            mailSmtpPort = app.Application.MailSmtpPort;
            mailSmtpSsl = app.Application.MailSmtpSsl;
            mailUsername = app.Application.MailUsername;
            mailPassword = app.Application.MailPassword;
            receiver = app.Application.Receiver;
            ccEmail = app.Application.CcEmail;
        }

        //public string GetMailTemplate(long Id = 0)
        //{
        //    string subject;
        //    string message;

        //    DateTime now = DateTime.Now;

        //    var data = _flowMail.GetAsync(2);
        //    var FormatEmail = data.Result.FormatEmailId;
            

        //    subject = "Testing Email dari Ali " + now.ToString("ddMMyyyy");
        //    message =
        //        $"Dear Tim Logistik <br><br>" + now.ToString("ddMMyyy") +
        //        $"<p>Document BW untuk periode bulan sudah diproses, Silahkan kepada Tim Logistik diharapkan segera melakukan upload File Delivery Order (DO) <br></p> <br><br>" +
        //        $"Best Regards, <br>" +
        //        $"SCRS Application";

        //    SmtpClient smtpClient = new SmtpClient(mailSmtp);

        //    if (!string.IsNullOrWhiteSpace(mailSmtpPort))
        //        smtpClient = new SmtpClient(mailSmtp, Convert.ToInt32(mailSmtpPort));

        //    smtpClient.UseDefaultCredentials = false;
        //    System.Net.NetworkCredential basicAuthenticationInfo = new
        //       System.Net.NetworkCredential(mailUsername, mailPassword);
        //    smtpClient.Credentials = basicAuthenticationInfo;
        //    smtpClient.EnableSsl = string.IsNullOrWhiteSpace(mailSmtpSsl) ? false : Convert.ToBoolean(mailSmtpSsl);

        //    MailAddress from = new MailAddress(mailFrom, "Supply Chain Report System (Finance)");
        //    MailMessage mailSender = new MailMessage();
        //    mailSender.From = from;

        //    if (!string.IsNullOrEmpty(receiver))
        //    {
        //        var receivers = receiver.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (var address in receivers)
        //        {
        //            mailSender.To.Add(address);
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(ccEmail))
        //    {
        //        foreach (var address in ccEmail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            mailSender.CC.Add(address);
        //        }
        //    }

        //    mailSender.Subject = subject;
        //    mailSender.SubjectEncoding = System.Text.Encoding.UTF8;

        //    mailSender.Body = message;
        //    mailSender.BodyEncoding = System.Text.Encoding.UTF8;

        //    mailSender.IsBodyHtml = true;

        //    smtpClient.Send(mailSender);
        //    return "Testing";
        //}

        //public string SendEmail(string info)
        //{
        //    string subject;
        //    string message;

        //    DateTime now = DateTime.Now;

        //    subject = "Testing Email dari Ali " + now.ToString("ddMMyyyy");
        //    message =
        //        $"Dear Tim Logistik <br><br>" + now.ToString("ddMMyyy") +
        //        $"<p>Document BW untuk periode bulan sudah diproses, Silahkan kepada Tim Logistik diharapkan segera melakukan upload File Delivery Order (DO) <br></p> <br><br>" +
        //        $"Best Regards, <br>" +
        //        $"SCRS Application";

        //    SmtpClient smtpClient = new SmtpClient(mailSmtp);

        //    if (!string.IsNullOrWhiteSpace(mailSmtpPort))
        //        smtpClient = new SmtpClient(mailSmtp, Convert.ToInt32(mailSmtpPort));

        //    smtpClient.UseDefaultCredentials = false;
        //    System.Net.NetworkCredential basicAuthenticationInfo = new
        //       System.Net.NetworkCredential(mailUsername, mailPassword);
        //    smtpClient.Credentials = basicAuthenticationInfo;
        //    smtpClient.EnableSsl = string.IsNullOrWhiteSpace(mailSmtpSsl) ? false : Convert.ToBoolean(mailSmtpSsl);

        //    MailAddress from = new MailAddress(mailFrom, "Supply Chain Report System (Finance)");
        //    MailMessage mailSender = new MailMessage();
        //    mailSender.From = from;

        //    if (!string.IsNullOrEmpty(receiver))
        //    {
        //        var receivers = receiver.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (var address in receivers)
        //        {
        //            mailSender.To.Add(address);
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(ccEmail))
        //    {
        //        foreach (var address in ccEmail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            mailSender.CC.Add(address);
        //        }
        //    }

        //    mailSender.Subject = subject;
        //    mailSender.SubjectEncoding = System.Text.Encoding.UTF8;

        //    mailSender.Body = message;
        //    mailSender.BodyEncoding = System.Text.Encoding.UTF8;

        //    mailSender.IsBodyHtml = true;

        //    smtpClient.Send(mailSender);
        //    return "Testing";
        //}

        //public bool SendEmailNotif(string to, string cc, string subject, string message = "")
        //{
        //    DateTime now = DateTime.Now;
        //    SmtpClient smtpClient = new SmtpClient(mailSmtp);

        //    if (!string.IsNullOrWhiteSpace(mailSmtpPort))
        //        smtpClient = new SmtpClient(mailSmtp, Convert.ToInt32(mailSmtpPort));

        //    smtpClient.UseDefaultCredentials = false;
        //    System.Net.NetworkCredential basicAuthenticationInfo = new
        //       System.Net.NetworkCredential(mailUsername, mailPassword);
        //    smtpClient.Credentials = basicAuthenticationInfo;
        //    smtpClient.EnableSsl = string.IsNullOrWhiteSpace(mailSmtpSsl) ? false : Convert.ToBoolean(mailSmtpSsl);

        //    MailAddress from = new MailAddress(mailFrom, _appHelper.Application.Name);
        //    MailMessage mailSender = new MailMessage();
        //    mailSender.From = from;

        //    if (!string.IsNullOrEmpty(receiver))
        //    {
        //        var receivers = receiver.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (var address in receivers)
        //        {
        //            mailSender.To.Add(address);
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(ccEmail))
        //    {
        //        foreach (var address in ccEmail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            mailSender.CC.Add(address);
        //        }
        //    }

        //    mailSender.Subject = subject;
        //    mailSender.SubjectEncoding = System.Text.Encoding.UTF8;

        //    mailSender.Body = message;
        //    mailSender.BodyEncoding = System.Text.Encoding.UTF8;

        //    mailSender.IsBodyHtml = true;

        //    smtpClient.Send(mailSender);
        //    return true;
        //}
    }
}
