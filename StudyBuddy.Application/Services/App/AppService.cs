using StudyBuddy.Application.Services.Shared.GetTagsFromMajors;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.App
{
    public class AppService : IAppService
    {
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly ITagsService tagsService;

        public AppService(IRepo<ClientUser> clientUserRepo,ITagsService tagsService)
        {
            this.clientUserRepo = clientUserRepo;
            this.tagsService = tagsService;
        }
        public async Task<Result> Start(int clientId , string rootPath)
        {
            var client = await clientUserRepo.GetQuery()
                .Where(c => c.Id == clientId)
                .Include(c => c.Major)
                .FirstOrDefaultAsync();

           

            if (client == null)
                return Result.Failure(Error.ClientUserNotFound);

            if (client.Bio == null)
                return Result.Success();

            //Bio
            if(!client.IsSkillFromMajor)
                return Result.Success();

            var result = await tagsService.GenerateTags(client, rootPath);
            if (!result.IsSuccess)
                return Result.Failure(result.Error!);
            if (result.Value != null)
                client = result.Value;
            clientUserRepo.Update(client);
            try
            {
                await clientUserRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.UpdateFailed);
            }
        }
    }
}
