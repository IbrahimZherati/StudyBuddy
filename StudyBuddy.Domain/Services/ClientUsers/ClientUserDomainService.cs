
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.ClientUsers
{
    public class ClientUserDomainService : IClientUserDomainService
    {
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<Major> majorRepo;
        private readonly IRepo<University> universityRepo;
        private readonly IRepo<City> cityRepo;
        private readonly IRepo<Country> countryRepo;
        private readonly IRepo<Friend> friendRepo;
        private readonly IRepo<FriendRequest> friendRequestRepo;

        public ClientUserDomainService(IRepo<ClientUser> clientUserRepo
        ,IRepo<Major> majorRepo
        ,IRepo<University> universityRepo
        ,IRepo<City> cityRepo
        ,IRepo<Country> countryRepo,
            IRepo<Friend> friendRepo,
            IRepo<FriendRequest> friendRequestRepo
        )
        {
            this.clientUserRepo = clientUserRepo;
            this.majorRepo = majorRepo;
            this.universityRepo = universityRepo;
            this.cityRepo = cityRepo;
            this.countryRepo = countryRepo;
            this.friendRepo = friendRepo;
            this.friendRequestRepo = friendRequestRepo;
        }

        public async Task<Result> AcceptFriendReqesut(int clientUserId ,int requestId)
        {
            var request = await friendRequestRepo.GetByIdAsync(requestId);
            if (request == null)
                return Result.Failure(Error.FriendRequestNotFound);
            if (request.ToClientUserId != clientUserId)
                return Result.Failure(Error.TheFriendRequestNotForThisClient);
            return Result.Success();
        }

        public async Task<Result> FriendReqesut(int clientUserId, int requestClientUserId)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == requestClientUserId))
                return Result.Failure(Error.ClientUserNotFound);
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientUserId))
                return Result.Failure(Error.ClientUserNotFound);

            if (await friendRepo.ExistsAsync(f =>
            f.FirstFriendId == clientUserId && f.SecondFriendId == requestClientUserId ||
            f.FirstFriendId == requestClientUserId && f.SecondFriendId == clientUserId))
                return Result.Failure(Error.FriendShipAlreadyExists);

            if(await friendRequestRepo.ExistsAsync(f => f.FromClientUserId == clientUserId && f.ToClientUserId == requestClientUserId))
                return Result.Failure(Error.ClientUserAlreadyRequestFriend);
            return Result.Success();
        }

        public async Task<Result> Update(int clientId ,UpdateClientUserDTO clientUserDTO)
        { 
            if (!await clientUserRepo.ExistsAsync(a => a.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            
         

            if (clientUserDTO.MajorId != null && !await majorRepo.ExistsAsync(m => m.Id == clientUserDTO.MajorId))
                return Result.Failure(Error.MajorNotFound);


            if (clientUserDTO.UniversityId != null && !await universityRepo.ExistsAsync(u => u.Id == clientUserDTO.UniversityId))
                return Result.Failure(Error.UniversityNotFound);


            if (clientUserDTO.CityId != null && !await cityRepo.ExistsAsync(c => c.Id == clientUserDTO.CityId))
                return Result.Failure(Error.CityNotFound);


            if (clientUserDTO.CountryId != null && !await countryRepo.ExistsAsync(c => c.Id == clientUserDTO.CountryId))
                return Result.Failure(Error.CountryNotFound);


            return Result.Success();
        }
    }
}
