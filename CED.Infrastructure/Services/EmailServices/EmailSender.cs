using System.Net;
using System.Net.Mail;
using CED.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace CED.Infrastructure.Services.EmailServices;

public class EmailSender : IEmailSender
{
    private readonly EmailSettingNames _emailSettingNames;
    public EmailSender(IOptions<EmailSettingNames> options)
    {
        _emailSettingNames = options.Value;
    }
    public async Task SendEmail(string email, string subject, string message)
    {
        var mail = _emailSettingNames.Email;
        var pw = _emailSettingNames.Password;

        var client = new SmtpClient(pw, 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, pw)
        };
        
         await client.SendMailAsync(
            new MailMessage(
                from:mail,
                to: email,
                subject,
                message
                )
            );
    }
}