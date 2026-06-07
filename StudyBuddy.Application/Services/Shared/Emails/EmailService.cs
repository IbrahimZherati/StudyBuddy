using MailKitSimplified.Sender.Services;

using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKitSimplified.Sender;
using StudyBuddy.Shared.Helpers;

namespace StudyBuddy.Application.Services.Shared.Emails
{
    public class EmailService : IEmailService
    {
        public async Task<Result> Send(string toEmail, string subject, string body)
        {
            try
            {

                using var smtpSender = SmtpSender.Create("smtp.gmail.com:587")
                    .SetCredential("studybody985", "studybodyadmin");

                await smtpSender.WriteEmail
                    .From("my.name@example.com")
                    .To(toEmail)
                    .Subject("Confirm Your Account")
                    .BodyHtml($"<h1>Welcome!</h1>")
                    .SendAsync();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }

            return Result.Success();
        }

        public async Task<Result> SendConfirmToken(string toEmail, string token)
        {
            try
            {

                using var smtpSender = SmtpSender.Create("smtp.gmail.com:587")
                    .SetCredential("studdybody@gmail.com", "fbck umby mxrs zlqn");

                await smtpSender.WriteEmail
                    .From("studdybody@gmail.com")
                    .To(toEmail)
                    .Subject("Confirm Your Account")
                    .BodyHtml($@"
    <h1>Welcome!</h1>
    <p>Please confirm your email:</p>
    <form action='{AppHelper.AppHost}/api/Auth/ConfirmEmail' method='post'>
        <input type='hidden' name='email' value='{toEmail}' />
        <input type='hidden' name='token' value='{token}' />
        <button type='submit' style='padding: 10px 20px; background: #4CAF50; color: white; border: none; border-radius: 5px; cursor: pointer;'>
            Confirm My Account
        </button>
    </form>
")
                    .SendAsync();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }

            return Result.Success();
        }
    }
}
