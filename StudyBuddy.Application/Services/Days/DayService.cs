using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.DayDTO;
using StudyBuddy.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace StudyBuddy.Application.Services.Days
{
    public class DayService : IDayService
    {
        private readonly IRepo<Day> dayRepo;

        public DayService(IRepo<Day> dayRepo)
        {
            this.dayRepo = dayRepo;
        }

        public async Task<Result> Create(CreateDayDTO dayDTO)
        {
            if (await dayRepo.ExistsAsync(m => m.Name == dayDTO.Name))
                return Result.Failure(Error.DayAlreadyExists);

            var day = new Day();
            dayDTO.Adapt(day);
            await dayRepo.AddAsync(day);
            try
            {
                await dayRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Delete(int id)
        {
            var day = await dayRepo.GetByIdAsync(id);
            if (day == null)
                return Result.Failure(Error.ItemNotFound);
            dayRepo.Remove(day);
            try
            {
                await dayRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }
            return Result.Success();
        }

        public async Task<Result<GetDayDTO>> GetDayById(int id)
        {
            var day = await dayRepo.GetByIdAsync(id);
            if (day == null)
                return Result<GetDayDTO>.Failure(Error.ItemNotFound);
            var dayDTO = day.Adapt<GetDayDTO>();
            return Result<GetDayDTO>.Success(dayDTO);
        }

        public async Task<Result<List<GetDayDTO>>> GetDays(int skip, int take)
        {
            var result = dayRepo.GetQuery();

            var query = result.ProjectToType<GetDayDTO>();

            var data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<List<GetDayDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateDayDTO dayDTO)
        {
            var day = await dayRepo.GetByIdAsync(dayDTO.Id);
            if (day == null)
                return Result.Failure(Error.ItemNotFound);

            if (await dayRepo.ExistsAsync(m => m.Name == dayDTO.Name && m.Id != dayDTO.Id))
                return Result.Failure(Error.DayAlreadyExists);

            dayDTO.Adapt(day);
            dayRepo.Update(day);
            try
            {
                await dayRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);
            }

            return Result.Success();
        }
    }
}