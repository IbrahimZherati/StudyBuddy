using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Shared.Emails
{
    public interface IEmailService
    {
        Task<Result> Send(string toEmail ,string subject, string body);
        Task<Result> SendConfirmToken(string toEmail, string token);
    }
}
