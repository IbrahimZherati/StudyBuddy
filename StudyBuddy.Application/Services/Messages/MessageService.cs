using Mapster;
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.DTOs.MessageDTO;
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
        private readonly IRepo<Message> messageRepo;

        public MessageService(
            IRepo<ClientUser> clientRepo,
            IRepo<Message> messageRepo)
        {
            this.clientRepo = clientRepo;
            this.messageRepo = messageRepo;
        }
        public async Task<Result> Create(CreateMessageDTO messageDTO)
        {
            if (messageDTO.Text == null)
                return Result.Failure(Error.MessageMustNotNull);

            var FromClient = await clientRepo.GetByIdAsync(messageDTO.FromClientUserId);

            if (FromClient == null)
                return Result.Failure(Error.UserNotFound);

            var ToClient = await clientRepo.GetByIdAsync(messageDTO.ToClientUserId);

            if (ToClient == null)
                return Result.Failure(Error.UserNotFound);

            var message = new Message();
            messageDTO.Adapt(message);
            message.CreateDate = DateTime.Now;

            await messageRepo.AddAsync(message);
            try
            {
                await messageRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Delete(int id)
        {
            var message = await messageRepo.GetByIdAsync(id);
            if (message == null)
                return Result.Failure(Error.ItemNotFound);

            messageRepo.Remove(message);

            try
            {
               await messageRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }

            return Result.Success();
        }

        public async Task<Result<GetMessageDTO>> GetById(int id)
        {
            var message = await messageRepo.GetByIdAsync(id);

            if (message == null)
                return Result<GetMessageDTO>.Failure(Error.ItemNotFound);

            var messageDTO = message.Adapt<GetMessageDTO>();

            return Result<GetMessageDTO>.Success(messageDTO);

        }

        public async Task<Result<DataResponse<GetMessageDTO>>> GetMessagesForPrivateChat(int FirstClientId, int SecondClientId, int skip, int take, Order orderby)
        {
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

        public async Task<Result> Update(UpdateMessageDTO messageDTO)
        {
            if (messageDTO.Text == null)
                return Result.Failure(Error.MessageMustNotNull);

            var FromClient = await clientRepo.GetByIdAsync(messageDTO.FromClientUserId);

            if (FromClient == null)
                return Result.Failure(Error.UserNotFound);

            var ToClient = await clientRepo.GetByIdAsync(messageDTO.ToClientUserId);

            if (ToClient == null)
                return Result.Failure(Error.UserNotFound);


            var message = await messageRepo.GetByIdAsync(messageDTO.Id);

            if(message == null)
                return Result.Failure(Error.ItemNotFound);


            messageDTO.Adapt(message);
            message.ModifyDate = DateTime.Now;
            messageRepo.Update(message);
            try
            {
                await messageRepo.SaveAsync(); 
            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);  
            }

            return Result.Success();
        }
    }
}
