using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Shared.DTOs.DayDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Days
{
    public interface IDayService
    {
        Task<Result> Create(CreateDayDTO dayDTO);
        Task<Result> Update(UpdateDayDTO dayDTO);
        Task<Result<GetDayDTO>> GetDayById(int id);
        Task<Result> Delete(int id);
        Task<Result<DataResponse<GetDayDTO>>> GetDays(int skip, int take);
    }
}