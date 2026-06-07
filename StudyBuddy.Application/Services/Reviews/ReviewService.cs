using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Reviews;
using StudyBuddy.Shared.DTOs.ReviewDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepo<Review> reviewRepo;
        private readonly IReviewDomainService reviewDomainService;


        public ReviewService(IRepo<Review> reviewRepo, IReviewDomainService reviewDomainService)
        {
            this.reviewRepo = reviewRepo;
            this.reviewDomainService = reviewDomainService;

        }

        public async Task<Result<GetReviewDTO>> Create(CreateReviewDTO reviewDTO)
        {
            var valid = await reviewDomainService.Create(reviewDTO);
            if (!valid.IsSuccess)
                return Result<GetReviewDTO>.Failure(valid.Error!);

            var result = Review.Create(reviewDTO);

            if (!result.IsSuccess)
                return Result<GetReviewDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetReviewDTO>.Failure(Error.CreateFailed);

            var review = result.Value;
            await reviewRepo.AddAsync(review);

            try
            {
                await reviewRepo.SaveAsync();
                var dto = review.Adapt<GetReviewDTO>();
                return Result<GetReviewDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetReviewDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int id)
        {
            var valid = await reviewDomainService.Delete(id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var review = await reviewRepo.GetByIdAsync(id);
            if (review == null)
                return Result.Failure(Error.ReviewNotFound);
            reviewRepo.Remove(review);
            try
            {
                await reviewRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetReviewDTO>> GetReviewById(int id)
        {
            var review = await reviewRepo.GetByIdAsync(id);
            if (review == null)
                return Result<GetReviewDTO>.Failure(Error.ReviewNotFound);
            var reviewDTO = review.Adapt<GetReviewDTO>();
            return Result<GetReviewDTO>.Success(reviewDTO);
        }

        public async Task<Result<DataResponse<GetReviewDTO>>> GetReviews(int skip, int take)
        {
            var result = reviewRepo.GetQuery();

            var query = result.ProjectToType<GetReviewDTO>();

            var data = new DataResponse<GetReviewDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetReviewDTO>>.Success(data);
        }

        public async Task<Result<GetReviewDTO>> Update(UpdateReviewDTO reviewDTO)
        {
            var valid = await reviewDomainService.Update(reviewDTO);
            if (!valid.IsSuccess)
                return Result<GetReviewDTO>.Failure(valid.Error!);

            var review = await reviewRepo.GetByIdAsync(reviewDTO.Id);
            if (review == null)
                return Result<GetReviewDTO>.Failure(Error.ReviewNotFound);

            var result = review.Update(reviewDTO);

            if (!result.IsSuccess)
                return Result<GetReviewDTO>.Failure(result.Error!);

            reviewRepo.Update(review);
            try
            {
                await reviewRepo.SaveAsync();
                var dto = review.Adapt<GetReviewDTO>();
                return Result<GetReviewDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetReviewDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
