using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudyBuddy.Shared.DTOs.ReviewDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Reviews
{
    public interface IReviewDomainService
    {
        Task<Result> Create(CreateReviewDTO reviewDTO);
        Task<Result> Update(UpdateReviewDTO reviewDTO);
        Task<Result> Delete(int Id);
    } 
}
