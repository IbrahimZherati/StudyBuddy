using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ReviewDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Reviews
{
    public class ReviewDomainService : IReviewDomainService
    {
        private readonly IRepo<Review> reviewRepo;
        private readonly IRepo<ClientUser> clientUserRepo;


        public ReviewDomainService(IRepo<Review> reviewRepo
        ,IRepo<ClientUser> clientUserRepo
        )
        {
            this.reviewRepo = reviewRepo;
            this.clientUserRepo = clientUserRepo;

        }

        public async Task<Result> Create(CreateReviewDTO reviewDTO)
        {
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == reviewDTO.ClientUserId))
                return Result.Failure(Error.ClientUserNotFound);

            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await reviewRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.ReviewNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateReviewDTO reviewDTO)
        { 
            if (!await reviewRepo.ExistsAsync(a => a.Id == reviewDTO.Id))
                return Result.Failure(Error.ReviewNotFound);
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == reviewDTO.ClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


       
            return Result.Success();
        }
    }
}
