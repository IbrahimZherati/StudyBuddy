using Mapster;
using Org.BouncyCastle.Asn1.Ocsp;
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Application.Services.Notifications;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Messages;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IRepo<ClientUser> clientRepo;
        private readonly IRepo<Message,Guid> messageRepo;
        private readonly INotificationService notificationService;
        private readonly IMessageDomainService messageDomainService;

        public MessageService(
            IRepo<ClientUser> clientRepo,
            IRepo<Message, Guid> messageRepo,
            INotificationService notificationService,
            IMessageDomainService messageDomainService)
        {
            this.clientRepo = clientRepo;
            this.messageRepo = messageRepo;
            this.notificationService = notificationService;
            this.messageDomainService = messageDomainService;
        }
        public async Task<Result<GetMessageDTO>> Create(int clientId, CreateMessageDTO messageDTO)
        {
            var valid = await messageDomainService.Create(clientId, messageDTO);
            if (!valid.IsSuccess)
                return Result<GetMessageDTO>.Failure(valid.Error!);

            var result = Message.Create(clientId, messageDTO);

            if (!result.IsSuccess)
                return Result<GetMessageDTO>.Failure(result.Error!);

            if (result.Value == null)
                return Result<GetMessageDTO>.Failure(Error.CreateFailed);

            var message = result.Value;
            await messageRepo.AddAsync(message);

            try
            {
                await messageRepo.SaveAsync();
                await notificationService.Create(new CreateNotificationDTO
                {
                    FromClientUserId = clientId,
                    ToClientUserId = messageDTO.ToClientUserId,
                    Type = NotificationTypes.Message.ToString(),
                    Title = "Message",
                    Description = message.Text

                });
                var dto = message.Adapt<GetMessageDTO>();
                return Result<GetMessageDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetMessageDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int clientId, Guid id)
        {
            var valid = await messageDomainService.Delete(clientId, id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var message = await messageRepo.GetByIdAsync(id);
            if (message == null)
                return Result.Failure(Error.MessageNotFound);
            messageRepo.Remove(message);
            try
            {
                await messageRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetMessageDTO>> GetMessageById(int clientId, Guid id)
        {
            var valid = await messageDomainService.GetMessageById(clientId, id);
            if (!valid.IsSuccess)
                return Result<GetMessageDTO>.Failure(valid.Error!);
            var message = await messageRepo.GetByIdAsync(id);
            if (message == null)
                return Result<GetMessageDTO>.Failure(Error.MessageNotFound);
            var messageDTO = message.Adapt<GetMessageDTO>();
            return Result<GetMessageDTO>.Success(messageDTO);
        }

        public async Task<Result<DataResponse<GetMessageDTO>>> GetMessagesForPrivateChat(int FirstClientId, int SecondClientId, int skip, int take, Order orderby)
        {
            var valid = await messageDomainService.GetMessagesForPrivateChat(FirstClientId, SecondClientId);
            if (!valid.IsSuccess)
                return Result<DataResponse<GetMessageDTO>>.Failure(valid.Error!);
            var result = messageRepo.GetQuery();

            result = result.Where(m => (
            m.FromClientUserId == FirstClientId &&
            m.ToClientUserId == SecondClientId) ||
            (m.FromClientUserId == SecondClientId &&
            m.ToClientUserId == FirstClientId));

            if (orderby == Order.Asc)
                result = result.OrderBy(m => m.CreateDate);
            else
                result = result.OrderByDescending(m => m.CreateDate);

                var query = result.ProjectToType<GetMessageDTO>();




            var data = new DataResponse<GetMessageDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetMessageDTO>>.Success(data);
        }

        public async Task<Result<GetMessageDTO>> Read(int clientId, Guid Id)
        {
            var valid = await messageDomainService.Read(clientId, Id);
            if (!valid.IsSuccess)
                return Result<GetMessageDTO>.Failure(valid.Error!);

            var message = await messageRepo.GetByIdAsync(Id);
            if (message == null)
                return Result<GetMessageDTO>.Failure(Error.MessageNotFound);

            message.Read();

            messageRepo.Update(message);
            try
            {
                await messageRepo.SaveAsync();
                var dto = message.Adapt<GetMessageDTO>();
                return Result<GetMessageDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetMessageDTO>.Failure(Error.UpdateFailed);
            }
        }

        public async Task<Result<GetMessageDTO>> Update(int clientId, UpdateMessageDTO messageDTO)
        {
            var valid = await messageDomainService.Update(clientId, messageDTO);
            if (!valid.IsSuccess)
                return Result<GetMessageDTO>.Failure(valid.Error!);

            var message = await messageRepo.GetByIdAsync(messageDTO.Id);
            if (message == null)
                return Result<GetMessageDTO>.Failure(Error.MessageNotFound);

            var result = message.Update(messageDTO);

            if (!result.IsSuccess)
                return Result<GetMessageDTO>.Failure(result.Error!);

            messageRepo.Update(message);
            try
            {
                await messageRepo.SaveAsync();
                var dto = message.Adapt<GetMessageDTO>();
                return Result<GetMessageDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetMessageDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
