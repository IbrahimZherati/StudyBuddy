using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Abstractions
{
    public interface INotificationSendService
    {
        Task<Result> Send(Notification notification);
    }
}
