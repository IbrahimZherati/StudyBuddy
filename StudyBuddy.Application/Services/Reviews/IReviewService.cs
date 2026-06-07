using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using StudyBuddy.Shared.DTOs.ReviewDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IReviewService
     {
         Task<Result<GetReviewDTO>> Create(CreateReviewDTO reviewDTO);
         Task<Result<GetReviewDTO>> Update(UpdateReviewDTO reviewDTO);
         Task<Result<GetReviewDTO>> GetReviewById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetReviewDTO>>> GetReviews(int skip, int take);
     }
}
     
